using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Users;
using MediatR;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using BaseConfig.JWT;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Infrastructure.Services;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Request;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Command for user
    /// </summary>
    public class CreateUserCommand : UserModelRequest, IRequest<MethodResult<UserModelResponse>>
    {
        /// <summary>
        /// Password register
        /// </summary>
        public string? PasswordHash { get; set; }

    }
    /// <summary>
    /// Handler create user
    /// </summary>
    public class CreateUserCommandHandler : BaseCommandHandler, IRequestHandler<CreateUserCommand, MethodResult<UserModelResponse>>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="configuration"></param>
        /// <param name="emailService"></param>
        /// <param name="logger"></param>
        public CreateUserCommandHandler(IMapper mapper,
            IUserRepository userRepository,
            IUserQueries userQueries,
            IConfiguration configuration,
            IEmailService emailService,
            ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
            _logger = logger.CreateLogger<CreateUserCommandHandler>();
        }
        /// <summary>
        /// Handle register
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<UserModelResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModelResponse> methodResult = new();
            try
            {

                #region Validation
                AppUser newUser = _mapper.Map<AppUser>(request);
                newUser.LastLogin = DateTime.UtcNow;
                if (!newUser.IsValid())
                {
                    throw new CustomException(newUser.ErrorMessages);
                }
                #endregion

                #region Check is exist user
                bool appUser = await _userRepository.ExistUserByUsernameAsync(newUser.UserName ?? "");
                if (appUser)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Genarate salt and password
                newUser.Salt = GenarateSalt();
                newUser.PasswordHash = HashPassword(request.PasswordHash ?? "", newUser.Salt);
                #endregion

                #region Create new user
                newUser.GroupId = SettingUserDefault.groupDefault;
                if (await _userRepository.CreateNewUserAsync(newUser) <= 0)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region  Send mail comfirm
                await GenerateEmailConfirmationTokenAsync(newUser);
                #endregion

                #region return info new user registed
                MethodResult<BaseUserResponse> getCreatedUser = await _userQueries.GetUserModelByGuidAsync(newUser.Id);
                if (!getCreatedUser.IsOK)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName ?? "") }
                    );
                    return methodResult;
                };
                UserModelResponse responseUserRegister = _mapper.Map<UserModelResponse>(getCreatedUser.Result);
                responseUserRegister.Name = string.Concat(getCreatedUser?.Result?.Surname + " ", getCreatedUser?.Result?.Name);
                methodResult.Result = responseUserRegister;
                #endregion
            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(REGISTER) STEP CUSTOMEXCEPTION --> ID USER {ex} ---->");
                methodResult.AddResultFromErrorList(ex?.ErrorMessages);
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(REGISTER) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(REGISTER) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
            methodResult.StatusCode = StatusCodes.Status200OK;
            return methodResult;
        }
        /// <summary>
        /// Genarate mail and token confirm user register
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        public async Task GenerateEmailConfirmationTokenAsync(AppUser identityUser)
        {
            GenarateJwtToken genarateJwtToken = new(_configuration);
            UserModelResponse userModel = _mapper.Map<UserModelResponse>(identityUser);
            string token = genarateJwtToken.GenarateJwt(userModel, SettingUserDefault.minuteExpitryConfirmEmail);
            if (!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationEmail(identityUser, token);
            }
        }
        private async Task SendEmailConfirmationEmail(AppUser user, string token)
        {
            string appDomain = _configuration.GetSection(ConstAppSettings.APPLICATIONAPPDOMAIN).Value;
            string confirmationLink = _configuration.GetSection(ConstAppSettings.APPLICATIONEMAILCONFIRMED).Value;

            UserEmailOptions options = new()
            {
                ToEmails = new List<string>() { user.Email ?? "" },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.UserName ?? ""),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }
    }
}

using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Users;
using MediatR;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Common.Requests.Users;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using BaseConfig.JWT;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Infrastructure.Services;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Command for user
    /// </summary>
    public class CreateUserCommand : CreateUserCommandModel, IRequest<MethodResult<UserModelRequest>>
    { }
    /// <summary>
    /// Handler create user
    /// </summary>
    public class CreateUserCommandHandler : BaseCommandHandler, IRequestHandler<CreateUserCommand, MethodResult<UserModelRequest>>
    {
        private readonly IEmailService _emailService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="configuration"></param>
        /// <param name="emailService"></param>
        public CreateUserCommandHandler(IMapper mapper,
            IUserRepository userRepository,
             IUserQueries userQueries,
            IConfiguration configuration,
            IEmailService emailService) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
        }
        /// <summary>
        /// Handle register
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<UserModelRequest>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModelRequest> methodResult = new();
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
                AppUser getCreatedUser = await _userQueries.GetByGuidAsync(newUser.Id);
                if (getCreatedUser == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(newUser.UserName), newUser.UserName ?? "") }
                    );
                    return methodResult;
                };
                UserModelRequest resultUser = _mapper.Map<UserModelRequest>(getCreatedUser);
                methodResult.Result = resultUser;
                methodResult.StatusCode = StatusCodes.Status200OK;
                #endregion

                return methodResult;
            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddResultFromErrorList(ex.ErrorMessages);
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
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
            UserModelRequest userModel = _mapper.Map<UserModelRequest>(identityUser);
            string token = genarateJwtToken.GenarateJwt(userModel);
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

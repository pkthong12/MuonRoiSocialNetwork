using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.JWT;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using MuonRoiSocialNetwork.Infrastructure.Services;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Forgot password command request
    /// </summary>
    public class ForgotPasswordUserCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// Username of user get password
        /// </summary>
        public string? Username { get; set; }
    }
    /// <summary>
    /// Handler forgot password
    /// </summary>
    public class ForgotPasswordUserCommandHandler : BaseCommandHandler, IRequestHandler<ForgotPasswordUserCommand, MethodResult<bool>>
    {
        private readonly IEmailService _emailService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="emailService"></param>
        public ForgotPasswordUserCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IEmailService emailService) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
        }
        /// <summary>
        /// Handler function
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(ForgotPasswordUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Check vaild username
                if (request.Username == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR03C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR03C), nameof(EnumUserErrorCodes.USR03C) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Check user is exist by username
                AppUser existUser = await _userQueries.GetByUsernameAsync(request.Username);
                if (existUser == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), nameof(request.Username) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region SendMail
                await GenerateEmailConfirmationTokenAsync(existUser);
                #endregion
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.Result = false;
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
            methodResult.Result = true;
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
            string token = genarateJwtToken.GenarateJwt(userModel, int.Parse(_configuration.GetSection(ConstAppSettings.LIFE_TIME).Value));
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
            await _emailService.SendEmailForForgotPassword(options);
        }
    }
}

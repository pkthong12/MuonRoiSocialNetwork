using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.JWT;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure.Extentions.Mail;
using MuonRoiSocialNetwork.Infrastructure.Helpers;
using MuonRoiSocialNetwork.Infrastructure.Services;

namespace MuonRoiSocialNetwork.Application.Commands.Email
{
    /// <summary>
    /// Request resend mail
    /// </summary>
    public class ResendMailVeritificationCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// User resend mail
        /// </summary>
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// Class handle
    /// </summary>
    public class ResendMailVeritificationCommandHandler : BaseCommandHandler, IRequestHandler<ResendMailVeritificationCommand, MethodResult<bool>>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ResendMailVeritificationCommandHandler> _logger;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="emailService"></param>
        /// <param name="logger"></param>
        /// <param name="cache"></param>
        public ResendMailVeritificationCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository,
            IEmailService emailService,
            ILoggerFactory logger, IDistributedCache cache) : base(mapper, configuration, userQueries, userRepository)
        {
            _emailService = emailService;
            _logger = logger.CreateLogger<ResendMailVeritificationCommandHandler>();
            _cache = cache;
        }
        /// <summary>
        /// Func handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(ResendMailVeritificationCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Get cache Get user by user id
#pragma warning disable CS8602
                UserModelResponse? userGetLogin = await _cache?.GetRecordAsync<UserModelResponse>($"{RefreshTokenDefault.keyUserModelResponseLogin}_{request.UserId}");
#pragma warning restore CS8602
                if (userGetLogin is null)
                {
                    MethodResult<BaseUserResponse> resultBaseUserLogin = await _userQueries.GetUserModelByGuidAsync(request.UserId);
                    userGetLogin = _mapper.Map<UserModelResponse>(resultBaseUserLogin.Result);
                    if (userGetLogin is null)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USRC47C),
                            new[] { Helpers.GenerateErrorResult(nameof(userGetLogin.Username), userGetLogin?.Username ?? "") }
                        );
                        methodResult.Result = false;
                        return methodResult;
                    }

                }
                #endregion

                #region Get user by user id
                AppUser appUser = await _userQueries.GetByGuidAsync(userGetLogin.Id);
                if (appUser is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(userGetLogin.Username), userGetLogin?.Username ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Check lock time
                DateTime timeNow = DateTime.UtcNow.AddHours(SettingUserDefault.hourAsia);
                TimeSpan? checkTimeLock = timeNow - appUser.LockoutEnd;
                if (appUser.LockoutEnabled && appUser.LockoutEnd != null && checkTimeLock > TimeSpan.Zero)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC48C),
                        new[] { Helpers.GenerateErrorResult(nameof(userGetLogin.Username), userGetLogin?.Username ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Reset lock time
                if (!appUser.LockoutEnabled || !(checkTimeLock > TimeSpan.Zero))
                {
                    appUser.CountRequestSendMail = 0;
                    appUser.LockoutEnd = null;
                    appUser.LockoutEnabled = false;
                }
                #endregion

                #region Check count request
                if (appUser.CountRequestSendMail >= SettingUserDefault.maxNumberRequestSendMail)
                {
                    appUser.LockoutEnabled = true;
                    DateTimeOffset lockTime = new DateTimeOffset(DateTime.UtcNow).ToOffset(TimeSpan.FromHours(SettingUserDefault.hourAsia));
                    appUser.LockoutEnd = lockTime.AddHours(2);
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC48C),
                        new[] { Helpers.GenerateErrorResult(nameof(userGetLogin.Username), userGetLogin?.Username ?? "") }
                    );
                    if (await _userRepository.UpdateUserAsync(appUser) < 1)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USR29C),
                            new[] { Helpers.GenerateErrorResult(nameof(appUser.UserName), appUser.UserName ?? "") }
                        );
                        methodResult.Result = false;
                        return methodResult;
                    }
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region update count request
                appUser.CountRequestSendMail++;
                if (await _userRepository.UpdateUserAsync(appUser) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(appUser.UserName), appUser.UserName ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region  Send mail comfirm
                await GenerateEmailConfirmationTokenAsync(appUser);
                #endregion

                methodResult.StatusCode = StatusCodes.Status200OK;
                methodResult.Result = true;
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(ResendMailVeritification) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(ResendMailVeritification) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                methodResult.Result = false;
                return methodResult;
            }

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

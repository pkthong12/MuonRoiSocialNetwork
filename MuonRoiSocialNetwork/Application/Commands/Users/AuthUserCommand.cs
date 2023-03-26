using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Extentions.Datetime;
using BaseConfig.JWT;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Application.Commands.RefreshToken;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoiSocialNetwork.Infrastructure.Helpers;
using MuonRoiSocialNetwork.Infrastructure.Repositories;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Login auth user command
    /// </summary>
    public class AuthUserCommand : IRequest<MethodResult<UserModelResponse>>
    {
        #region Property
        /// <summary>
        /// Username login
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// Password Login
        /// </summary>
        public string? Password { get; set; }
        #endregion
    }
    /// <summary>
    /// Handler login user
    /// </summary>
    public class AuthUserCommandHandler : BaseCommandHandler, IRequestHandler<AuthUserCommand, MethodResult<UserModelResponse>>
    {
        private readonly ILogger<AuthUserCommandHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        /// <param name="mediator"></param>
        /// <param name="cache"></param>
        public AuthUserCommandHandler(IMapper mapper,
            IUserRepository userRepository,
            IUserQueries userQueries,
            IConfiguration configuration,
            ILoggerFactory logger,
            IMediator mediator, IDistributedCache cache) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<AuthUserCommandHandler>();
            _mediator = mediator;
            _cache = cache;
        }
        /// <summary>
        /// Handler function
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<UserModelResponse>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModelResponse> methodResult = new();
            GenarateJwtToken genarateJwtToken = new(_configuration);
            try
            {

                #region Check valid username and password
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    _logger.LogError($" --> STEP CHECK {"Check valid username and password".ToUpper()} --> USERNAME: {request.Username} | PASSWORD: {request.Password} -->");
                    methodResult.AddApiErrorMessage(
                        string.IsNullOrEmpty(request.Username) ? nameof(EnumUserErrorCodes.USR05C) : nameof(EnumUserErrorCodes.USR06C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Check user is exsit ?
                bool isExistUser = await _userRepository.ExistUserByUsernameAsync(request.Username);
                if (!isExistUser)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region check user have been locked
                AppUser appUser = await _userQueries.GetByUsernameAsync(request.Username);
                if (appUser.Status == EnumAccountStatus.Locked)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR28C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region check password user
                string password = HashPassword(request.Password, appUser.Salt ?? "");
                if (password != appUser.PasswordHash)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC39C),
                        new[] { Helpers.GenerateErrorResult(nameof(SettingUserDefault.loginAttempDefault), SettingUserDefault.loginAttempDefault - appUser.AccessFailedCount) }
                    );
                    appUser.AccessFailedCount++;
                    if (appUser.AccessFailedCount >= 5)
                    {
                        appUser.Status = EnumAccountStatus.Locked;
                        appUser.LockReason = $"Login failed {appUser.AccessFailedCount} times";
                    }
                    if (await _userRepository.UpdateUserAsync(appUser) < 1)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USR29C),
                            new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                        );
                        return methodResult;
                    }
                    return methodResult;
                }
                #endregion

                #region check user is renew password
                if (appUser.AccountStatus == EnumAccountStatus.IsRenew)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC43C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                    );
                    return methodResult;
                }
                #endregion


                #region Update info user when login success
                appUser.AccessFailedCount = 0;
                appUser.LastLogin = DateTime.UtcNow;
                appUser.AccountStatus = EnumAccountStatus.IsOnl;
                appUser.Status = EnumAccountStatus.Active;
                if (await _userRepository.UpdateUserAsync(appUser) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Return info user was login
                UserModelResponse resultInforLoginUser = _mapper.Map<UserModelResponse>(appUser);
                resultInforLoginUser.CreateDate = DateTimeExtensions.TimeStampToDateTime(appUser.CreatedDateTS.GetValueOrDefault()).AddHours(SettingUserDefault.hourAsia);
                resultInforLoginUser.UpdateDate = DateTimeExtensions.TimeStampToDateTime(appUser.UpdatedDateTS.GetValueOrDefault()).AddHours(SettingUserDefault.hourAsia);
                MethodResult<BaseUserResponse> userInfo = await _userQueries.GetUserModelByGuidAsync(resultInforLoginUser.Id);
                resultInforLoginUser.RoleName = userInfo.Result?.RoleName;
                resultInforLoginUser.GroupName = userInfo.Result?.GroupName;

                resultInforLoginUser.JwtToken = genarateJwtToken.GenarateJwt(resultInforLoginUser, SettingUserDefault.minuteExpitryLogin);
                #endregion

#pragma warning disable CS8602
                UserModelResponse? userGet = await _cache?.GetRecordAsync<UserModelResponse>($"{RefreshTokenDefault.keyUserModelResponse}_{resultInforLoginUser.Id}");
#pragma warning restore CS8602
                MethodResult<string> refreshToken = null;
                #region Check -> genarate refresh token and set cache
                if (userGet is null)
                {
                    TimeSpan expirationTime = RefreshTokenDefault.expirationTime;
                    TimeSpan slidingExpirationTime = RefreshTokenDefault.slidingExpiration;
#pragma warning disable CS8602
                    await _cache?.SetRecordAsync<BaseUserResponse>($"{RefreshTokenDefault.keyUserModelResponse}_{resultInforLoginUser.Id}", resultInforLoginUser, expirationTime, slidingExpirationTime);
#pragma warning restore CS8602
                    GennerateRefreshTokenCommand gennerateRefreshToken = new()
                    {
                        UserId = resultInforLoginUser.Id,
                    };
                    refreshToken = await _mediator.Send(gennerateRefreshToken).ConfigureAwait(false);
                    if (!refreshToken.IsOK)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USR29C),
                            new[] { Helpers.GenerateErrorResult(nameof(request.Username), request.Username ?? "") }
                        );
                        return methodResult;
                    }
                }
                #endregion
                resultInforLoginUser.RefreshToken = refreshToken?.Result;
                methodResult.Result = resultInforLoginUser;
                methodResult.StatusCode = StatusCodes.Status200OK;
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(AUTH) STEP {"Exception".ToUpper()} --> EXEPTION: {ex} ---->");
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR29C),
                    new[] { Helpers.GenerateErrorResult(nameof(ex.Message), ex.Message ?? "") }
                );
            }
            return methodResult;
        }
    }
}

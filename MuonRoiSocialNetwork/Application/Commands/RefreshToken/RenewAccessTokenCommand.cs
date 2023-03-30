using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Extentions.Datetime;
using BaseConfig.JWT;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;
using MuonRoiSocialNetwork.Infrastructure.Helpers;

namespace MuonRoiSocialNetwork.Application.Commands.RefreshToken
{
    /// <summary>
    /// Request renew access token
    /// </summary>
    public class RenewAccessTokenCommand : IRequest<MethodResult<string>>
    {
        /// <summary>
        /// RefreshToken request
        /// </summary>
        public string? RefreshToken { get; set; }
        /// <summary>
        /// UserId create refresh token
        /// </summary>
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// Class handler
    /// </summary>
    public class RenewAccessTokenCommandHandler : BaseUserCommandHandler, IRequestHandler<RenewAccessTokenCommand, MethodResult<string>>
    {
        private readonly IRefreshtokenRepository _refreshtokenRepository;
        private readonly IDistributedCache _cache;
        private readonly ILogger<RenewAccessTokenCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="refreshtokenRepository"></param>
        /// <param name="cache"></param>
        /// <param name="logger"></param>
        public RenewAccessTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository, IDistributedCache cache,
            ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _refreshtokenRepository = refreshtokenRepository;
            _cache = cache;
            _logger = logger.CreateLogger<RenewAccessTokenCommandHandler>();
        }

        /// <summary>
        /// Func handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<string>> Handle(RenewAccessTokenCommand request, CancellationToken cancellationToken)
        {
            MethodResult<string> methodResult = new();
            GenarateJwtToken genarateJwtToken = new(_configuration);
            string accessToken = string.Empty;
            try
            {
                #region Check is exist User
                AppUser checkUser = await _userQueries.GetByGuidAsync(request.UserId);
                if (checkUser is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                    );
                    return methodResult;
                }
                #endregion

                #region Get info refresh token

                Dictionary<string, string[]> keyValuePairs = await _refreshtokenRepository.GetInfoRefreshTokenAsync(request.UserId);
                if (keyValuePairs.Any(x => x.Key == "false"))
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC44C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                    );
                    return methodResult;
                }
                string[] valueRefreshToken = keyValuePairs.FirstOrDefault(x => x.Key == request.UserId.ToString()).Value;
                string salt = valueRefreshToken.Length <= 0 ? "" : valueRefreshToken[0];
                string refreshToken = valueRefreshToken.Length <= 0 ? "" : valueRefreshToken[1];
                double timeExpired = valueRefreshToken.Length <= 0 ? 0 : Convert.ToDouble(valueRefreshToken[2]);
                if (DateTime.UtcNow >= DateTimeExtensions.TimeStampToDateTime(timeExpired))
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC36C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                    );
                    return methodResult;
                }
                #endregion

                #region Check valid refresh token
#pragma warning disable CS8604
                if (refreshToken != HashPassword(request.RefreshToken, salt))
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC45C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.RefreshToken), request.RefreshToken) }
                    );
                    return methodResult;
                }
#pragma warning restore CS8604
                #endregion

                #region Get cache
                UserModelResponse? resultInforLoginUser = await _cache.GetRecordAsync<UserModelResponse>($"{RefreshTokenDefault.keyUserModelResponseRegister}_{request.UserId}");
                MethodResult<BaseUserResponse> userResponse = new();
                if (resultInforLoginUser is null)
                {
                    userResponse = await _userQueries.GetUserModelByGuidAsync(request.UserId);
                }
                resultInforLoginUser = _mapper.Map<UserModelResponse>(userResponse.Result);
                #endregion

                #region Renew access token
#pragma warning disable CS8604
                accessToken = genarateJwtToken.GenarateJwt(resultInforLoginUser, SettingUserDefault.minuteExpitryLogin);
#pragma warning restore CS8604
                #endregion
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(RenewAccessToken) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(RenewAccessToken) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
            methodResult.Result = accessToken;
            return methodResult;
        }
    }
}

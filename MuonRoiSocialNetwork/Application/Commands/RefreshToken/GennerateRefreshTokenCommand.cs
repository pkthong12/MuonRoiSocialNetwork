using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Extentions.Datetime;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.DomainObjects.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;

namespace MuonRoiSocialNetwork.Application.Commands.RefreshToken
{
    /// <summary>
    /// Request create refresh token
    /// </summary>
    public class GennerateRefreshTokenCommand : IRequest<MethodResult<string>>
    {
        /// <summary>
        /// UserId create refresh token
        /// </summary>
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// Class handler
    /// </summary>
    public class GennerateRefreshTokenCommandHandler : BaseUserCommandHandler, IRequestHandler<GennerateRefreshTokenCommand, MethodResult<string>>
    {
        private readonly IRefreshtokenRepository _refreshtokenRepository;
        private readonly ILogger<GennerateRefreshTokenCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="refreshtokenRepository"></param>
        /// <param name="logger"></param>
        public GennerateRefreshTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository,
            ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _refreshtokenRepository = refreshtokenRepository;
            _logger = logger.CreateLogger<GennerateRefreshTokenCommandHandler>();
        }

        /// <summary>
        /// Func handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<string>> Handle(GennerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            MethodResult<string> methodResult = new();
            string genareRefreshToken = string.Empty;
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

                #region Check refresh token is exist
                if (refreshToken is not null)
                {
                    methodResult.StatusCode = StatusCodes.Status200OK;
                    methodResult.Result = refreshToken;
                    return methodResult;
                }
                #endregion

                #region genarate refresh token
                UserLogin userLoggin = new();
                genareRefreshToken = RandomString(SettingUserDefault.genareRefreshToken);
                userLoggin.KeySalt = GenarateSalt();
                userLoggin.RefreshToken = HashPassword(genareRefreshToken ?? "", userLoggin.KeySalt);
                userLoggin.UserId = request.UserId;
                if (await _refreshtokenRepository.CreateRefreshTokenAsync(userLoggin) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                    );
                    return methodResult;
                }
                #endregion

                methodResult.StatusCode = StatusCodes.Status200OK;
                methodResult.Result = genareRefreshToken;
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(GennerateRefreshToken) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(GennerateRefreshToken) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
        }
    }
}

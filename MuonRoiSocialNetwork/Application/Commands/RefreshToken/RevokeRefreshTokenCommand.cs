using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Token;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;

namespace MuonRoiSocialNetwork.Application.Commands.RefreshToken
{
    /// <summary>
    /// Request revoke refresh token
    /// </summary>
    public class RevokeRefreshTokenCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// UserId create refresh token
        /// </summary>
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// Class handler
    /// </summary>
    public class RevokeRefreshTokenCommandHandler : BaseUserCommandHandler, IRequestHandler<RevokeRefreshTokenCommand, MethodResult<bool>>
    {
        private readonly IRefreshtokenRepository _refreshtokenRepository;
        private readonly ILogger<RevokeRefreshTokenCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="refreshtokenRepository"></param>
        /// <param name="logger"></param>
        public RevokeRefreshTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository,
            ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<RevokeRefreshTokenCommandHandler>();
            _refreshtokenRepository = refreshtokenRepository;
        }

        /// <summary>
        /// Func handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
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
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Revoke refresh token

                if (await _refreshtokenRepository.RevokeRefreshTokenAsync(checkUser.Id) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Set status user is off
                checkUser.AccountStatus = EnumAccountStatus.IsOf;
                if (await _userRepository.UpdateUserAsync(checkUser) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(checkUser.UserName), checkUser.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                methodResult.Result = true;
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(RevokeRefreshToken) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(RevokeRefreshToken) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                methodResult.Result = false;
                return methodResult;
            }
        }
    }
}

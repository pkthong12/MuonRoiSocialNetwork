using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

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
    public class RevokeRefreshTokenCommandHandler : BaseCommandHandler, IRequestHandler<RevokeRefreshTokenCommand, MethodResult<bool>>
    {
        private readonly IRefreshtokenRepository _refreshtokenRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="refreshtokenRepository"></param>
        public RevokeRefreshTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository) : base(mapper, configuration, userQueries, userRepository)
        {
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

            #region Check is exist User
            AppUser checkUser = await _userQueries.GetByGuidAsync(request.UserId);
            if (checkUser == null)
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
            methodResult.Result = true;
            return methodResult;
        }
    }
}

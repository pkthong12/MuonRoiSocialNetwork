using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.DomainObjects.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

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
    public class GennerateRefreshTokenCommandHandler : BaseCommandHandler, IRequestHandler<GennerateRefreshTokenCommand, MethodResult<string>>
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
        public GennerateRefreshTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository) : base(mapper, configuration, userQueries, userRepository)
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
        public async Task<MethodResult<string>> Handle(GennerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            MethodResult<string> methodResult = new();

            #region Check is exist User
            AppUser checkUser = await _userQueries.GetByGuidAsync(request.UserId);
            if (checkUser == null)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR02C),
                    new[] { Helpers.GenerateErrorResult(nameof(request.UserId), request.UserId) }
                );
                return methodResult;
            }
            #endregion
            UserLogin userLoggin = new();
            string rawRefreshToken = RandomString(SettingUserDefault.genareRefreshToken);
            userLoggin.KeySalt = GenarateSalt();
            userLoggin.RefreshToken = HashPassword(rawRefreshToken ?? "", userLoggin.KeySalt);
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
            methodResult.Result = rawRefreshToken;
            return methodResult;
        }
    }
}

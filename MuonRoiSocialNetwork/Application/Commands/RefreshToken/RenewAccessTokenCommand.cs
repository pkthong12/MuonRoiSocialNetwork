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
using MuonRoiSocialNetwork.Common.Settings.RefreshTokenSettings;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.DomainObjects.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
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
    public class RenewAccessTokenCommandHandler : BaseCommandHandler, IRequestHandler<RenewAccessTokenCommand, MethodResult<string>>
    {
        private readonly IRefreshtokenRepository _refreshtokenRepository;
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="refreshtokenRepository"></param>
        /// <param name="cache"></param>
        public RenewAccessTokenCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, IRefreshtokenRepository refreshtokenRepository, IDistributedCache cache) : base(mapper, configuration, userQueries, userRepository)
        {
            _refreshtokenRepository = refreshtokenRepository;
            _cache = cache;
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

            UserModelResponse? resultInforLoginUser = await _cache.GetRecordAsync<UserModelResponse>($"{RefreshTokenDefault.keyUserModelResponse}_{request.UserId}");
            if (resultInforLoginUser == null)
            {
                MethodResult<BaseUserResponse> userResponse = await _userQueries.GetUserModelByGuidAsync(request.UserId);
                _mapper.Map(userResponse.Result, resultInforLoginUser);
            }
            string newAccessToken = genarateJwtToken.GenarateJwt(resultInforLoginUser, SettingUserDefault.minuteExpitryLogin);
            methodResult.Result = newAccessToken;
            return methodResult;
        }
    }
}

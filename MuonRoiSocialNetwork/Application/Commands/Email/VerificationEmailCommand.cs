using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MuonRoi.Social_Network.Users;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Settings.Appsettings;
using System.IdentityModel.Tokens.Jwt;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;

namespace MuonRoiSocialNetwork.Application.Commands.Email
{
    /// <summary>
    /// Verification email
    /// </summary>
    public class VerificationEmailCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// Guid user active
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Token active
        /// </summary>
        public string? TokenJWT { get; set; }

    }
    /// <summary>
    /// Handle command class
    /// </summary>
    public class VerificationEmailCommandHandler : BaseUserCommandHandler, IRequestHandler<VerificationEmailCommand, MethodResult<bool>>
    {
        private readonly ILogger<VerificationEmailCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="logger"></param>
        public VerificationEmailCommandHandler(IMapper mapper,
            IConfiguration configuration, IUserRepository userRepository, IUserQueries userQueries, ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<VerificationEmailCommandHandler>();
        }
        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(VerificationEmailCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Check is exist User
                AppUser checkUser = await _userQueries.GetByGuidAsync(request.UserGuid);
                if (checkUser is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserGuid), request.UserGuid) }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Valid token and check exp time
                SymmetricSecurityKey symmetricKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration.GetSection(ConstAppSettings.APPLICATIONSERECT).Value));
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                string myIssuer = _configuration.GetSection(ConstAppSettings.ENV_SERECT).Value;
                string myAudience = _configuration.GetSection(ConstAppSettings.APPLICATIONAPPDOMAIN).Value;
                try
                {
                    tokenHandler.ValidateToken(request.TokenJWT, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = myIssuer,
                        ValidAudience = myAudience,
                        IssuerSigningKey = symmetricKey
                    }, out SecurityToken validatedToken);
                }
                catch
                {
                    methodResult.Result = false;
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    _logger.LogError($" --> STEP {"Valid token and check exp time".ToUpper()} --> JWT TOKEN {request.TokenJWT} ---->");
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC36C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.TokenJWT), request.TokenJWT ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region update status confirmed
                if (await _userRepository.ConfirmedEmail(checkUser) < 1)
                {
                    methodResult.Result = false;
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC37C),
                        new[] { Helpers.GenerateErrorResult(nameof(checkUser.UserName), checkUser.UserName ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                methodResult.Result = true;
                methodResult.StatusCode = StatusCodes.Status200OK;
                return methodResult;
                #endregion
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(VERIFICATION EMAIL) STEP {"Exception".ToUpper()} --> MESSAGE: {ex} ---->");
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR29C),
                    new[] { Helpers.GenerateErrorResult(nameof(ex.Message), ex.Message) }
                );
                methodResult.Result = false;
                return methodResult;
            }
        }
    }
}

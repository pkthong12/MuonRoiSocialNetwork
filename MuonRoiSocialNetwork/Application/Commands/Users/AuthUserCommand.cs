using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.JWT;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Common.Settings.UserSettings;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Login auth user command
    /// </summary>
    public class AuthUserCommand : IRequest<MethodResult<UserModelRequest>>
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
    public class AuthUserCommandHandler : BaseCommandHandler, IRequestHandler<AuthUserCommand, MethodResult<UserModelRequest>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        /// <param name="configuration"></param>
        public AuthUserCommandHandler(IMapper mapper,
            IUserRepository userRepository,
            IUserQueries userQueries,
            IConfiguration configuration) : base(mapper, configuration, userQueries, userRepository)
        { }
        /// <summary>
        /// Handler function
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<UserModelRequest>> Handle(AuthUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModelRequest> methodResult = new();
            GenarateJwtToken genarateJwtToken = new(_configuration);
            try
            {
                #region Check valid username and password
                if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
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
                        new[] { Helpers.GenerateErrorResult(nameof(LoginAttemp.loginAttemp), LoginAttemp.loginAttemp - appUser.AccessFailedCount) }
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

                #region Update info user when login success
                appUser.AccessFailedCount = 0;
                appUser.LastLogin = DateTime.UtcNow;
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
                UserModelRequest resultInforLoginUser = _mapper.Map<UserModelRequest>(appUser);
                resultInforLoginUser.JwtToken = genarateJwtToken.GenarateJwt(resultInforLoginUser);
                methodResult.Result = resultInforLoginUser;
                methodResult.StatusCode = StatusCodes.Status200OK;
                #endregion
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR29C),
                    new[] { Helpers.GenerateErrorResult(nameof(ex.Message), ex.Message ?? "") }
                );
            }
            return methodResult;
        }
    }
}

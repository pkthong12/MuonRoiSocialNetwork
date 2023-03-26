using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using System.Text.RegularExpressions;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Handler request change password
    /// </summary>
    public class ChangePasswordCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// User change password
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// New password
        /// </summary>
        public string? NewPassword { get; set; }
        /// <summary>
        /// confirm password
        /// </summary>
        public string? ConfirmPassword { get; set; }
    }
    /// <summary>
    /// Handler command
    /// </summary>
    public class ChangePasswordCommandHandler : BaseCommandHandler, IRequestHandler<ChangePasswordCommand, MethodResult<bool>>
    {
        private readonly ILogger<ChangePasswordCommandHandler> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        public ChangePasswordCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, ILoggerFactory logger) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<ChangePasswordCommandHandler>();
        }
        /// <summary>
        /// Function handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Validator data
                bool existUser = await _userRepository.ExistUserByGuidAsync(request.UserGuid);
                if (!existUser)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR02C), nameof(EnumUserErrorCodes.USR02C) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                if (request.NewPassword.IsNullOrEmpty() || request.NewPassword.IsNullOrEmpty())
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR06C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR06C), nameof(EnumUserErrorCodes.USR06C) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                string pwdPattern = @"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
                bool isComplex = Regex.IsMatch(request.ConfirmPassword ?? "", pwdPattern);
                if (!isComplex)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR17C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.ConfirmPassword), request.ConfirmPassword ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Change password
                string salt = GenarateSalt();
                string passwordHash = HashPassword(request.ConfirmPassword ?? "", salt);
                bool checkStatus = await _userRepository.UpdatePassworAsync(request.UserGuid, salt, passwordHash);
                if (!checkStatus)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR29C), nameof(EnumUserErrorCodes.USR29C) ?? "") }
                    );
                    methodResult.Result = false;
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    return methodResult;
                }
                #endregion
                methodResult.StatusCode = StatusCodes.Status200OK;
                methodResult.Result = true;
                return methodResult;
            }
            catch (Exception ex)
            {
                _logger.LogError($" -->(CHANGE PASSWORD) STEP CHECK {"Exception".ToUpper()} --> EXEPTION: {ex}");
                _logger.LogError($" -->(CHANGE PASSWORD) STEP CHECK {"Exception".ToUpper()} --> EXEPTION{" StackTrace".ToUpper()}: {ex.StackTrace}");
                throw;
            }
        }
    }
}

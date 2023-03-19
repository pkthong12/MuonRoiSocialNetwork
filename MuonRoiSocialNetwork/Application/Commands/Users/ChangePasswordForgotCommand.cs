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
    public class ChangePasswordForgotCommand : IRequest<MethodResult<bool>>
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
    public class ChangePasswordForgotCommandHandler : BaseCommandHandler, IRequestHandler<ChangePasswordForgotCommand, MethodResult<bool>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        public ChangePasswordForgotCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository) : base(mapper, configuration, userQueries, userRepository)
        { }
        /// <summary>
        /// Function handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<bool>> Handle(ChangePasswordForgotCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
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
            bool checkStatus = await _userRepository.UpdatePassworAsync(request.ConfirmPassword ?? "", request.UserGuid);
            if (!checkStatus)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddApiErrorMessage(
                    nameof(EnumUserErrorCodes.USR29C),
                    new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR29C), nameof(EnumUserErrorCodes.USR29C) ?? "") }
                );
                methodResult.Result = false;
                return methodResult;
            }
            #endregion
            return methodResult;
        }
    }
}

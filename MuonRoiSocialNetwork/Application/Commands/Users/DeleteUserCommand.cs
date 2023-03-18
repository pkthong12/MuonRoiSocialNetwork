using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Request delete user
    /// </summary>
    public class DeleteUserCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// Guid for user
        /// </summary>
        public Guid GuidUser { get; set; }
    }
    /// <summary>
    /// Handler delete user
    /// </summary>
    public class DeleteUserCommandHandler : BaseCommandHandler, IRequestHandler<DeleteUserCommand, MethodResult<bool>>
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        public DeleteUserCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository) : base(mapper, configuration, userQueries, userRepository)
        { }
        /// <summary>
        /// Function handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Check valid request
                if (request == null)
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

                #region Check is exist user
                MethodResult<BaseUserResponse> appUser = await _userQueries.GetUserModelByGuidAsync(request.GuidUser);
                if (appUser.Result == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(appUser.Result.Username), appUser.Result?.Username ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Delete User
                if (await _userRepository.DeleteUserAsync(request.GuidUser) <= 0)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                       new[] { Helpers.GenerateErrorResult(nameof(appUser.Result.Username), appUser.Result?.Username ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.Result = false;
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
            methodResult.Result = true;
            return methodResult;
        }
    }
}

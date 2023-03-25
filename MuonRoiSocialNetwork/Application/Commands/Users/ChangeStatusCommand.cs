using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Request;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Command change status account request
    /// </summary>
    public class ChangeStatusCommand : ChangeStatusUserModel, IRequest<MethodResult<bool>>
    { }
    /// <summary>
    /// Handler command
    /// </summary>
    public class ChangeStatusCommandHandler : BaseCommandHandler, IRequestHandler<ChangeStatusCommand, MethodResult<bool>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        public ChangeStatusCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository) : base(mapper, configuration, userQueries, userRepository)
        {
        }
        /// <summary>
        /// Function handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<bool>> Handle(ChangeStatusCommand request, CancellationToken cancellationToken)
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
                MethodResult<BaseUserResponse> baseUserResponse = await _userQueries.GetUserModelByGuidAsync(request.Id);
                if (baseUserResponse.Result == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR13C),
                        new[] { Helpers.GenerateErrorResult(nameof(baseUserResponse.Result.Username), baseUserResponse.Result?.Username ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Update status account
                AppUser userChange = await _userQueries.GetByGuidAsync(baseUserResponse.Result.Id);
                userChange.AccountStatus = request.AccountStatus;
                userChange.LockReason = request.Reason;
                if (await _userRepository.UpdateUserAsync(userChange) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR29C), EnumUserErrorCodes.USR29C.ToString()) }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                methodResult.Result = true;
                methodResult.StatusCode = StatusCodes.Status200OK;
                return methodResult;
                #endregion
            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddResultFromErrorList(ex.ErrorMessages);
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
            }
            return methodResult;
        }
    }
}

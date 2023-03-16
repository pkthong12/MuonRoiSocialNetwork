using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Common.Requests.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Update user command
    /// </summary>
    public class UpdateInformationCommand : CreateUserCommandModel, IRequest<MethodResult<UserModelRequest>>
    {
        /// <summary>
        /// Guid user update
        /// </summary>
        public Guid UserGuid { get; set; }
    }
    /// <summary>
    /// Handler update infor user
    /// </summary>
    public class UpdateInformationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateInformationCommand, MethodResult<UserModelRequest>>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userRepository"></param>
        /// <param name="userQueries"></param>
        public UpdateInformationCommandHandler(IMapper mapper,
            IConfiguration configuration, IUserRepository userRepository, IUserQueries userQueries) : base(mapper, configuration, userQueries, userRepository)
        { }
        /// <summary>
        /// Function Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<UserModelRequest>> Handle(UpdateInformationCommand request, CancellationToken cancellationToken)
        {
            MethodResult<UserModelRequest> methodResult = new();
            try
            {
                #region Validation
                AppUser appUser = _mapper.Map<AppUser>(request);
                if (appUser.IsValid())
                {
                    throw new CustomException(appUser.ErrorMessages);
                }
                #endregion

                #region Check is exist user
                bool userIsExist = await _userRepository.ExistUserByUsernameAsync(appUser.UserName ?? "");
                if (!userIsExist)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(appUser.UserName), appUser.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Update info user
                if (await _userRepository.UpdateUserAsync(appUser) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(appUser.UserName), appUser.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Return info user updated
                AppUser inforResult = await _userQueries.GetByUsernameAsync(appUser.UserName ?? "");
                UserModelRequest resultInforLoginUser = _mapper.Map<UserModelRequest>(inforResult);
                methodResult.Result = resultInforLoginUser;
                methodResult.StatusCode = StatusCodes.Status200OK;
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

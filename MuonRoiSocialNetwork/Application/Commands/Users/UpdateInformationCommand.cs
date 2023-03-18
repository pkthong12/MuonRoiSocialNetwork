using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.Infrashtructure;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Request;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Update user command
    /// </summary>
    public class UpdateInformationCommand : UserModelRequest, IRequest<MethodResult<BaseUserResponse>>
    {
        /// <summary>
        /// Guid user update
        /// </summary>
        public Guid UserGuid { get; set; }
    }
    /// <summary>
    /// Handler update infor user
    /// </summary>
    public class UpdateInformationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateInformationCommand, MethodResult<BaseUserResponse>>
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
        public async Task<MethodResult<BaseUserResponse>> Handle(UpdateInformationCommand request, CancellationToken cancellationToken)
        {
            MethodResult<BaseUserResponse> methodResult = new();
            try
            {
                #region Check is exist user
                AppUser userIsExist = await _userQueries.GetByUsernameAsync(request.UserName ?? "");
                if (userIsExist == null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR02C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.UserName), request.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion
                if (request.Email != userIsExist.Email)
                {
                    AppUser existingUser = await _userQueries
                    .GetUserByEmailAsync(request.Email ?? "")
                    .ConfigureAwait(false);
                    if (existingUser != null)
                    {
                        methodResult.StatusCode = StatusCodes.Status400BadRequest;
                        methodResult.AddApiErrorMessage(
                            nameof(EnumUserErrorCodes.USRC34C),
                            new[] { Helpers.GenerateErrorResult(nameof(request.Email), request.Email) }
                        );
                        return methodResult;
                    }
                }
                #region Validation
                _mapper.Map(request, userIsExist);
                if (!userIsExist.IsValid())
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddResultFromErrorList(userIsExist.ErrorMessages);
                    return methodResult;
                }
                #endregion

                #region Update info user
                userIsExist.Salt = GenarateSalt();
                userIsExist.PasswordHash = HashPassword(request.PasswordHash ?? "", userIsExist.Salt);
                if (await _userRepository.UpdateUserAsync(userIsExist) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(userIsExist.UserName), userIsExist.UserName ?? "") }
                    );
                    return methodResult;
                }
                #endregion

                #region Return info user updated
                AppUser inforResult = await _userQueries.GetByUsernameAsync(userIsExist.UserName ?? "");
                BaseUserResponse resultInforLoginUser = _mapper.Map<BaseUserResponse>(inforResult);
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

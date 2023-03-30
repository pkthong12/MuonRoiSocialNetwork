using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.Infrashtructure;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Request;
using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Response;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles;

namespace MuonRoiSocialNetwork.Application.Commands.GroupAndRoles
{
    /// <summary>
    /// Request init group
    /// </summary>
    public class InitialGroupCommand : GroupInitialBaseRequest, IRequest<MethodResult<GroupInitialBaseResponse>>
    { }
    /// <summary>
    /// Handler request
    /// </summary>
    public class InitialGroupCommandHandler : BaseGroupAndRoleCommandHandler, IRequestHandler<InitialGroupCommand, MethodResult<GroupInitialBaseResponse>>
    {
        private readonly ILogger<InitialGroupCommandHandler> _logger;
        private readonly AuthContext _authContext;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="roleRepository"></param>
        /// <param name="groupRepository"></param>
        /// <param name="logger"></param>
        /// <param name="roleQueries"></param>
        /// <param name="groupQueries"></param>
        /// <param name="authContext"></param>
        public InitialGroupCommandHandler(IMapper mapper, IConfiguration configuration, IRoleRepository roleRepository, IGroupRepository groupRepository, IRoleQueries roleQueries, ILoggerFactory logger, AuthContext authContext, IGroupQueries groupQueries) : base(mapper, configuration, roleRepository, groupRepository, roleQueries, groupQueries)
        {
            _logger = logger.CreateLogger<InitialGroupCommandHandler>();
            _authContext = authContext;
        }
        /// <summary>
        /// Function handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<GroupInitialBaseResponse>> Handle(InitialGroupCommand request, CancellationToken cancellationToken)
        {
            MethodResult<GroupInitialBaseResponse> methodResult = new();
            try
            {
                #region Validation
                GroupUserMember newGroup = _mapper.Map<GroupUserMember>(request);
                newGroup.CreatedUserGuid = new Guid(_authContext.Guid);
                if (!newGroup.IsValid())
                {
                    throw new CustomException(newGroup.ErrorMessages);
                }
                #endregion

                #region Check group is not exist
                bool isExistGroup = await _groupQueries.GetByNameAsync(request.GroupName);
                if (!isExistGroup)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumGroupErrorCodes.GRP13C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.GroupName), request.GroupName) }
                    );
                    return methodResult;
                }
                #endregion

                #region Create new group
                _groupRepository.Add(newGroup);
                int checkStatus = await _groupRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                if (checkStatus < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USR29C), nameof(EnumUserErrorCodes.USR29C)) }
                    );
                    return methodResult;
                }
                #endregion

                #region Return group info
                GroupUserMember responseNewGroup = await _groupRepository.GetByIdAsync(newGroup.Id);
                if (responseNewGroup is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumGroupErrorCodes.GRP04C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumGroupErrorCodes.GRP04C), nameof(EnumGroupErrorCodes.GRP04C)) }
                    );
                    return methodResult;
                }
                methodResult.Result = _mapper.Map(responseNewGroup, methodResult.Result);
                #endregion

                return methodResult;
            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(InitialGroupCommand) STEP CUSTOMEXCEPTION --> ID USER {ex} ---->");
#pragma warning disable CS8604
                methodResult.AddResultFromErrorList(ex?.ErrorMessages);
#pragma warning restore CS8604
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(InitialGroupCommand) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(InitialGroupCommand) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }
        }
    }
}

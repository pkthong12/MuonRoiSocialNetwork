using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Request;
using MuonRoiSocialNetwork.Common.Models.GroupAndRoles.Base.Response;
using MuonRoiSocialNetwork.Domains.DomainObjects.Groups;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.GroupAndRoles;

namespace MuonRoiSocialNetwork.Application.Commands.GroupAndRoles
{
    /// <summary>
    /// Request init role
    /// </summary>
    public class InitialRoleCommand : RoleInitialBaseRequest, IRequest<MethodResult<RoleInitialBaseResponse>>
    { }
    /// <summary>
    /// Handler command init role
    /// </summary>
    public class InitialRoleCommandHandler : BaseGroupAndRoleCommandHandler, IRequestHandler<InitialRoleCommand, MethodResult<RoleInitialBaseResponse>>
    {
        private readonly ILogger<InitialRoleCommandHandler> _logger;
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
        public InitialRoleCommandHandler(IMapper mapper, IConfiguration configuration, IRoleRepository roleRepository, IGroupRepository groupRepository, ILoggerFactory logger, IRoleQueries roleQueries, IGroupQueries groupQueries) : base(mapper, configuration, roleRepository, groupRepository, roleQueries, groupQueries)
        {
            _logger = logger.CreateLogger<InitialRoleCommandHandler>();
        }

        /// <summary>
        /// Function handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<MethodResult<RoleInitialBaseResponse>> Handle(InitialRoleCommand request, CancellationToken cancellationToken)
        {
            MethodResult<RoleInitialBaseResponse> methodResult = new();
            try
            {
                #region Validation
                AppRole newRole = _mapper.Map<AppRole>(request);
                if (!newRole.IsValid())
                {
                    throw new CustomException(newRole.ErrorMessages);
                }
                #endregion

                #region Check role is not exist
                bool isExistRole = await _roleQueries.GetRoleByNameAsync(request.Name);
                if (!isExistRole)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumRoleErrorCodes.ROL07C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.Name), request.Name) }
                    );
                    return methodResult;
                }
                #endregion

                #region Check group id is exist
                GroupUserMember group = await _groupRepository.GetByIdAsync(request.GroupId);
                if (group is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumGroupErrorCodes.GRP04C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.GroupId), request.GroupId) }
                    );
                    return methodResult;
                }
                #endregion

                #region Create Role
                newRole.NormalizedName = newRole.Name.ToUpper();
                if (await _roleRepository.InitialRoleAsync(newRole) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(newRole.Name), newRole.Name) }
                    );
                    return methodResult;
                }
                RoleInitialBaseResponse roleInfo = await _roleQueries.GetRoleByGuidAsync(newRole.Id);
                roleInfo.GroupName = group.GroupName;
                methodResult.Result = roleInfo;
                methodResult.StatusCode = StatusCodes.Status200OK;
                #endregion

                return methodResult;
            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(InitialRoleCommand) STEP CUSTOMEXCEPTION --> ID USER {ex} ---->");
#pragma warning disable CS8604
                methodResult.AddResultFromErrorList(ex?.ErrorMessages);
#pragma warning restore CS8604
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(InitialRoleCommand) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(InitialRoleCommand) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return methodResult;
            }

        }
    }
}

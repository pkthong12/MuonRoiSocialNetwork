using AutoMapper;
using BaseConfig.EntityObject.Entity;
using BaseConfig.Exeptions;
using BaseConfig.MethodResult;
using MediatR;
using MuonRoi.Social_Network.Roles;
using MuonRoi.Social_Network.Users;
using MuonRoiSocialNetwork.Application.Commands.Base;
using MuonRoiSocialNetwork.Application.Commands.GroupAndRoles;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.GroupAndRoles;
using MuonRoiSocialNetwork.Domains.Interfaces.Commands.Users;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries.Users;

namespace MuonRoiSocialNetwork.Application.Commands.Users
{
    /// <summary>
    /// Assign user request
    /// </summary>
    public class AssignUserToGroupCommand : IRequest<MethodResult<bool>>
    {
        /// <summary>
        /// Guid of user
        /// </summary>
        public Guid UserGuid { get; set; }
        /// <summary>
        /// Group id
        /// </summary>
        public int GroupId { get; set; }
    }
    /// <summary>
    /// Assign user handler
    /// </summary>
    public class AssignUserToGroupCommandHandler : BaseUserCommandHandler, IRequestHandler<AssignUserToGroupCommand, MethodResult<bool>>
    {
        private readonly ILogger<AssignRoleCommandHandler> _logger;
        private readonly IGroupRepository _groupRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="configuration"></param>
        /// <param name="userQueries"></param>
        /// <param name="userRepository"></param>
        /// <param name="logger"></param>
        /// <param name="groupRepository"></param>
        public AssignUserToGroupCommandHandler(IMapper mapper, IConfiguration configuration, IUserQueries userQueries, IUserRepository userRepository, ILoggerFactory logger, IGroupRepository groupRepository) : base(mapper, configuration, userQueries, userRepository)
        {
            _logger = logger.CreateLogger<AssignRoleCommandHandler>();
            _groupRepository = groupRepository;
        }
        /// <summary>
        /// Function handler
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<MethodResult<bool>> Handle(AssignUserToGroupCommand request, CancellationToken cancellationToken)
        {
            MethodResult<bool> methodResult = new();
            try
            {
                #region Check valid request
                if (request is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USRC49C),
                        new[] { Helpers.GenerateErrorResult(nameof(EnumUserErrorCodes.USRC49C), nameof(EnumUserErrorCodes.USRC49C) ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Check is exist user
                MethodResult<BaseUserResponse> baseUserResponse = await _userQueries.GetUserModelByGuidAsync(request.UserGuid);
                if (baseUserResponse.Result is null)
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

                #region Check is exist group
                GroupUserMember existGroup = await _groupRepository.GetByIdAsync(request.GroupId);
                if (existGroup is null)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumGroupErrorCodes.GRP04C),
                        new[] { Helpers.GenerateErrorResult(nameof(request.GroupId), request.GroupId) }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                #region Assign groupId to user table
                AppUser infoUser = _mapper.Map<AppUser>(baseUserResponse.Result);
                infoUser.GroupId = existGroup.Id;
                if (await _userRepository.UpdateUserAsync(infoUser) < 1)
                {
                    methodResult.StatusCode = StatusCodes.Status400BadRequest;
                    methodResult.AddApiErrorMessage(
                        nameof(EnumUserErrorCodes.USR29C),
                        new[] { Helpers.GenerateErrorResult(nameof(infoUser.Name), infoUser.Name ?? "") }
                    );
                    methodResult.Result = false;
                    return methodResult;
                }
                #endregion

                methodResult.Result = true;
                return methodResult;

            }
            catch (CustomException ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(AssignUserToGroupCommand) STEP CUSTOMEXCEPTION --> ID USER {ex} ---->");
#pragma warning disable CS8604
                methodResult.AddResultFromErrorList(ex?.ErrorMessages);
#pragma warning restore CS8604
                methodResult.Result = false;
                return methodResult;
            }
            catch (Exception ex)
            {
                methodResult.StatusCode = StatusCodes.Status400BadRequest;
                _logger.LogError($" -->(AssignUserToGroupCommand) STEP EXEPTION MESSAGE --> ID USER {ex} ---->");
                _logger.LogError($" -->(AssignUserToGroupCommand) STEP EXEPTION STACK --> ID USER {ex.StackTrace} ---->");
                methodResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                methodResult.Result = false;
                return methodResult;
            }
        }
    }
}

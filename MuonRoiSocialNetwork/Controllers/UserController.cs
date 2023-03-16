using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MuonRoiSocialNetwork.Common.Models.Users;
using MuonRoiSocialNetwork.Application.Commands.Users;
using MuonRoiSocialNetwork.Application.Commands.Email;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using MuonRoi.Social_Network.Users;

namespace MuonRoiSocialNetwork.Controllers
{
    /// <summary>
    /// Auth: PhiLe 20230305
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserQueries _userQueries;

        /// <summary>
        /// Constructure
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="userQueries"></param>
        public UserController(IMediator mediator, IUserQueries userQueries)
        {
            _mediator = mediator;
            _userQueries = userQueries;
        }
        /// <summary>
        /// Register new user API
        /// </summary>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(MethodResult<UserModelRequest>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand cmd)
        {
            try
            {
                MethodResult<UserModelRequest> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        /// <summary>
        /// Login API
        /// </summary>
        /// <returns></returns>
        [HttpPost("auth")]
        [ProducesResponseType(typeof(MethodResult<UserModelRequest>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAuth([FromBody] AuthUserCommand cmd)
        {
            try
            {
                MethodResult<UserModelRequest> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        /// <summary>
        /// Verification email
        /// </summary>
        /// <returns></returns>
        [HttpPatch("verificationEmail/{uid}/{token}")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VerificationEmail([FromRoute] Guid uid, string token)
        {
            try
            {
                VerificationEmailCommand cmd = new()
                {
                    UserGuid = uid,
                    TokenJWT = token
                };
                MethodResult<bool> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        /// <summary>
        /// Update informations for user
        /// </summary>
        /// <returns></returns>
        [HttpPut("{uid}")]
        [ProducesResponseType(typeof(MethodResult<UserModelRequest>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateInformation([FromBody] Guid uid)
        {
            try
            {
                UpdateInformationCommand cmd = new()
                {
                    UserGuid = uid,
                };
                MethodResult<UserModelRequest> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpGet("{username}")]
        [ProducesResponseType(typeof(MethodResult<UserModelResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByUserName([FromBody] string username)
        {
            try
            {
                MethodResult<UserModelResponse> methodResult = await _userQueries.GetUserModelBynameAsync(username).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        /// <summary>
        /// Get user by Guid
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpGet("{guidUser}")]
        [ProducesResponseType(typeof(MethodResult<UserModelResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByGuid([FromBody] Guid guidUser)
        {
            try
            {
                MethodResult<UserModelResponse> methodResult = await _userQueries.GetUserModelByGuidAsync(guidUser).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
    }
}
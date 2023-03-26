using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MuonRoiSocialNetwork.Application.Commands.Users;
using MuonRoiSocialNetwork.Application.Commands.Email;
using MuonRoiSocialNetwork.Domains.Interfaces.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MuonRoiSocialNetwork.Common.Models.Users.Response;
using MuonRoiSocialNetwork.Common.Models.Users.Base.Response;

namespace MuonRoiSocialNetwork.Controllers
{
    /// <summary>
    /// Auth: PhiLe 20230305
    /// </summary>
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        #region Repository
        /// <summary>
        /// Register new user API
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MethodResult<UserModelResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand cmd)
        {
            try
            {
                MethodResult<UserModelResponse> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
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
        [AllowAnonymous]
        [ProducesResponseType(typeof(MethodResult<UserModelResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> LoginAuth([FromBody] AuthUserCommand cmd)
        {
            try
            {
                MethodResult<UserModelResponse> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
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
        [HttpPatch("mail/{uid}/{token}")]
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
        [HttpPut]
        [ProducesResponseType(typeof(MethodResult<BaseUserResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateInformation([FromForm] UpdateInformationCommand userChange)
        {
            try
            {
                MethodResult<BaseUserResponse> methodResult = await _mediator.Send(userChange).ConfigureAwait(false);
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
        /// Delete user by Guid
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpDelete("{uid}")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteUserByGuid([FromRoute] Guid uid)
        {
            try
            {
                DeleteUserCommand cmd = new()
                {
                    GuidUser = uid
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
        /// User forgot password
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpPost("forgot-password")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FotgotPassword([FromForm] ForgotPasswordUserCommand cmd)
        {
            try
            {
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
        /// User change password
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpPatch("change-passoword")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangePasswordForgot([FromForm] ChangePasswordCommand cmd)
        {
            try
            {
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
        /// Change status account ( lock | active )
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpPatch("statusAccount")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeStatusAccount([FromForm] ChangeStatusCommand cmd)
        {
            try
            {
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
        /// Resend veritifycation mail
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpPost("mail/resend")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResendMailVeritification([FromForm] ResendMailVeritificationCommand cmd)
        {
            try
            {
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
        #endregion

        #region Queries
        /// <summary>
        /// Get user by username
        /// </summary>
        /// <returns>UserModel</returns>
        [HttpGet("default/{username}")]
        [ProducesResponseType(typeof(MethodResult<BaseUserResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByUserName([FromRoute] string username)
        {
            try
            {
                MethodResult<BaseUserResponse> methodResult = await _userQueries.GetUserModelBynameAsync(username).ConfigureAwait(false);
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
        [HttpGet("{uid}")]
        [ProducesResponseType(typeof(MethodResult<BaseUserResponse>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetUserByGuid([FromRoute] Guid uid)
        {
            try
            {
                MethodResult<BaseUserResponse> methodResult = await _userQueries.GetUserModelByGuidAsync(uid).ConfigureAwait(false);
                return methodResult.GetActionResult();
            }
            catch (Exception ex)
            {
                var errCommandResult = new VoidMethodResult();
                errCommandResult.AddErrorMessage(Helpers.GetExceptionMessage(ex), ex.StackTrace ?? "");
                return errCommandResult.GetActionResult();
            }
        }
        #endregion
    }
}
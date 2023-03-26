using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MuonRoiSocialNetwork.Application.Commands.RefreshToken;
using System.Net;

namespace MuonRoiSocialNetwork.Controllers
{
    /// <summary>
    /// Auth: PhiLe 20230325
    /// </summary>
    [Route("api/token")]
    [ApiController]
    public class RefreshTokenController : Controller
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public RefreshTokenController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Repository
        /// <summary>
        /// Genarate refresh token
        /// </summary>
        /// <returns></returns>
        [HttpPost("{userid}")]
        [ProducesResponseType(typeof(MethodResult<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GennerateRefreshToken([FromRoute] Guid userid)
        {
            try
            {
                GennerateRefreshTokenCommand cmd = new()
                {
                    UserId = userid
                };
                MethodResult<string> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
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
        /// Revoke refresh token | logout
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout/{userid}")]
        [ProducesResponseType(typeof(MethodResult<bool>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RevokeRefreshToken([FromRoute] Guid userid)
        {
            try
            {
                RevokeRefreshTokenCommand cmd = new()
                {
                    UserId = userid
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
        /// Renew access token
        /// </summary>
        /// <returns></returns>
        [HttpPost("{userid}/{refreshToken}")]
        [ProducesResponseType(typeof(MethodResult<string>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RenewAccessToken([FromRoute] string refreshToken, Guid userid)
        {
            try
            {
                RenewAccessTokenCommand cmd = new()
                {
                    UserId = userid,
                    RefreshToken = refreshToken
                };
                MethodResult<string> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
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
        #endregion
    }
}

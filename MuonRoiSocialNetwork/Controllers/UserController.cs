using BaseConfig.EntityObject.Entity;
using BaseConfig.MethodResult;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using MuonRoiSocialNetwork.Common.Models.Users;
using System.Threading.Tasks;
using MuonRoiSocialNetwork.Application.Commands.Users;

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
        /// <summary>
        /// Constructure
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Register new user API
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(MethodResult<UserModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(VoidMethodResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserCommand cmd)
        {
            try
            {
                MethodResult<UserModel> methodResult = await _mediator.Send(cmd).ConfigureAwait(false);
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

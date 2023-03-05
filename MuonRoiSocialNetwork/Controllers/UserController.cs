using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MuonRoiSocialNetwork.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

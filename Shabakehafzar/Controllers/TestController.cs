using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shabakehafzar.API.Base;

namespace Shabakehafzar.API.Controllers
{
    [Route("api/Shabakehafzar/[controller]")]
    [ApiController]
    public class TestController : ApiControllerBase
    {
        [HttpGet("Public")]
        public  IActionResult PublicAction()
        {
            return Ok();
        }

        [HttpGet("Private")]
        public async Task<IActionResult> PrivateAction()
        {
            var havePermission = UserRoles.Any(c=>c.Equals("admin_role"));
            if (!havePermission)
                return Forbid();

            return Ok();
        }
    }
}

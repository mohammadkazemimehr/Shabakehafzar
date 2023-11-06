using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shabakehafzar.API.Base.Model;
using Shabakehafzar.API.Helper;

namespace Shabakehafzar.API.Base
{
    [ApiController]
    [Authorize]
    public class ApiControllerBase : ControllerBase
    {
        protected string GetAccessToken(HttpRequest httpRequest) => httpRequest.Headers.ContainsKey("Authorization")
                                                        ? httpRequest.Headers["Authorization"].ToString().Split(" ")[1]
                                                        : string.Empty;
        protected string AccessToken => GetAccessToken(Request);
        protected virtual string UserName => ClaimHelper.GetUserName(this.AccessToken);
        protected virtual Guid UserId => ClaimHelper.GetUserId(this.AccessToken);

        protected virtual IEnumerable<string> UserRoles => ClaimHelper.GetUserRole(AccessToken);
        protected ApiControllerBase()
        {
        }

        #region responses 
        [NonAction]
        protected virtual IActionResult OkResult()
        {
            return this.OkResult("", null);
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message)
        {
            return this.OkResult(message, null);
        }

        [NonAction]
        protected virtual IActionResult OkContentResult(object content)
        {
            return this.OkResult("", content);
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message, object content)
        {
            return Ok(new ResponseMessage(message, content));
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message, object content, int total)
        {
            return Ok(new ResponseMessage(message, content, total));
        }
        #endregion

    }
}

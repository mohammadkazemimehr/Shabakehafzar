using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shabakehafzar.API.Helper.Config;
using Shabakehafzar.Application.DTOs.RequestModels;
using Shabakehafzar.Application.Service.Authentication;

namespace Shabakehafzar.API.Controllers
{
    [Route("api/Shabakehafzar/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationservice _authenticationService;
        public AuthenticationController(IAuthenticationservice authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var result =await _authenticationService.Login(loginRequest);
            if (!result.IsSuccess)
                return Unauthorized(new { Message = "Username or password is incorrect." });

            return Ok(result);
        }
    }
}

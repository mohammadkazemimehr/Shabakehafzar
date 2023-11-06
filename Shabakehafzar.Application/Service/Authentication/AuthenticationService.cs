using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shabakehafzar.Application.Helpers;
using Shabakehafzar.Core.Models;

namespace Shabakehafzar.Application.Service.Authentication
{
    public class Authenticationservice : IAuthenticationservice
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;


        public Authenticationservice(
            UserManager<User> userManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserTokenResponseDto> Login(LoginRequest loginRequestDto)
        {
            var user = await _userManager.FindByNameAsync(loginRequestDto.UserName);
            
            if (user == null)
                return new UserTokenResponseDto { IsSuccess = false };

            var passwordIsCorrect = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

            if (passwordIsCorrect is false)
                return new UserTokenResponseDto { IsSuccess = false };


            var userRoles = await _userManager.GetRolesAsync(user);

            var token = AuthHelper.GenerateEncodedToken(_configuration, userRoles, user.Id,user.UserName);

            return new UserTokenResponseDto
            {
                IsSuccess = true,
                AccessToken = token
            };
        }
    }
}

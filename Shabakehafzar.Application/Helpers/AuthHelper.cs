using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shabakehafzar.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shabakehafzar.Application.Helpers
{
    public static class AuthHelper
    {
        public static string GenerateEncodedToken(IConfiguration configuration, IList<string> userRoles, string userId, string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtIssuerOptions:SecretKey"]);
            var claims = new List<Claim>()
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userId),
                 new Claim(JwtRegisteredClaimNames.UniqueName, userName),
                 new Claim("iss",configuration["JwtIssuerOptions:Issuer"] ),
                 new Claim( "aud" , configuration["JwtIssuerOptions:Audience"] )
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var tokenDescriptor = new SecurityTokenDescriptor();

            tokenDescriptor.Subject = new ClaimsIdentity(claims);

            tokenDescriptor.Expires = DateTime.Now.AddDays(7);
            tokenDescriptor.SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}

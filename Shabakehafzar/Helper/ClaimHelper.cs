using Shabakehafzar.API.Helper.Extentions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shabakehafzar.API.Helper
{
    public static class ClaimHelper
    {
        public static T GetClaim<T>(string accessToken, string claimType)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadToken(accessToken) as JwtSecurityToken;

            var claim = token.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            var result = claim.ToType<T>();
            return result;
        }

        public static IEnumerable<string> GetUserRole(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "role");
            var roleClaimAsString = hasClaim ? GetClaim<string>(accessToken, "role") :String.Empty;
            var roles = roleClaimAsString.Split(',');

            return roles.AsEnumerable();
        }

        public static bool HasClaim(string accessToken, string claimType)
        {
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrWhiteSpace(accessToken))
                return false;

            var handler = new JwtSecurityTokenHandler();
            var tokenS = handler.ReadToken(accessToken) as JwtSecurityToken;

            var claim = tokenS.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;

            return claim != null;
        }

        public static Guid GetUserId(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "userId");
            return hasClaim ? GetClaim<Guid>(accessToken, "userId") : Guid.Empty;
        }

        public static string GetUserName(string accessToken)
        {
            var hasClaim = HasClaim(accessToken, "userName");
            return hasClaim ? GetClaim<string>(accessToken, "userId") : "";
        }
    }
}

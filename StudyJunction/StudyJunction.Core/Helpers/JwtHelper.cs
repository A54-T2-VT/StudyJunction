

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudyJunction.Core.Helpers
{
    public class JwtHelper
    {
        public static string GetNameClaimFromJwt(string jwt)
        {
            jwt = jwt.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(jwt) as JwtSecurityToken;

            var username = string.Empty;

            if (jsonToken != null)
            {
                var nameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (nameClaim != null)
                {
                    username = nameClaim.Value;
                }
            }

            if(username == string.Empty)
            {
                throw new NotImplementedException();
            }

            return username; // Return null if the name claim is not found
        }

        public static string GetNameIdentifierClaimFromJwt(string jwt)
        {
            {
                jwt = jwt.Replace("Bearer ", "");

                var tokenHandler = new JwtSecurityTokenHandler();
                var jsonToken = tokenHandler.ReadToken(jwt) as JwtSecurityToken;

                var id = string.Empty;

                if (jsonToken != null)
                {
                    var nameClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (nameClaim != null)
                    {
                        id = nameClaim.Value;
                    }
                }

                if (id == string.Empty)
                {
                    throw new NotImplementedException();
                }

                return id; // Return null if the name claim is not found
            }
        }

    }
}

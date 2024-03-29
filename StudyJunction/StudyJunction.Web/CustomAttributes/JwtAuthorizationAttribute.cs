﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyJunction.Infrastructure.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudyJunction.Web.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class JwtAuthorizationAttribute : Attribute, IAuthorizationFilter
    {
        private readonly byte[] key = Encoding.ASCII.GetBytes("0xCCFA1F3F7D74D68C7FC9BEDB7CA791BB5D2D3DE15B6E0A9729608C6B0551993E1DCF49CF1FDF1BAADF");

        public string[] ClearedRoles { get; set; } = new string[0];

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Retrieve the Authorization header from the HTTP request
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Extract the token from the Authorization header (assuming it's in the "Bearer" format)
            var token = authorizationHeader.Replace("Bearer ", "");

            if (IsTokenValid(token))
            {
                // Perform any additional authorization logic here if needed

                if(ClearedRoles.Length == 0)
                {
                    return;
                }

                var tokenRole = GetRoleFromToken(token);
                if (!ClearedRoles.Contains(tokenRole))
                {
                    context.Result = new UnauthorizedResult();
                }

            }
            else
            {
                context.Result = new UnauthorizedResult();
            }
        }

        private string GetRoleFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            string role = string.Empty;

            if (jsonToken != null)
            {
                role = jsonToken.Claims
                    .Where(c => c.Type == ClaimTypes.Role || c.Type == "roles")
                    .Select(c => c.Value)
                    .First();


            }
            return role;
        }

        private bool IsTokenValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {

                // Validate the token (add your own validation logic here)
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),// Add your key validation logic
                    ValidateIssuer = false,  // Modify as needed
                    ValidateAudience = false, 
                    ClockSkew = TimeSpan.Zero
                }, out _);

                return true;
            }
            catch
            {
                return false;
            }

            //var tokenHandler = new JwtSecurityTokenHandler();

            //try
            //{
            //    var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            //    if (jsonToken == null)
            //        return false; // Token is not a valid JWT

            //    var expiration = jsonToken.ValidTo;

            //    if (expiration == null)
            //        return false; // Token does not have an expiration date

            //    return expiration <= DateTime.UtcNow;
            //}
            //catch (Exception)
            //{
            //    return true; // An error occurred while trying to read the token
            //}
        }
    }
}

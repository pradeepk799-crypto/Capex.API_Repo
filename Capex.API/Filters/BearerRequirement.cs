using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Capex.Models.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Capex.API.Filters
{
    public class BearerRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// IsTokenValid.
        /// </summary>
        /// <param name="token">token.</param>
        /// <returns>bool.</returns>
        public async Task<bool> IsTokenValid(string token)
        {
            //LoggerUtil.Current.Debug(LoggerMessage.Begin);
            if (string.IsNullOrEmpty(token))
                return false;
            //var key = Encoding.ASCII.GetBytes(AppSettings.Current.Secret);
            var key = Encoding.ASCII.GetBytes("abc");
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            };

            try
            {
                new JwtSecurityTokenHandler()
                    .ValidateToken(token, validationParameters, out var rawValidatedToken);
                if (rawValidatedToken == null)
                    return false;
                else
                {
                    JwtSecurityToken returntoken = (JwtSecurityToken)rawValidatedToken;
                    var tokenType = returntoken.Claims.FirstOrDefault(c => c.Type == "TokenType");

                    if (tokenType != null && Convert.ToString(tokenType.Value) == "AccessToken")
                        return true;
                    else
                        throw new SecurityTokenValidationException();
                }
            }
            catch (SecurityTokenValidationException ex)
            {
                //LoggerUtil.Current.Error(LoggerMessage.UnauthorizedUser + ex.Message);
                return false;
            }
            finally
            {
                //LoggerUtil.Current.Debug(LoggerMessage.End);
            }
        }
    }

    /// <summary>
    /// BearerAuthorizationHandler.
    /// </summary>
    public class BearerAuthorizationHandler : AuthorizationHandler<BearerRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="BearerAuthorizationHandler"/> class.
        /// BearerAuthorizationHandler.
        /// </summary>
        /// <param name="httpContextAccessor">The HttpContextAccessor.</param>
        public BearerAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, BearerRequirement requirement)
        {
            var authFilterCtx = (AuthorizationFilterContext)context.Resource;
            string authHeader = authFilterCtx.HttpContext.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.Contains("Bearer"))
            {
                var token = authHeader.Replace("Bearer ", string.Empty);
                if (await requirement.IsTokenValid(token))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    Exception ex = new Exception("401");
                    throw ex;
                }
            }
        }
    }
}

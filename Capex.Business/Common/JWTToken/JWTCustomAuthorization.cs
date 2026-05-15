using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Common.JWTToken
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JWTCustomAuthorization :Attribute , IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {

            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues authTokens;
                Microsoft.Extensions.Primitives.StringValues apiKey;
                filterContext.HttpContext.Request.Headers.TryGetValue("authToken", out authTokens);
                filterContext.HttpContext.Request.Headers.TryGetValue("apiKey", out apiKey);

                var _token = authTokens.FirstOrDefault();
                var key = apiKey.FirstOrDefault();

                if (_token != null && key != null)
                {
                    string authToken = _token;
                    if (authToken != null)
                    {
                        if (IsValidToken(authToken, key))
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
                    filterContext.Result = new JsonResult("Please Provide authToken")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authToken"
                        },
                    };
                }
            }
        }

        public bool IsValidToken(string authToken, string apiKey)
        {
            if (!string.IsNullOrEmpty(authToken) && !string.IsNullOrEmpty(apiKey))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(apiKey);
                try
                {
                    tokenHandler.ValidateToken(authToken, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var userId = jwtToken.Claims.First(x => x.Type == "macToken").Value;
                    // return user id from JWT token if validation successful
                    if (!string.IsNullOrEmpty(userId))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                catch
                {
                    // return null if validation fails
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}

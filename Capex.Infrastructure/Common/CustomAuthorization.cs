using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Capex.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;


namespace Capex.Infrastructure.Common
{
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        private string ApiKeyValue = string.Empty;
        private readonly ServiceConfigSettings _service;
        private List<AuthenticationKey> _authKey;
        private List<AuthenticationKeyIGRS> _authKeyIGRS;
        public CustomAuthorization()
        {
            _service = ServiceConfiguration.serviceConfigSettings;
            _authKey = _service.Services.Where(x => x.ServiceName == "KeyAuthentication").FirstOrDefault().Authkey;
            _authKeyIGRS = _service.Services.Where(x => x.ServiceName == "KeyAuthenticationIGRS").FirstOrDefault().AuthkeyIGRS;
        }
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext != null)
            {
                Microsoft.Extensions.Primitives.StringValues apiKey;
                Microsoft.Extensions.Primitives.StringValues apiKeyigrs;

                //  filterContext.HttpContext.Request.Headers.TryGetValue("authToken", out authTokens);
                filterContext.HttpContext.Request.Headers.TryGetValue("authKey", out apiKey);
                filterContext.HttpContext.Request.Headers.TryGetValue("AuthkeyIGRS", out apiKeyigrs);

                //var _token = authTokens.FirstOrDefault();
                var key = apiKey.FirstOrDefault();
                var keyigrs = apiKeyigrs.FirstOrDefault();


                //  if (_token != null && key != null)
                if (key != null)
                {
                    string authKey = key;
                    if (authKey != null)
                    {
                        if (IsValidKey(authKey))
                        {
                            filterContext.HttpContext.Response.Headers.Add("authKey", authKey);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("authKey", authKey);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Key"
                                },
                            };
                        }

                    }

                }
                if (keyigrs != null)
                {
                    string authKey = keyigrs;
                    if (authKey != null)
                    {
                        if (IsValidKeyIGRS(authKey))
                        {
                            filterContext.HttpContext.Response.Headers.Add("AuthkeyIGRS", authKey);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");
                            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
                            return;
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("AuthkeyIGRS", authKey);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Key"
                                },
                            };
                        }

                    }

                }
                else
                {
                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authKey";
                    filterContext.Result = new JsonResult("Please Provide authKey")
                    {
                        Value = new
                        {
                            Status = "Error",
                            Message = "Please Provide authKey"
                        },
                    };
                }
            }
        }
        public bool IsValidKey(string apiKey)
        {
            var ApiKeyValueg = _authKey.Where(x => x.Key == "MLq5ilMhq6eKrWq1TxN5qMy").FirstOrDefault().Key;
            if (!string.IsNullOrEmpty(apiKey))
            {
                try
                {

                    if (apiKey == ApiKeyValue)
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
        public bool IsValidKeyIGRS(string apiKey)
        {
            var ApiKeyValueg = _authKeyIGRS.Where(x => x.Key == "MLq5ilMhq6eKrWq1IGrsqMy").FirstOrDefault().Key;
            if (!string.IsNullOrEmpty(apiKey))
            {
                try
                {

                    if (apiKey == ApiKeyValueg)
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

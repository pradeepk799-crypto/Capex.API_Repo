using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Capex.Models.RequestModel;
using Capex.Utilities.Common;
using Serilog;
using static Capex.Models.Common.APIResult;

namespace Capex.API.Areas.Common.Controllers
{
    public class BaseController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        /// <summary>
        /// Gets the authentication header.
        /// </summary>
        /// <typeparam name="T">type of T.</typeparam>
        /// <param name="out_put_object">The out put object.</param>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns> T.</returns>
        public T GetAuthHeader<T>(T out_put_object, HttpContext httpContext)
            where T : RequestModelBase, new()
        {
            try
            {
                out_put_object.AuthHeader = httpContext.User.Claims.FirstOrDefault(c => c.Type == APIConstants.AuthHeader).Value;
            }
            catch (Exception ex)
            {
                Log.Error("Error Message", ex);
                
                throw;
            }

            return out_put_object;
        }

        /// <summary>
        /// This method used to Provide Custom Error Response for validation failed.
        /// </summary>
        /// <typeparam name="T">T.</typeparam>
        /// <param name="out_put_object">The out put object.</param>
        /// <param name="modelState">State of the model.</param>
        /// <param name="language">The language.</param>
        /// <returns>
        /// type of T.
        /// </returns>
        public ApiResult<T> CustomBadRequest<T>(ApiResult<T> out_put_object, ModelStateDictionary modelState)
        {
            try
            {
                string errorMessage = string.Empty;
                string errorCode = string.Empty;

                
                var state = modelState.FirstOrDefault();
                if (state.Value != null)
                {
                    var error = state.Value.Errors.FirstOrDefault();
                    if (error != null)
                    {
                        errorCode = error.ErrorMessage;
                    }
                }

                out_put_object.ErrorCode = !string.IsNullOrEmpty(errorMessage) ? errorCode : string.Empty;
                out_put_object.Message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : errorCode;
                out_put_object.Status = false;
            }
            catch (Exception ex)
            {
                Log.Error("Error Message", ex);
                throw;
            }

            return out_put_object;
        }
    }
}

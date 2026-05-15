using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static Capex.Models.Common.APIResult;
using Capex.Utilities.Common;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Business.Interfaces;
using Capex.Models.ResponseModel;
using Capex.Models.RequestModel;
using System.Net;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Capex.API.Areas.Common.Models;
using System.Security.Cryptography;
using System.Text;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TokenController : BaseController
    {
        private readonly ILoginService loginService;
        private readonly IWebHostEnvironment _environment;
        private readonly LdapURL _Ldurl;
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenController"/> class.
        /// </summary>
        /// <param name="loginService">The login service.</param>
        public TokenController(ILoginService loginService, IOptions<LdapURL> options, IWebHostEnvironment environment)
        {
            this.loginService = loginService;
            this._environment = environment;
            this._Ldurl = options.Value;
        }

        #region Token 

        /// <summary>
        /// This API is used to Generate Access token and Refresh Token for given user.
        /// </summary>
        /// <remarks>
        /// <text>This API is used to Generate Access token and Refresh Token for given user.</text>
        /// <version>1.0</version>       
        /// </remarks>
        /// <param name="requestModel">TokenRequestModel tokenRequest.</param>
        /// <returns>TokenResponseModel.</returns>
        /// <response code="Err100001">[TagName] can not be blank</response>
        /// <response code="Err100028">Invalid Grant Type</response>
        /// <response code="Err100041">Invalid RefreshToken</response>
        /// <response code="Err00049">Please check your User ID and Password : Invalid User ID and Password</response>
        /// <response code="Err00016">System could not process your request at this time. Please try again</response>
        [AllowAnonymous]
        [HttpPost("Token")]
        public async Task<ApiResult<TokenResponseModel>> Token([FromBody] TokenRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<TokenResponseModel> responseModel = new ApiResult<TokenResponseModel>();
            //var useragent = Request.Headers["User-Agent"].ToString();     
            //requestModel.UserAgent = useragent;
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                responseModel= await this.loginService.Token(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        [AllowAnonymous]
        [HttpPost("CheckValidUser")]
        public async Task<ApiResult<ValidUserResponseModel>> CheckValidUser([FromBody] ValidUserRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.loginService.GetValidUser(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [AllowAnonymous]
        [HttpPost("UserChangePassword")]
        public async Task<ApiResult<ValidUserResponseModel>> UserChangePassword([FromBody] ChangePwsRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.loginService.ChangeUserPwd(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [AllowAnonymous]
        [HttpPost("CitiZenForgotPassword")]
        public async Task<ApiResult<ForgotPasswordResponseModel>> CitiZenForgotPassword([FromBody] CitizenForgotPwdModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ForgotPasswordResponseModel> responseModel = new ApiResult<ForgotPasswordResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return responseModel = await this.loginService.ForGotUserPwd(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        //[AllowAnonymous]
        //[HttpPost("CitiZenForgotPassword")]
        //public async Task<ApiResult<ForgotPasswordResponseModel>> ForgotPassword([FromBody] CitizenForgotPwdModel requestModel)
        //{
        //    Log.Debug(LoggerMessage.Begin);
        //    ApiResult<ForgotPasswordResponseModel> responseModel = new ApiResult<ForgotPasswordResponseModel>();
        //    if (this.ModelState.IsValid)
        //    {
        //        Log.Warning(LoggerMessage.ModelStateValidate);
        //        return responseModel = await this.loginService.ForGotUserPwd(requestModel);
        //    }
        //    else
        //    {
        //        Log.Warning(LoggerMessage.ModelStateInValid);
        //        this.CustomBadRequest(responseModel, this.ModelState);
        //    }

        //    Log.Debug(LoggerMessage.End);
        //    return responseModel;
        //}
        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<ApiResult<UserForgotPasswordResponseModel>> ForgotPassword(ForgotPasswordRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<UserForgotPasswordResponseModel> responseModel = new ApiResult<UserForgotPasswordResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return responseModel = await this.loginService.ForgotPassword(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        #endregion



    }
}

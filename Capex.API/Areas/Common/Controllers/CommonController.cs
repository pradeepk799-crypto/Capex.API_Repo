using Microsoft.AspNetCore.Mvc;
using Capex.Business.Interfaces;
using Capex.Infrastructure.Common;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Models.ResponseModel;
using System.Net;
using Capex.Models.RequestModel.Masters;
using static System.Net.WebRequestMethods;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class CommonController : BaseController
    {
        private readonly ICommon _common;
        private readonly ILogger<CommonController> _logger;
        public CommonController(ILogger<CommonController> logger, ICommon common)
        {
            this._logger = logger;
            this._common = common;
        }
        [HttpPost("GenerateOTP")]
        public async Task<ApiResult<OTPResponseModel>> GenerateOTP([FromBody] OTPRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();
            //if (this.ModelState.IsValid)
            //{
            _logger.LogWarning(LoggerMessage.ModelStateValidate);

            responseModel = await this._common.GenerateOTP(requestModel);

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }
        [HttpPost("SubmitOTP")]
        public async Task<ApiResult<OTPResponseModel>> SubmitOTP(OTPRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> otpResponseModel = new ApiResult<OTPResponseModel>();
           
            _logger.LogWarning(LoggerMessage.ModelStateValidate);
            ApiResult<bool> optResponse = await _common.ValidateOTP(requestModel);
            if (optResponse.ResponseData)
            {
                otpResponseModel.Status = optResponse.Status;
                otpResponseModel.Message = optResponse.Message;
            }
            else
            {
               
                otpResponseModel.Status = optResponse.Status;
                otpResponseModel.Message = optResponse.Message;
            }
            //}
            _logger.LogDebug(LoggerMessage.End);
            return otpResponseModel;
        }
        [HttpPost("ReSendOTP")]
        public async Task<ApiResult<OTPResponseModel>> ReSendOTP([FromBody] OTPRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._common.GenerateOTP(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }

        [HttpPost("GetCaptcha")]
        public async Task<ApiResult<CaptchaResponseModel>> GetCaptcha(HeaderHelperRequest request)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this._common.GenerateCaptcha(request);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                //this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;

        }

        [HttpPost("GenerateCaptcha")]
        public ApiResult<CaptchaResponseModel> GenerateCaptcha()
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();
            try
            {
                CaptchaResponseModel data = new CaptchaResponseModel();
                CaptchaGenerator generatedcaptcha = new CaptchaGenerator();

                string CapchaReferenceId = Guid.NewGuid().ToString();

                var captcha = generatedcaptcha.CaptchaWithCipher();
                if (!string.IsNullOrEmpty(captcha.CaptchaBase64))
                {
                    RedisHelper.SetData(CapchaReferenceId, captcha.Captcha, DateTimeOffset.Now.AddMinutes(AppSettings.Current.OTPExpiry));
                    //var ResCaptcha = RedisHelper.GetData(CapchaReferenceId);
                    data = new CaptchaResponseModel()
                    {
                        Cipher = CapchaReferenceId,
                        CaptchaBase64 = captcha.CaptchaBase64,
                        //Captcha = captcha.Captcha,


                    };
                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00000;
                    responseModel.Message = null;
                    responseModel.Status = true;

                }
                else
                {

                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00023;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, ""); ;
                    responseModel.Status = false;
                }

            }
            catch (Exception ex)
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00023;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, ""); ;
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [HttpPost("VerifyCaptcha")]
        public async Task<ApiResult<CaptchaResponseModel>> VerifyCaptcha([FromBody] CaptchaRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return this._common.VerifyCaptcha(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                //this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        [HttpPost("VaidateCaptcha")]
        public async Task<ApiResult<CaptchaResponseModel>> VaidateCaptcha([FromBody] CaptchaRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return this._common.VerifyCaptcha(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                //this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [HttpPost("RedisCacheClear")]
        public async Task<ApiResult<bool>> RedisCacheClear(RedisCacheRequestModel redisCacheRequestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<bool> responseModel = new ApiResult<bool>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                RedisHelper.RemoveData(redisCacheRequestModel.Key);
                responseModel.ResponseData = true;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, redisCacheRequestModel.Language);
                responseModel.Status = true;
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                //this.CustomBadRequest(responseModel, this.ModelState);
                responseModel.ResponseData = false;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, redisCacheRequestModel.Language);
                responseModel.Status = true;
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;

        }

        [Route("VerifyOTP")]
        [HttpPost]
        public async Task<OTPmodelResponce> VerifyOTP([FromBody] OTPmodel otp)
        {
            try
            {
                OTPmodelResponce response = await this._common.VerifyOTP(otp);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost("GetGSTDetails")]
        public  async Task<GSTDataModel> GetGSTDetails(GstRequestModel requestModel)
        {

            GSTDataModel dataModel = null;
            dataModel = await this._common.GetGSTDetails(requestModel.GstNumber);
            return dataModel;

        }
        [HttpPost("VerifyPAN")]
        public async Task<ResultPAN> VerifyPAN(PANRequestModel pANRequestModel)
        {

            ResultPAN dataModel = null;
            dataModel = await this._common.VerifyPAN(pANRequestModel);
            return dataModel;

        }
        [HttpPost("sendTestOTP")]
        public async Task<ApiResult<OTPResponseModel>> sendTestOTP(OTPRequestModel requestModel)
        {
           
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();
            //if (this.ModelState.IsValid)
            //{
            _logger.LogWarning(LoggerMessage.ModelStateValidate);

            responseModel = await this._common.sendTestOTP(requestModel);

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;

        }
        [HttpPost("sendOTP")]
        public async Task<ApiResult<OTPResponseModel>> sendOTP(OTPRequestModel requestModel)
        {

            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();
            //if (this.ModelState.IsValid)
            //{
            _logger.LogWarning(LoggerMessage.ModelStateValidate);

            responseModel = await this._common.sendTestOTP(requestModel);

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;

        }

    }
}

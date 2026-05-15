using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static Capex.Models.Common.APIResult;
using Capex.Utilities.Common;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Business.Interfaces;
using Capex.Models.ResponseModel;
using Capex.Business.Services;
using Capex.Models.ResponseModel.WebGIS;
using Capex.Models.RequestModel.WebGIS;
using Capex.Infrastructure.Interfaces;
using Capex.Infrastructure.Common;
using Capex.DomainModels.DomainResponseModel.WebGIS;
using Newtonsoft.Json;
using System.Text;

namespace Capex.API.Areas.Common.Controllers
{
    [ApiController]
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    public class WebgisController : Controller
    {
        private readonly IWebGISService webgisService;

        public WebgisController(IWebGISService webgisService)
        {
            this.webgisService = webgisService;
        }
        [AllowAnonymous]
        [HttpPost("getKhasraList")]
        public async Task<ApiResult<WebGISKhasraListResponseModel>> getKhasraList([FromBody] WebGISRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraListResponseModel> responseModel = new ApiResult<WebGISKhasraListResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);

                responseModel = await this.webgisService.GetKhasraList(requestModel);
                //return await this.webgisService.GetKhasraList(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [AllowAnonymous]
        [HttpPost("getKhasraDetails")]
        public async Task<ApiResult<WebGISKhasraDetailsResponseModel>> getKhasraDetails([FromBody] WebGISRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraDetailsResponseModel> responseModel = new ApiResult<WebGISKhasraDetailsResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.webgisService.GetKhasraDetails(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [AllowAnonymous]
        [HttpPost("getOwnerDetails")]
        public async Task<ApiResult<WebGISOwnerDetailsResponseModel>> getOwnerDetails([FromBody] WebGISRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<WebGISOwnerDetailsResponseModel> responseModel = new ApiResult<WebGISOwnerDetailsResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.webgisService.GetOwnerDetails(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        [AllowAnonymous]
        [HttpPost("GetKhasraAndOwnerDetails")]
        public async Task<ApiResult<WebGISResponseModel>> getKhasraAndOwnerDetails([FromBody] WebGISRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<WebGISResponseModel> responseModel = new ApiResult<WebGISResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.webgisService.GetKhasraAndOwnerDetails(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }


        [AllowAnonymous]
        [HttpPost("GetKhasraAndOwnerDetailsbyKhasraNo")]
        public async Task<ApiResult<WebGISKhasraNoResponseModel>> getKhasraAndOwnerDetailsbyKhasraNo([FromBody] WebGISKhasraRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraNoResponseModel> responseModel = new ApiResult<WebGISKhasraNoResponseModel>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                return await this.webgisService.GetKhasraAndOwnerDetailsbyKhasraNo(requestModel);
            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }


    }
}

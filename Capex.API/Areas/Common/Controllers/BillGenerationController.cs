using Capex.Business.Interfaces;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.API.Areas.Common.Controllers
{

    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class BillGenerationController : BaseController
    {
        private readonly IBillGeneration _billGeneration;
        private readonly ILogger<BillGenerationController> _logger;
        public BillGenerationController(ILogger<BillGenerationController> logger, IBillGeneration billGeneration)
        {
            this._logger = logger;
            this._billGeneration = billGeneration;
        }

        /// <summary>
        /// SaveBillGeneration
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveBillGeneration Data Accordingly Request</returns>
        //[HttpPost("SaveBillGeneration")]
        //public async Task<ApiResult<BillGenerationResponseModel>> SaveBillGeneration(BillGenerationRequestModel requestModel)
        //{
        //    _logger.LogDebug(LoggerMessage.Begin);
        //    ApiResult<BillGenerationResponseModel> responseModel = new ApiResult<BillGenerationResponseModel>();
        //    if (this.ModelState.IsValid)
        //    {
        //        _logger.LogWarning(LoggerMessage.ModelStateValidate);
        //        responseModel = await this._billGeneration.SaveBillGeneration(requestModel);
        //    }
        //    else
        //    {
        //        _logger.LogWarning(LoggerMessage.ModelStateInValid);
        //        this.CustomBadRequest(responseModel, this.ModelState);
        //    }
        //    _logger.LogDebug(LoggerMessage.End);
        //    return responseModel;
        //}
        /// <summary>
        /// GetBillGenerationData
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetBillGenerationData Data Accordingly Request</returns>
        [HttpPost("GetBillGenerationData")]
        public async Task<ApiResult<GeBillGenerationResponseModel>> GetBillGenerationData(GetBillDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<GeBillGenerationResponseModel> responseModel = new ApiResult<GeBillGenerationResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._billGeneration.GetBillGenerationData(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }
            _logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        /// <summary>
        /// BuildingDetailsByDDO
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All BuildingDetailsByDDO Data Accordingly Request</returns>
        [HttpPost("BuildingDetailsByDDO")]
        public async Task<ApiResult<BillGenerationBuildingDetailsByVendorResponseModel>> BuildingDetailsByDDO(BuildingDetailsByDDORequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<BillGenerationBuildingDetailsByVendorResponseModel> responseModel = new ApiResult<BillGenerationBuildingDetailsByVendorResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._billGeneration.BuildingDetailsByDDO(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }
            _logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        /// <summary>
        /// SaveBillGeneration
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveBillGeneration Data Accordingly Request</returns>
        [HttpPost("SaveBillGeneration")]
        public async Task<ApiResult<BillGenerationResponseModel>> SaveBillGeneration(BillGenerationBuildingDetailsByVendorRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<BillGenerationResponseModel> responseModel = new ApiResult<BillGenerationResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._billGeneration.SaveBillGeneration(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }
            _logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }

    }
}

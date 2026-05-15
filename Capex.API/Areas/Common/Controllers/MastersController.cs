using Microsoft.AspNetCore.Mvc;
using Capex.Business.Interfaces;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Models.Common;
using Capex.Utilities.Common;
using Humanizer;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class MastersController : BaseController
    {
        private readonly IMasters _masters;
        private readonly ILogger<MastersController> _logger;
        public MastersController(ILogger<MastersController> logger, IMasters masters)
        {
            this._logger = logger;
            this._masters = masters;
        }

        /// <summary>
        /// Get Demography
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All Master Data Accordingly Request</returns>
        [HttpPost("GetDemography")]
        public async Task<ApiResult<DemographyResponseModel>> GetDemography([FromBody] DemographyRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<DemographyResponseModel> responseModel = new ApiResult<DemographyResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetDemography(requestModel);
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
        /// SaveOrUpdateMstDDO
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveOrUpdateMstDDO Data Accordingly Request</returns>
        [HttpPost("SaveOrUpdateMstDDO")]
        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateMstDDO(DDORequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.SaveOrUpdateDDO(requestModel);
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
        /// GetDOODetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetDOODetails Data Accordingly Request</returns>
        [HttpPost("GetDOODetails")]
        public async Task<ApiResult<List<DDODetailsResponseModel>>> GetDOODetails(DDODetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DDODetailsResponseModel>> responseModel = new ApiResult<List<DDODetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetDOODetails(requestModel);
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
        /// GetDOODetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetDOODetails Data Accordingly Request</returns>
        [HttpPost("GetDOOByDistricts")]
        public async Task<ApiResult<List<DDODetailsResponseModel>>> GetDOOByDistricts([FromBody] DistrictsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DDODetailsResponseModel>> responseModel = new ApiResult<List<DDODetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetDOOByDistrict(requestModel);
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
        /// SaveBankDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveBankDetails Data Accordingly Request</returns>
        [HttpPost("SaveBankDetails")]
        public async Task<ApiResult<MasterResponseModel>> SaveBankDetails(BankDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.SaveBankDetails(requestModel);
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
        /// GetBankDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetBankDetails Data Accordingly Request</returns>
        [HttpPost("GetBankDetailByIfsc")]
        public async Task<ApiResult<BankDetailsResponseModel>> GetBankDetailByIfsc(BankSearchRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<BankDetailsResponseModel> responseModel = new ApiResult<BankDetailsResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetBankDetailByIfsc(requestModel);
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
        /// GetBankDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetBankDetails Data Accordingly Request</returns>
        [HttpPost("GetBankDetails")]
        public async Task<ApiResult<List<BankDetailsResponseModel>>> GetBankDetails(BankSearchRequestModel requestModel)
        {
            
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<BankDetailsResponseModel>> responseModel = new ApiResult<List<BankDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetBankDetails(requestModel);
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
        /// SaveOrUpdateBuildingDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveOrUpdateBuildingDetails Data Accordingly Request</returns>
        [HttpPost("SaveOrUpdateBuildingDetails1")]
        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails1(BuildingRegistrationRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.SaveOrUpdateBuildingDetails(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }
        [HttpPost("SaveOrUpdateBuildingDetails")]
        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails(SaveBuildingRequest requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.SaveOrUpdateBuildingDetails(requestModel);
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
        /// GetBuildingDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetBuildingDetails Data Accordingly Request</returns>
        [HttpPost("GetBuildingDetails")]
        public async Task<ApiResult<List<BuildingRegistrationResponseModel>>> GetBuildingDetails(BuildingDetailsSearchRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<BuildingRegistrationResponseModel>> responseModel = new ApiResult<List<BuildingRegistrationResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetBuildingDetails(requestModel);
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
        /// GetBuildingById
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All BuildingDetailsSearchRequestModel Data Accordingly Request</returns>
        [HttpPost("GetBuildingById")]
        public async Task<ApiResult<GetBuildingResponse>> GetBuildingById(BuildingDetailsSearchRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<GetBuildingResponse> responseModel = new ApiResult<GetBuildingResponse>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetBuildingById(requestModel);
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
        /// SaveOrUpdateVendorData
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveOrUpdateVendorData Data Accordingly Request</returns>
        [HttpPost("SaveOrUpdateVendorData")]
        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateVendorData(VendorDataRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();

            _logger.LogWarning(LoggerMessage.ModelStateValidate);
            responseModel = await this._masters.SaveOrUpdateVendorData(requestModel);

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }
        /// <summary>
        /// GetBuildingDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetBuildingDetails Data Accordingly Request</returns>
        [HttpPost("GetVendorData")]
        public async Task<ApiResult<VendorDataListResponseModel>> GetVendorData(VendorSearchDRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<VendorDataListResponseModel> responseModel = new ApiResult<VendorDataListResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetVendorData(requestModel);
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
        /// SaveUnitPriceDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All SaveOrUpdateBuildingDetails Data Accordingly Request</returns>
        [HttpPost("SaveUnitPriceDetails")]
        public async Task<ApiResult<MasterResponseModel>> SaveUnitPriceDetails(UnitPriceRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<MasterResponseModel> responseModel = new ApiResult<MasterResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.SaveUnitPriceDetails(requestModel);
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
        /// GetUnitPriceDetails
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All GetUnitPriceDetails Data Accordingly Request</returns>
        [HttpPost("GetUnitPriceDetails")]
        public async Task<ApiResult<List<UnitPriceResponseModel>>> GetUnitPriceDetails(UnitPriceRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<UnitPriceResponseModel>> responseModel = new ApiResult<List<UnitPriceResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.GetUnitPriceDetails(requestModel);
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
        /// ValidateIVRSAndMeterExist
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns>List Of All ValidateIVRSAndMeterExist Data Accordingly Request</returns>
        [HttpPost("ValidateIVRSAndMeterExist")]
        public async Task<ApiResult<ValidateIVRSAndMeterExistResponseModel>> ValidateIVRSAndMeterExist(ValidateIVRSAndMeterExistRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ValidateIVRSAndMeterExistResponseModel> responseModel = new ApiResult<ValidateIVRSAndMeterExistResponseModel>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._masters.ValidateIVRSAndMeterExist(requestModel);
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

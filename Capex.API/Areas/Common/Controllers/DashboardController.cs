using Capex.Models.RequestModel.Dashboard;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Dashboard;
using Capex.Models.ResponseModel.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capex.Business.Interfaces;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController] 
    public class DashboardController  : BaseController
    {
        private readonly IDashboard _Dashboard;
        private readonly ILogger<DashboardController> _logger;
        public DashboardController(ILogger<DashboardController> logger, IDashboard Dashboard)
        {
            this._logger = logger;
            this._Dashboard = Dashboard;
        }

        [HttpPost("GetDashboardCountList")]
        public async Task<ApiResult<List<DashboardResponseModel>>> GetDashboardCountList(DashboardRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardResponseModel>> responseModel = new ApiResult<List<DashboardResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardCountList(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }

        [HttpPost("GetDashboardVenderDistrictList")]
        public async Task<ApiResult<List<DashboardVenderDistrictDetailsResponseModel>>> GetDashboardVenderDistrictList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardVenderDistrictDetailsResponseModel>> responseModel = new ApiResult<List<DashboardVenderDistrictDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardVenderDistrictList(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }
        [HttpPost("GetDashboardVenderDdoList")]
        public async Task<ApiResult<List<DashboardVenderDdoDetailsResponseModel>>> GetDashboardVenderDdoList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardVenderDdoDetailsResponseModel>> responseModel = new ApiResult<List<DashboardVenderDdoDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardVenderDdoList(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }

        [HttpPost("GetDashboardVenderBuildingList")]
        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardVenderBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardVenderBuildingList(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }

        [HttpPost("GetDashboardDDOBuildingList")]
        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardDDOBuildingList(requestModel);
            }
            else
            {
                _logger.LogWarning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            _logger.LogDebug(LoggerMessage.End);

            return responseModel;
        }

        [HttpPost("GetDashboardDDOMeterList")]
        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOMeterList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            _logger.LogDebug(LoggerMessage.Begin);
            ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>();
            if (this.ModelState.IsValid)
            {
                _logger.LogWarning(LoggerMessage.ModelStateValidate);
                responseModel = await this._Dashboard.GetDashboardDDOMeterList(requestModel);
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

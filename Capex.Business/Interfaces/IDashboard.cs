using Capex.Models.RequestModel.Dashboard;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Dashboard;
using Capex.Models.ResponseModel.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IDashboard
    {
        public Task<ApiResult<List<DashboardResponseModel>>> GetDashboardCountList(DashboardRequestModel requestModel);
        public Task<ApiResult<List<DashboardVenderDistrictDetailsResponseModel>>> GetDashboardVenderDistrictList(DashboardVenderDistrictDetailsRequestModel requestModel);
        public Task<ApiResult<List<DashboardVenderDdoDetailsResponseModel>>> GetDashboardVenderDdoList(DashboardVenderDistrictDetailsRequestModel requestModel);
        public Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardVenderBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel);
        public Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel);
        public Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOMeterList(DashboardVenderDistrictDetailsRequestModel requestModel);
    
    }
}

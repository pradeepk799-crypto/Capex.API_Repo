using Capex.DomainModels.DomainRequestModel.Dashboard;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Dashboard;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Models.Common;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using static Capex.Models.Common.APIResult;

//using RoleMenuMappingResponseModel = Capex.DomainModels.DomainResponseModel.Masters.RoleMenuMappingResponseModel;

namespace Capex.Infrastructure.Interfaces
{
    public interface IDashboard
    {
        public Task<List<DashboardDomainResponseModel>> GetDashboardCountList(DashboardDomainRequestModel requestModel);
        public Task<List<DashboardVenderDistrictDetailsDomainResponseModel>> GetDashboardVenderDistrictList(DashboardVenderDistrictDetailsDomainRequestModel requestModel);
        public Task<List<DashboardVenderDdoDetailsDomainResponseModel>> GetDashboardVenderDdoList(DashboardVenderDistrictDetailsDomainRequestModel requestModel);
        public Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardVenderBuildingList(DashboardVenderDistrictDetailsDomainRequestModel requestModel);
        public Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardDDOBuildingList(DashboardVenderDistrictDetailsDomainRequestModel requestModel);
        public Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardDDOMeterList(DashboardVenderDistrictDetailsDomainRequestModel requestModel);

    }
    
}

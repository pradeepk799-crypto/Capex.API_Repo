using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Models.Common;
using static Capex.Models.Common.APIResult;

namespace Capex.Infrastructure.Interfaces
{
    public interface IBillGenerationInfra
    {
        Task<ApiResult<SaveDataDomainResponseModel>> SaveBillGeneration(BillGenerationBuildingDetailsByVendorDomainRequest requestModel);
        Task<GetBillGenerationDomainResponse> GetBillGenerationData(GetBillDetailsDomainRequestModel requestModel);
        Task<GetBillGenerationDomainResponse> BuildingDetailsByDDO(BuildingDetailsByDDODomainRequestModel requestModel);

    }
}

using Capex.DomainModels.DomainRequestModel;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Models.RequestModel.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IBillGeneration
    {

        Task<ApiResult<BillGenerationResponseModel>> SaveBillGeneration(BillGenerationBuildingDetailsByVendorRequestModel requestModel);
        Task<ApiResult<GeBillGenerationResponseModel>> GetBillGenerationData(GetBillDetailsRequestModel requestModel);
        Task<ApiResult<BillGenerationBuildingDetailsByVendorResponseModel>> BuildingDetailsByDDO(BuildingDetailsByDDORequestModel requestModel);
    }
}

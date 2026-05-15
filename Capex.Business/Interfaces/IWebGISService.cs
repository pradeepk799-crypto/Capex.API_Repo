using Capex.DomainModels.DomainRequestModel.WebGIS;
using Capex.DomainModels.DomainResponseModel.WebGIS;
using Capex.Models.RequestModel.WebGIS;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.WebGIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IWebGISService
    {
        Task<ApiResult<WebGISKhasraListResponseModel>> GetKhasraList(WebGISRequestModel request);
        Task<ApiResult<WebGISKhasraDetailsResponseModel>> GetKhasraDetails(WebGISRequestModel request);
        Task<ApiResult<WebGISOwnerDetailsResponseModel>> GetOwnerDetails(WebGISRequestModel request);
        Task<ApiResult<WebGISResponseModel>> GetKhasraAndOwnerDetails(WebGISRequestModel request);
        Task<ApiResult<WebGISKhasraNoResponseModel>> GetKhasraAndOwnerDetailsbyKhasraNo(WebGISKhasraRequestModel khasraidreq);
      



    }
}

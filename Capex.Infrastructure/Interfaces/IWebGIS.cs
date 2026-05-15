using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainRequestModel.WebGIS;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.DomainModels.DomainResponseModel.WebGIS;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.WebGIS;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.WebGIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;
using KhasraAdjDetails = Capex.DomainModels.DomainResponseModel.WebGIS.KhasraAdjDetailsResponse;

namespace Capex.Infrastructure.Interfaces
{
    public interface IWebGIS
    {
        Task<WebGISKhasraListResponse> GetKhasraList(WebGISRequest request);
        Task<WebGISKhasraDetailsResponse> GetKhasraDetails(WebGISRequest request);
        Task<WebGISOwnerDetailsResponse> GetOwnerDetails(WebGISRequest request);
        Task<WebGISResponse> GetKhasraAndOwnerDetails(WebGISRequest request);
        Task<WebGISResponse> GetBasraDetailList(WebGISRequest request);

     


    }
}

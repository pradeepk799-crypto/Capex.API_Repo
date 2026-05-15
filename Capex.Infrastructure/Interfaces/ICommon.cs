using Capex.DomainModels.Common;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Interfaces
{
    public interface ICommon
    {
        public Task<List<ModelValidateDetailResponse>> GetModelValidation(ModelValidateRequest request);
        public Task<string> InsertAPILogStatus(APILogStatusDomainRequestModel request);
    }
}

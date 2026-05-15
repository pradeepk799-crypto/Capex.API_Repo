
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.Masters;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IUser
    {
        public string GetName();      
        public Task<ApiResult<UserLoginResponseModel>> GetLoginUser(TokenRequestModel requestModel);
        public Task<ApiResult<ValidUserResponseModel>> GetValidUser(ValidUserRequestModel request);
        public Task<ApiResult<ValidUserResponseModel>> ChangeUserPWD(ChangePwsRequestModel request);



    }
}

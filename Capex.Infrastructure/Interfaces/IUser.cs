using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel;
using Capex.DomainModels.DomainResponseModel.Masters;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Infrastructure.Interfaces
{
    public interface IUser
    {
        public string GetName();
        public Task<UserLoginResponse> GetLoginUser(TokenRequest request);
        public Task<ValidUserResponse> GetValidUser(ValidUserRequest requestmodel);
        public Task<ValidUserResponse> ChangeUserPassword(ChangePasswordRequest requestmodel);
        public Task<CitizenForgotPwdResponse> ForgotUserPassword(CitizenForgotPwdRequest requestmodel);

        public Task<UserApplicationInfoDomainRequestModel> GetLoginDetails(TokenRequest requestmodel);
        public Task<ApiResult<UserForgotPasswordDomainResponseModel>> ForgotPassword(ForgotPasswordDomainRequestModel requestModel);
    }
}

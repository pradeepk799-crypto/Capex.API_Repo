using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Utilities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface ILoginService
    {
        /// <summary>
        /// Tokens the specified token request.
        /// </summary>
        /// <param name="tokenRequest">The token request.</param>
        /// <returns>TokenResponseModel.</returns>
        Task<ApiResult<TokenResponseModel>> Token(TokenRequestModel tokenRequest);
        Task<ApiResult<ValidUserResponseModel>> GetValidUser(ValidUserRequestModel request);
        Task<ApiResult<ValidUserResponseModel>> ChangeUserPwd(ChangePwsRequestModel request);
        Task<ApiResult<ForgotPasswordResponseModel>> ForGotUserPwd(CitizenForgotPwdModel request);
        Task<ApiResult<UserForgotPasswordResponseModel>> ForgotPassword(ForgotPasswordRequestModel requestModel);


    }
}

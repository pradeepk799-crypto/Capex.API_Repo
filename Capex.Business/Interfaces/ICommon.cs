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
    public interface ICommon
    {
        Task<ApiResult<OTPResponseModel>> GenerateOTP(OTPRequestModel requestModel);
        Task<ApiResult<object>> Validate(Object request);
        Task<ApiResult<bool>> ValidateOTP(OTPRequestModel requestModel);
        Task<ApiResult<CaptchaResponseModel>> GenerateCaptcha(HeaderHelperRequest request);
        Task<ApiResult<bool>> ValidateCaptcha(CaptchaRequestModel request);
        ApiResult<CaptchaResponseModel> VerifyCaptcha(CaptchaRequestModel request);
        Task<OTPResponseModel> SendOTP(OTPRequestModel requestModel);
        Task<TokenResponseModel> GenerateToken(TokenRequestModel requestModel);
        Task<OTPmodelResponce> VerifyOTP(OTPmodel Otp);
        T CreateRequest<T>(RequestModelBase model, out bool isValid, out string errorCode)
            where T : DomainRequestModelBase, new();

        Task<GSTDataModel> GetGSTDetails(string gstNumber);
        Task<ResultPAN> VerifyPAN(PANRequestModel pANRequestModel);
        Task<ApiResult<OTPResponseModel>> sendTestOTP(OTPRequestModel requestModel);

    }
}

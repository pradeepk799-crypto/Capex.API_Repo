
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capex.Infrastructure.Services;
using Capex.Infrastructure.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Capex.Models.CustomValidation;
using Capex.DomainModels.Common;
using Capex.Models.Common;
using static Capex.Models.Common.APIResult;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using System.Text.RegularExpressions;

namespace Capex.Business.Common.ModelValidations
{
    public class ModelValidation
    {
        public static ICommon _common;
        public ModelValidation(ICommon common)
        {
            _common = common;
        }
        public async Task<ApiResult<object>> Validate(Object request)
        {
            
                List<ModelValidateDetailResponse> response = new List<ModelValidateDetailResponse>();
                ModelValidateRequest modelValidateRequest = new ModelValidateRequest();
                ApiResult<object> result = new ApiResult<object> { Status=true};
                modelValidateRequest.Area = request.GetType().GetProperty("Area").GetValue(request, null).ToString();
                modelValidateRequest.ActionName = request.GetType().GetProperty("ActionName").GetValue(request, null).ToString();
                modelValidateRequest.Controller = request.GetType().GetProperty("Controller").GetValue(request, null).ToString();
            try
            {
                response = await _common.GetModelValidation(modelValidateRequest);
                if (response != null)
                {
                    foreach (ModelValidateDetailResponse item in response)
                    {
                        var value = request.GetType().GetProperty(item.PropertyName).GetValue(request, null);

                        if (item.IsRequired == true && (value == null || value.ToString() == ""))
                        {
                            result.ErrorCode = item.ErrorCode;
                            result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                            result.Status = false;
                            return result;
                        }
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {

                            if (item.Type != value.GetType().Name)
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            if (item.MaxLength > value.ToString().Length)
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            if (item.MinLength < value.ToString().Length)
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            var regex = new Regex(item.Regx);
                            if (!regex.IsMatch(value.ToString()))
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            if (item.ErrorCode != value.ToString())
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }

                        }
                        else
                        {
                            result.ErrorCode = item.ErrorCode;
                            result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                            result.Status = false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace);
                result.Message = ex.Message;
                result.ErrorCode = "Err";
                result.Status = false;
            }
            return result;
           
        }
    }
}

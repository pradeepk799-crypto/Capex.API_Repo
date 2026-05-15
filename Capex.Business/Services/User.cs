using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Capex.Business.Interfaces;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.Masters;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.Business.Services
{
    public class User : Interfaces.IUser
    {
        private readonly IInfrastructureServices infrastructureServices;
        private readonly ILogger<Masters> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public User(IInfrastructureServices infrastructureServices, IHttpContextAccessor httpContextAccessor)
        {
            this._logger = _logger;
            this.infrastructureServices = infrastructureServices;
            _httpContextAccessor = httpContextAccessor;


        }

        public string GetName()
        {
            Log.Information("Business");
            this.infrastructureServices.User.GetName();
            return "";
        }

        public async Task<ApiResult<UserLoginResponseModel>> GetLoginUser(TokenRequestModel request)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<UserLoginResponseModel> responseModel = new ApiResult<UserLoginResponseModel>();
            UserLoginResponseModel data = new UserLoginResponseModel();
            TokenRequest requestmodel = new TokenRequest();
            //Check Menu Permission and Data Permission.
            UserLoginResponse response;

            requestmodel.UserName = request.UserName;
            requestmodel.Password = request.Password;

            response = await this.infrastructureServices.User.GetLoginUser(requestmodel);

            if (response != null)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, request.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<ValidUserResponseModel>> GetValidUser(ValidUserRequestModel request)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            ValidUserResponseModel data = new ValidUserResponseModel();
            ValidUserRequest requestmodel = new ValidUserRequest();
            //Check Menu Permission and Data Permission.
            ValidUserResponse response;

            requestmodel.UserName = request.UserName;
            requestmodel.Mobno = request.Mobno;

            response = await this.infrastructureServices.User.GetValidUser(requestmodel);

            if (response != null)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, request.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<ValidUserResponseModel>> ChangeUserPWD(ChangePwsRequestModel request)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            ValidUserResponseModel data = new ValidUserResponseModel();
            ChangePasswordRequest requestmodel = new ChangePasswordRequest();
            //Check Menu Permission and Data Permission.
            ValidUserResponse response;

            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress;

            requestmodel.UserName = request.UserName;
            requestmodel.NewPassword = request.NewPassword;
            requestmodel.OldPassword = request.OldPassword;
            requestmodel.ModifyIP = ipAddress?.ToString();

            response = await this.infrastructureServices.User.ChangeUserPassword(requestmodel);

            if (response != null)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, request.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }


    }
}

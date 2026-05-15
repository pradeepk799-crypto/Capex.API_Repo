using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Capex.Business.Common;
using Capex.Business.Interfaces;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using static Capex.Models.Common.APIResult;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Capex.DomainModels.DomainResponseModel;
using Capex.DomainModels.DomainRequestModel;

namespace Capex.Business.Services
{
    public class FileUploadService : IFileUploadService
    {

        private readonly IInfrastructureServices infrastructureServices;
        private readonly Interfaces.IUser user;
        private readonly ILogger<FileUploadService> _logger;
        private readonly IBusinessServices businessServices;



        public FileUploadService(IInfrastructureServices infrastructureServices, IBusinessServices businessServices, ILogger<FileUploadService> logger)
        {

            this._logger = logger;
            this.infrastructureServices = infrastructureServices;
            this.businessServices = businessServices;

        }

        public async Task<ApiResult<ResponseUploadID>> FileUpload(FileUploadRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ResponseUploadID> responseModel = new ApiResult<ResponseUploadID>();
            ResponseUploadID data = new ResponseUploadID();
            //DResponseUploadID response;
            DResponseUploadID response = new DResponseUploadID();
            DomainFileUploadRequestModel request = new DomainFileUploadRequestModel();
            request.files = tokenRequest.files;
            request.Filepath = tokenRequest.Filepath;
            try
            {
                response = await this.infrastructureServices.UploadFileService.FileUploadDMS(request);
                if (response.status)
                {
                    data = new ResponseUploadID()
                    {
                        UploadID = response.UploadID
                    };

                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00036;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00036, tokenRequest.Language);
                    responseModel.Status = true;

                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00035;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00035, tokenRequest.Language);
                    responseModel.Status = false;
                }

            }
            catch (Exception ex)
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00035;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00035, tokenRequest.Language);
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<ApiResult<ResponseDownloadFile>> FileDownload(FilePathRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ResponseDownloadFile> responseModel = new ApiResult<ResponseDownloadFile>();
            DomainResponseDownloadFile response;
            DomainFilePath request = new DomainFilePath();
            request.FileId = tokenRequest.FileId;

            try
            {
                response = await this.infrastructureServices.UploadFileService.FileDownloadDMS(request);
                if (response.status)
                {
                    ResponseDownloadFile resmodel = new ResponseDownloadFile();
                    resmodel.status = response.status;
                    resmodel.msg = response.msg;
                    resmodel.FilebyteArray = response.FilebyteArray;
                    resmodel.FileType = response.FileType;
                    resmodel.FileName = response.FileName;

                    responseModel.ResponseData = resmodel;
                    responseModel.ErrorCode = ErrorCodes.Err00036;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00036, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00035;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00035, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00035;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00035, "");
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }




    }
}

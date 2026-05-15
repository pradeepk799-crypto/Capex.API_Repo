using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using static Capex.Models.Common.APIResult;
using Capex.Utilities.Common;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Business.Interfaces;
using Capex.Models.ResponseModel;
using Capex.Models.RequestModel;
using System.Configuration;
using Capex.Models.ResponseModel.Masters;
using System.Net;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileUploadController : BaseController
    {
        private readonly IFileUploadService fileuploadService;
        public FileUploadController(IFileUploadService fileuploadService)
        {
            this.fileuploadService = fileuploadService;
        }
        [AllowAnonymous]
        [HttpPost("FileUpload")]
        public async Task<ApiResult<ResponseUploadID>> FileUpload()
        {

            Log.Debug(LoggerMessage.Begin);
            ApiResult<ResponseUploadID> responseModel = new ApiResult<ResponseUploadID>();
            if (this.ModelState.IsValid)
            {
                DateTime currentDate = DateTime.Now;

                // Get year and month as strings
                string year = currentDate.ToString("yyyy");
                string month = currentDate.ToString("MM");

                // Concatenate year and month with '/'
                string yearMonth = $"{year}/{month}";


                var file = Request.Form.Files[0];
                string docPath = HttpContext.Request.Form["Filepath"][0].ToString();
                FileUploadRequestModel requestModel = new FileUploadRequestModel();
                requestModel.files = file;
                requestModel.Filepath = $"{yearMonth}/{docPath}";
                Log.Warning(LoggerMessage.ModelStateValidate);
                //requestModel.Language = request.Language;
                responseModel = await this.fileuploadService.FileUpload(requestModel);

            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        [AllowAnonymous]
        [HttpPost("FileDownload")]
        public async Task<ApiResult<ResponseDownloadFile>> FileDownload([FromBody] FilePathRequestModel requestModel)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<ResponseDownloadFile> responseModel = new ApiResult<ResponseDownloadFile>();
            if (this.ModelState.IsValid)
            {
                Log.Warning(LoggerMessage.ModelStateValidate);
                responseModel = await this.fileuploadService.FileDownload(requestModel);

            }
            else
            {
                Log.Warning(LoggerMessage.ModelStateInValid);
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }


        
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Capex.Business.Interfaces;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Document;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.Document;
using Serilog;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.API.Areas.Common.Controllers
{
    [Area("Common")]
    [Route("api/[area]/[controller]")]
    [ApiController]
    public class DocumentController : BaseController
    {
        private readonly IDocument _document;
        private readonly ILogger<DocumentController> _logger;

        // Constructor to inject dependencies
        public DocumentController(ILogger<DocumentController> logger, IDocument document)
        {
            this._logger = logger;
            _document = document;
        }

        // Endpoint to retrieve HTML document
        [HttpPost("GetHTMLDocument")]
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocument(HTMLDocumentRequestModel requestModel)
        {
            // Log the beginning of the action
            Log.Debug(LoggerMessage.Begin);

            // Initialize response model
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();

            // Check if model state is valid
            if (this.ModelState.IsValid)
            {
                // Call service to get HTML document
                responseModel = await this._document.GetHTMLDocument(requestModel);
            }
            else
            {
                // Log warning for invalid model state
                Log.Warning(LoggerMessage.ModelStateInValid);
                // Return custom bad request response
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            // Log the end of the action
            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        // Endpoint to retrieve HTML document and convert to PDF base64
        [HttpPost("GetHTMLDocumentPDFBase64")]
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDFBase64(HTMLDocumentRequestModel requestModel)
        {
            // Log the beginning of the action
            Log.Debug(LoggerMessage.Begin);

            // Initialize response model
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();

            // Check if model state is valid
            if (this.ModelState.IsValid)
            {
                // Call service to get HTML document and convert to PDF base64
                responseModel = await this._document.GetHTMLDocumentPDF(requestModel);
            }
            else
            {
                // Log warning for invalid model state
                Log.Warning(LoggerMessage.ModelStateInValid);
                // Return custom bad request response
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            // Log the end of the action
            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        [HttpPost("GetHTMLDocumentPDFBase64New")]
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDFBase64New(HTMLDocumentRequestModel requestModel)
        {
            // Log the beginning of the action
            Log.Debug(LoggerMessage.Begin);

            // Initialize response model
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();

            // Check if model state is valid
            if (this.ModelState.IsValid)
            {
                // Call service to get HTML document and convert to PDF base64
                responseModel = await this._document.GetHTMLDocumentPDFNew(requestModel);
            }
            else
            {
                // Log warning for invalid model state
                Log.Warning(LoggerMessage.ModelStateInValid);
                // Return custom bad request response
                this.CustomBadRequest(responseModel, this.ModelState);
            }

            // Log the end of the action
            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
    }
}

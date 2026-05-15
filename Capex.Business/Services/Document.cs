using DinkToPdf;
using HandlebarsDotNet;
using HandlebarsDotNet.Extension.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel.Document;
using Capex.Models.ResponseModel.Document;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using System;

namespace Capex.Business.Services
{
    public class Document : Interfaces.IDocument
    {
        private readonly ILogger<Document> _logger;
        private readonly IMessageNotification messageNotification;
        private static readonly SynchronizedConverter converter = new SynchronizedConverter(new PdfTools());

        // Constructor to inject dependencies
        public Document(ILogger<Document> logger, IMessageNotification messageNotification)
        {
            this._logger = logger;
            this.messageNotification = messageNotification;
        }

        // Method to retrieve HTML document
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocument(HTMLDocumentRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();
            HTMLDocumentResponseModel response = new HTMLDocumentResponseModel();
            MessagesNotificationModel templates = await this.messageNotification.GetTemplateData(requestModel.TemplateId);
            int applicationId = requestModel.ApplicationId != 0 ? requestModel.ApplicationId : Convert.ToInt32(EncDnc.DecryptString(requestModel.EncApplicationId));
            if (templates != null)
            {
                dynamic jsonData = await this.messageNotification.GetTemplateQueryData(applicationId, templates.Query);

                JObject data = JObject.Parse(jsonData);

                var properties = data.Properties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    JProperty property = properties.ElementAt(i);
                    if (data[property.Name] != null && data[property.Name].Type == JTokenType.String)
                    {

                        try
                        {
                            data[property.Name] = JObject.Parse((string)data[property.Name]);
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }
                    Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                }

                if (data != null)
                {
                    var handlebars = Handlebars.Create();
                    handlebars.Configuration.UseNewtonsoftJson();
                    var template = handlebars.Compile(templates.Body);
                    var result = template(data);
                    result = System.Net.WebUtility.HtmlDecode(result);
                    response.HtmlDocument = result;
                    responseModel.ResponseData = response;
                    responseModel.ErrorCode = ErrorCodes.Err00000;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00001;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, requestModel.Language);
                    responseModel.Status = false;
                }
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }

        // Method to retrieve HTML document and convert to PDF
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDF(HTMLDocumentRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();
            HTMLDocumentResponseModel response = new HTMLDocumentResponseModel();
            MessagesNotificationModel templates = await this.messageNotification.GetTemplateData(requestModel.TemplateId);

           

            if (templates != null)
            {
                dynamic jsonData = await this.messageNotification.GetTemplateQueryData(requestModel.ApplicationId, templates.Query);
                JObject data = JObject.Parse(jsonData);
                var properties = data.Properties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    JProperty property = properties.ElementAt(i);
                    if (data[property.Name] != null && data[property.Name].Type == JTokenType.String)
                    {
                        data[property.Name] = JObject.Parse((string)data[property.Name]);
                    }
                    Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                }

                if (data != null)
                {

                    try
                    {
                        var handlebars = Handlebars.Create();
                        handlebars.Configuration.UseNewtonsoftJson();
                        var template = handlebars.Compile(templates.Body);
                        var result = template(data);
                        result = System.Net.WebUtility.HtmlDecode(result);
                        byte[] pdfBytes = ConvertHtmlToPdfBytes(result);
                        response.HtmlDocumentPDFBase64 = Convert.ToBase64String(pdfBytes);
                        responseModel.ResponseData = response;
                        responseModel.ErrorCode = ErrorCodes.Err00000;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);
                        responseModel.Status = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    }

                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00001;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, requestModel.Language);
                    responseModel.Status = false;
                }
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDFNew(HTMLDocumentRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<HTMLDocumentResponseModel> responseModel = new ApiResult<HTMLDocumentResponseModel>();
            HTMLDocumentResponseModel response = new HTMLDocumentResponseModel();
            MessagesNotificationModel templates = await this.messageNotification.GetTemplateData(requestModel.TemplateId);



            if (templates != null)
            {
                dynamic jsonData = await this.messageNotification.GetTemplateQueryDataNew(requestModel.ApplicationId, templates.Query, requestModel.KeyValuePairs);
                JObject data = JObject.Parse(jsonData);
                var properties = data.Properties();

                for (int i = 0; i < properties.Count(); i++)
                {
                    JProperty property = properties.ElementAt(i);
                    if (data[property.Name] != null && data[property.Name].Type == JTokenType.String)
                    {
                        data[property.Name] = JObject.Parse((string)data[property.Name]);
                    }
                    Console.WriteLine($"Key: {property.Name}, Value: {property.Value}");
                }

                if (data != null)
                {

                    try
                    {
                        var handlebars = Handlebars.Create();
                        handlebars.Configuration.UseNewtonsoftJson();
                        var template = handlebars.Compile(templates.Body);
                        var result = template(data);
                        result = System.Net.WebUtility.HtmlDecode(result);
                        byte[] pdfBytes = ConvertHtmlToPdfBytes(result);
                        response.HtmlDocumentPDFBase64 = Convert.ToBase64String(pdfBytes);
                        responseModel.ResponseData = response;
                        responseModel.ErrorCode = ErrorCodes.Err00000;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);
                        responseModel.Status = true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}\nStackTrace: {ex.StackTrace}");
                    }

                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00001;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, requestModel.Language);
                    responseModel.Status = false;
                }
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        // Method to retrieve template data
        public async Task<MessagesNotificationModel> GetTemplateData(int templateId)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            MessagesNotificationModel templates = await this.messageNotification.GetTemplateData(templateId);
            if (templates == null)
            {
                return new MessagesNotificationModel();
            }
            this._logger.LogDebug(LoggerMessage.End);
            return templates;
        }

        // Method to convert HTML content to PDF bytes
        private byte[] ConvertHtmlToPdfBytes(string htmlContent)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = DinkToPdf.ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = DinkToPdf.PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                DPI = 300
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = htmlContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            try
            {
                return converter.Convert(pdf);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PDF Conversion Error: {ex.Message}");
                throw;
            }
        }
    }
}

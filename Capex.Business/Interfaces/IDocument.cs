using Capex.Models.RequestModel.Document;
using Capex.Models.ResponseModel.Document;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IDocument
    {
        Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocument(HTMLDocumentRequestModel requestModel);
        Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDF(HTMLDocumentRequestModel requestModel);

        Task<ApiResult<HTMLDocumentResponseModel>> GetHTMLDocumentPDFNew(HTMLDocumentRequestModel requestModel);
    }
}

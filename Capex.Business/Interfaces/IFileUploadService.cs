using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IFileUploadService
    {
        Task<ApiResult<ResponseUploadID>> FileUpload(FileUploadRequestModel tokenRequest);
        Task<ApiResult<ResponseDownloadFile>> FileDownload(FilePathRequestModel tokenRequest);
    }
}

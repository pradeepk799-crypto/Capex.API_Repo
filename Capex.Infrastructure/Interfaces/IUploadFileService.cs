using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Infrastructure.Interfaces
{
    public interface IUploadFileService
    {
        Task<DResponseUploadID> SaveUploadFile(DFileUploadRequestModel requestmodel);
        Task<DResponseUploadID> FileUploadDMS(DomainFileUploadRequestModel tokenRequest);
        Task<DomainResponseDownloadFile> FileDownloadDMS(DomainFilePath tokenRequest); 
    }
}

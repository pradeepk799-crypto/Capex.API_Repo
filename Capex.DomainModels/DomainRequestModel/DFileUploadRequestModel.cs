using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class DFileUploadRequestModel: DomainRequestModelBase
    {
        
        public string? FileUpload_Id { get; set; }
        public string? FilePath { get; set; }
        public string? FileContentType { get; set; }
        public string? File_Name { get; set; }
        public string? FolderName { get; set; }

    }

    public class DomainFileUploadRequestModel : DomainRequestModelBase
    {
        public IFormFile files { get; set; }
        public string Filepath { get; set; }
    }
    public class DomainFileDownload
    {
        public string? FolderName { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
    public class DomainResponseDownloadFile 
    {
        public byte[] FilebyteArray { get; set; }
        public Boolean status { get; set; }
        public string? msg { get; set; }
        public string? Base64String { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }

    }
    public class DomainFilePath: DomainRequestModelBase
    {
        public string? FileId { get; set; }
   
    }
}

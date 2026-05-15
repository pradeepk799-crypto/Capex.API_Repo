using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Capex.Models.RequestModel
{
    public class FileUploadRequestModel : RequestModelBase
    {
        public IFormFile files { get; set; }
        public string Filepath { get; set; }
    }
    public class FileDownload 
    {
        public string? FolderName { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }
    }
    public class FileDetails
    {
        public byte[] FileMsArray { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
    public class FilePathRequestModel : RequestModelBase
    {
        public string? FileId { get; set; }

    }
}

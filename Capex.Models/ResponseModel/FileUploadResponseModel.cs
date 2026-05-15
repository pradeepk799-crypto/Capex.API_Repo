using Capex.Models.RequestModel;

namespace Capex.Models.ResponseModel
{
    public class FileUploadResponseModel : RequestModelBase
    {
     
        public string? FilePath { get; set; }
        public Guid FileUpload_Id { get; set; }       
        public int DocumentType_Id { get; set; }
        public string? FileContentType { get; set; }
        public string? File_Name { get; set; }
        public Boolean status { get; set; }
        public string? msg { get; set; }
        public string? FolderName { get; set; }

    }

    public class ResponseUploadID : RequestModelBase
    {
        public int UploadID { get; set; }
        public Boolean status { get; set; }

    }
    public class ResponseDownloadFile : RequestModelBase
    {
        public byte[] FilebyteArray { get; set; }
        public Boolean status { get; set; }
        public string? msg { get; set; }
        public string? FileName { get; set; }
        public string? FileType { get; set; }

    }

  
}

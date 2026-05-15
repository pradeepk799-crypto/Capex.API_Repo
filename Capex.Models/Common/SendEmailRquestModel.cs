namespace Capex.Models.Common
{
    public class SendEmailRquestModel : EmailSendModel
    {
        public string? MailServer { get; set; }
        public string? SenderEmail_Id { get; set; }
        public string? SenderPassword { get; set; }
        public string? MailServerPort { get; set; }
        public List<Guid> FileUploadList { get; set; }
        public List<string> FilePathList { get; set; }
        public string? FilePath { get; set; }
        public bool? EnableEmail { get; set; }
        public string? Port { get; set; }

    }

    public class EmailSendModel
    {
        public string? Recipient { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
    }
}

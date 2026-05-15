namespace Capex.Models.ResponseModel
{
    public class SMSResponseModel:ResponseModelBase
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public string Data { get; set; }

    }

    //public class Data<T>
    //{
    //    public int? TotalRecords { get; set; }
    //    public T Records { get; set; }
    //    public int? OTP { get; set; }
    //    public T ReferenceId { get; set; }
    //}

    public class SmsLogModel
    {
        public string Url { get; set; }
        public string Mobile { get; set; }
        public string TemplateId { get; set; }
        public string QueryString { get; set; }
        public string Response { get; set; }
        public string Exception { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}


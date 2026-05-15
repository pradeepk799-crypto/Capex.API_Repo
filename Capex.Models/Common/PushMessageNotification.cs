namespace Capex.Models.Common
{

    public class MessageNotificationResponseModel 
    {
        public IList<PushMessageNotificationModel>? MessageNotificationModelList { get; set; }
    }
    public class PushMessageNotificationModel
    {
        public int? TemplateTypeId { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public int? CreateBy { get; set; }
        public int? ID { get; set;}
    }
}

using Capex.DomainModels.DomainRequestModel;

namespace Capex.Infrastructure.Common
{


    public class PushMessageNotificationResponse 
    {
        public IList<PushMessageNotification>? PushMessageNotificationList { get; set; }
    }

    public class PushMessageNotification : DomainRequestModelBase
    {
        public int? Id { get; set; }
        public int? TemplateTypeId { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? RecipientEmail { get; set; }
        public string? RecipientPhoneNumber { get; set; }
        public int? CreateBy { get; set; }
    }

    public class PushMessageNotificationRequest : DomainRequestModelBase
    {
        public int? Type { get; set; }
        public int? Id { get; set; }
        public bool? Isent { get; set; }
        public string? FailureMsg { get; set; }

    }
}

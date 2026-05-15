namespace Capex.Models.Common
{
    public class MessagesNotificationModelResponse
    {
        public IList<MessagesNotificationModel>? MessagesNotificationModelList { get; set; }
    }
    public class MessagesNotificationModel
    {
        public int? ModuleId { get; set; }
        public int TemplateId { get; set; }
        public int? TemplateTypeId { get; set; }
        public string? TemplateDesription { get; set; }
        public string? TemplateType { get; set; }
        public string? Subject { get; set; }
        public string? Query { get; set; }
        public string? Body { get; set; }
        public string? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? IsActive { get; set; }
        public string? TemplateCode { get; set; }
        public string? ActiveLink { get; set; }

    }
}

namespace Capex.DomainModels.DomainResponseModel.Masters
{
    public class TemplateResponse
    {
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
    }
}

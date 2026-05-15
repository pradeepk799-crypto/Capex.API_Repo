namespace Capex.DomainModels.DomainRequestModel.Masters
{
    public class TemplateRequest : DomainRequestModelBase
    {
        public int? TemplateId { get; set; }
        public int? TemplateTypeId { get; set; }
    }
}

using Capex.Models.RequestModel;

namespace Capex.Models.RequestModel
{
    public class APILogStatusRequestModel: RequestModelBase
    {
        public string? RequestMethod { get; set; }
        public int RequestId { get; set; }
        public string? RequestPayload { get; set; }
        public int ResponseId { get; set; }
        public string? ResponsePayload { get; set; }
        public int ResponseStatus { get; set; }
        public string? ClientIP { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

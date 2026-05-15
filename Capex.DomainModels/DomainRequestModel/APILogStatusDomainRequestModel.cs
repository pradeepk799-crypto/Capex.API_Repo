using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class APILogStatusDomainRequestModel: DomainRequestModelBase
    {
        public string? UserId { get; set; }
        public string? RequestMethod { get; set; }
        public string? RequestPayload {get;set;}
        public string? ResponsePayload { get; set; }
        public int ResponseStatus { get; set; }
        public string? ClientIP { get; set; }
        public string? ErrorMessage { get; set; }
        
    }
}

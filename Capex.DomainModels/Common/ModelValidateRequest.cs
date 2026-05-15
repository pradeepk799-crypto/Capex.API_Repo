using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.Common
{
    public class ModelValidateRequest:DomainRequestModelBase
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }
    }
}

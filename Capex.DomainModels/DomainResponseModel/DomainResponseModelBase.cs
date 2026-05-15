using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel
{
    public class DomainResponseModelBase
    {
        public int StatusCode { get; set; }
        public string  Message { get; set; }
    }
}

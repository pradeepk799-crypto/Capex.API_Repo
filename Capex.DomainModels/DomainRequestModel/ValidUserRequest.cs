using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class ValidUserRequest : DomainRequestModelBase
    {
      
        public string UserName { get; set; }      
        public string Mobno { get; set; }
        public int flag { get; set; }
        public int TemplateId { get; set; }
    }
}

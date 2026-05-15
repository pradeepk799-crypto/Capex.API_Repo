using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class DomainRequestModelBase  
    {
        public string? Language { get; set; }
        public string? AuthHeader { get; set; }
        public int? UID { get; set; }
        public int? UserOfficeId { get; set; }
        public int? UserRoleId { get; set; }
    }
}

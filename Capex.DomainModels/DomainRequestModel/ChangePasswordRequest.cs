using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class ChangePasswordRequest : DomainRequestModelBase
    {
        public string UserName { get; set; }
        public string UserID { get; set; }
       public string Mobno { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ModifyIP { get; set; }
        public int flag { get; set; }
      
    }
}

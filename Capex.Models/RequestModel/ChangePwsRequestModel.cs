using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel
{
    public class ChangePwsRequestModel : RequestModelBase
    {
        public string UserName { get; set; }
        public string UID { get; set; }
        public string Mobno { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public int flag { get; set; }
        public string Cipher { get; set; }
        public string Captcha { get; set; }
        //public string ModifyIP { get; set; }
        //public string salt { get; set; }
    }
}

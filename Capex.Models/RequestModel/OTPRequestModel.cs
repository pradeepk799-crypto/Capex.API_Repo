using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel
{
    public class OTPRequestModel : RequestModelBase
    {
        public string OTPType { get; set; }
        public string OTPFor { get; set; }
     
        public string EmailId { get; set; }
        public bool AuthCheck { get; set; }
        public string OTPReferenceId { get; set; }
        public string OTP { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserName { get; set; }
        public int? TemplateTypeId { get; set; }


    }

}

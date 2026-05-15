using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel
{
    public class CaptchaRequestModel   : RequestModelBase
    {
        public string Cipher { get; set; }
        public string Captcha { get; set; }


    }
    public class GstRequestModel : RequestModelBase
    {
        public string GstNumber { get; set; }      


    }
    public class PANRequestModel : RequestModelBase
    {
        public string PAN { get; set; }
        public string? Name { get; set; }
        public string? Fathername { get; set; } = string.Empty;
        public string? Dob { get; set; }
        public string? Secret_key { get; set; }

    }

}

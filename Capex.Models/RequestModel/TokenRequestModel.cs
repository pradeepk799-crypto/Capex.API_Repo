using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel
{
    public class TokenRequestModel : RequestModelBase
    {

        [DataMember]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        [StringLength(128)]
        public string UserName { get; set; }
        [DataMember]
        [StringLength(200)]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        public string Password { get; set; }
        public string GrantType { get; set; }
        [DataMember]
        public string RefreshToken { get; set; }
        public string type { get; set; }
        public string Cipher { get; set; }
        public string Captcha { get; set; }
        public long Rfrenceid { get; set; }
        public string OTP { get; set; }
        //public string salt { get; set; }
        public int LoginType { get; set; }
        public string? UserAgent { get; set; }

    }


    public class CitizenForgotPwdModel : RequestModelBase
    {

        [DataMember]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        [StringLength(128)]
        public string UserName { get; set; }
        public string type { get; set; }
        public string Cipher { get; set; }
        public string Captcha { get; set; }


    }
    public class ApplicationUser
    {

        public string? Password { get; set; }
        public string ProvidePassword { get; set; }


    }
    public class ForgotPasswordRequestModel : RequestModelBase
    {

       
        public string MobileNumber { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }


    }
}

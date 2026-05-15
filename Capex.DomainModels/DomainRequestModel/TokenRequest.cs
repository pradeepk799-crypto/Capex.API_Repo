using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class TokenRequest: DomainRequestModelBase
    {
        [DataMember]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        [StringLength(128)]
        public string UserName { get; set; }
        [DataMember]
        [StringLength(50)]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        public string Password { get; set; }
        public string GrantType { get; set; }
        [DataMember]
        public string RefreshToken { get; set; }
        public string type { get; set; }
        //public string salt { get; set; }
    }

    public class CitizenForgotPwdRequest : DomainRequestModelBase
    {
        [DataMember]
        // [Required(ErrorMessage = "Err100001")] // Removed required for RefreshToken
        [StringLength(128)]
        public string UserName { get; set; }
        public string type { get; set; }

    }
    public class ForgotPasswordDomainRequestModel : DomainRequestModelBase
    {


        public string MobileNumber { get; set; }
        public string Type { get; set; }
        public string Password { get; set; }


    }
}

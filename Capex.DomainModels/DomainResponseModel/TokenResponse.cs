
using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel
{
    public class TokenResponse : DomainRequestModelBase
    {
        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public string RefreshToken { get; set; }

        [DataMember]
        public DateTime? IssuedAt { get; set; }

        [DataMember]
        public DateTime? Expires { get; set; }

        [DataMember]
        public DateTime? RefreshTokenExpires { get; set; }
        [DataMember]
        public AdditionalUserLoginResponse AdditionalUserLoginResponse { get; set; }
    }


        /// AdditionalUserLoginResponseModel

        [DataContract]
        public class AdditionalUserLoginResponse
        {

            [DataMember]
            public UserLoginResponse UserLoginResponseModel { get; set; }
        }


    public class CitizenForgotPwdResponse : DomainRequestModelBase
    {
        [DataMember]
        public string MobileNo { get; set; }
        public Boolean Status { get; set; }

    }

    public class UserForgotPasswordDomainResponseModel
    {
        public string MobileNo { get; set; }
        public Boolean Status { get; set; }

    }
}


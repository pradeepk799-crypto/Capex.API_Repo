using System.Runtime.Serialization;

namespace Capex.Models.ResponseModel
{
    public class ValidUserResponseModel 
    {
     
        public string Msg { get; set; }    
        public bool Status { get; set; }
        public string Data { get; set; }
    }

    public class ForgotPasswordResponseModel  
    {
        public long? RefrenceId { get; set; }
        public string UserName { get; set; }
    }
    public class UserForgotPasswordResponseModel
    {
        public string MobileNo { get; set; }
        public Boolean Response { get; set; }

    }

}

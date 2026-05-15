using Capex.Models.RequestModel;


namespace Capex.Models.Common
{
    public class OTPmodel : RequestModelBase
    {
        public string? mobilenumber { get; set; }
        public long? Rfrenceid { get; set; }
        public string? OTP { get; set; }
    }

}

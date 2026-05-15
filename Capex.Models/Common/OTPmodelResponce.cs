using Capex.Models.ResponseModel;

namespace Capex.Models.Common
{
    public class OTPmodelResponce:ResponseModelBase
    {
        public string Message{  get; set; }
        public bool Status { get; set; }
        
    }
}

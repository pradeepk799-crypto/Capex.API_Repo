using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel
{
    public class ValidUserResponse 
    {

        public string Msg { get; set; }
        public bool Status { get; set; }
    }
    public class AadharLogResponse
    {

        public string LogId { get; set; }
    }

}

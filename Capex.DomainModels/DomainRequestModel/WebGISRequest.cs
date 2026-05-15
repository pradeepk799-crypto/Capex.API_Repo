using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel.WebGIS
{
    public class WebGISRequest: DomainRequestModelBase
    {
        public int? Bhucode { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
    }

    public class WebGISApplicationRequest: DomainRequestModelBase
    {
        public string ApplicationNo { get; set; }

    }

    

}

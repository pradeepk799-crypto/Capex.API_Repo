using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel.WebGIS
{
    public class WebGISRequestModel: RequestModelBase
    {
        public int? Bhucode { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
    }


    public class WebGISKhasraRequestModel : RequestModelBase
    {
        public int? Bhucode { get; set; }
         public List<KhasraNolist>? KhasraNolist { get; set; }
    }
    public class KhasraNolist
    {
        public string? khasraNo { get; set; }
        public string? khasraId { get; set; }
    }

    public class WebGISDraftDataRequestModel : RequestModelBase
    {
        public string ApplicationNo { get; set; }

    }
}

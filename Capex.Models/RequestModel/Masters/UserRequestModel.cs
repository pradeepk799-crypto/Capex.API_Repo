using Capex.Models.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel.Masters
{
    public class UserRequestModel : RequestModelBase
    {
       
    }
    public class Address
    {
        public string Dst { get; set; } // District Name
        public string Loc { get; set; } // City Name
        public string Pncd { get; set; } // Pincode
        public string stcd { get; set; } // Duty Type
    }

    public class PrincipalAddress
    {
        public Address Addr { get; set; }
    }

    public class GstData
    {
        public string Gstin { get; set; } // GSTIN
        public string Lgnm { get; set; } // Legal Name
        public string TradeNam { get; set; } // Trade Name
        public string Sts { get; set; } // Status
        public string Ctb { get; set; } // Constitution of Business
        public PrincipalAddress Pradr { get; set; } // Principal Address
        public string Dty { get; set; } // Duty Type
       

    }

    public class GSTDataModel
    {
        public GstData Data { get; set; }
    }

    public class ResultPAN
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
        public PANOutputData OutputData { get; set; }
    }
    public class PANOutputData
    {
        public string PAN { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string DOB { get; set; }
        public string pan_status { get; set; }
        public string seeding_status { get; set; } // Add this if needed
    }
}

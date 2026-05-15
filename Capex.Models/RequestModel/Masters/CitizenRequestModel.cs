using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RCMS4._0.Models.RequestModel.Masters
{
    public class CitizenRequestModel : RequestModelBase
    {
        public string UserName { get; set; }
    }

    public class CitizenPostRequestModel : RequestModelBase
    {
        public int CitizenId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Gender { get; set; }
        public DateTime? DOB { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public int? PINCode { get; set; }



    }
}

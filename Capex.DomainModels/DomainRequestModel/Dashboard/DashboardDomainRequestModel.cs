using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel.Dashboard
{
    public class DashboardDomainRequestModel : DomainRequestModelBase
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
    
    public class DashboardVenderDistrictDetailsDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}

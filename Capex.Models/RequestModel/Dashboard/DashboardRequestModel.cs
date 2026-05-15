using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel.Dashboard
{
    public class DashboardRequestModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
    public class DashboardVenderDistrictDetailsRequestModel
    {
        public string? Flag { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}

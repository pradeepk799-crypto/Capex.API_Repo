using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS4._0.Models.ResponseModel
{
    public class LdapResponseModel
    {
        public string ssoid { get; set; }
        public string name { get; set; }
        public string designation { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string grant_type { get; set; }
        public string status { get; set; }
    }
}

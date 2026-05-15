using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.RequestModel
{
    public class RedisCacheRequestModel : RequestModelBase
    {
        public string? Key { get; set; }
    }
}

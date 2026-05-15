using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.CustomValidation
{
    public class ModelValidateRequestModel
    {
        public string Area { get; set; }
        public string Controller { get; set; }
        public string ActionName { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.Common
{
    public class ModelValidateDetailResponse
    {
        public string PropertyName { get; set; }
        public bool IsRequired { get; set; }
        public string Type { get; set; }
        public int   MinLength  { get; set; }
        public int MaxLength { get; set; }
        public string Regx { get; set; }
        public string CustomValidation { get; set; }
        public string ErrorCode { get; set; }
    }
}

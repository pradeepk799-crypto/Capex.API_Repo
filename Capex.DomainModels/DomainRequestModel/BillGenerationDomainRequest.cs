using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel
{
    public class BillGenerationDomainRequest : DomainRequestModelBase
    {
        public int? BillGenerationId { get; set; } = null;
        public int? DistrictId { get; set; } = null;
        public int? DDO { get; set; } = null;
        public int? Building { get; set; } = null;
        public decimal? Price { get; set; } = null;
        public string? CiRation { get; set; } = null;
        public DateTime? StartReadingDate { get; set; } = null;
        public DateTime? EndReadingDate { get; set; } = null;
        public decimal? StartMeterReading_kWh_X { get; set; } = null;
        public decimal? EndMeterReading_kWh_Y { get; set; } = null;
        public decimal? TotalNetGeneration_kWh { get; set; } = null;
        public decimal? TotalSolarUnitGeneration_kWh { get; set; } = null;
        public int? CreatedBy { get; set; } = null;
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }


    }
    public class BillGenerationBuildingDetailsByVendorDomainRequest : DomainRequestModelBase
    {
        public string? billGenerationBuildingDetails { get; set; }
    }
}

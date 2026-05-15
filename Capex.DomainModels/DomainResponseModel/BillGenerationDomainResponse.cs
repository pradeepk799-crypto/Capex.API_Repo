
using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel
{
    public class BillGenerationDomainResponse : DomainRequestModelBase
    {
        public string? Message { get; set; }
        public bool? Result { get; set; } = default(bool?);
    }
    public class GetBillGenerationDomainResponse : DomainRequestModelBase
    {
        public string? BillDetails { get; set; }
        public string? BuildingDetails { get; set; }
        public bool? IsCombinedDateInvalid { get; set; }
        public bool? IsBillAlreadyGenerated { get; set; }
        public bool? IsPreviousBillAlreadyGenerated { get; set; }



    }
    public class BuildingDetailsByDDODomainResponse : DomainRequestModelBase
    {
        public int? BuildingId { get; set; }
        public string? BeneficiaryName { get; set; }
        public string? BuildingIdNumber { get; set; }
        public string? MeterSerialNo { get; set; }
        public decimal? Price { get; set; }
        public string? CIRatio { get; set; }
        public TimeSpan? StartTime { get; set; }
        public DateTime? StartReadingDate { get; set; }
        public Decimal? EndMeterReading_kWh_X { get; set; }
        public Decimal? TotalSolarUnitGeneration_kWh { get; set; }





    }
}


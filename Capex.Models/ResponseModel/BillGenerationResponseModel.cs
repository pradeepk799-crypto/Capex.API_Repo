namespace Capex.Models.ResponseModel
{
    public class BillGenerationResponseModel : ResponseModelBase
    {

        public string? Message { get; set; }
        public bool? Result { get; set; } = default(bool?);
    }
    public class GeBillGenerationResponseModel : ResponseModelBase
    {
        public List<GetBillGenerationDetailsResponseModel> billGenerationDetailsResponseModels { get; set; }
    }
    public class GetBillGenerationDetailsResponseModel : ResponseModelBase
    {
        public int? BillGenerationId { get; set; }
        public int? DistrictId { get; set; }
        public int? DDO { get; set; }
        public int? Building { get; set; }
        public decimal? Price { get; set; }
        public string? CiRation { get; set; }
        public DateTime? StartReadingDate { get; set; }
        public DateTime? EndReadingDate { get; set; }
        public decimal? StartMeterReading_kWh_X { get; set; }
        public decimal? EndMeterReading_kWh_Y { get; set; }
        public decimal? TotalNetGeneration_kWh { get; set; }
        public decimal? TotalSolarUnitGeneration_kWh { get; set; }

        // Additional Properties
        public string? BeneficiaryName { get; set; }
        public string? MeterSerialNo { get; set; }
        public string? IVRS { get; set; }
        public string? DistrictName { get; set; }
        public string? DDONameEn { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
    }
    public class BuildingDetailsByDDOResponseModel : ResponseModelBase
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

    public class BillGenerationBuildingDetailsByVendorResponseModel : ResponseModelBase
    {
        public List<BillGenerationBuildingDetailsByVendor> buildingDetailsResponse { get; set; }
        public bool? IsCombinedDateInvalid { get; set; }
        public bool? IsBillAlreadyGenerated { get; set; }
        public bool? IsPreviousBillAlreadyGenerated { get; set; }
        
    }

    public class BillGenerationBuildingDetailsByVendor
    {
        public int? BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? MeterSerialNo { get; set; }
        public string? IVRS { get; set; }
        public decimal? Price { get; set; }
        public int? MeterId { get; set; }

        public int? BillGenerationId { get; set; }
        public int? DistrictId { get; set; }
        public string? DDO { get; set; }
        public int? Building { get; set; }
        public decimal? CiRation { get; set; }

        public DateTime? StartReadingDate { get; set; }
        public DateTime? EndReadingDate { get; set; }
        public decimal? StartMeterReading_kWh_X { get; set; }
        public decimal? EndMeterReading_kWh_Y { get; set; }
        public decimal? TotalNetGeneration_kWh { get; set; }
        public decimal? TotalSolarUnitGeneration_kWh { get; set; }
    }
}

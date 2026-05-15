using Capex.Models.ResponseModel;

namespace Capex.Models.RequestModel.Masters
{
    public class MasterDetailsRequestModel : RequestModelBase
    {

    }



    public class DDORequestModel : RequestModelBase
    {
        public int? DDOId { get; set; }
        public string? DDOCode { get; set; }
        public string? DDONameEn { get; set; }
        public string? DDOName_Hi { get; set; }
        public string? NodalPersonName_En { get; set; }
        public string? ContactDetails { get; set; }
        public string? EmailID { get; set; }
        public int? DistrictId { get; set; }
        public string? Address { get; set; }
        public int? CreatedBy { get; set; }
        public int? TrsId { get; set; }
        public int? DeptId { get; set; }
        public string? Password { get; set; }
        public int? IsPasswordChanged { get; set; }



    }
    public class DDODetailsRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? DDOId { get; set; }
        public string? DDOCode { get; set; }
        public string? DDONameEn { get; set; }

        public string? NodalPersonName_En { get; set; }
        public string? ContactDetails { get; set; }
        public string? EmailID { get; set; }
        public int? DistrictId { get; set; }
        public int? IsActive { get; set; }

    }
    public class BankDetailsRequestModel : RequestModelBase
    {
        public string IFSCCode { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Centre { get; set; }
        public string Address { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string MICR { get; set; }
        public string BankCode { get; set; }
        public string SWIFT { get; set; }
        public string Contact { get; set; }
        public bool RTGS { get; set; }
        public bool IMPS { get; set; }
        public bool UPI { get; set; }
        public bool NEFT { get; set; }
        public string? CreatedBy { get; set; }
    }
    public class BankSearchRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public int? BankId { get; set; }
        public int? IsActive { get; set; }


    }

    public class BuildingRegistrationRequestModel : RequestModelBase
    {

        public string? MeterSerialNo { get; set; }
        public string SiteAddress { get; set; } = string.Empty;
        public string BeneficiaryName { get; set; } = string.Empty;
        public long SanctionedLoad { get; set; }
        public string HESName { get; set; } = string.Empty;
        public int Phase { get; set; }
        public string MeterMaker { get; set; } = string.Empty;
        public string TariffCategory { get; set; } = string.Empty;
        public string FeederName { get; set; } = string.Empty;
        public string DTRName { get; set; } = string.Empty;
        public string? PhoneNo { get; set; }
        public string? EmailID { get; set; }
        public string Region { get; set; } = string.Empty;
        public string Circle { get; set; } = string.Empty;
        public string Division { get; set; } = string.Empty;
        public string District { get; set; } = string.Empty;
        public string DDOId { get; set; } = string.Empty;
        public string CircleId { get; set; } = string.Empty;
        public string DivisionId { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;

    }
    public class BuildingDetailsSearchRequestModel : RequestModelBase
    {
        public string Flag { get; set; } = string.Empty;
        public string BuildingNumber { get; set; } = string.Empty;
        public string BuildingId { get; set; } = string.Empty;
        public string? MeterSerialNo { get; set; }
        public string BeneficiaryName { get; set; } = string.Empty;
        public string HESName { get; set; } = string.Empty;
        public string? EmailID { get; set; }
        public string DDOId { get; set; } = string.Empty;
        public string CircleId { get; set; } = string.Empty;
        public string DivisionId { get; set; } = string.Empty;
        public string DistrictId { get; set; } = string.Empty;

        public int? IsActive { get; set; }
    }

    public class VendorDataRequestModel : RequestModelBase
    {

        public List<Vendor>? Vendors { get; set; }
        public List<VendorNodalPerson>? VendorNodalPersons { get; set; }
        public List<VendorAccount>? VendorAccounts { get; set; }
        public List<VendorDistrict>? VendorDistricts { get; set; }
        public List<VendorDDO>? VendorDDOs { get; set; }
    }


    public class Vendor
    {
        public int? VendorId { get; set; }
        public int? UserId { get; set; }
        public string? PANNumber { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }

        public string? Address { get; set; }
        public int? district { get; set; }
        public DateTime? DOB { get; set; }

        public string Email { get; set; }
        public string? ContactDetails { get; set; }
        public int? CreatedBy { get; set; }
        public string? Password { get; set; }

    }
    public class VendorNodalPerson
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactDetails { get; set; }
        public string Email { get; set; }
    }
    public class VendorAccount
    {
        public int? BankId { get; set; }
        public string? IfscCode { get; set; }
        public string? AccountNo { get; set; }

    }
    public class VendorDistrict
    {
        public int DistrictId { get; set; }
    }
    public class VendorDDO
    {
        public int DDOId { get; set; }
    }
    public class DistrictsRequestModel : RequestModelBase
    {
        public List<int> DistrictIds { get; set; }
    }

    public class VendorSearchDRequestModel : RequestModelBase
    {
        public string? VendorId { get; set; }
        public string? DistrictId { get; set; }
        public string? EmailId { get; set; }
        public string? Contact { get; set; }
        public string? PAN { get; set; }
        public string? VendorName { get; set; }
    }


    public class UnitPriceRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public int? PriceId { get; set; }
        public decimal Price { get; set; }
        public int? VendorId { get; set; }
        public int? DistrictId { get; set; }
        public int? UnitId { get; set; }

        public int? IsActive { get; set; }

    }
    public class GetBillDetailsRequestModel : RequestModelBase
    {
        public string? MeterNo { get; set; }
        public string? BuildingName { get; set; }
        public DateTime? StartReadingDate { get; set; }
        public DateTime? EndReadingDate { get; set; }


    }
    public class BuildingDetailsByDDORequestModel : RequestModelBase
    {
        public int? DistrictId { get; set; }
        public int? BuildingId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }


    }


    #region Building Information
    public class SaveBuildingRequest : RequestModelBase
    {
        public BuildingModel Building { get; set; }
        public List<BuildingMappingModel> BuildingMapping { get; set; }
        public List<GenerationMeterModel> GenerationMeter { get; set; }
        public OtherBuildingDetailsModel OtherBuildingDetails { get; set; }
    }
    public class BuildingModel
    {
        public int? BuildingId { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Mobile { get; set; }
        public decimal SanctionedLoad { get; set; }
        public string Consumer_No { get; set; }
        public string Address { get; set; }
        public string Zone { get; set; }
        public string Region { get; set; }
        public string Circle { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string? Installation { get; set; }

    }
    public class BuildingMappingModel
    {
        public int? MappingId { get; set; }
        public int? BuildingId { get; set; }
        public int? DDOId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DistrictId { get; set; }

    }
    public class GenerationMeterModel
    {
        public int? MeterId { get; set; }
        public int? BuildingId { get; set; }
        public string MeterSerialNo { get; set; }
        public string NameOfConsumer { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string Manufacturer { get; set; }
        public string HESName { get; set; }
    }
    public class OtherBuildingDetailsModel
    {
        public int? DetailId { get; set; }
        public int? BuildingId { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? ExistingRoofSpaceAvailable_SQFT { get; set; }
        public decimal? AvailableCapacity_KW { get; set; }
        public decimal? ProposedCapacity_KW { get; set; }
        public decimal? CombinedCapacity { get; set; }
        public DateTime? CommissionedDate { get; set; }

    }
    public class BillGenerationBuildingDetailsByVendorRequestModel : RequestModelBase
    {
        public List<BillGenerationBuildingDetailsByVendorList> billGenerationBuildingDetails { get; set; }
    }

    public class BillGenerationBuildingDetailsByVendorList
    {
        public int? BuildingId { get; set; }
        public string? BuildingName { get; set; }
        public string? MeterSerialNo { get; set; }
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

    public class ValidateIVRSAndMeterExistRequestModel : RequestModelBase
    {
        public string? Flag { get; set; }
        public string? MeterSerialNo { get; set; }
        public string? ConsumerNo { get; set; }



    }


    #endregion
}

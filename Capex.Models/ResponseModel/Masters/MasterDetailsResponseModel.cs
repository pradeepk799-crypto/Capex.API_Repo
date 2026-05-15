using Capex.Models.RequestModel;

namespace Capex.Models.ResponseModel.Masters
{
    public class MasterDetailsResponseModel : ResponseModelBase
    {


    }
    public class MasterResponseModel : ResponseModelBase
    {
        public bool response { get; set; }
        public int? UserId { get; set; }



    }
    public class DDODetailsResponseModel : ResponseModelBase
    {
        public int? DDOId { get; set; }
        public string? DDOCode { get; set; }
        public string? DDONameEn { get; set; }
        public string? DDOName_Hi { get; set; }
        public string? NodalPersonName_En { get; set; }
        public string? ContactDetails { get; set; }
        public string? EmailID { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public string? Address { get; set; }
        public int? CreatedBy { get; set; }
        public int? IsActive { get; set; }
        public int? TrsId { get; set; }
        public int? DeptId { get; set; }

        public string? TrsName { get; set; }
        public string? DeptName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? IsPasswordChanged { get; set; }

    }
    public class BankDetailsResponseModel : ResponseModelBase
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
        public int? BankId { get; set; }

        public string? CreatedDate { get; set; }
        public int? IsActive { get; set; }

    }
    public class BuildingRegistrationResponseModel : ResponseModelBase
    {

        public int BuildingId { get; set; }
        public string? BuildingIdNumber { get; set; }
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
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int? IsActive { get; set; }
        public string? PhaseName { get; set; }
        public string? DDONameEn { get; set; }
        public string? DeptName { get; set; }

        public decimal? ProposedCapacity_KW { get; set; }

    }
    public class VendorDataListResponseModel : ResponseModelBase
    {

        public List<VendorD> Vendors { get; set; }
        public List<VendorNodalPersonD> VendorNodalPersons { get; set; }
        public List<VendorAccountD> VendorAccounts { get; set; }
        public List<VendorDistrictD> VendorDistricts { get; set; }
        public List<VendorDDOD> VendorDDOs { get; set; }
    }


    public class VendorD
    {
        public int? VendorId { get; set; }
        public int? UserId { get; set; }
        public string PANNumber { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public DateTime? DOB { get; set; }
        public string Email { get; set; }
        public string? ContactDetails { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
      
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? DistrictName { get; set; }

    }
    public class VendorNodalPersonD
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string ContactDetails { get; set; }
        public string Email { get; set; }
    }
    public class VendorAccountD
    {
        public string? BankName { get; set; }
        public string? AccountNo { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankId { get; set; }



    }
    public class VendorDistrictD
    {
        public int DistrictId { get; set; }
    }
    public class VendorDDOD
    {
        public int DDOId { get; set; }
    }
    public class UnitPriceResponseModel : ResponseModelBase
    {

        public int? PriceId { get; set; }
        public decimal Price { get; set; }
        public int? VendorId { get; set; }
        public int? DistrictId { get; set; }
        public int? UnitId { get; set; }
        public string? DistrictName { get; set; }
        public string? VendorName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Unit { get; set; }
        public int? IsActive { get; set; }



    }

    #region Building Information
    public class GetBuildingResponse : ResponseModelBase
    {
        public List<BuildingModelJson> Building { get; set; }
        public List<BuildingMappingJsonModel> BuildingMapping { get; set; }
        public List<GenerationMeterModelJson> GenerationMeter { get; set; }
        public List<OtherBuildingDetailsModelJson> OtherBuildingDetails { get; set; }
    }
    public class BuildingModelJson
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
    public class BuildingMappingJsonModel
    {
        public int? MappingId { get; set; }
        public int? BuildingId { get; set; }
        public int? DDOId { get; set; }
        public int? DepartmentId { get; set; }
        public int? DistrictId { get; set; }

    }
    public class GenerationMeterModelJson
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
    public class OtherBuildingDetailsModelJson
    {
        public int? DetailId { get; set; }
        public int? BuildingId { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public decimal? ExistingRoofSpaceAvailable_SQFT { get; set; }
        public decimal? AvailableCapacity_KW { get; set; }
        public decimal? ProposedCapacity_KW { get; set; }
        public decimal? CombinedCapacity { get; set; }
        public DateTime? CombinedDate { get; set; }

    }
    public class ValidateIVRSAndMeterExistResponseModel : ResponseModelBase
    {

        public bool? IsConsumerExists { get; set; }
        public bool? IsMeterSerialExists { get; set; }
    }
    #endregion
}

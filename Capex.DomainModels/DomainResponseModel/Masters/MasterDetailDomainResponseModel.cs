using Capex.DomainModels.DomainRequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel.Masters
{
    public class MasterDetailDomainResponseModel : DomainResponseModelBase
    {

    }

    public class MasterDomainResponseModel : DomainResponseModelBase
    {
        public bool response { get; set; }
        public int? UserId { get; set; }

    }
    public class DDODetailsDomainResponseModel : DomainResponseModelBase
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
        public string? CreatedBy { get; set; }
        public int? IsActive { get; set; }
        public int? TrsId { get; set; }
        public int? DeptId { get; set; }

        public string? TrsName { get; set; }
        public string? DeptName { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? IsPasswordChanged { get; set; }

    }

    public class BankDetailsDomainResponseModel : DomainResponseModelBase
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

        public string? CreatedDate { get; set; }
        public int? BankId { get; set; }
        public int? IsActive { get; set; }


    }
    public class BuildingRegistrationDomainResponseModel : DomainResponseModelBase
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
    public class VendorDataDomainResponseModel : DomainResponseModelBase
    {
        public string? Users { get; set; }
        public string Vendors { get; set; }
        public string VendorNodalPersons { get; set; }
        public string VendorAccounts { get; set; }
        public string VendorDistricts { get; set; }
        public string VendorDDOs { get; set; }
    }
    public class UnitPriceDomainResponseModel : DomainResponseModelBase
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
    public class GetBuildingResponseModel : DomainResponseModelBase
    {
        public string? Building { get; set; }
        public string BuildingMapping { get; set; }
        public string GenerationMeter { get; set; }
        public string OtherBuildingDetails { get; set; }
       
    }

    public class ValidateIVRSAndMeterExistDomainResponseModel : DomainResponseModelBase
    {
   

        public bool? IsConsumerExists { get; set; }
        public bool? IsMeterSerialExists { get; set; }
    }
    public class SaveDataDomainResponseModel : DomainResponseModelBase
    {
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsSuccess { get; set; }
        public int? TemplateTypeId { get; set; }
        public int? UserId { get; set; }
        public string? DistrictName { get; set; }
        public string? VendorName { get; set; }
        public string? DDOName { get; set; }
        public string? IVRSNO { get; set; }
        public string? CurrentDate { get; set; }


    }
}

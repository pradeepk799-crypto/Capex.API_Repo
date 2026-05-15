using Capex.DomainModels.DomainResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainRequestModel.Masters
{
    public class MasterDetailDomainRequestModel : DomainRequestModelBase
    {

    }
    public class DDODomainRequestModel : DomainRequestModelBase
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
    public class DDODetailsDomainRequestModel : DomainRequestModelBase
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

    public class BankDetailsDomainRequestModel : DomainRequestModelBase
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
    public class BankSearchDomainRequestModel : DomainRequestModelBase
    {

        public string? Flag { get; set; }
        public string? IFSCCode { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public int? BankId { get; set; }

        public int? IsActive { get; set; }

    }

    public class BuildingRegistrationDomainRequestModel : DomainRequestModelBase
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

    public class BuildingDetailsSearchDomainRequestModel : DomainRequestModelBase
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

    public class VendorDataDomainRequestModel : DomainRequestModelBase
    {
        public string? Users { get; set; }
        public string Vendors { get; set; }
        public string VendorNodalPersons { get; set; }
        public string VendorAccounts { get; set; }
        public string VendorDistricts { get; set; }
        public string VendorDDOs { get; set; }
    }

    public class DistrictsDomainRequestModel : DomainRequestModelBase
    {
        public List<string> DistrictIds { get; set; }
    }
    public class VendorSearchDomainRequestModel : DomainRequestModelBase
    {
        public string? VendorId { get; set; }
        public string? DistrictId { get; set; }
        public string? EmailId { get; set; }
        public string? Contact { get; set; }

        public string? PAN { get; set; }
        public string? VendorName { get; set; }


    }
    public class UnitPriceDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public int? PriceId { get; set; }
        public decimal Price { get; set; }
        public int? VendorId { get; set; }
        public int? DistrictId { get; set; }
        public int? UnitId { get; set; }
        public int? IsActive { get; set; }

    }
    public class GetBillDetailsDomainRequestModel : DomainRequestModelBase
    {
        public string? MeterNo { get; set; }
        public string? BuildingName { get; set; }
        public string? StartReadingDate { get; set; }
        public string? EndReadingDate { get; set; }

    }
    public class BuildingDetailsByDDODomainRequestModel : DomainRequestModelBase
    {
        public int? DistrictId { get; set; }
        public int? BuildingId { get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }


    }

    #region BuildingInformation 
    public class BuildingDomainRequestModel : DomainRequestModelBase
    {

        public string Building { get; set; }
        public string BuildingMapping { get; set; }
        public string GenerationMeter { get; set; }
        public string OtherBuildingDetails { get; set; }

    }
    public class ValidateIVRSAndMeterExistDomainRequestModel : DomainRequestModelBase
    {
        public string? Flag { get; set; }
        public string? MeterSerialNo { get; set; }
        public string? ConsumerNo { get; set; }

       
    }
    #endregion
}

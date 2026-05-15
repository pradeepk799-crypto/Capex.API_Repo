using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.DomainModels.DomainResponseModel.WebGIS
{
    public class WebGISResponse: DomainResponseModelBase
    {
        public IList<WebGISKhasraAndOwnerDetails>? WebGISKhasraAndOwnerResponse { get; set; }
    }
    public class WebGISKhasraAndOwnerDetails
    {
        public string? BhuCode { get; set; }
        public string? khasraNo { get; set; }
        public string? khasraId { get; set; }
        public string SurveyArea { get; set; }
        public string IsLandIrrigated { get; set; }
        public string? LandOwnershipType { get; set; }
        public string? Noyiyat { get; set; }
        public string? LagaanToPay { get; set; }
        public string? CessToPay { get; set; }
        public string? LoanFlag { get; set; }
        public string? LoanArea { get; set; }
        public string? LandUseType { get; set; }
        public string? Lemarks { get; set; }
        public string? Remarks { get; set; }
       public List<WebGISOwnerDetails>? OwnerDetailsRes { get; set; }
    }

 
    #region WebGIS Khasra List
    public class WebGISKhasraListResponse : DomainResponseModelBase
    {
        public IList<WebGISKhasraList>? WebGISKhasraListsResponse { get; set; }
    }
    public class WebGISKhasraList
    {
        public string? khasraNo { get; set; }
        public string? khasraId { get; set; }
    }
    #endregion
    #region WebGIS Khasra Details
    public class WebGISKhasraDetailsResponse : DomainResponseModelBase
    {
        public IList<WebGISKhasraDetailsDomain>? WebGISKhasraDetResponse { get; set; }
    }
    public class WebGISKhasraDetailsDomain
    {
        public string? BhuCode { get; set; }
        public string? khasraNo { get; set; }
        public string? khasraId { get; set; }
        public double SurveyArea { get; set; }
        public int? IsLandIrrigated { get; set; }
        public string? LandOwnershipType { get; set; }
        public string? Noyiyat { get; set; }
        public double? LagaanToPay { get; set; }
        public double? CessToPay { get; set; }
        public int? LoanFlag { get; set; }
        public double? LoanArea { get; set; }
        public string? LandUseType { get; set; }
        public string? Remarks { get; set; }

    }
    #endregion
    #region WebGIS Khasra Owner Details
    public class WebGISOwnerDetailsResponse : DomainResponseModelBase
    {
        public IList<WebGISOwnerDetails>? WebGISOwnerResponse { get; set; }
    }
    public class WebGISOwnerDetails
    {
        public string? OwnerId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? OwnershipType { get; set; }
        public string? OwnershipTypeCode { get; set; }
        public string? RelationType { get; set; }
        public string? FatherName { get; set; }
        public string? Gender { get; set; }
        public string? Caste { get; set; }
        public string? SubCaste { get; set; }
        public string? HouseNo { get; set; }
        public string? Street { get; set; }
        public string? PostOffice { get; set; }
        public string? Thana { get; set; }
        public string? State { get; set; }
        public string? District { get; set; }
        public string? Tehsil { get; set; }
        public string? Village { get; set; }
        public string? PinCode { get; set; }
        public string? Remarks { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? Bank { get; set; }
        public string? BankAccountNo { get; set; }
        public string? PanCard { get; set; }
        public string? KisanCreditCard { get; set; }
        public string? AadharCard { get; set; }
        public string? DrivingLicense { get; set; }
        public string? Passport { get; set; }
        public string? VoterId { get; set; }
        public string? RationCard { get; set; }
        public string? OwnerShare { get; set; }
    }
    #endregion
    #region WebGIS Basra Details
    public class WebGISBasraDetailsResponse : DomainResponseModelBase
    {
        public IList<WebGISBasraDetails> BasraDetailsResponse { get; set; }
    }
    public class WebGISBasraDetails
    {
        public string? BasraNo { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
        public int? LandType { get; set; }
    }
    #endregion

    #region Chatur Seema Details List

    public class KhasraAdjDetailsResponse
    {
        public string ServiceFlag { get; set; }
        public string North { get; set; }
        public string South { get; set; }
        public string East { get; set; }
        public string West { get; set; }
    }
    public class WebGisTokenResponseDomain
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public string issuedAt { get; set; }
        public string expiresAt { get; set; }
    }
    #endregion



    public class WebGISDraftDataDomainResponse : DomainResponseModelBase
    {
        public string district_id { get; set; }
        public string tehsil_id { get; set; }
        public string r_i_id { get; set; }
        public string halka_id { get; set; }
        public string village_id { get; set; }
        public string request_id { get; set; }
        public string lgd_code { get; set; }
        public List<khasraData> khasraData { get; set; }
    }
    public class khasraData
    {
        public string khasra_id { get; set; }
        public string basra_id { get; set; }
        public string khasra_no { get; set; }
        public string khasra_area { get; set; }
        public string land_type_id { get; set; }
        public DateTime yearStartDate { get; set; }
        public DateTime yearEndDate { get; set; }
        public string old_khasra_id { get; set; }
        public string owner_id { get; set; }
        public string ownership_type_id { get; set; }
        public string owner_share { get; set; }
        public string relation_id { get; set; }
        public string name { get; set; }
        public string father_name { get; set; }
        public string sex { get; set; }
        public string caste_id { get; set; }
        public string mobile_no { get; set; }
        public string address { get; set; }
        public string house_no { get; set; }
        public string street_name { get; set; }
        public string village_name { get; set; }
        public string postoffice { get; set; }
        public string thana { get; set; }
        public string tehsil_name { get; set; }
        public string district { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public string email_id { get; set; }
        public string tax { get; set; }
        public string aadhar_no { get; set; }
        public string samagra_id { get; set; }
        public string guardian { get; set; }
    }


}

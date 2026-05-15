using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.ResponseModel.WebGIS
{
    public class WebGISResponseModel: ResponseModelBase
    {
      public  IList<WebGISKhasraAndOwnerDetailsModel>? KhasraAndOwnerLst { get; set; }
    }

    public class WebGISKhasraNoResponseModel : ResponseModelBase
    {
        public IList<WebGISResponseModel>? KhasraAndOwnerLstbyMultikhasra { get; set; }
    }

    public class WebGISKhasraAndOwnerDetailsModel
    {
        public int SeemaId { get; set; }
        public string? BhuCode { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
        public string SurveyArea { get; set; }
        public string? IsLandIrrigated { get; set; }
        public string? LandOwnershipType { get; set; }
        public string? Noyiyat { get; set; }
        public string? LagaanToPay { get; set; }
        public string? CessToPay { get; set; }
        public string? LoanFlag { get; set; }
        public string? LoanArea { get; set; }
        public string? LandUseType { get; set; }
        public string? Remarks { get; set; }

        public List<WebGISOwnerDetailsModel>? OwnerDetails { get; set; }
    }
    #region WebGIS Khasra List
    public class WebGISKhasraListResponseModel : ResponseModelBase
    {
        public IList<WebGISKhasraListModel> WebGISKhasrListResponse { get; set; }
    }
    public class WebGISKhasraListModel
    {
        public string? khasraNo { get; set; }
        public string? khasraId { get; set; }
    }
    #endregion
    #region WebGIS Khasra Details 
    public class WebGISKhasraDetailsResponseModel : ResponseModelBase
    {
        public IList<WebGISKhasraDetailsModel> WebGISKhasraDetails { get; set; }

    }
    public class WebGISKhasraDetailsModel
    {
        public string? BhuCode { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
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
    #region WebGIS Owner Details
    public class WebGISOwnerDetailsResponseModel : ResponseModelBase
    {
        public IList<WebGISOwnerDetailsModel> WebGISKhasraDetails { get; set; }
    }
    public class WebGISOwnerDetailsModel
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
        public string? Pincode { get; set; }
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
    public class WebGISBasraDetailsResponseModel : ResponseModelBase
    {
        public IList<WebGISBasraDetailsModel> WebGISBasraDetails { get; set; }
    }
    public class WebGISBasraDetailsModel
    {
        public string? BasraNo { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
        public int? LandType { get; set; }
    }
    #endregion

    #region Chatur Seema Details List

    public class ChaturSeemaResponseModel: ResponseModelBase
    {
        public IList<KhasraAdjDetails> khasraAdjDetails { get; set; }
    }
    public class KhasraAdjDetails
    {
        public int SeemaId { get; set; } = 0;
        public string ServiceFlag { get; set; }
        public string? Directions { get; set; }
        public string? DirectionDetails { get; set; }
        public Boolean IsSeemaFlag { get; set; } = false;
        public string? KhasraId { get; set; }
        public string? OwnerName { get; set; }
        public string? GuardianName { get; set; }
        public string? FullAddress { get; set; }
        public string? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public bool IsWebgisData { get; set; }     
    }
    public class WebGisTokenResponse : ResponseModelBase
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; }
        public string issuedAt { get; set; }
        public string expiresAt { get; set; }
    }



       public class WebGISDraftDataResponse : ResponseModelBase
    {
        public string DistrictId { get; set; }
        public string TehsilId { get; set; }
        public string RIId { get; set; }
        public string HalkaId { get; set; }
        public string VillageId { get; set; }
        public string RequestId { get; set; }
        public string LgdCode { get; set; }
        public List<KhasraresData> KhasraresData { get; set; }
    }
    public class KhasraresData
    {
        public string KhasraId { get; set; }
        public string BasraId { get; set; }
        public string KhasraNo { get; set; }
        public string KhasraArea { get; set; }
        public string LandTypeId { get; set; }
        public DateTime YearStartDate { get; set; }
        public DateTime YearEndDate { get; set; }
        public string OldKhasraId { get; set; }
        public string OwnerId { get; set; }
        public string OwnershipTypeId { get; set; }
        public string OwnerShare { get; set; }
        public string RelationId { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Sex { get; set; }
        public string CasteId { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string HouseNo { get; set; }
        public string StreetName { get; set; }
        public string VillageName { get; set; }
        public string PostOffice { get; set; }
        public string Thana { get; set; }
        public string TehsilName { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Pincode { get; set; }
        public string EmailId { get; set; }
        public string Tax { get; set; }
        public string AadharNo { get; set; }
        public string SamagraId { get; set; }
        public string Guardian { get; set; }
    }
    #endregion
}


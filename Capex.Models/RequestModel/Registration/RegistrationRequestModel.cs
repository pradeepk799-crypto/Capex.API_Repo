namespace Capex.Models.RequestModel.Registration
{
    public class EmailOTPRequestModel : RequestModelBase
    {
        public string? Type { get; set; }
        public string? EmailId { get; set; }
        public string? RefKey { get; set; }
        //public ToEmailOTPRequestModel? OtpRequestModel { get; set; }

    }
    public class EmailLoginRequestModel : RequestModelBase
    {
        public string? Password { get; set; }
        public string? EmailId { get; set; }
        public string? LoginType { get; set; }
        public CaptchaRequestModel? captchaRequestModel { get; set; }


    }
    public class ProfileRegistrationRequestModel : RequestModelBase
    {
        public ProfileRequestModel? ProfileRequestModel { get; set; }
    }


    public class ProfileRequestModel
    {
        // Basic Information
        public string? Id { get; set; }
        public string? EmailId { get; set; }
        public string? ApplicationType { get; set; }
        public string? LoginType { get; set; }
        public string? ProfileName { get; set; }
        public DateTime? DOB { get; set; }
        public string? FatherName { get; set; }

        // Identification Details
        public string? PAN { get; set; }
        public string? GST { get; set; }

        // Profile Location Information
        public int? ProfileStateId { get; set; }
        public int? ProfileDistrictId { get; set; }
        public string? ProfileAddress { get; set; }
        // Business and Entity Information
        public int? EntityId { get; set; }
        public string? OtherEntity { get; set; }
        public int? BusinessEntityId { get; set; }
        public string? OtherBusiness { get; set; }
        public string? NameOfCOE { get; set; }
        public bool? IsGovt { get; set; }

        // Registration Location Details
        public int? RegStateId { get; set; }
        public int? RegDistrictId { get; set; }

        // Correspondence Location Details
        public int? CorrStateId { get; set; }
        public int? CorrDistrictId { get; set; }
        public string? CorrAddress { get; set; }

        // Additional Information
        public string? NameOfNO { get; set; }
        public string? DesignationId { get; set; }
        public int? SectorId { get; set; }
        public bool? IsAgency { get; set; }
        public string? EstablishmentTradeName { get; set; }
        public string? ConstitiutionTypeofEstablishment { get; set; }
        public string? EstablishmentOperationalStatus { get; set; }
        public string? RegistrationCity { get; set; }
        public string? RegistrationPincode { get; set; }
        public string? CorrespondanceCity { get; set; }
        public string? CorrespondancePincode { get; set; }
    }



    public class GetProfileRequestModel : RequestModelBase
    {
        public string? UserType { get; set; }

    }
    public class ProjectDetailsRequestModel : RequestModelBase
    {
        public ProjectDetailsModel? ProjectDetails { get; set; }
    }

    public class ProjectDetailsModel
    {
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectCompany { get; set; }
        public int? ProjectTypeId { get; set; }
        public string? OtherProjectType { get; set; }
        public int? ProjectCapacityId { get; set; }
        public string? SchematicLayoutDetails { get; set; }
        public int? ExpAnnualGenId { get; set; }
        public string? MinimumAnnualCUF { get; set; }
        public string? ProjectInvestment { get; set; }
        public bool? TypeOfArea { get; set; }
        public int? PstateId { get; set; }
        public int? PDistrictId { get; set; }
        public int? PTehsiId { get; set; }
        public int? PVillageId { get; set; }
        public string? Address { get; set; }
        public string? ApproachRoad { get; set; }
        public string? RailwayStation { get; set; }
        public string? CoordinatesOfProject { get; set; }
        public string? LandCapacity { get; set; }
        public string? IsApplicantName { get; set; }
        public string? IsPurchased { get; set; }
        public string? IsUnderAgreement { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? EntityTypeId { get; set; }
        public string? OtherEntity { get; set; }

    }
    public class GetProjectRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }


    }
    public class LandRegistrationRequestModel : RequestModelBase
    {
        public string ApplicationId { get; set; }
        public int ProjectId { get; set; }


        public LandRecordsRequestModel? LandRecordsRequestModel { get; set; }


    }
    public class LandRecordsRequestModel
    {

        public int LandType { get; set; }
        public int DistrictID { get; set; }
        public string? DistrictName { get; set; }
        public int TehsilId { get; set; }
        public string? TehsilName { get; set; }
        public int VillageID { get; set; }
        public string? VillageName { get; set; }
        public int CourtID { get; set; }
        public string? PatwariHalkaNo { get; set; }
        public string? VillageUrbanAreaName { get; set; }
        public List<LandDeailsOwnerRequestModel>? LandDeailsOwnerRequestModel { get; set; }
    }
    public class LandDeailsOwnerRequestModel
    {
        public int LandId { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
        public decimal SurveyArea { get; set; }
        public decimal LagaanToPay { get; set; }
        public List<LandOwnerNameList>? landownerlist { get; set; }

    }
    public class LandOwnerNameList
    {
        public int LandOwnerId { get; set; }
        public string? OwnerId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FatherName { get; set; }
        public string? OwnerShare { get; set; }
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
        public string? MobileNo { get; set; }
        public string? Email { get; set; }
        public string? PanCard { get; set; }
        public string? AadharCard { get; set; }
        public string? OwnershipType { get; set; }
        public string? Remarks { get; set; }
        public int? LandId { get; set; }
        public string? Address { get; set; }


    }
    public class LandDetailsRequestModel : RequestModelBase
    {
        public string ApplicationId { get; set; }
        public int? ProjectId { get; set; }

        public int flag { get; set; } = 0;
    }
    public class PowerEvacuationModel : RequestModelBase
    {
        public int? ProjectId { get; set; }
        public int? LandDetailsId { get; set; }
        public int? ApplicationId { get; set; }
        public string? IsEvacuation { get; set; }
        public int? VoltageId { get; set; }
        public int? ConnectivityId { get; set; }
        public string? DiscomId { get; set; }
        public string? CircleId { get; set; }
        public string? SubstationId { get; set; }
        public string? Distance { get; set; }
        public int? PowerGeneratedId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class GetPowerEvacuationModel : RequestModelBase
    {

        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }


    }
    public class GetUploadDocumentMasterRequestModel : RequestModelBase
    {

        public string? Flag { get; set; }
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }

    }

    public class UploadedDocDetailsRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }

        public List<UploadDocDetails>? UploadDocDetails { get; set; }
    }
    public class UploadDocDetails
    {
        public int? Id { get; set; }
        public int? UploadId { get; set; }
        public int? MasterFileId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string? DocumentName { get; set; }
        public string? FileSize { get; set; }
        public string? FileType { get; set; }

    }

    public class ProjectSearchRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? UserTypeId  { get; set; }

        public string? ApplicationNumber { get; set; }
        public string? ProjectName { get; set; }
        public int? UserId { get; set; }
        public int? ProcessStatus { get; set; }
        public int? ApplicationStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
    public class ApplicationPreviewRequestModel : RequestModelBase
    {
        public int? ProjectId { get; set; }
        public int? ApplicationId { get; set; }

    }
    public class PANRequest : RequestModelBase
    {
        public string PAN { get; set; }
        public string Name { get; set; }

        public string Fathername { get; set; } = string.Empty;

        public DateTime Dob { get; set; }
        public string Secret_key { get; set; }

    }
    #region ProcessWorlFlow

    public class ApplicationWorkFlowRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public int? UserTypeId { get; set; }
        public string? ActionType { get; set; }
        public string? Comments { get; set; }

        
    }
    public class ApplicationFeesRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }
        public int? UserId { get; set; }
        public int? UserTypeId { get; set; }
        public string? ActionType { get; set; }
        public decimal? FeesAmount { get; set; }


    }
   
    public class GetApplicationWorkflowRequestModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }


    }
    #endregion

    public class DashboardDetailsRequiredModel : RequestModelBase
    {
        public int? ApplicationId { get; set; }
        public int? Year { get; set; }
        public int? RoleID { get; set; }

    }
}

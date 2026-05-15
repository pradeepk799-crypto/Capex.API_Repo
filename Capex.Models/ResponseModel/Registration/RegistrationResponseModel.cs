using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Registration;

namespace Capex.Models.ResponseModel.Registration
{

    public class EmailOTPResponseModel : ResponseModelBase
    {
        public int Type { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public string OTPReferenceId { get; set; }


    }
    public class EmailLoginResponseModel : ResponseModelBase
    {
        public bool? Result { get; set; }

    }
    public class GetProfileResponseModel : ResponseModelBase
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

    public class GetProjectDetailsResponseModel : ResponseModelBase
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
    public class LandDetailsResponseModel : ResponseModelBase
    {
        public string EnApplicationId { get; set; }

        public List<LandRecordsRequestModel> LandRecordsModel { get; set; }
        public List<LandOwnerNameList> OwnerModel { get; set; }


    }
    public class PowerEvacuationResponseModel : ResponseModelBase
    {
        public int? ApplicationId { get; set; }
        public string? IsEvacuation { get; set; }
        public int? VoltageId { get; set; }
        public int? ConnectivityId { get; set; }
        public string? DiscomId { get; set; }
        public string? CircleId { get; set; }
        public string? SubstationId { get; set; }

        public int? PowerGeneratedId { get; set; }
    }
    public class GetUploadDocumentMasterResponseModel : ResponseModelBase
    {
        public int? DocId { get; set; }
        public int? FileId { get; set; }
        public int? FileUploadId { get; set; }
        public string? DocumentName { get; set; }

    }


    public class ProjectSearchResponseModel : ResponseModelBase
    {
        public int? ProjectId { get; set; }
        public string? ProjectRegistrationNumber { get; set; }
        public int? ApplicationId { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectCompany { get; set; }
        public int? ProjectTypeId { get; set; }
        public string? ProjectType { get; set; }

        public string? OtherProjectType { get; set; }
        public int ProjectCapacityId { get; set; }
        public string? SchematicLayoutDetails { get; set; }
        public int? ExpAnnualGenId { get; set; }
        public decimal? MinimumAnnualCUF { get; set; }
        public decimal? ProjectInvestment { get; set; }
        public string? TypeOfArea { get; set; }
        public int? PstateId { get; set; }
        public int? PDistrictId { get; set; }
        public int? PTehsiId { get; set; }
        public int? PVillageId { get; set; }
        public string? Address { get; set; }
        public string? ApproachRoad { get; set; }
        public string? RailwayStation { get; set; }
        public string? CoordinatesOfProject { get; set; }
        public decimal? LandCapacity { get; set; }
        public bool? IsApplicantName { get; set; }
        public bool? IsPurchased { get; set; }
        public bool? IsUnderAgreement { get; set; }
        public int? ApplicationStatus { get; set; }
        public string? ProcessStatus { get; set; }
        public string? ApplicationDate { get; set; }
        public int? CreatedBy { get; set; }
        public string? ProcessStatusName { get; set; }
        public string? ApplicationStatusName { get; set; }
        public string? ApplicationNumber { get; set; }
        public string? FeesId { get; set; }
        public decimal? Amount { get; set; }
        public string? FeesStatus { get; set; }

        public int? IsRejectedL1 { get; set; }
        public int? IsRevertedL1 { get; set; }
        public int? IsApprovedL1 { get; set; }

        public int? IsRejectedL2 { get; set; }
        public int? IsRevertedL2 { get; set; }
        public int? IsApprovedL2 { get; set; }

        public int? IsRejectedL3 { get; set; }
        public int? IsRevertedL3 { get; set; }
        public int? IsApprovedL3 { get; set; }
    }

    #region Application Preview Details
    public class ApplicationPreviewResponseModel : ResponseModelBase
    {
        public int? ApplicationId { get; set; }
        public int? ProjectId { get; set; }

        public List<ProfileDetailsJsonResponseModel> ProfileDetailsJsonResponseModel { get; set; }
        public List<LandRecordsJsonResponseModel> LandRecordsModel { get; set; }
        public List<LandOwnerNameJsonResponseModel> OwnerModel { get; set; }
        public List<ProjectDetailsJsonResponseModel> ProjectDetailsJsonResponseModel { get; set; }
        public List<PowerEvacuationJsonResponseModel> PowerEvacuationJsonResponseModel { get; set; }
        public List<DocsResponseModel> DocsResponseModel { get; set; }


    }
    public class ProfileDetailsJsonResponseModel
    {
        public int ApplicationId { get; set; }
        public string ApplicationNumber { get; set; }
        public int ApplicationTypeId { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string ApplicantName { get; set; }
        public string FatherName { get; set; }
        public string EmailId { get; set; }
        public int EntityTypeId { get; set; }
        public string OtherEntity { get; set; }
        public int BusinessEntityId { get; set; }
        public string OtherBusinessEntity { get; set; }
        public string COEName { get; set; }
        public bool IsGovernment { get; set; }
        public int RegistrationStateId { get; set; }
        public int RegistrationDistrictId { get; set; }
        public string RegistrationAddress { get; set; }
        public string NodalOfficerName { get; set; }
        public string NodalOfficerDesignation { get; set; }
        public int CorrespondenceStateId { get; set; }
        public int CorrespondenceDistrictId { get; set; }
        public string CorrespondenceAddress { get; set; }
        public int SectorId { get; set; }
        public bool IsAgency { get; set; }
        public bool IsActive { get; set; }
        public string EntityName_E { get; set; }
        public string BusinessEntity_Name_E { get; set; }
        public string RegistrationDistrict { get; set; }
        public string RegistrationState { get; set; }
        public string CorrespondenceDistrict { get; set; }
        public string CorrespondenceState { get; set; }
        public string SectorName_E { get; set; }
    }
    public class ProjectDetailsJsonResponseModel
    {
        public int ProjectId { get; set; }
        public int ApplicationId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCompany { get; set; }
        public int ProjectTypeId { get; set; }
        public string OtherProjectType { get; set; }
        public int ProjectCapacityId { get; set; }
        public string SchematicLayoutDetails { get; set; }
        public int ExpAnnualGenId { get; set; }
        public string MinimumAnnualCUF { get; set; }
        public string ProjectInvestment { get; set; }
        public bool TypeOfArea { get; set; }
        public int PstateId { get; set; }
        public int PDistrictId { get; set; }
        public int PTehsiId { get; set; }
        public int PVillageId { get; set; }
        public string ApproachRoad { get; set; }
        public string RailwayStation { get; set; }
        public string CoordinatesOfProject { get; set; }
        public string LandCapacity { get; set; }
        public string IsApplicantName { get; set; }
        public string IsPurchased { get; set; }
        public string IsUnderAgreement { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string PDistrict { get; set; }
        public string PState { get; set; }
        public string PTehsi { get; set; }
        public string Project_Capacity_Name_E { get; set; }
        public string ExpAnnual_GenerationName_E { get; set; }

        public string? EstablishmentTradeName { get; set; }
        public string? ConstitiutionTypeofEstablishment { get; set; }
        public string? EstablishmentOperationalStatus { get; set; }
        public string? RegistrationCity { get; set; }
        public string? RegistrationPincode { get; set; }
        public string? CorrespondanceCity { get; set; }
        public string? CorrespondancePincode { get; set; }

        public string? FeesId { get; set; }
        public decimal? Amount { get; set; }
        public string? FeesStatus { get; set; }

        public int? IsRejectedL1 { get; set; }
        public int? IsRevertedL1 { get; set; }
        public int? IsApprovedL1 { get; set; }

        public int? IsRejectedL2 { get; set; }
        public int? IsRevertedL2 { get; set; }
        public int? IsApprovedL2 { get; set; }

        public int? IsRejectedL3 { get; set; }
        public int? IsRevertedL3 { get; set; }
        public int? IsApprovedL3 { get; set; }
    }
    public class PowerEvacuationJsonResponseModel
    {
        public int PowerEvacuation_Id { get; set; }
        public int ProjectId { get; set; }
        public int ApplicationId { get; set; }
        public string IsEvacuation { get; set; }
        public int VoltageId { get; set; }
        public int ConnectivityId { get; set; }
        public string DiscomId { get; set; }
        public string CircleId { get; set; }
        public string SubstationId { get; set; }
        public int PowerGeneratedId { get; set; }
        public DateTime Created_Date { get; set; }
        public int Created_By { get; set; }
        public string VoltageLevel_Name_E { get; set; }
        public string Connectivity_Name_E { get; set; }
    }
    public class LandRecordsJsonResponseModel
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
        public List<LandDeailsOwnerJsonRequestModel>? LandDeailsOwnerRequestModel { get; set; }
    }
    public class LandDeailsOwnerJsonRequestModel
    {
        public int LandId { get; set; }
        public string? KhasraNo { get; set; }
        public string? KhasraId { get; set; }
        public decimal SurveyArea { get; set; }
        public decimal LagaanToPay { get; set; }
        public List<LandOwnerNameJsonResponseModel>? landownerlist { get; set; }

    }
    public class LandOwnerNameJsonResponseModel
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
    public class DocsResponseModel
    {
        public string? Document_Name { get; set; }
        public string? CreatedDate { get; set; }
        public int? UploadFileId { get; set; }
        public string? UploadedFileName { get; set; }

    }
    #endregion
    public class ApplicationWorkflowDetails
    {
        public int WorkflowId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActionType { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string DesignationName { get; set; }
    }

    public class ApplicationFeesDetails
    {
        public int WorkflowId { get; set; }
        public string Comments { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ActionType { get; set; }
        public string EmailId { get; set; }
        public string FullName { get; set; }
        public string UserType { get; set; }
        public string DesignationName { get; set; }
    }
    public class ApplicationWorkflowResponseModel
    {
        public List<ApplicationFeesDetails> ApplicationFeesDetails { get; set; }
        public List<ApplicationWorkflowDetails> ApplicationWorkflowDetails { get; set; }

    }
    public class DashboardDetailsResponseModel
    {
        public int? Draft { get; set; }
        public int? Completed { get; set; }
        public int? ApprovedL1 { get; set; }
        public int? RejectedL1 { get; set; }
        public int? RevertedL1 { get; set; }
        public int? ApprovedL2 { get; set; }
        public int? RejectedL2 { get; set; }
        public int? RevertedL2 { get; set; }
        public int? ApprovedL3 { get; set; }
        public int? RejectedL3 { get; set; }
        public int? RevertedL3 { get; set; }
        public int? TotalApproved { get; set; }
        public int? PendingApproval { get; set; }
        public int? FeesPendingCount { get; set; }
        public int? FeesDoneCount { get; set; }



    }
}

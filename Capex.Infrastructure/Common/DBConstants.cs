using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Common
{
    public class DBConstants
    {
        public struct StoredProcedures
        {

        }
        public struct MastersSP
        {
            public const string GetDemography = "USP_GetDemography";
            public const string GetCountryList = "ProcGetCountryList";
            public const string GetRole = "ProcGetAllRoles";
            public const string GetOffice = "USP_GetDemography";
            public const string GetEmployee = "ProcGetEmployee";
            public const string SearchEmployee = "ProcSearchEmployee";
            //    public const string GetCitizen = "USP_GetDemography";
            public const string GetUser = "ProcUsers";
            public const string GetAllMenu = "ProcGetAllMenu";
            public const string SaveRoleMenuMapping = "ProcInsertRoleMenuMapping";
            public const string GetRoleMenuMapping = "ProcGetRoleMenuMapping";
            public const string DeactivateRole = "ProcDeactivateRole";
            public const string GetTemplate = "GetTemplate";
            public const string GetModelValidationDetails = "ProcGetModelValidate";
            public const string GetEmployeJoining = "ProcSearchEmpByCodeOrMobileno";
            public const string GetEmpByOfficeId = "ProcGetEmpMapOfficeWise";
            public const string SetEmployeeMapping = "ProcSetOfficeEmployeeMapping";
            public const string GetAllOfficeType = "ProcGetAllOfficeType";
            public const string GetUpdateEmployeeJoinging = "ProcGetUpdateEmployeeJoinging";
            public const string GetAllMasterData = "ProcGetAllMasterData";
            public const string SaveAPILogStatus = "ProcInsertAPILogStatus";
            public const string GetApplicationDetailsForPayment = "USP_GetApplicationDetailForPayment";
            public const string InsertUpdatePaymentDetails = "USP_InsertUpdatePaymentDetails";
            public const string ProGetAllOfficeList = "ProGetAllOfficeList";
            public const string ProInsertAadhaarLog = "ProInsertAadhaarLog";
            public const string GetMultipleDemography = "GetMultipleDemographyList";
            public const string GetApplicationForwardDetails = "USP_GetApplicationForwardDetails";
            public const string GetOfficesByHead = "ProcGetOfficesByVillageAndRevenueHead";
            public const string GetMenuByTypeAndParentId = "GetMenuByTypeAndParentId";
            public const string GetCaseHearingDate = "ProcGetCaseHearingDate";
            public const string GetCaseHearingDateHistory = "ProcGetCaseHearingDateHistory";
            public const string GetOfficewiseCaseHearingDates = "ProcGetOfficewiseCaseHearingDates";
            public const string GetDistrictByState = "ProcGetDistrictByState";
            public const string GetSamagraDetails = "USP_GetSamagraDetails";
            public const string GetAppVersion = "ProcGetAppVersion";
            public const string ProcGetAllEmployeeListByName = "ProcGetAllEmployeeListByName";
            public const string Usp_GetAllJurisdictionGroupDDLList = "GetAllJurisdictionGroupDDLList";
            public const string GetApplicationList = "Proc_GetApplicationCaseList";
            public const string GetDocumentTypes = "Proc_GetDocumentTypes";
            public const string GetRevenueHeads = "Proc_GetRevenueHeads";
            public const string GetMasterForCaseList = "Proc_GetMasterForCaseList";
            public const string UpdateApplicationForwardDetails = "USP_UpdateApplicationForwardDetails";
            public const string IGRSRequests = "Proc_IGRSRequests";
            public const string GetPatwariPrativedan = "GetPatwariQuestion";
            public const string PostMessageNotification = "ProcInsertMessageNotification";
            public const string ProcGetDashboardCount = "ProcGetDashboardCount";
            public const string Proc_GetActivityMaster = "Proc_GetActivityMaster";
            public const string GetManageDocumentType = "procGetManageDocumentType";
            public const string GetPatwariPrativedans = "GetPatwariPrativedan";

        }
        public struct RCMSSP
        {
            public const string SP_ADDAPPLICATIONERRORLOG = "sp_AddApplicationErrorLog";
            public const string CheckValidUser = "ProCheckUserExists";
            public const string prochangePwd = "ProcChangeUserPassword";
            public const string proCitizenRegistration = "ProcCitizenRegistration";
            public const string ProcInsertCitizenRegistration = "ProcInsertCitizenRegistration";
            public const string SP_PostEmployee = "ProcPostEmployeeProfile";
            public const string SP_DeleteEmployee = "ProcDeleteEmployee";
            public const string SP_UpdateEmployeeProfile = "ProcUpdateEmployeeProfile";
            public const string GetCitizen = "ProcGetCitizen";
            public const string SP_UpdateCitizenProfile = "ProcUpdateCitizenProfile";
            public const string InsertSamagraDetails = "USP_InsertSamagraDetails";
            public const string ProcUpdateCitizenKYC = "ProcUpdateCitizenKYC";
            public const string ProcInsertUploadFile = "ProcInsertUploadFile";
            public const string ProcRegistrationNRICitizen = "ProcRegistrationNRICitizen";
            public const string CheckNonCitizenUser = "ProcCheckNonCitizenUser";
            public const string SaveAadharDetails = "ProcSaveAadharDetails";
            //public const string RegisterNonCitizen = "ProcRegisterNonCitizen";
            public const string RegisterNonCitizen = "ProcRegisterCitizen";


            public const string GetNonCitizenProfile = "procGetProfile";
            public const string UpdateNonCitizenProfile = "ProcUpdateNonCitizenProfile";

            //public const string GetNonCitizenProfile = "procGetProfile";
            public const string CheckUser = "procCheckUser";
            public const string UpdateCitizenProfile = "procUpdateCitizenProfile";
            public const string GetFileDetails = "ProcGetFileDetails";

            public const string GetAdvocateDetails = "procGetAdvocateDetails";
            public const string EkycDetailsUpdate = "procEkycDetailsUpdate";
        }

        public struct OfficeProfileSSP
        {
            public const string USP_GetAllOfficeProfile = "USP_GetAllOfficeProfile";
            public const string USP_GetByIdOfficeProfile = "USP_GetByIdOfficeProfile";
            public const string USP_AddOfficeProfile = "USP_AddOfficeProfile";
            public const string USP_UpdateOfficeProfile = "USP_UpdateOfficeProfile";
            public const string USP_DeleteOfficeProfile = "USP_DeleteOfficeProfile";
            public const string USP_GetDistrict = "USP_GetDistrict";
            public const string USP_GetDivision = "USP_GetDivision";
            public const string USP_GetOfficeLevel = "USP_GetOfficeLevel";
            public const string USP_GetSubDivision = "USP_GetSubDivision";
            public const string USP_GetTehsil = "USP_GetTehsil";
            public const string USP_GetDepartment = "GetDepartment";
            public const string USP_GetByProfileIdOfficeProfile = "USP_GetByProfileIdOfficeProfile";
            public const string Proc_GetOfficeListByOfficeId = "Proc_GetOfficeListByOfficeId";
            public const string ProcGetFinancialYear = "ProcGetFinancialYear";
            ///////Dashboard/////
            public const string ProcGetDashboardCountList = "Proc_GetDashboardCountList";
            public const string Procusp_GetDashboardVendorReports = "usp_GetDashboardVendorReports";
            public const string Procusp_GetDashboardDDOReportsCount = "usp_GetDashboardDDOReportCount";
            public const string ProcGetDashboardVendorDetailsReports = "usp_GetDashboardVendorDetailsReports";
            public const string ProcGetDashboardDDODetailsReports = "usp_GetDashboardDDODetailsReports";
            


        }
        public struct MutationSP
        {
            public const string SP_GetMutationType = "mutation.ProcGetMutationType";
            public const string SP_SaveApplicantDetails = "[mutation].[SP_SaveMutationApplicationDetails]";
            public const string SP_SavePatwariPrativedan = "proSavePatwariQuestion";
            public const string GetPatwariPrativedan = "GetPatwariPrativedanDetails";


        }


        public struct DesignationSSP
        {
            public const string USP_GetDesignation = "ProcDesignation";
        }

        public struct MutationSSP
        {
            public const string USP_GetDashboard = "GetDashboard";
            public const string USP_GetDashboardGrid = "GetDashboardGridByStatus";
            public const string GetMutationAapplicationDetails = "mutation.Proc_GetMutationAapplicationDetails";
            public const string AdvocateCaseMapping = "proc_AdvocateCaseMapping";
            public const string SaveCaseHearingDetails = "proc_SavCaseHearingDetails";
            public const string GetCaseHearing = "procGetCaseHearing";
            public const string GetRegistryDetails = "mutation.Proc_GetMutationAapplicationDetails";
            public const string SaveActivityDocuments = "procSaveActivityDocuments";
            public const string DeleteLandDetails = "procDeleteLandDetails";
            public const string UpdatePatwariQuestionDetails = "procUpdatePatwariQuestionDetails";



            #region NRED
            public const string ValidateEmail = "proc_ValidateEmail";
            public const string SaveEmailLogin = "proc_SaveLoginEmail";
            public const string SaveOrUpdateProfile = "SaveOrUpdateProfile";
            public const string SaveLoginEmail = "proc_SaveLoginEmail";
            public const string GetEntityMastersDetails = "proc_GetMastersDetails";
            public const string EntityDetailForInsertUpdate = "proc_InsertOrUpdateEntity";
            public const string GetCompanyProfileById = "proc_GetCompanyProfileById";
            public const string Porc_SaveOrUpdateProjectDetails = "Porc_SaveOrUpdateProjectDetails";
            public const string proc_GetProjectDetailById = "proc_GetProjectDetailById";
            public const string Proc_InsertLandDetails = "Proc_InsertLandDetails";
            public const string Proc_GetLandDetails = "Proc_GetLandDetails";
            public const string ConnectivityDetailForInsertUpdate = "proc_InsertOrUpdateConnectivity";
            public const string GetUserDetails = "proc_GetUserDetails";
            public const string UserDetailsForInsertUpdate = "proc_InsertOrUpdateUserDetails";
            public const string EntityUserMappingForInsertUpdate = "proc_InsertOrUpdateEntityUserMapping";

            public const string InsertOrUpdatePowerEvacuation = "proc_InsertOrUpdatePowerEvacuation";

            public const string GetPowerEvacuation = "proc_GetPowerEvacuation";
            public const string GetUploadDocumentMaster = "proc_GetUploadDocumentMaster";
            public const string GetProjectDetails = "proc_GetProjectDetails";
            public const string SaveOrUpdateUploadedApplicationDecoments = "Proc_SaveOrUpdateUploadedDocument";
            public const string GetApplicationPreview = "proc_GetApplicationPreview";
            public const string FinalSubmitPreview = "pro_FinalSubmit";
            public const string proc_Login = "proc_Login";

            public const string proc_GetEntityList = "proc_GetEntityList";
            public const string SearchUserDetails = "proc_SearchUserDetails";
            public const string ProcessL1Approval = "ProcessL1Approval";
            public const string ProcessL2Approval = "ProcessL2Approval";

            public const string ProcessL3Approval = "ProcessL3Approval";
            public const string SetFeeForApplication = "SetFeeForApplication";
            public const string proc_GetApplicationWorkflowDetails = "proc_GetApplicationWorkflowDetails";
            public const string DashboardCount = "proc_DashboardCount";





            #endregion


            #region Capex
            public const string Proc_SaveOrUpdateMstDDO = "Proc_SaveOrUpdateMstDDO";
            public const string GetOrUpdateDDODetails = "usp_GetOrUpdateDDODetails";
            public const string SaveBankDetails = "usp_SaveBankDetails";
            public const string GetBankDetails = "usp_GetBankDetails";
            public const string SaveOrUpdateBuildingRegistration = "usp_SaveOrUpdateBuildingRegistration";
            public const string GetBuildingDetails = "usp_GetBuildingDetails";
            public const string SaveOrUpdateVendorData = "usp_SaveOrUpdateVendorData";
            public const string GetDDOByDistrict = "usp_GetDDOByDistrict";
            public const string GetVendorData = "usp_GetVendorData";
            public const string usp_SaveUnitPrice = "usp_SaveUnitPrice";

            public const string usp_GetUnitPriceDetails = "usp_GetUnitPriceDetails";
            public const string SaveBillGeneration = "usp_SaveBillGenerationDetails";
            public const string GetBuildingDetailsByDDO = "usp_GetBuildingDetailsByDDO";

            public const string GetBillGenerationDetails = "usp_GetBillGenerationDetails";


            public const string SaveBuildingDetails = "usp_SaveBuildingDetails";
            public const string GetBuildingById = "usp_GetBuildingById";

            public const string BillGeneration_GetBuildingDetailsByVendor = "usp_BillGeneration_GetBuildingDetailsByVendor";
            public const string usp_ValidateIVRSAndMeterExist = "usp_ValidateIVRSAndMeterExist";
            public const string GetUserNameByMobile = "usp_GetUserNameByMobile";
            public const string GetDDODetailForSendSMS = "proc_GetDDODetailForSendSMS";
            public const string ForgotPassword = "proc_ForgotPassword";













            #endregion
        }

        public struct JurisdictionSSP
        {
            public const string USP_JurisdictionGroupCreate = "ProcJurisdictionGroupCreate";
            public const string USP_ProcGetAllJurisdictionList = "ProcGetAllJurisdictionList";
            public const string USP_GetJurisdictionGroupsById = "GetJurisdictionGroupsById";
            public const string USP_UpdateJurisdictionGroup = "ProcUpdateJurisdictionGroup";
            public const string USP_URemoveJurisdictionGroup = "ProcRemoveJurisdictionGroup";
        }

        public struct Application
        {
            public const string GetFlySheetData = "Proc_GetFlySheetData";
            public const string OrderSheetDetails = "Proc_OrderSheetDetails";
            public const string GetSearchcauseList = "ProcGetCaseHearingInfo";
            public const string GetCourListByDistrictIdList = "ProcGetDDLCauseList";
            public const string GetListByCourtId = "ProcGetDDLCauseList";
            public const string SearchRaiseObjection = "Usp_XXXXXXXX";

        }

        public struct PartitionSP
        {
            public const string SP_SavePartitionApplicantDetails = "[partition].[SP_SavePartitionApplicationDetails]";
            public const string GetPartitionApplicantDetails = "[partition].[Proc_GetMutationAapplicationDetails]";

        }
    }
}

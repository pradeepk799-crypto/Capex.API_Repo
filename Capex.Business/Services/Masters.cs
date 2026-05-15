using Capex.Business.Interfaces;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Interfaces;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Capex.Models.Common.APIResult;
using IMasters = Capex.Business.Interfaces.IMasters;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.Business.Services
{
    public class Masters : IMasters
    {
        private readonly IInfrastructureServices infrastructureServices;
        private readonly ILogger<Masters> _logger;
        private readonly HttpClient _httpClient;
        private readonly INotification _notification;
        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;




        public Masters(IInfrastructureServices infrastructureServices, ILogger<Masters> logger, INotification notification, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this._logger = logger;
            this.infrastructureServices = infrastructureServices;
            _httpClient = new HttpClient();
            this._notification = notification;
            this._passwordHasher = passwordHasher;
            
           
        }



        public async Task<ApiResult<DemographyResponseModel>> GetDemography(DemographyRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<DemographyResponseModel> responseModel = new ApiResult<DemographyResponseModel>();
            DemographyResponseModel data = new DemographyResponseModel();
            DemographyRequest request = new DemographyRequest();
            // Check Menu Permission and Data Permission.
            DemographyResponse response;
            #region || requestModel to request Mapping ||
            request.DemographyTypeId = requestModel.DemographyTypeId;
            request.ParentDemographyId = requestModel.ParentDemographyId;//requestModel.ParentDemographyId==0?null: requestModel.ParentDemographyId;
            if (requestModel.DemographyId > 0)
            {
                request.DemographyId = requestModel.DemographyId;
            }
            else
            {
                request.DemographyId = requestModel.ParentDemographyId == 0 ? null : requestModel.ParentDemographyId;
            }

            #endregion

            response = await this.infrastructureServices.Masters.GetDemography(request);
            if (response != null && response.DemographyList != null && response.DemographyList.Count > 0)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                data.DemographyResponse = response.DemographyList.Select(i => new DemographyModel()
                {
                    DemographyId = i.DemographyId,
                    DemographyType = i.DemographyType,
                    DemographyTypeId = i.DemographyTypeId,
                    Demography_Name_Eng = i.Demography_Name_Eng,
                    Demography_Name_Hi = i.Demography_Name_Hi,
                    LGDCode = i.LGDCode,
                    PatwariHalkaNumber = i.PatwariHalkaNumber
                }).ToList();
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, requestModel.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateDDO(DDORequestModel requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(), // Ensure ResponseData is initialized
                Status = false
            };
            ApplicationUser applicationUser = null;
            string _passwordHash = _passwordHasher.HashPassword(applicationUser, "123456");
            if (requestModel != null)
            {
                var obj = new DDODomainRequestModel
                {
                    DDOId = requestModel.DDOId,
                    DDOCode = requestModel.DDOCode,
                    DDONameEn = requestModel.DDONameEn,
                    DDOName_Hi = requestModel.DDOName_Hi,
                    NodalPersonName_En = requestModel.NodalPersonName_En,
                    ContactDetails = requestModel.ContactDetails,
                    EmailID = requestModel.EmailID,
                    DistrictId = requestModel.DistrictId,
                    Address = requestModel.Address,
                    CreatedBy = requestModel.CreatedBy,
                    DeptId = requestModel.DeptId,
                    TrsId = requestModel.TrsId,
                    Password = requestModel.IsPasswordChanged == 0 ? _passwordHash : requestModel.Password,
                    IsPasswordChanged = requestModel.IsPasswordChanged,
                };




                ApiResult<MasterDomainResponseModel> apiResult = new ApiResult<MasterDomainResponseModel>();
                apiResult = await infrastructureServices.Masters.SaveOrUpdateDDO(obj);

                if (apiResult?.ResponseData?.UserId > 0)
                {
                    responseModel.ResponseData.UserId = apiResult.ResponseData.UserId;
                    responseModel.Status = true;
                    if ((requestModel.IsPasswordChanged == 0 || requestModel.IsPasswordChanged == null))
                    {
                        this._notification.SendSMSUser(apiResult?.ResponseData, NotificationTemplateConstants.DDOREGISTRATION);

                    }
                }
            }

            return responseModel;
        }


        public async Task<ApiResult<List<DDODetailsResponseModel>>> GetDOODetails(DDODetailsRequestModel requestModel)
        {
            this._logger.LogDebug("Fetching DDO Details for DDOCode: {DDOCode}", requestModel.DDOCode);

            var responseModel = new ApiResult<List<DDODetailsResponseModel>> { ResponseData = new List<DDODetailsResponseModel>() };

            var requestModelDomain = new DDODetailsDomainRequestModel
            {
                Flag = requestModel.Flag,
                DDOId = requestModel.DDOId,
                DDOCode = requestModel.DDOCode,
                DDONameEn = requestModel.DDONameEn,
                NodalPersonName_En = requestModel.NodalPersonName_En,
                ContactDetails = requestModel.ContactDetails,
                EmailID = requestModel.EmailID,
                DistrictId = requestModel.DistrictId,
                IsActive = requestModel.IsActive,


            };

            try
            {
                var responseModelDomain = await this.infrastructureServices.Masters.GetDOODetails(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DDODetailsResponseModel
                    {
                        DDOCode = item.DDOCode,
                        DDOId = item.DDOId,
                        DDONameEn = item.DDONameEn,
                        DDOName_Hi = item.DDOName_Hi,
                        NodalPersonName_En = item.NodalPersonName_En,
                        ContactDetails = item.ContactDetails,
                        EmailID = item.EmailID,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        Address = item.Address,
                        IsActive = item.IsActive,
                        DeptId = item.DeptId,
                        DeptName = item.DeptName,
                        TrsId = item.TrsId,
                        TrsName = item.TrsName,
                        CreatedDate = item.CreatedDate,
                        IsPasswordChanged = item.IsPasswordChanged,

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DDODetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching DDO Details for DDOCode: {DDOCode}", requestModel.DDOCode);
                responseModel.ResponseData = new List<DDODetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }
        public async Task<ApiResult<List<DDODetailsResponseModel>>> GetDOOByDistrict(DistrictsRequestModel requestModel)
        {
            this._logger.LogDebug("Fetching GetDOOByDistrict for DDOCode: {DDOCode}");

            var responseModel = new ApiResult<List<DDODetailsResponseModel>> { ResponseData = new List<DDODetailsResponseModel>() };

            var requestModelDomain = new DistrictsDomainRequestModel
            {
                DistrictIds = requestModel.DistrictIds.Select(x => Convert.ToString(x).Trim()).ToList()
            };

            try
            {
                var responseModelDomain = await this.infrastructureServices.Masters.GetDOOByDistricts(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DDODetailsResponseModel
                    {
                        DDOCode = item.DDOCode,
                        DDOId = item.DDOId,
                        DDONameEn = item.DDONameEn,
                        DDOName_Hi = item.DDOName_Hi,
                        NodalPersonName_En = item.NodalPersonName_En,
                        ContactDetails = item.ContactDetails,
                        EmailID = item.EmailID,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        Address = item.Address,
                        IsActive = item.IsActive
                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DDODetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching DDO Details for DDOCode: {DDOCode}");
                responseModel.ResponseData = new List<DDODetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }


        public async Task<ApiResult<BankDetailsResponseModel>> GetBankDetailByIfsc(BankSearchRequestModel requestModel)
        {
            _logger.LogDebug("Fetching Bank Details for BankId: {BankId}", requestModel.BankId);

            ApiResult<BankDetailsResponseModel> responseModel = new ApiResult<BankDetailsResponseModel>
            {
                ResponseData = new BankDetailsResponseModel()
            };

            try
            {
                string ifscCode = requestModel.IFSCCode;
                string apiUrl = $"https://ifsc.razorpay.com/{ifscCode}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject jsonData = JObject.Parse(responseBody);

                    responseModel.ResponseData.IFSCCode = jsonData["IFSC"]?.ToString();
                    responseModel.ResponseData.BankName = jsonData["BANK"]?.ToString();
                    responseModel.ResponseData.BranchName = jsonData["BRANCH"]?.ToString();
                    responseModel.ResponseData.Centre = jsonData["CENTRE"]?.ToString();
                    responseModel.ResponseData.Address = jsonData["ADDRESS"]?.ToString();
                    responseModel.ResponseData.District = jsonData["DISTRICT"]?.ToString();
                    responseModel.ResponseData.State = jsonData["STATE"]?.ToString();
                    responseModel.ResponseData.City = jsonData["CITY"]?.ToString();
                    responseModel.ResponseData.MICR = jsonData["MICR"]?.ToString();
                    responseModel.ResponseData.BankCode = jsonData["BANKCODE"]?.ToString();
                    responseModel.ResponseData.SWIFT = jsonData["SWIFT"]?.ToString();
                    responseModel.ResponseData.Contact = jsonData["CONTACT"]?.ToString();

                    responseModel.ResponseData.RTGS = jsonData["RTGS"] != null && Convert.ToBoolean(jsonData["RTGS"]);
                    responseModel.ResponseData.IMPS = jsonData["IMPS"] != null && Convert.ToBoolean(jsonData["IMPS"]);
                    responseModel.ResponseData.UPI = jsonData["UPI"] != null && Convert.ToBoolean(jsonData["UPI"]);
                    responseModel.ResponseData.NEFT = jsonData["NEFT"] != null && Convert.ToBoolean(jsonData["NEFT"]);

                    responseModel.ErrorCode = ErrorCodes.Err00060;

                    responseModel.Status = true;
                }
                else
                {
                    _logger.LogWarning("Invalid IFSC Code or API request failed for IFSC: {IFSC}", ifscCode);
                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {BankId}", requestModel.BankId);
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }


        public async Task<ApiResult<MasterResponseModel>> SaveBankDetails(BankDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(), // Ensure ResponseData is initialized
                Status = false
            };

            if (requestModel != null)
            {
                var obj = new BankDetailsDomainRequestModel
                {
                    IFSCCode = requestModel.IFSCCode,
                    BankName = requestModel.BankName,
                    BranchName = requestModel.BranchName,
                    Centre = requestModel.Centre,
                    Address = requestModel.Address,
                    District = requestModel.District,
                    State = requestModel.State,
                    City = requestModel.City,
                    MICR = requestModel.MICR,
                    BankCode = requestModel.BankCode,
                    SWIFT = requestModel.SWIFT,
                    Contact = requestModel.Contact,
                    RTGS = requestModel.RTGS,
                    IMPS = requestModel.IMPS,
                    UPI = requestModel.UPI,
                    NEFT = requestModel.NEFT,
                    CreatedBy = requestModel.CreatedBy,
                };

                var result = await this.infrastructureServices.Masters.SaveBankDetails(obj);

                if (result?.ResponseData != null)
                {
                    responseModel.ResponseData.response = result.ResponseData.response;
                    responseModel.Status = true;
                }
            }

            return responseModel;
        }


        public async Task<ApiResult<List<BankDetailsResponseModel>>> GetBankDetails(BankSearchRequestModel requestModel)
        {
            this._logger.LogDebug("Fetching Bank Details for Bank: {BankId}", requestModel.BankId);

            var responseModel = new ApiResult<List<BankDetailsResponseModel>> { ResponseData = new List<BankDetailsResponseModel>() };

            var requestModelDomain = new BankSearchDomainRequestModel
            {
                Flag = requestModel.Flag,
                BankId = requestModel.BankId,
                IFSCCode = requestModel.IFSCCode,
                BankName = requestModel.BankName,
                BranchName = requestModel.BranchName,
                IsActive = requestModel.IsActive,

            };

            try
            {
                var responseModelDomain = await this.infrastructureServices.Masters.GetBankDetails(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new BankDetailsResponseModel
                    {
                        BankId = item.BankId,
                        IFSCCode = item.IFSCCode,
                        BankName = item.BankName,
                        BranchName = item.BranchName,
                        Centre = item.Centre,
                        Address = item.Address,
                        District = item.District,
                        State = item.State,
                        City = item.City,
                        MICR = item.MICR,
                        BankCode = item.BankCode,
                        SWIFT = item.SWIFT,
                        Contact = item.Contact,
                        RTGS = item.RTGS,
                        IMPS = item.IMPS,
                        UPI = item.UPI,
                        NEFT = item.NEFT,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        IsActive = item.IsActive,
                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<BankDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.BankId);
                responseModel.ResponseData = new List<BankDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }


        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails(BuildingRegistrationRequestModel requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(), // Ensure ResponseData is initialized
                Status = false
            };

            if (requestModel != null)
            {
                var obj = new BuildingRegistrationDomainRequestModel
                {
                    MeterSerialNo = requestModel.MeterSerialNo,
                    SiteAddress = requestModel.SiteAddress,
                    BeneficiaryName = requestModel.BeneficiaryName,
                    SanctionedLoad = requestModel.SanctionedLoad,
                    HESName = requestModel.HESName,
                    Phase = requestModel.Phase,
                    MeterMaker = requestModel.MeterMaker,
                    TariffCategory = requestModel.TariffCategory,
                    FeederName = requestModel.FeederName,
                    DTRName = requestModel.DTRName,
                    PhoneNo = requestModel.PhoneNo,
                    EmailID = requestModel.EmailID,
                    Region = requestModel.Region,
                    Circle = requestModel.Circle,
                    Division = requestModel.Division,
                    District = requestModel.District,
                    DDOId = requestModel.DDOId,
                    CircleId = requestModel.CircleId,
                    DivisionId = requestModel.DivisionId,
                    DistrictId = requestModel.DistrictId,
                    CreatedBy = requestModel.CreatedBy
                };

                var result = await this.infrastructureServices.Masters.SaveOrUpdateBuildingDetails(obj);

                if (result?.ResponseData != null)
                {
                    responseModel.ResponseData.response = result.ResponseData.response;
                    responseModel.Status = true;
                }
            }

            return responseModel;
        }
        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails(SaveBuildingRequest requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(), // Ensure ResponseData is initialized
                Status = false
            };


            if (requestModel == null) return responseModel;

            var buildingDomainRequestModel = new BuildingDomainRequestModel
            {
                Building = JsonConvert.SerializeObject(new BuildingModel
                {
                    BuildingId = requestModel.Building.BuildingId,
                    Name = requestModel.Building.Name,
                    Email = requestModel.Building.Email,
                    Mobile = requestModel.Building.Mobile,
                    SanctionedLoad = requestModel.Building.SanctionedLoad,
                    Consumer_No = requestModel.Building.Consumer_No,
                    Address = requestModel.Building.Address,
                    Zone = requestModel.Building.Zone,
                    Region = requestModel.Building.Region,
                    Circle = requestModel.Building.Circle,
                    Division = requestModel.Building.Division,
                    District = requestModel.Building.District,
                    Installation = requestModel.Building.Installation,
                }),

                BuildingMapping = JsonConvert.SerializeObject(
         requestModel.BuildingMapping.Select(m => new BuildingMappingModel
         {
             MappingId = m.MappingId,
             BuildingId = 0,
             DDOId = m.DDOId,
             DepartmentId = m.DepartmentId,
             DistrictId = m.DistrictId
         }).ToList()
     ),

                GenerationMeter = JsonConvert.SerializeObject(
         requestModel.GenerationMeter.Select(m => new GenerationMeterModel
         {
             MeterId = m.MeterId,
             BuildingId = 0,
             MeterSerialNo = m.MeterSerialNo,
             NameOfConsumer = m.NameOfConsumer,
             PhoneNo = m.PhoneNo,
             Address = m.Address,
             Manufacturer = m.Manufacturer,
             HESName = m.HESName
         }).ToList()
     ),

                OtherBuildingDetails = JsonConvert.SerializeObject(new OtherBuildingDetailsModel
                {
                    DetailId = requestModel.OtherBuildingDetails.DetailId,
                    BuildingId = 0,
                    Latitude = requestModel.OtherBuildingDetails.Latitude,
                    Longitude = requestModel.OtherBuildingDetails.Longitude,
                    ExistingRoofSpaceAvailable_SQFT = requestModel.OtherBuildingDetails.ExistingRoofSpaceAvailable_SQFT,
                    AvailableCapacity_KW = requestModel.OtherBuildingDetails.AvailableCapacity_KW,
                    ProposedCapacity_KW = requestModel.OtherBuildingDetails.ProposedCapacity_KW,
                    CombinedCapacity = requestModel.OtherBuildingDetails.CombinedCapacity,
                    CommissionedDate = requestModel.OtherBuildingDetails.CommissionedDate,
                })
            };



            ApiResult<SaveDataDomainResponseModel> apiResult = new ApiResult<SaveDataDomainResponseModel>();
            apiResult = await infrastructureServices.Masters.SaveOrUpdateBuildingDetails(buildingDomainRequestModel);

            if (apiResult?.ResponseData.IsSuccess == true)
            {
                responseModel.ResponseData.response = apiResult.ResponseData.IsSuccess;
                responseModel.Status = true;

                this._notification.SendSMSUser(apiResult?.ResponseData, NotificationTemplateConstants.DDOBUILDINGMAPPING);


            }

            return responseModel;
        }


        public async Task<ApiResult<List<BuildingRegistrationResponseModel>>> GetBuildingDetails(BuildingDetailsSearchRequestModel requestModel)
        {
            this._logger.LogDebug("Fetching Building Details for BuildingNumber: {BuildingNumber}", requestModel.BuildingNumber);

            var responseModel = new ApiResult<List<BuildingRegistrationResponseModel>> { ResponseData = new List<BuildingRegistrationResponseModel>() };

            var requestModelDomain = new BuildingDetailsSearchDomainRequestModel
            {
                Flag = requestModel.Flag,
                BuildingNumber = requestModel.BuildingNumber,
                BuildingId = requestModel.BuildingId,
                MeterSerialNo = requestModel.MeterSerialNo,
                BeneficiaryName = requestModel.BeneficiaryName,
                HESName = requestModel.HESName,
                EmailID = requestModel.EmailID,
                DDOId = requestModel.DDOId,
                CircleId = requestModel.CircleId,
                DivisionId = requestModel.DivisionId,
                DistrictId = requestModel.DistrictId,
                IsActive = requestModel.IsActive

            };

            try
            {
                var responseModelDomain = await this.infrastructureServices.Masters.GetBuildingDetails(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new BuildingRegistrationResponseModel
                    {
                        BuildingId = item.BuildingId,
                        BuildingIdNumber = item.BuildingIdNumber,
                        MeterSerialNo = item.MeterSerialNo,
                        SiteAddress = item.SiteAddress,
                        BeneficiaryName = item.BeneficiaryName,
                        SanctionedLoad = item.SanctionedLoad,
                        HESName = item.HESName,
                        Phase = item.Phase,
                        MeterMaker = item.MeterMaker,
                        TariffCategory = item.TariffCategory,
                        FeederName = item.FeederName,
                        DTRName = item.DTRName,
                        PhoneNo = item.PhoneNo,
                        EmailID = item.EmailID,
                        Region = item.Region,
                        Circle = item.Circle,
                        Division = item.Division,
                        District = item.District,
                        DDOId = item.DDOId,
                        CircleId = item.CircleId,
                        DivisionId = item.DivisionId,
                        DistrictId = item.DistrictId,
                        CreatedBy = item.CreatedBy,
                        CreatedDate = item.CreatedDate,
                        IsActive = item.IsActive,
                        PhaseName = item.PhaseName,
                        DDONameEn = item.DDONameEn,
                        DeptName = item.DeptName,
                        ProposedCapacity_KW = item.ProposedCapacity_KW,
                    }).ToList();


                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<BuildingRegistrationResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Building Details for BuildingNumber: {BuildingNumber}", requestModel.BuildingNumber);
                responseModel.ResponseData = new List<BuildingRegistrationResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }

        public async Task<ApiResult<GetBuildingResponse>> GetBuildingById(BuildingDetailsSearchRequestModel requestModel)
        {
            var responseModel = new ApiResult<GetBuildingResponse>
            {
                ResponseData = new GetBuildingResponse(),
                Status = false
            };

            if (requestModel == null) return responseModel;
            BuildingDetailsSearchDomainRequestModel vendorSearchDomainRequestModel = new BuildingDetailsSearchDomainRequestModel();
            vendorSearchDomainRequestModel.BuildingId = requestModel.BuildingId;




            var response = await infrastructureServices.Masters.GetBuildingById(vendorSearchDomainRequestModel);


            GetBuildingResponse vendorDataListResponseModel = new GetBuildingResponse();

            if (response != null)
            {
                // Check if response.LandJson is not null before deserializing
                List<BuildingModelJson> buildingModelJson = response.Building != null
                    ? JsonConvert.DeserializeObject<List<BuildingModelJson>>(response.Building)
                    : new List<BuildingModelJson>();


                List<BuildingMappingJsonModel> buildingMappingJsonModel = response.BuildingMapping != null
                   ? JsonConvert.DeserializeObject<List<BuildingMappingJsonModel>>(response.BuildingMapping)
                   : new List<BuildingMappingJsonModel>();


                List<GenerationMeterModelJson> generationMeter = response.GenerationMeter != null
                               ? JsonConvert.DeserializeObject<List<GenerationMeterModelJson>>(response.GenerationMeter)
                               : new List<GenerationMeterModelJson>();



                List<OtherBuildingDetailsModelJson> otherBuildingDetails = response.OtherBuildingDetails != null
                               ? JsonConvert.DeserializeObject<List<OtherBuildingDetailsModelJson>>(response.OtherBuildingDetails)
                               : new List<OtherBuildingDetailsModelJson>();



                vendorDataListResponseModel.Building = buildingModelJson;
                vendorDataListResponseModel.BuildingMapping = buildingMappingJsonModel;
                vendorDataListResponseModel.GenerationMeter = generationMeter;
                vendorDataListResponseModel.OtherBuildingDetails = otherBuildingDetails;

                //vendorDataListResponseModel.VendorDistricts = vendorDistrict;
                //vendorDataListResponseModel.VendorDDOs = vendorDDO;

                responseModel.ResponseData = vendorDataListResponseModel;
                responseModel.Status = true;
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Status = false;
            }
            return responseModel;
        }


        public async Task<ApiResult<MasterResponseModel>> SaveOrUpdateVendorData(VendorDataRequestModel requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(),
                Status = false
            };
            ApplicationUser applicationUser = null;
            string _passwordHash = _passwordHasher.HashPassword(applicationUser, "123456");
            if (requestModel == null) return responseModel;

            var vendorDataDomainRequestModel = new VendorDataDomainRequestModel
            {
                Vendors = JsonConvert.SerializeObject(requestModel.Vendors.Select(v => new Vendor
                {
                    VendorId = v.VendorId,
                    PANNumber = v.PANNumber,
                    Name = v.Name,
                    FatherName = v.FatherName,
                    DOB = v.DOB,
                    Email = v.Email,
                    ContactDetails = v.ContactDetails,
                    CreatedBy = v.CreatedBy,
                    Address = v.Address,
                    district = v.district,
                    Password = _passwordHash
                     ,
                }).ToList()),

                VendorNodalPersons = JsonConvert.SerializeObject(requestModel.VendorNodalPersons.Select(np => new VendorNodalPerson
                {
                    Name = np.Name,
                    Designation = np.Designation,
                    ContactDetails = np.ContactDetails,
                    Email = np.Email
                }).ToList()),

                VendorAccounts = JsonConvert.SerializeObject(requestModel.VendorAccounts.Select(ac => new VendorAccount
                {
                    BankId = ac.BankId,
                    IfscCode = ac.IfscCode,
                    AccountNo = ac.AccountNo,
                }).ToList()),


                UID = requestModel.UserId
            };

            ApiResult<SaveDataDomainResponseModel> apiResult = new ApiResult<SaveDataDomainResponseModel>();
            apiResult = await infrastructureServices.Masters.SaveOrUpdateVendorData(vendorDataDomainRequestModel);
            // apiResult.ResponseData.PasswordHash = EncDnc.Decryption1(apiResult.ResponseData.PasswordHash);
            if (apiResult?.ResponseData.IsSuccess == true)
            {
                responseModel.ResponseData.response = apiResult.ResponseData.IsSuccess;
                responseModel.Status = true;
                int? vendorId = requestModel.Vendors
   .Select(v => v.VendorId)
   .FirstOrDefault();
                if (vendorId == null)
                {
                    this._notification.SendSMSUser(apiResult?.ResponseData, NotificationTemplateConstants.VENDORREGISTRATION);

                }
            }

            return responseModel;
        }

        public async Task<ApiResult<VendorDataListResponseModel>> GetVendorData(VendorSearchDRequestModel requestModel)
        {
            var responseModel = new ApiResult<VendorDataListResponseModel>
            {
                ResponseData = new VendorDataListResponseModel(),
                Status = false
            };

            if (requestModel == null) return responseModel;
            VendorSearchDomainRequestModel vendorSearchDomainRequestModel = new VendorSearchDomainRequestModel();
            vendorSearchDomainRequestModel.VendorId = requestModel.VendorId;
            vendorSearchDomainRequestModel.PAN = requestModel.PAN;
            vendorSearchDomainRequestModel.EmailId = requestModel.EmailId;
            vendorSearchDomainRequestModel.VendorName = requestModel.VendorName;



            var response = await infrastructureServices.Masters.GetVendorData(vendorSearchDomainRequestModel);


            VendorDataListResponseModel vendorDataListResponseModel = new VendorDataListResponseModel();

            if (response != null)
            {
                // Check if response.LandJson is not null before deserializing
                List<VendorD> vendors = response.Vendors != null
                    ? JsonConvert.DeserializeObject<List<VendorD>>(response.Vendors)
                    : new List<VendorD>();


                List<VendorNodalPersonD> vendorNodalPerson = response.VendorNodalPersons != null
                   ? JsonConvert.DeserializeObject<List<VendorNodalPersonD>>(response.VendorNodalPersons)
                   : new List<VendorNodalPersonD>();


                List<VendorAccountD> vendorAccount = response.VendorAccounts != null
                               ? JsonConvert.DeserializeObject<List<VendorAccountD>>(response.VendorAccounts)
                               : new List<VendorAccountD>();



                //List<VendorDistrictD> vendorDistrict = response.VendorDistricts != null
                //               ? JsonConvert.DeserializeObject<List<VendorDistrictD>>(response.VendorDistricts)
                //               : new List<VendorDistrictD>();



                //List<VendorDDOD> vendorDDO = response.VendorDDOs != null
                //               ? JsonConvert.DeserializeObject<List<VendorDDOD>>(response.VendorDDOs)
                //               : new List<VendorDDOD>();

                vendorDataListResponseModel.Vendors = vendors;
                vendorDataListResponseModel.VendorNodalPersons = vendorNodalPerson;
                vendorDataListResponseModel.VendorAccounts = vendorAccount;
                //vendorDataListResponseModel.VendorDistricts = vendorDistrict;
                //vendorDataListResponseModel.VendorDDOs = vendorDDO;

                responseModel.ResponseData = vendorDataListResponseModel;
                responseModel.Status = true;
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Status = false;
            }
            return responseModel;
        }
        public async Task<ApiResult<MasterResponseModel>> SaveUnitPriceDetails(UnitPriceRequestModel requestModel)
        {
            var responseModel = new ApiResult<MasterResponseModel>
            {
                ResponseData = new MasterResponseModel(),
                Status = false
            };

            if (requestModel != null)
            {
                var obj = new UnitPriceDomainRequestModel
                {
                    Price = requestModel.Price,
                    PriceId = requestModel.PriceId,
                    DistrictId = requestModel.DistrictId,
                    UnitId = requestModel.UnitId,
                    VendorId = requestModel.VendorId,
                    UID = requestModel.UserId,
                    IsActive = requestModel.IsActive,

                };
                ApiResult<SaveDataDomainResponseModel> apiResult = new ApiResult<SaveDataDomainResponseModel>();
                apiResult = await infrastructureServices.Masters.SaveUnitPriceDetails(obj);

                if (apiResult?.ResponseData.IsSuccess == true)
                {
                    responseModel.ResponseData.response = apiResult.ResponseData.IsSuccess;
                    responseModel.Status = true;

                    this._notification.SendSMSUser(apiResult?.ResponseData, NotificationTemplateConstants.VENDORMAPPING);


                }
            }

            return responseModel;
        }


        public async Task<ApiResult<List<UnitPriceResponseModel>>> GetUnitPriceDetails(UnitPriceRequestModel requestModel)
        {
            this._logger.LogDebug("Fetching GetUnitPriceDetails: {priceid}", requestModel.PriceId);

            var responseModel = new ApiResult<List<UnitPriceResponseModel>> { ResponseData = new List<UnitPriceResponseModel>() };

            var requestModelDomain = new UnitPriceDomainRequestModel();

            if (requestModel.Flag == "GET")
            {

                requestModelDomain.PriceId = requestModel.PriceId;
                requestModelDomain.VendorId = requestModel.VendorId;
                requestModelDomain.DistrictId = requestModel.DistrictId;
                requestModelDomain.Price = requestModel.Price;
                requestModelDomain.UnitId = requestModel.UnitId;
                requestModelDomain.Flag = requestModel.Flag;
                requestModelDomain.IsActive = requestModel.IsActive;


            }
            if (requestModel.Flag == "UPDATE")
            {

                requestModelDomain.PriceId = requestModel.PriceId;
                requestModelDomain.Flag = requestModel.Flag;
                requestModelDomain.IsActive = requestModel.IsActive;


            }
            try
            {
                var responseModelDomain = await this.infrastructureServices.Masters.GetUnitPriceDetails(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new UnitPriceResponseModel
                    {
                        PriceId = item.PriceId,
                        Price = item.Price,
                        VendorId = item.VendorId,
                        DistrictId = item.DistrictId,
                        DistrictName = item.DistrictName,
                        CreatedDate = item.CreatedDate,
                        Unit = item.Unit,
                        UnitId = item.UnitId,
                        VendorName = item.VendorName,
                        UpdatedDate = item.UpdatedDate,
                        IsActive = item.IsActive,


                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<UnitPriceResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.PriceId);
                responseModel.ResponseData = new List<UnitPriceResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }

        public async Task<ApiResult<ValidateIVRSAndMeterExistResponseModel>> ValidateIVRSAndMeterExist(ValidateIVRSAndMeterExistRequestModel requestModel)
        {
            var responseModel = new ApiResult<ValidateIVRSAndMeterExistResponseModel>
            {
                ResponseData = new ValidateIVRSAndMeterExistResponseModel(),
                Status = false
            };

            if (requestModel != null)
            {
                var obj = new ValidateIVRSAndMeterExistDomainRequestModel
                {
                    Flag = requestModel.Flag,
                    ConsumerNo = requestModel.ConsumerNo,
                    MeterSerialNo = requestModel.MeterSerialNo,


                };

                var result = await this.infrastructureServices.Masters.ValidateIVRSAndMeterExist(obj);

                if (result?.ResponseData != null)
                {
                    responseModel.ResponseData.IsMeterSerialExists = result.ResponseData.IsMeterSerialExists;
                    responseModel.ResponseData.IsConsumerExists = result.ResponseData.IsConsumerExists;

                    responseModel.Status = true;
                }
            }

            return responseModel;
        }
    }

}


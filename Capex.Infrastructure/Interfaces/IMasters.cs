using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Models.Common;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using static Capex.Models.Common.APIResult;

//using RoleMenuMappingResponseModel = Capex.DomainModels.DomainResponseModel.Masters.RoleMenuMappingResponseModel;

namespace Capex.Infrastructure.Interfaces
{
    public interface IMasters
    {
        public Task<string> GetStates();
        public Task<DemographyResponse> GetDemography(DemographyRequest request);

        //public Task<ApiResult<SaveDataDomainResponseModel>> SaveOrUpdateDDO(DDODomainRequestModel requestModel);

        Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveOrUpdateDDO(DDODomainRequestModel requestModel);

        public Task<List<DDODetailsDomainResponseModel>> GetDOODetails(DDODetailsDomainRequestModel requestModel);

        public Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveBankDetails(BankDetailsDomainRequestModel requestModel);

        public Task<List<BankDetailsDomainResponseModel>> GetBankDetails(BankSearchDomainRequestModel requestModel);

        Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveOrUpdateBuildingDetails(BuildingRegistrationDomainRequestModel requestModel);
        Task<ApiResult<SaveDataDomainResponseModel>> SaveOrUpdateBuildingDetails(BuildingDomainRequestModel requestModel);

        Task<GetBuildingResponseModel> GetBuildingById(BuildingDetailsSearchDomainRequestModel requestModel);
        Task<List<BuildingRegistrationDomainResponseModel>> GetBuildingDetails(BuildingDetailsSearchDomainRequestModel requestModel);
        Task<ApiResult<ValidateIVRSAndMeterExistDomainResponseModel>> ValidateIVRSAndMeterExist(ValidateIVRSAndMeterExistDomainRequestModel requestModel);
        Task<ApiResult<SaveDataDomainResponseModel>> SaveOrUpdateVendorData(VendorDataDomainRequestModel requestModel);
        Task<List<DDODetailsDomainResponseModel>> GetDOOByDistricts(DistrictsDomainRequestModel requestModel);

        Task<VendorDataDomainResponseModel> GetVendorData(VendorSearchDomainRequestModel requestModel);
        Task<APIResult.ApiResult<SaveDataDomainResponseModel>> SaveUnitPriceDetails(UnitPriceDomainRequestModel requestModel);

        Task<List<UnitPriceDomainResponseModel>> GetUnitPriceDetails(UnitPriceDomainRequestModel requestModel);

        Task<APIResult.ApiResult<SaveDataDomainResponseModel>> GetDDODetailForSendSMS(string userId);

    }
}

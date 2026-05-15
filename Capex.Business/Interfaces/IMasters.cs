using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Models.Common;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface IMasters
    {
        public Task<ApiResult<DemographyResponseModel>> GetDemography(DemographyRequestModel requestModel);

        public Task<ApiResult<MasterResponseModel>> SaveOrUpdateDDO(DDORequestModel requestModel);

        public Task<ApiResult<List<DDODetailsResponseModel>>> GetDOODetails(DDODetailsRequestModel requestModel);

        public Task<ApiResult<BankDetailsResponseModel>> GetBankDetailByIfsc(BankSearchRequestModel requestModel);
        public Task<ApiResult<MasterResponseModel>> SaveBankDetails(BankDetailsRequestModel requestModel);

        public Task<ApiResult<List<BankDetailsResponseModel>>> GetBankDetails(BankSearchRequestModel requestModel);
        public Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails(BuildingRegistrationRequestModel requestModel);


        Task<ApiResult<MasterResponseModel>> SaveOrUpdateBuildingDetails(SaveBuildingRequest requestModel);

        Task<ApiResult<ValidateIVRSAndMeterExistResponseModel>> ValidateIVRSAndMeterExist(ValidateIVRSAndMeterExistRequestModel requestModel);

        public Task<ApiResult<List<BuildingRegistrationResponseModel>>> GetBuildingDetails(BuildingDetailsSearchRequestModel requestModel);
        Task<ApiResult<GetBuildingResponse>> GetBuildingById(BuildingDetailsSearchRequestModel requestModel);


        public Task<ApiResult<MasterResponseModel>> SaveOrUpdateVendorData(VendorDataRequestModel requestModel);

        public Task<ApiResult<List<DDODetailsResponseModel>>> GetDOOByDistrict(DistrictsRequestModel requestModel);

        public Task<ApiResult<VendorDataListResponseModel>> GetVendorData(VendorSearchDRequestModel requestModel);

        public Task<ApiResult<MasterResponseModel>> SaveUnitPriceDetails(UnitPriceRequestModel requestModel);

        public Task<ApiResult<List<UnitPriceResponseModel>>> GetUnitPriceDetails(UnitPriceRequestModel requestModel);


    }
}

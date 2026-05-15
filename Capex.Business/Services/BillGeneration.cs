using Capex.Business.Interfaces;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Interfaces;
using Capex.Infrastructure.Services;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.Masters;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Services
{
    public class BillGeneration : IBillGeneration
    {
        private readonly ILogger<Common> _logger;
        private readonly AppSettings appSettings;
        private readonly INotification _notification;
        private readonly Capex.Infrastructure.Interfaces.ICommon _common;
        private readonly IInfrastructureServices infrastructureServices;
        private readonly IBillGenerationInfra _IBillGenerationInfra;

        public BillGeneration(ILogger<Common> logger, INotification notification, IInfrastructureServices infrastructureServices, IOptions<AppSettings> appSettings, IBillGenerationInfra iBillGenerationInfra)
        {
            this._logger = logger;
            this._notification = notification;

            this.infrastructureServices = infrastructureServices;
            this.appSettings = appSettings.Value;
            _IBillGenerationInfra = iBillGenerationInfra;
        }
        public async Task<ApiResult<BillGenerationResponseModel>> SaveBillGeneration(BillGenerationBuildingDetailsByVendorRequestModel requestModel)
        {
            var responseModel = new ApiResult<BillGenerationResponseModel>
            {
                ResponseData = new BillGenerationResponseModel(),
                Status = false
            };
            if (requestModel != null)
            {
                var obj = new BillGenerationBuildingDetailsByVendorDomainRequest
                {

                    billGenerationBuildingDetails = JsonConvert.SerializeObject(requestModel.billGenerationBuildingDetails.Select(v => new BillGenerationBuildingDetailsByVendorList
                    {
                        BuildingId = v.BuildingId,
                        MeterSerialNo = v.MeterSerialNo,
                        BuildingName = v.BuildingName,
                        Price = v.Price,
                        MeterId = v.MeterId,
                        BillGenerationId = v.BillGenerationId,
                        DistrictId = v.DistrictId,
                        DDO = v.DDO,
                        Building = v.Building,
                        StartReadingDate = v.StartReadingDate,
                        EndReadingDate = v.EndReadingDate,
                        StartMeterReading_kWh_X = v.StartMeterReading_kWh_X,
                        EndMeterReading_kWh_Y = v.EndMeterReading_kWh_Y,
                        TotalNetGeneration_kWh = v.TotalNetGeneration_kWh,
                        TotalSolarUnitGeneration_kWh = v.TotalSolarUnitGeneration_kWh,


                    }).ToList()),


                };
                obj.UID = requestModel.UserId;


                ApiResult<SaveDataDomainResponseModel> apiResult = new ApiResult<SaveDataDomainResponseModel>();
                apiResult = await this._IBillGenerationInfra.SaveBillGeneration(obj);

                if (apiResult?.ResponseData.IsSuccess == true)
                {
                    responseModel.ResponseData.Result = apiResult.ResponseData.IsSuccess;
                    responseModel.Status = true;

                    this._notification.SendSMSUser(apiResult?.ResponseData, NotificationTemplateConstants.BILLGENERATION);


                }


                //if (result?.ResponseData != null)
                //{
                //    responseModel.ResponseData.Result = result.ResponseData.Result;
                //    responseModel.ResponseData.Message = result.ResponseData.Message;
                //    responseModel.Status = true;
                //}
            }
            return responseModel;
        }


        public async Task<ApiResult<GeBillGenerationResponseModel>> GetBillGenerationData(GetBillDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<GeBillGenerationResponseModel>
            {
                ResponseData = new GeBillGenerationResponseModel(),
                Status = false
            };

            if (requestModel == null) return responseModel;
            GetBillDetailsDomainRequestModel vendorSearchDomainRequestModel = new GetBillDetailsDomainRequestModel();
            vendorSearchDomainRequestModel.UID = requestModel.UserId;
            vendorSearchDomainRequestModel.MeterNo = requestModel.MeterNo;
            vendorSearchDomainRequestModel.BuildingName = requestModel.BuildingName;
            vendorSearchDomainRequestModel.StartReadingDate = requestModel.StartReadingDate?.ToString("yyyy-MM-dd HH:mm:ss");
            vendorSearchDomainRequestModel.EndReadingDate = requestModel.EndReadingDate?.ToString("yyyy-MM-dd HH:mm:ss");



            var response = await _IBillGenerationInfra.GetBillGenerationData(vendorSearchDomainRequestModel);


            GeBillGenerationResponseModel vendorDataListResponseModel = new GeBillGenerationResponseModel();

            if (response != null)
            {
                // Check if response.LandJson is not null before deserializing
                List<GetBillGenerationDetailsResponseModel> billDetailsJson = response.BillDetails != null
                    ? JsonConvert.DeserializeObject<List<GetBillGenerationDetailsResponseModel>>(response.BillDetails)
                    : new List<GetBillGenerationDetailsResponseModel>();



                vendorDataListResponseModel.billGenerationDetailsResponseModels = billDetailsJson;

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
        public async Task<ApiResult<BillGenerationBuildingDetailsByVendorResponseModel>> BuildingDetailsByDDO(BuildingDetailsByDDORequestModel requestModel)
        {
            this._logger.LogDebug("Fetching BuildingDetailsByDDO: {userid}", requestModel.UserId);

            var responseModel = new ApiResult<BillGenerationBuildingDetailsByVendorResponseModel>
            {
                ResponseData = new BillGenerationBuildingDetailsByVendorResponseModel(),
                Status = false
            };

            if (requestModel == null) return responseModel;
            BuildingDetailsByDDODomainRequestModel vendorSearchDomainRequestModel = new BuildingDetailsByDDODomainRequestModel();
            vendorSearchDomainRequestModel.UID = requestModel.UserId;
            vendorSearchDomainRequestModel.BuildingId = requestModel.BuildingId;
            vendorSearchDomainRequestModel.Year = requestModel.Year;
            vendorSearchDomainRequestModel.Month = requestModel.Month;



            var response = await _IBillGenerationInfra.BuildingDetailsByDDO(vendorSearchDomainRequestModel);


            BillGenerationBuildingDetailsByVendorResponseModel vendorDataListResponseModel = new BillGenerationBuildingDetailsByVendorResponseModel();

            if (response != null)
            {
                // Check if response.LandJson is not null before deserializing
                List<BillGenerationBuildingDetailsByVendor> billDetailsJson = response.BuildingDetails != null
                    ? JsonConvert.DeserializeObject<List<BillGenerationBuildingDetailsByVendor>>(response.BuildingDetails)
                    : new List<BillGenerationBuildingDetailsByVendor>();



                vendorDataListResponseModel.buildingDetailsResponse = billDetailsJson;
                vendorDataListResponseModel.IsBillAlreadyGenerated = response.IsBillAlreadyGenerated;
                vendorDataListResponseModel.IsCombinedDateInvalid = response.IsCombinedDateInvalid;
                vendorDataListResponseModel.IsPreviousBillAlreadyGenerated = response.IsPreviousBillAlreadyGenerated;



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

    }
}
using DinkToPdf;
using HandlebarsDotNet;
using HandlebarsDotNet.Extension.NewtonsoftJson;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel.Document;
using Capex.Models.ResponseModel.Document;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Models.ResponseModel.Dashboard;
using Capex.Models.RequestModel.Dashboard;
using Capex.DomainModels.DomainRequestModel.Dashboard;
using IDashboard = Capex.Business.Interfaces.IDashboard;
using StackExchange.Redis;

namespace Capex.Business.Services
{
    public class Dashboard : IDashboard
    {
        private readonly ILogger<Dashboard> _logger;
        private readonly IMessageNotification messageNotification;
        private readonly IInfrastructureServices infrastructureServices;

        // Constructor to inject dependencies
        public Dashboard(ILogger<Dashboard> logger, IMessageNotification messageNotification, IInfrastructureServices infrastructureServices)
        {
            this._logger = logger;
            this.messageNotification = messageNotification;
            this.infrastructureServices = infrastructureServices;
        }

        public async Task<ApiResult<List<DashboardResponseModel>>> GetDashboardCountList(DashboardRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardResponseModel>> { ResponseData = new List<DashboardResponseModel>() };

            var requestModelDomain = new DashboardDomainRequestModel();
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardCountList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardResponseModel
                    {

                        RoleID = item.DRoleID,
                        TotalBuildingRegistered = item.DTotalBuildingRegistered,
                        TotalVendorRegistered = item.DTotalVendorRegistered,
                        TotalNumberOfDDO = item.DTotalNumberOfDDO,
                        TotalPendingPayment = item.DTotalPendingPayment,
                        totalDistrict=item.totalDistrict,
                        totalDDOs=item.totalDDOs,
                        totalBuilding = item.totalBuilding,
                        totalMeter = item.totalMeter

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }

        public async Task<ApiResult<List<DashboardVenderDistrictDetailsResponseModel>>> GetDashboardVenderDistrictList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardVenderDistrictDetailsResponseModel>> { ResponseData = new List<DashboardVenderDistrictDetailsResponseModel>() };

            var requestModelDomain = new DashboardVenderDistrictDetailsDomainRequestModel();
            requestModelDomain.Flag = requestModel.Flag;
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardVenderDistrictList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardVenderDistrictDetailsResponseModel
                    {

                        RoleID = item.RoleID,
                        DistrictName = item.DistrictName,
                        DistrictCode = item.DistrictCode,
                        Price = item.Price,

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardVenderDistrictDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardVenderDistrictDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }
        public async Task<ApiResult<List<DashboardVenderDdoDetailsResponseModel>>> GetDashboardVenderDdoList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardVenderDdoDetailsResponseModel>> { ResponseData = new List<DashboardVenderDdoDetailsResponseModel>() };

            var requestModelDomain = new DashboardVenderDistrictDetailsDomainRequestModel();
            requestModelDomain.Flag = requestModel.Flag;
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardVenderDdoList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardVenderDdoDetailsResponseModel
                    {

                        RoleID = item.RoleID,
                        DDOCode = item.DDOCode,
                        DDONameEn = item.DDONameEn,
                        EmailID = item.EmailID,
                        NodalPersonName_En = item.NodalPersonName_En,
                        ContactDetails = item.ContactDetails,
                      


                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardVenderDdoDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardVenderDdoDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }
        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardVenderBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> { ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>() };

            var requestModelDomain = new DashboardVenderDistrictDetailsDomainRequestModel();
            requestModelDomain.Flag = requestModel.Flag;
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardVenderBuildingList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardVenderBuildingDetailsResponseModel
                    {

                        RoleID = item.RoleID,
                        BuildingId = item.BuildingId,
                        BuildingIdNumber = item.BuildingIdNumber,
                        MeterSerialNo = item.MeterSerialNo,
                        SiteAddress = item.SiteAddress,
                        BeneficiaryName = item.BeneficiaryName,
                        SanctionedLoad = item.SanctionedLoad,
                        HESName = item.HESName,
                        District=item.District,
                        DDOName = item.DDOName,

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }

        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOBuildingList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> { ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>() };

            var requestModelDomain = new DashboardVenderDistrictDetailsDomainRequestModel();
            requestModelDomain.Flag = requestModel.Flag;
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardDDOBuildingList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardVenderBuildingDetailsResponseModel
                    {

                        RoleID = item.RoleID,
                        BuildingId = item.BuildingId,
                        BuildingIdNumber = item.BuildingIdNumber,
                        MeterSerialNo = item.MeterSerialNo,
                        SiteAddress = item.SiteAddress,
                        BeneficiaryName = item.BeneficiaryName,
                        SanctionedLoad = item.SanctionedLoad,
                        HESName = item.HESName,
                        District = item.District,
                        DDOName = item.DDOName,

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }
    
        public async Task<ApiResult<List<DashboardVenderBuildingDetailsResponseModel>>> GetDashboardDDOMeterList(DashboardVenderDistrictDetailsRequestModel requestModel)
        {
            var responseModel = new ApiResult<List<DashboardVenderBuildingDetailsResponseModel>> { ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>() };

            var requestModelDomain = new DashboardVenderDistrictDetailsDomainRequestModel();
            requestModelDomain.Flag = requestModel.Flag;
            requestModelDomain.UserID = requestModel.UserID;
            requestModelDomain.RoleID = requestModel.RoleID;
            try
            {
                var responseModelDomain = await this.infrastructureServices.Dashboard.GetDashboardDDOMeterList(requestModelDomain);

                if (responseModelDomain != null && responseModelDomain.Any())
                {
                    responseModel.ResponseData = responseModelDomain.Select(item => new DashboardVenderBuildingDetailsResponseModel
                    {

                        RoleID = item.RoleID,
                        BuildingId = item.BuildingId,
                        BuildingIdNumber = item.BuildingIdNumber,
                        MeterSerialNo = item.MeterSerialNo,
                        SiteAddress = item.SiteAddress,
                        BeneficiaryName = item.BeneficiaryName,
                        SanctionedLoad = item.SanctionedLoad,
                        HESName = item.HESName,
                        District = item.District,
                        DDOName = item.DDOName,

                    }).ToList();

                    responseModel.ErrorCode = ErrorCodes.Err00060;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00060, "");
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Ensures ResponseData is always a list
                    responseModel.ErrorCode = ErrorCodes.Err00030;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00030, "");
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred while fetching Bank Details for BankId: {DDOCode}", requestModel.RoleID);
                responseModel.ResponseData = new List<DashboardVenderBuildingDetailsResponseModel>(); // Prevents null issues
                responseModel.Message = "An error occurred while processing the request.";
                responseModel.Status = false;
            }

            return responseModel;
        }

    }
}

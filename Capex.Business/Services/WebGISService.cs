using Capex.Business.Interfaces;
using Microsoft.Extensions.Logging;
using Capex.Infrastructure.Interfaces;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.Models.ResponseModel.WebGIS;
using Capex.Models.RequestModel.WebGIS;
using Capex.DomainModels.DomainResponseModel.WebGIS;
using Capex.DomainModels.DomainRequestModel.WebGIS;
//using Org.BouncyCastle.Asn1.Ocsp;
//using DocumentFormat.OpenXml.Bibliography;

namespace Capex.Business.Services
{
    public class WebGISService : IWebGISService
    {
        private readonly ILogger<WebGISService> _logger;
        private readonly IWebGIS _webGIS;
        public WebGISService(ILogger<WebGISService> logger, IWebGIS webGIS)
        {
            this._logger = logger;
            this._webGIS = webGIS;
        }
        public async Task<ApiResult<WebGISKhasraListResponseModel>> GetKhasraList(WebGISRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraListResponseModel> responseModel = new ApiResult<WebGISKhasraListResponseModel>();
            WebGISKhasraListResponseModel data = new WebGISKhasraListResponseModel();
            WebGISRequest request = new WebGISRequest();
            // Check Menu Permission and Data Permission.
            WebGISKhasraListResponse response;
            #region || requestModel to request Mapping ||
            request.Bhucode = requestModel.Bhucode;

            #endregion

            response = await this._webGIS.GetKhasraList(request);
            if (response != null && response.WebGISKhasraListsResponse.Count > 0)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                data.WebGISKhasrListResponse = response.WebGISKhasraListsResponse.Select(i => new WebGISKhasraListModel()
                {
                    khasraNo = i.khasraNo,
                    khasraId = i.khasraId
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
        public async Task<ApiResult<WebGISKhasraDetailsResponseModel>> GetKhasraDetails(WebGISRequestModel khasrareq)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraDetailsResponseModel> responseModel = new ApiResult<WebGISKhasraDetailsResponseModel>();
            WebGISKhasraDetailsResponseModel data = new WebGISKhasraDetailsResponseModel();
            WebGISRequest request = new WebGISRequest();
            // Check Menu Permission and Data Permission.
            WebGISKhasraDetailsResponse response;
            #region || requestModel to request Mapping ||
            request.Bhucode = khasrareq.Bhucode;
            request.KhasraNo = khasrareq.KhasraNo;
            #endregion

            response = await this._webGIS.GetKhasraDetails(request);
            if (response != null && response.WebGISKhasraDetResponse.Count > 0)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                data.WebGISKhasraDetails = response.WebGISKhasraDetResponse.Select(i => new WebGISKhasraDetailsModel()
                {
                    BhuCode = i.BhuCode,
                    KhasraNo = i.khasraNo,
                    KhasraId = i.khasraId,
                    SurveyArea = i.SurveyArea,
                    IsLandIrrigated = i.IsLandIrrigated,
                    LandOwnershipType = i.LandOwnershipType,
                    Noyiyat = i.Noyiyat,
                    LagaanToPay = i.LagaanToPay,
                    CessToPay = i.CessToPay,
                    LoanFlag = i.LoanFlag,
                    LoanArea = i.LoanArea,
                    LandUseType = i.LandUseType,
                    Remarks = i.Remarks
                }).ToList();
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, khasrareq.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, khasrareq.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<WebGISOwnerDetailsResponseModel>> GetOwnerDetails(WebGISRequestModel khasraidreq)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISOwnerDetailsResponseModel> responseModel = new ApiResult<WebGISOwnerDetailsResponseModel>();
            WebGISOwnerDetailsResponseModel data = new WebGISOwnerDetailsResponseModel();
            WebGISRequest request = new WebGISRequest();
            // Check Menu Permission and Data Permission.
            WebGISOwnerDetailsResponse response;
            #region || requestModel to request Mapping ||
            request.KhasraId = khasraidreq.KhasraId;

            #endregion

            response = await this._webGIS.GetOwnerDetails(request);
            if (response != null && response.WebGISOwnerResponse.Count > 0)
            {
                this._logger.LogWarning(LoggerMessage.ResponseBegin);
                data.WebGISKhasraDetails = response.WebGISOwnerResponse.Select(i => new WebGISOwnerDetailsModel()
                {
                    OwnerId = i.OwnerId,
                    FirstName = i.FirstName,
                    MiddleName = i.MiddleName,
                    LastName = i.LastName,
                    OwnershipType = i.OwnershipType,
                    OwnershipTypeCode = i.OwnershipTypeCode,
                    RelationType = i.RelationType,
                    FatherName = i.FatherName,
                    Gender = i.Gender,
                    Caste = i.Caste,
                    SubCaste = i.SubCaste,
                    HouseNo = i.HouseNo,
                    Street = i.Street,
                    PostOffice = i.PostOffice,
                    Thana = i.Thana,
                    State = i.State,
                    District = i.District,
                    Tehsil = i.Tehsil,
                    Village = i.Village,
                    Pincode = i.PinCode,
                    Remarks = i.Remarks,
                    PhoneNo = i.PhoneNo,
                    MobileNo = i.MobileNo,
                    Email = i.Email,
                    Bank = i.Bank,
                    BankAccountNo = i.BankAccountNo,
                    PanCard = i.PanCard,
                    KisanCreditCard = i.KisanCreditCard,
                    AadharCard = i.AadharCard,
                    DrivingLicense = i.DrivingLicense,
                    Passport = i.Passport,
                    VoterId = i.VoterId,
                    RationCard = i.RationCard,
                    OwnerShare = i.OwnerShare
                }).ToList();
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, khasraidreq.Language);
                responseModel.Status = true;
                this._logger.LogWarning(LoggerMessage.ResponseEnd);
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, khasraidreq.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<WebGISResponseModel>> GetKhasraAndOwnerDetails(WebGISRequestModel khasraidreq)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISResponseModel> responseModel = new ApiResult<WebGISResponseModel>();
            WebGISResponseModel data = new WebGISResponseModel();
            WebGISRequest request = new WebGISRequest();
            WebGISResponse response;
            #region || requestModel to request Mapping ||
            request.KhasraId = khasraidreq.KhasraId;
            request.KhasraNo = khasraidreq.KhasraNo;
            request.Bhucode = khasraidreq.Bhucode;
            #endregion
            response = await this._webGIS.GetKhasraAndOwnerDetails(request);
            if (response != null && response.WebGISKhasraAndOwnerResponse != null)
            {
                data.KhasraAndOwnerLst = response.WebGISKhasraAndOwnerResponse.Select(i => new WebGISKhasraAndOwnerDetailsModel()
                {

                    BhuCode = i.BhuCode,
                    KhasraNo = i.khasraNo,
                    KhasraId = i.khasraId,
                    SurveyArea = i.SurveyArea,
                    IsLandIrrigated = i.IsLandIrrigated,
                    LandOwnershipType = i.LandOwnershipType,
                    Noyiyat = i.Noyiyat,
                    LagaanToPay = i.LagaanToPay,
                    CessToPay = i.CessToPay,
                    LoanFlag = i.LoanFlag,
                    LoanArea = i.LoanArea,
                    LandUseType = i.LandUseType,
                    Remarks = i.Remarks,
                    OwnerDetails = i.OwnerDetailsRes.Select(a => new WebGISOwnerDetailsModel
                    {
                        OwnerId = a.OwnerId,
                        FirstName = a.FirstName,
                        MiddleName = a.MiddleName,
                        LastName = a.LastName,
                        OwnershipType = a.OwnershipType,
                        OwnershipTypeCode = a.OwnershipTypeCode,
                        RelationType = a.RelationType,
                        FatherName = a.FatherName,
                        Gender = a.Gender,
                        Caste = a.Caste,
                        SubCaste = a.SubCaste,
                        HouseNo = a.HouseNo,
                        Street = a.Street,
                        PostOffice = a.PostOffice,
                        Thana = a.Thana,
                        State = a.State,
                        District = a.District,
                        Tehsil = a.Tehsil,
                        Village = a.Village,
                        Pincode = a.PinCode,
                        Remarks = a.Remarks,
                        PhoneNo = a.PhoneNo,
                        MobileNo = a.MobileNo,
                        Email = a.Email,
                        Bank = a.Bank,
                        BankAccountNo = a.BankAccountNo,
                        PanCard = a.PanCard,
                        KisanCreditCard = a.KisanCreditCard,
                        AadharCard = a.AadharCard,
                        DrivingLicense = a.DrivingLicense,
                        Passport = a.Passport,
                        VoterId = a.VoterId,
                        RationCard = a.RationCard,
                        OwnerShare = a.OwnerShare
                    }).ToList()
                }).ToList();
                responseModel.ResponseData = data;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, khasraidreq.Language);
                responseModel.Status = true;
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, khasraidreq.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }


        public async Task<ApiResult<WebGISKhasraNoResponseModel>> GetKhasraAndOwnerDetailsbyKhasraNo(WebGISKhasraRequestModel khasraidreq)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraNoResponseModel> responseModel = new ApiResult<WebGISKhasraNoResponseModel>();
            WebGISKhasraNoResponseModel Finaldata = new WebGISKhasraNoResponseModel();
            List<WebGISResponseModel> KhasraList = new List<WebGISResponseModel>();
            WebGISResponse response;
            #region || requestModel to request Mapping ||
            foreach (var item in khasraidreq.KhasraNolist)
            {
                WebGISResponseModel data = new WebGISResponseModel();
                WebGISRequest request = new WebGISRequest();
                request.KhasraId = item.khasraId;
                request.KhasraNo = item.khasraNo;
                request.Bhucode = khasraidreq.Bhucode;
                #endregion
                response = await this._webGIS.GetKhasraAndOwnerDetails(request);
                if (response != null && response.WebGISKhasraAndOwnerResponse != null)
                {

                    data.KhasraAndOwnerLst = response.WebGISKhasraAndOwnerResponse.Select(i => new WebGISKhasraAndOwnerDetailsModel()
                    {
                        SeemaId = 0,
                        BhuCode = i.BhuCode,
                        KhasraNo = i.khasraNo,
                        KhasraId = i.khasraId,
                        SurveyArea = i.SurveyArea,
                        IsLandIrrigated = i.IsLandIrrigated,
                        LandOwnershipType = i.LandOwnershipType,
                        Noyiyat = i.Noyiyat,
                        LagaanToPay = i.LagaanToPay,
                        CessToPay = i.CessToPay,
                        LoanFlag = i.LoanFlag,
                        LoanArea = i.LoanArea,
                        LandUseType = i.LandUseType,
                        Remarks = i.Remarks,
                        OwnerDetails = i.OwnerDetailsRes.Select(a => new WebGISOwnerDetailsModel
                        {
                            OwnerId = a.OwnerId,
                            FirstName = a.FirstName,
                            MiddleName = a.MiddleName,
                            LastName = a.LastName,
                            OwnershipType = a.OwnershipType,
                            OwnershipTypeCode = a.OwnershipTypeCode,
                            RelationType = a.RelationType,
                            FatherName = a.FatherName,
                            Gender = a.Gender,
                            Caste = a.Caste,
                            SubCaste = a.SubCaste,
                            HouseNo = a.HouseNo,
                            Street = a.Street,
                            PostOffice = a.PostOffice,
                            Thana = a.Thana,
                            State = a.State,
                            District = a.District,
                            Tehsil = a.Tehsil,
                            Village = a.Village,
                            Pincode = a.PinCode,
                            Remarks = a.Remarks,
                            PhoneNo = a.PhoneNo,
                            MobileNo = a.MobileNo,
                            Email = a.Email,
                            Bank = a.Bank,
                            BankAccountNo = a.BankAccountNo,
                            PanCard = a.PanCard,
                            KisanCreditCard = a.KisanCreditCard,
                            AadharCard = a.AadharCard,
                            DrivingLicense = a.DrivingLicense,
                            Passport = a.Passport,
                            VoterId = a.VoterId,
                            RationCard = a.RationCard,
                            OwnerShare = a.OwnerShare
                        }).ToList()
                    }).ToList();


                }
                KhasraList.Add(data);

            }

            Finaldata.KhasraAndOwnerLstbyMultikhasra = KhasraList;
            if (Finaldata != null)
            {

                responseModel.ResponseData = Finaldata;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, khasraidreq.Language);
                responseModel.Status = true;
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, khasraidreq.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<ApiResult<WebGISKhasraNoResponseModel>> GetBasraDetailList(WebGISKhasraRequestModel requestBasra)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<WebGISKhasraNoResponseModel> responseModel = new ApiResult<WebGISKhasraNoResponseModel>();
            WebGISKhasraNoResponseModel Finaldata = new WebGISKhasraNoResponseModel();
            List<WebGISResponseModel> KhasraList = new List<WebGISResponseModel>();
            WebGISResponseModel data = new WebGISResponseModel();


            // Check Menu Permission and Data Permission.

            #region || requestModel to request Mapping ||


            #endregion
            foreach (var item in requestBasra.KhasraNolist)
            {
                WebGISRequest request = new WebGISRequest();
                WebGISResponse response;
                request.KhasraId = item.khasraId;
                request.KhasraNo = item.khasraNo;
                request.Bhucode = requestBasra.Bhucode;

                if (request != null)
                {

                    response = await this._webGIS.GetBasraDetailList(request);
                    if (response != null && response.WebGISKhasraAndOwnerResponse != null)
                    {

                        data.KhasraAndOwnerLst = response.WebGISKhasraAndOwnerResponse.Select(i => new WebGISKhasraAndOwnerDetailsModel()
                        {

                            BhuCode = i.BhuCode,
                            KhasraNo = i.khasraNo,
                            KhasraId = i.khasraId,
                            SurveyArea = i.SurveyArea,
                            IsLandIrrigated = i.IsLandIrrigated,
                            LandOwnershipType = i.LandOwnershipType,
                            Noyiyat = i.Noyiyat,
                            LagaanToPay = i.LagaanToPay,
                            CessToPay = i.CessToPay,
                            LoanFlag = i.LoanFlag,
                            LoanArea = i.LoanArea,
                            LandUseType = i.LandUseType,
                            Remarks = i.Remarks,
                            OwnerDetails = i.OwnerDetailsRes.Select(a => new WebGISOwnerDetailsModel
                            {
                                OwnerId = a.OwnerId,
                                FirstName = a.FirstName,
                                MiddleName = a.MiddleName,
                                LastName = a.LastName,
                                OwnershipType = a.OwnershipType,
                                OwnershipTypeCode = a.OwnershipTypeCode,
                                RelationType = a.RelationType,
                                FatherName = a.FatherName,
                                Gender = a.Gender,
                                Caste = a.Caste,
                                SubCaste = a.SubCaste,
                                HouseNo = a.HouseNo,
                                Street = a.Street,
                                PostOffice = a.PostOffice,
                                Thana = a.Thana,
                                State = a.State,
                                District = a.District,
                                Tehsil = a.Tehsil,
                                Village = a.Village,
                                Pincode = a.PinCode,
                                Remarks = a.Remarks,
                                PhoneNo = a.PhoneNo,
                                MobileNo = a.MobileNo,
                                Email = a.Email,
                                Bank = a.Bank,
                                BankAccountNo = a.BankAccountNo,
                                PanCard = a.PanCard,
                                KisanCreditCard = a.KisanCreditCard,
                                AadharCard = a.AadharCard,
                                DrivingLicense = a.DrivingLicense,
                                Passport = a.Passport,
                                VoterId = a.VoterId,
                                RationCard = a.RationCard,
                                OwnerShare = a.OwnerShare
                            }).ToList()
                        }).ToList();


                    }
                    KhasraList.Add(data);
                }

            }
            Finaldata.KhasraAndOwnerLstbyMultikhasra = KhasraList;
            if (Finaldata != null)
            {

                responseModel.ResponseData = Finaldata;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestBasra.Language);
                responseModel.Status = true;
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, requestBasra.Language);
                responseModel.Status = false;
            }
            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }



    }
}

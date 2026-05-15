using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.WebGIS;
using Capex.DomainModels.DomainResponseModel.WebGIS;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Serilog;
using System.Text;
using System.Xml.Linq;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.Infrastructure.Services
{
    public class WebGISInfraService : IWebGIS
    {
        /// <summary>
        /// Gets the data base.
        /// </summary>
        /// <value>
        /// The data base.
        /// </value>
        private DBType DataBase => DBType.MasterDB;
        private ILogger<WebGISInfraService> _logger { get; }
        private readonly HttpClient _httpClient;
        private readonly ICommon _common;
        private readonly ServiceConfigSettings _service;
        private List<MethodDetails> _methList;

        public WebGISInfraService(ILogger<WebGISInfraService> logger, ICommon common)
        {
            this._logger = logger;
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(500);
            _common = common;
            _service = ServiceConfiguration.serviceConfigSettings;
            _methList = _service.Services.Where(x => x.ServiceName == "WebGIS").FirstOrDefault().Methods;


            
        }
        public async Task<WebGISKhasraListResponse> GetKhasraList(WebGISRequest bhucodereq)
        {

            try
            {
                var khasraParList = _methList.Where(x => x.MethodName == "getKhasraList").FirstOrDefault();
                string WebGISUrl = "";
                string FuncationName = "";

                FuncationName = khasraParList.MethodName;
                WebGISUrl = khasraParList.MethodURL;
                this._logger.LogDebug(LoggerMessage.Begin);
                WebGISKhasraListResponse response = new WebGISKhasraListResponse();
                List<WebGISKhasraList> list = new List<WebGISKhasraList>();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();

                var urlRequest = WebGISUrl + FuncationName + "?bhucode=" + bhucodereq.Bhucode;
                var swatch = new System.Diagnostics.Stopwatch();

                //try
                //{
                //    var Getresponse1 = await _httpClient.GetAsync(urlRequest);
                //}
                //catch (Exception ex)
                //{

                //    throw;
                //}

                var Getresponse = await _httpClient.GetAsync(urlRequest);

                var responseXmlString = Getresponse.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(responseXmlString))
                {
                    var doc = XDocument.Parse(responseXmlString);
                    list = doc.Descendants("Khasra").Select(d =>
                        new WebGISKhasraList
                        {
                            khasraNo = (string)d.Element("khasraNo").Value,
                            khasraId = (string)d.Element("khasraId").Value
                        }).ToList();
                    response.WebGISKhasraListsResponse = list;
                    if (list.Count > 0)
                    {

                        apiRequest.UserId = "NA";
                        apiRequest.RequestMethod = FuncationName;
                        apiRequest.RequestPayload = "?bhucode=" + bhucodereq.Bhucode;
                        apiRequest.ResponsePayload = responseXmlString;
                        apiRequest.ResponseStatus = 1;
                        apiRequest.ClientIP = "NA";
                        apiRequest.ErrorMessage = "NA";
                        var task = _common.InsertAPILogStatus(apiRequest);
                    }
                }

                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
        }
        public async Task<WebGISKhasraDetailsResponse> GetKhasraDetails(WebGISRequest khasrareq)
        {
            try
            {

                string WebGISUrl = "";
                string FuncationName = "";
                var KhasraDetailPar = _methList.Where(x => x.MethodName == "getKhasraDetails").FirstOrDefault();

                FuncationName = KhasraDetailPar.MethodName;
                WebGISUrl = KhasraDetailPar.MethodURL;

                this._logger.LogDebug(LoggerMessage.Begin);

                WebGISKhasraDetailsResponse response = new WebGISKhasraDetailsResponse();
                List<WebGISKhasraDetailsDomain> list = new List<WebGISKhasraDetailsDomain>();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();

                var valuesstr = new
                {
                    bhucode = khasrareq.Bhucode,
                    khasraNo = khasrareq.KhasraNo
                };
                string datastr = JsonConvert.SerializeObject(valuesstr);
                var urlRequest = WebGISUrl + FuncationName + "?bhucode=" + khasrareq.Bhucode + "&khasraNo=" + khasrareq.KhasraNo;
                var swatch = new System.Diagnostics.Stopwatch();
                var Getresponse = await _httpClient.GetAsync(urlRequest);
                var responseXmlString = Getresponse.Content.ReadAsStringAsync().Result;
                if (!string.IsNullOrEmpty(responseXmlString))
                {
                    var doc = XDocument.Parse(responseXmlString);
                    list = doc.Descendants("KhasraDetails").Select(d =>
                        new WebGISKhasraDetailsDomain
                        {
                            BhuCode = d.Element("bhucode").Value,
                            khasraNo = d.Element("khasraNo").Value,
                            khasraId = d.Element("khasraId").Value,
                            SurveyArea = Convert.ToDouble(d.Element("surveyArea").Value),
                            IsLandIrrigated = Convert.ToInt32(d.Element("isLandIrrigated").Value),
                            LandOwnershipType = d.Element("landOwnershipType").Value,
                            Noyiyat = d.Element("noyiyat").Value,
                            LagaanToPay = Convert.ToDouble(d.Element("lagaanToPay").Value),
                            CessToPay = Convert.ToDouble(d.Element("cessToPay").Value),
                            LoanFlag = Convert.ToInt32(d.Element("loanFlag").Value),
                            LoanArea = Convert.ToDouble(d.Element("loanArea").Value),
                            LandUseType = d.Element("landUseType").Value,
                            Remarks = d.Element("remarks").Value
                        }).ToList();
                    response.WebGISKhasraDetResponse = list;
                    if (list.Count > 0)
                    {
                        apiRequest.UserId = "NA";
                        apiRequest.RequestMethod = FuncationName;
                        apiRequest.RequestPayload = FuncationName + "?bhucode=" + khasrareq.Bhucode + "&khasraNo=" + khasrareq.KhasraNo; ;
                        apiRequest.ResponsePayload = responseXmlString;
                        apiRequest.ResponseStatus = 1;
                        apiRequest.ClientIP = "NA";
                        apiRequest.ErrorMessage = "NA";
                        var task = _common.InsertAPILogStatus(apiRequest);

                    }
                }

                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
        }
        public async Task<WebGISOwnerDetailsResponse> GetOwnerDetails(WebGISRequest khasraIdreq)
        {
            try
            {
                string FuncationName = "";
                string WebGISUrl = "";
                var KhasraDetailPar = _methList.Where(x => x.MethodName == "getOwnerDetails").FirstOrDefault();

                FuncationName = KhasraDetailPar.MethodName;
                WebGISUrl = KhasraDetailPar.MethodURL;
                this._logger.LogDebug(LoggerMessage.Begin);
                WebGISOwnerDetailsResponse response = new WebGISOwnerDetailsResponse();
                List<WebGISOwnerDetails> list = new List<WebGISOwnerDetails>();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();
                var valuesstr = new
                {
                    khasraId = khasraIdreq.KhasraId

                };
                string datastr = JsonConvert.SerializeObject(valuesstr);
                var urlRequest = WebGISUrl + FuncationName + "?khasraId=" + khasraIdreq.KhasraId.Trim();


                var Getresponse = await _httpClient.GetAsync(urlRequest);
                var responseXmlString = Getresponse.Content.ReadAsStringAsync().Result;
                var swatch = new System.Diagnostics.Stopwatch();
                if (!string.IsNullOrEmpty(responseXmlString))
                {
                    var doc = XDocument.Parse(responseXmlString);
                    list = doc.Descendants("owner").Select(d =>
new WebGISOwnerDetails
{
    OwnerId = d.Element("ownerId")?.Value ?? "",
    FirstName = d.Element("firstName")?.Value ?? "",
    MiddleName = d.Element("middleName")?.Value ?? "",
    LastName = d.Element("lastName")?.Value ?? "",
    OwnershipType = d.Element("ownershipType")?.Value ?? "",
    OwnershipTypeCode = d.Element("ownershipTypeCode")?.Value ?? "",
    RelationType = d.Element("relationType")?.Value ?? "",
    FatherName = d.Element("fatherName")?.Value ?? "",
    Gender = d.Element("gender")?.Value ?? "",
    Caste = d.Element("caste")?.Value ?? "",
    SubCaste = d.Element("subCaste")?.Value ?? "",
    HouseNo = d.Element("houseNo")?.Value ?? "",
    Street = d.Element("street")?.Value ?? "",
    PostOffice = d.Element("postOffice")?.Value ?? "",
    Thana = d.Element("thana")?.Value ?? "",
    State = d.Element("state")?.Value ?? "",
    District = d.Element("district")?.Value ?? "",
    Tehsil = d.Element("tehsil")?.Value ?? "",
    Village = d.Element("village")?.Value ?? "",
    PinCode = d.Element("pincode")?.Value ?? "",
    Remarks = d.Element("remarks")?.Value ?? "",
    PhoneNo = d.Element("phoneNo")?.Value ?? "",
    MobileNo = d.Element("mobileNo")?.Value ?? "",
    Email = d.Element("email")?.Value ?? "",
    PanCard = d.Element("panCard")?.Value ?? "",
    KisanCreditCard = d.Element("kisanCreditCard")?.Value ?? "",
    AadharCard = d.Element("aadharCard")?.Value ?? "",
    DrivingLicense = d.Element("drivingLicense")?.Value ?? "",
    Passport = d.Element("passport")?.Value ?? "",
    VoterId = d.Element("voterId")?.Value ?? "",
    RationCard = d.Element("rationCard")?.Value ?? "",
    OwnerShare = d.Element("ownerShare")?.Value ?? ""
}).ToList();
                    response.WebGISOwnerResponse = list;
                    if (!string.IsNullOrEmpty(responseXmlString))
                    {
                        apiRequest.UserId = "NA";
                        apiRequest.RequestMethod = FuncationName;
                        apiRequest.RequestPayload = FuncationName + "?KhasraId=" + khasraIdreq.KhasraId; ;
                        apiRequest.ResponsePayload = responseXmlString;
                        apiRequest.ResponseStatus = 1;
                        apiRequest.ClientIP = "NA";
                        apiRequest.ErrorMessage = "NA";
                        var task = _common.InsertAPILogStatus(apiRequest);

                    }
                }

                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }

        }
        public async Task<WebGISResponse> GetKhasraAndOwnerDetails(WebGISRequest khasraIdreq)
        {
            try
            {
                string FuncationNameKhasra = "";
                string FuncationNameOwner = "";
                string WebGISUrl = "";
                var KhasraDetailPar = _methList.Where(x => x.MethodName == "getKhasraDetails").FirstOrDefault();
                var OwnerDetailPar = _methList.Where(x => x.MethodName == "getOwnerDetails").FirstOrDefault();
                FuncationNameKhasra = KhasraDetailPar.MethodName;
                FuncationNameOwner = OwnerDetailPar.MethodName;
                WebGISUrl = KhasraDetailPar.MethodURL;
                this._logger.LogDebug(LoggerMessage.Begin);
                WebGISResponse response = new WebGISResponse();
                List<WebGISKhasraAndOwnerDetails> List = new List<WebGISKhasraAndOwnerDetails>();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();
                #region This Khasra details
                var KhasrDetailsurl = WebGISUrl + FuncationNameKhasra + "?bhucode=" + khasraIdreq.Bhucode + "&khasraNo=" + khasraIdreq.KhasraNo;
                var GetKharaDetailresponse = await _httpClient.GetAsync(KhasrDetailsurl);
                var KhasraDetailresponseXmlString = GetKharaDetailresponse.Content.ReadAsStringAsync().Result;
                #endregion
                #region This Owner Khasra details
                var Ownerurl = WebGISUrl + FuncationNameOwner + "?khasraId=" + khasraIdreq.KhasraId;
                var GetOwnerresponse = await _httpClient.GetAsync(Ownerurl);
                var OwnerresponseXmlString = GetOwnerresponse.Content.ReadAsStringAsync().Result;
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                if (!string.IsNullOrEmpty(KhasraDetailresponseXmlString))
                {
                    var KhasraDetails = XDocument.Parse(KhasraDetailresponseXmlString);
                    var OwnerDetailResp = XDocument.Parse(OwnerresponseXmlString);
                    if (KhasraDetails.Root.HasElements)
                    {
                        List = KhasraDetails.Descendants("KhasraDetails").Select(d =>
                           new WebGISKhasraAndOwnerDetails
                           {
                               BhuCode = d.Element("bhucode")?.Value ?? "",
                               khasraNo = d.Element("khasraNo")?.Value,
                               khasraId = d.Element("khasraId")?.Value,
                               SurveyArea = d.Element("surveyArea")?.Value ?? "",
                               IsLandIrrigated = d.Element("isLandIrrigated")?.Value ?? "",
                               LandOwnershipType = d.Element("landOwnershipType")?.Value,
                               Noyiyat = d.Element("noyiyat")?.Value,
                               LagaanToPay = d.Element("lagaanToPay")?.Value,
                               CessToPay = d.Element("cessToPay")?.Value,
                               LoanFlag = d.Element("loanFlag")?.Value,
                               LoanArea = d.Element("loanArea")?.Value,
                               LandUseType = d.Element("landUseType")?.Value,
                               Remarks = d.Element("remarks")?.Value,
                               OwnerDetailsRes = OwnerDetailResp.Descendants("owner").Select(d => new WebGISOwnerDetails
                               {
                                   OwnerId = d.Element("ownerId")?.Value ?? "",
                                   FirstName = d.Element("firstName")?.Value ?? "",
                                   MiddleName = d.Element("middleName")?.Value ?? "",
                                   LastName = d.Element("lastName")?.Value ?? "",
                                   OwnershipType = d.Element("ownershipType")?.Value ?? "",
                                   OwnershipTypeCode = d.Element("ownershipTypeCode")?.Value ?? "",
                                   RelationType = d.Element("relationType")?.Value ?? "",
                                   FatherName = d.Element("fatherName")?.Value ?? "",
                                   Gender = d.Element("gender")?.Value ?? "",
                                   Caste = d.Element("caste")?.Value ?? "",
                                   SubCaste = d.Element("subCaste")?.Value ?? "",
                                   HouseNo = d.Element("houseNo")?.Value ?? "",
                                   Street = d.Element("street")?.Value ?? "",
                                   PostOffice = d.Element("postOffice")?.Value ?? "",
                                   Thana = d.Element("thana")?.Value ?? "",
                                   State = d.Element("state")?.Value ?? "",
                                   District = d.Element("district")?.Value ?? "",
                                   Tehsil = d.Element("tehsil")?.Value ?? "",
                                   Village = d.Element("village")?.Value ?? "",
                                   PinCode = d.Element("pincode")?.Value ?? "",
                                   Remarks = d.Element("remarks")?.Value ?? "",
                                   PhoneNo = d.Element("phoneNo")?.Value ?? "",
                                   MobileNo = d.Element("mobileNo")?.Value ?? "",
                                   Email = d.Element("email")?.Value ?? "",
                                   PanCard = d.Element("panCard")?.Value ?? "",
                                   KisanCreditCard = d.Element("kisanCreditCard")?.Value ?? "",
                                   AadharCard = d.Element("aadharCard")?.Value ?? "",
                                   DrivingLicense = d.Element("drivingLicense")?.Value ?? "",
                                   Passport = d.Element("passport")?.Value ?? "",
                                   VoterId = d.Element("voterId")?.Value ?? "",
                                   RationCard = d.Element("rationCard")?.Value ?? "",
                                   OwnerShare = d.Element("ownerShare")?.Value ?? ""
                               }).ToList()
                           }).ToList();
                        response.WebGISKhasraAndOwnerResponse = List;

                    }
                }
                if (!string.IsNullOrEmpty(OwnerresponseXmlString))
                {
                    apiRequest.UserId = "NA";
                    apiRequest.RequestMethod = FuncationNameKhasra + "~" + FuncationNameOwner;
                    apiRequest.RequestPayload = FuncationNameKhasra + "?bhucode=" + khasraIdreq.Bhucode + "&khasraNo=" + khasraIdreq.KhasraNo + "~" + FuncationNameOwner + "?KhasraId=" + khasraIdreq.KhasraId;
                    apiRequest.ResponsePayload = KhasraDetailresponseXmlString + " ~ " + OwnerresponseXmlString;
                    apiRequest.ResponseStatus = 1;
                    apiRequest.ClientIP = "NA";
                    apiRequest.ErrorMessage = "NA";
                    var task = _common.InsertAPILogStatus(apiRequest);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }

        }

        public async Task<WebGISResponse> GetBasraDetailList(WebGISRequest request)
        {
            try
            {
                WebGISResponse response = new WebGISResponse();
                List<WebGISKhasraAndOwnerDetails> List = new List<WebGISKhasraAndOwnerDetails>();
                WebGISKhasraAndOwnerDetails obj = new WebGISKhasraAndOwnerDetails();
                string funcationNameKhasraDetails = "";
                string funcationNameBasraDetails = "";
                string funcationNameOwnerDetails = "";
                string webGISUrl = "";
                var basraDetailPar = _methList.Where(x => x.MethodName == "getBasraDetailList").FirstOrDefault();
                var khasraDetailPar = _methList.Where(x => x.MethodName == "getKhasraDetails").FirstOrDefault();
                var OwnerDetailPar = _methList.Where(x => x.MethodName == "getOwnerDetails").FirstOrDefault();
                funcationNameBasraDetails = basraDetailPar.MethodName;
                funcationNameKhasraDetails = khasraDetailPar.MethodName;
                funcationNameOwnerDetails = OwnerDetailPar.MethodName;
                webGISUrl = basraDetailPar.MethodURL;

                this._logger.LogDebug(LoggerMessage.Begin);

                List<WebGISBasraDetails> BasraList = new List<WebGISBasraDetails>();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();
                #region Call API WebGis basra details
                var BasraDetailsurl = webGISUrl + funcationNameBasraDetails + "?bhucode=" + request.Bhucode + "&khasraNo=" + request.KhasraNo;
                var GetBasraListresponse = await _httpClient.GetAsync(BasraDetailsurl);
                var basraDetailresponseXmlString = GetBasraListresponse.Content.ReadAsStringAsync().Result;
                #endregion

                var swatch = new System.Diagnostics.Stopwatch();
                if (!string.IsNullOrEmpty(basraDetailresponseXmlString))
                {
                    var BasraDetails = XDocument.Parse(basraDetailresponseXmlString);

                    if (BasraDetails.Root.HasElements)
                    {
                        BasraList = BasraDetails.Descendants("Basra").Select(d =>
                           new WebGISBasraDetails
                           {
                               BasraNo = Convert.ToString(d.Element("bhucode")?.Value) ?? "",
                               KhasraNo = Convert.ToString(d.Element("khasraNo")?.Value),
                               KhasraId = d.Element("khasraId")?.Value,
                               LandType = Convert.ToInt32(d.Element("LandType")?.Value)
                           }).ToList();
                        foreach (var getBasra in BasraList)
                        {
                            #region Call API Webgis Khasra details
                            webGISUrl = khasraDetailPar.MethodURL;
                            var KhasraDetailsurl = webGISUrl + funcationNameKhasraDetails + "?bhucode=" + request.Bhucode + "&khasraNo=" + getBasra.KhasraNo;
                            var GetKhasraDetailsresponse = await _httpClient.GetAsync(KhasraDetailsurl);
                            var KhasraresponseDetailsXmlString = GetKhasraDetailsresponse.Content.ReadAsStringAsync().Result;

                            var KhasraOwnerurl = webGISUrl + funcationNameOwnerDetails + "?khasraId=" + getBasra.KhasraId;
                            var GetKhasraOwnerresponse = await _httpClient.GetAsync(KhasraOwnerurl);
                            var KhasraresponseOwnerDetailsXmlString = GetKhasraOwnerresponse.Content.ReadAsStringAsync().Result;

                            var KhasraDetail = XDocument.Parse(KhasraresponseDetailsXmlString);
                            var OwnerDetailResp = XDocument.Parse(KhasraresponseOwnerDetailsXmlString);
                            #endregion
                            if (KhasraDetail.Root.HasElements)
                            {
                                var getresponse = KhasraDetail.Descendants("KhasraDetails").Select(d =>
                                   new WebGISKhasraAndOwnerDetails
                                   {
                                       BhuCode = d.Element("bhucode")?.Value ?? "",
                                       khasraNo = d.Element("khasraNo")?.Value,
                                       khasraId = d.Element("khasraId")?.Value,
                                       SurveyArea = d.Element("surveyArea")?.Value ?? "",
                                       IsLandIrrigated = d.Element("isLandIrrigated")?.Value ?? "",
                                       LandOwnershipType = d.Element("landOwnershipType")?.Value,
                                       Noyiyat = d.Element("noyiyat")?.Value,
                                       LagaanToPay = d.Element("lagaanToPay")?.Value,
                                       CessToPay = d.Element("cessToPay")?.Value,
                                       LoanFlag = d.Element("loanFlag")?.Value,
                                       LoanArea = d.Element("loanArea")?.Value,
                                       LandUseType = d.Element("landUseType")?.Value,
                                       Remarks = d.Element("remarks")?.Value,
                                       OwnerDetailsRes = OwnerDetailResp.Descendants("owner").Select(d => new WebGISOwnerDetails
                                       {
                                           OwnerId = d.Element("ownerId")?.Value ?? "",
                                           FirstName = d.Element("firstName")?.Value ?? "",
                                           MiddleName = d.Element("middleName")?.Value ?? "",
                                           LastName = d.Element("lastName")?.Value ?? "",
                                           OwnershipType = d.Element("ownershipType")?.Value ?? "",
                                           OwnershipTypeCode = d.Element("ownershipTypeCode")?.Value ?? "",
                                           RelationType = d.Element("relationType")?.Value ?? "",
                                           FatherName = d.Element("fatherName")?.Value ?? "",
                                           Gender = d.Element("gender")?.Value ?? "",
                                           Caste = d.Element("caste")?.Value ?? "",
                                           SubCaste = d.Element("subCaste")?.Value ?? "",
                                           HouseNo = d.Element("houseNo")?.Value ?? "",
                                           Street = d.Element("street")?.Value ?? "",
                                           PostOffice = d.Element("postOffice")?.Value ?? "",
                                           Thana = d.Element("thana")?.Value ?? "",
                                           State = d.Element("state")?.Value ?? "",
                                           District = d.Element("district")?.Value ?? "",
                                           Tehsil = d.Element("tehsil")?.Value ?? "",
                                           Village = d.Element("village")?.Value ?? "",
                                           PinCode = d.Element("pincode")?.Value ?? "",
                                           Remarks = d.Element("remarks")?.Value ?? "",
                                           PhoneNo = d.Element("phoneNo")?.Value ?? "",
                                           MobileNo = d.Element("mobileNo")?.Value ?? "",
                                           Email = d.Element("email")?.Value ?? "",
                                           PanCard = d.Element("panCard")?.Value ?? "",
                                           KisanCreditCard = d.Element("kisanCreditCard")?.Value ?? "",
                                           AadharCard = d.Element("aadharCard")?.Value ?? "",
                                           DrivingLicense = d.Element("drivingLicense")?.Value ?? "",
                                           Passport = d.Element("passport")?.Value ?? "",
                                           VoterId = d.Element("voterId")?.Value ?? "",
                                           RationCard = d.Element("rationCard")?.Value ?? "",
                                           OwnerShare = d.Element("ownerShare")?.Value ?? ""
                                       }).ToList()
                                   }).FirstOrDefault();
                                List.Add(getresponse);

                            }

                        }
                        response.WebGISKhasraAndOwnerResponse = List;

                    }
                }
                if (!string.IsNullOrEmpty(basraDetailresponseXmlString))
                {
                    apiRequest.UserId = "NA";
                    apiRequest.RequestMethod = funcationNameBasraDetails + "~" + funcationNameKhasraDetails;
                    apiRequest.RequestPayload = funcationNameBasraDetails + "?bhucode=" + request.Bhucode + "&khasraNo=" + request.KhasraNo + "~" + funcationNameKhasraDetails + "?bhucode=" + request.Bhucode + "&khasraNo=" + request.KhasraNo;
                    apiRequest.ResponsePayload = basraDetailresponseXmlString;
                    apiRequest.ResponseStatus = 1;
                    apiRequest.ClientIP = "NA";
                    apiRequest.ErrorMessage = "NA";
                    var task = _common.InsertAPILogStatus(apiRequest);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
        }

        public async Task<KhasraAdjDetailsResponse> GetChaturSeemaDetailsByKhasraId(WebGISRequest khasraIdreq)
        {
            try
            {
                string FuncationNameKhasra = "";
                string FuncationNameOwner = "";
                string WebGISUrl = "";
                var KhasraDetailAdj = _methList.Where(x => x.MethodName == "getKhasraAdj").FirstOrDefault();
                FuncationNameKhasra = KhasraDetailAdj.MethodName;
                WebGISUrl = KhasraDetailAdj.MethodURL;
                this._logger.LogDebug(LoggerMessage.Begin);
                KhasraAdjDetailsResponse response = new KhasraAdjDetailsResponse();
                APILogStatusDomainRequestModel apiRequest = new APILogStatusDomainRequestModel();
                #region This Khasra details
                var AdjDetailsurl = WebGISUrl + FuncationNameKhasra + "?khasraId=" + khasraIdreq.KhasraId;
                var AdjKharaDetailresponse = await _httpClient.GetAsync(AdjDetailsurl);
                var AdjKhasraDetailresponseXmlString = AdjKharaDetailresponse.Content.ReadAsStringAsync().Result;
                #endregion
                #region This Owner Khasra details
                //var Ownerurl = WebGISUrl + FuncationNameOwner + "?khasraId=" + khasraIdreq.KhasraId;
                //var GetOwnerresponse = await _httpClient.GetAsync(Ownerurl);
                //var OwnerresponseXmlString = GetOwnerresponse.Content.ReadAsStringAsync().Result;
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                if (!string.IsNullOrEmpty(AdjKhasraDetailresponseXmlString))
                {
                    var AdjKhasraDetails = XDocument.Parse(AdjKhasraDetailresponseXmlString);
                    if (AdjKhasraDetails.Root.HasElements)
                    {
                        var result = AdjKhasraDetails.Descendants("khasraAdjDetails").Select(d =>
                           new KhasraAdjDetailsResponse
                           {
                               ServiceFlag = d.Element("serviceFlag")?.Value ?? "",
                               East = d.Element("east")?.Value,
                               West = d.Element("west")?.Value,
                               North = d.Element("north")?.Value ?? "",
                               South = d.Element("south")?.Value ?? "",


                           }).FirstOrDefault();
                        response = result;

                    }
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }

        }


       


      

    }
}
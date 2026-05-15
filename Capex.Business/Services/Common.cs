using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Capex.Business.Common;
using Capex.Business.Interfaces;
using Capex.DomainModels.Common;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Capex.Models.RequestModel.Masters;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Core.Tokens;
using static Capex.Models.Common.APIResult;
using ICommon = Capex.Business.Interfaces.ICommon;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Newtonsoft.Json;

namespace Capex.Business.Services
{
    public class Common : ICommon
    {
        private readonly ILogger<Common> _logger;
        private readonly AppSettings appSettings;
        private readonly INotification _notification;
        private readonly Capex.Infrastructure.Interfaces.ICommon _common;
        private readonly IInfrastructureServices infrastructureServices;
        public Common(ILogger<Common> logger, INotification notification,  IInfrastructureServices infrastructureServices, IOptions<AppSettings> appSettings)
        {
            this._logger = logger;
            this._notification = notification;
           
            this.infrastructureServices = infrastructureServices;
            this.appSettings = appSettings.Value;
        }
        public async Task<ApiResult<OTPResponseModel>> GenerateOTP(OTPRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();

            OTPResponseModel otpResponse = new OTPResponseModel();
            otpResponse = await this.SendOTP(requestModel);
            responseModel.Status = true;
            responseModel.ResponseData = otpResponse;
            responseModel.ErrorCode = ErrorCodes.Err00000;
            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<OTPResponseModel>> sendTestOTP(OTPRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<OTPResponseModel> responseModel = new ApiResult<OTPResponseModel>();

            OTPResponseModel otpResponse = new OTPResponseModel();
            otpResponse = await this.SendOTP(requestModel);
            responseModel.Status = true;
            responseModel.ResponseData = otpResponse;
            responseModel.ErrorCode = ErrorCodes.Err00000;
            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, requestModel.Language);

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<OTPResponseModel> SendOTP(OTPRequestModel requestModel)
        {

            OTPResponseModel responseModel = new OTPResponseModel();
            responseModel.OTPReferenceId = Guid.NewGuid().ToString();
            string OTP = CommonUtility.GetRandomOTP();


            //RedisHelper.SetData(!String.IsNullOrEmpty(requestModel.UserId.ToString()) ? requestModel.UserId.ToString() : "" + requestModel.OTPFor + responseModel.OTPReferenceId, OTP, DateTimeOffset.Now.AddMinutes(AppSettings.Current.OTPExpiry));


            //string otp = (string)RedisHelper.GetData(!String.IsNullOrEmpty(requestModel.UserId.ToString()) ? requestModel.UserId.ToString() : "" + requestModel.OTPFor + responseModel.OTPReferenceId);


            RedisHelper.SetData(requestModel.OTPFor + responseModel.OTPReferenceId, OTP, DateTimeOffset.Now.AddMinutes(AppSettings.Current.OTPExpiry));


            string otp = (string)RedisHelper.GetData(requestModel.OTPFor + responseModel.OTPReferenceId);


            requestModel.OTP = OTP;
            this._notification.SendSMS(requestModel, NotificationTemplateConstants.OTPSMS);

            //if (requestModel.OTPType == OTPType.SMS || requestModel.OTPType == OTPType.Both)
            //{
            //    this._notification.SendSMS(requestModel, NotificationTemplateConstants.OTPSMS);
            //}
            //if (requestModel.OTPType == OTPType.Email || requestModel.OTPType == OTPType.Both)
            //{
            //    this._notification.SendMail(requestModel, NotificationTemplateConstants.OTPEmail);
            //}
            //if (requestModel.OTPType == OTPType.WhatsApp || requestModel.OTPType == OTPType.Both)
            //{
            //    this._notification.SendWhatsApp(requestModel, NotificationTemplateConstants.OTPWhatsApp);
            //}
            //if (requestModel.OTPType == OTPType.WhatsAppConsent)
            //{
            //    this._notification.SendWhatsAppOptInOut(requestModel.MobileNumber, requestModel.WhatsAppConsentType);
            //}
            return responseModel;
        }
        public async Task<ApiResult<object>> Validate(Object request)
        {

            List<ModelValidateDetailResponse> response = new List<ModelValidateDetailResponse>();
            ModelValidateRequest modelValidateRequest = new ModelValidateRequest();
            ApiResult<object> result = new ApiResult<object> { Status = true };
            modelValidateRequest.Area = request.GetType().GetProperty("Area").GetValue(request, null).ToString();
            modelValidateRequest.ActionName = request.GetType().GetProperty("ActionName").GetValue(request, null).ToString();
            modelValidateRequest.Controller = request.GetType().GetProperty("Controller").GetValue(request, null).ToString();
            try
            {
                response = await _common.GetModelValidation(modelValidateRequest);
                if (response != null)
                {
                    foreach (ModelValidateDetailResponse item in response)
                    {
                        var value = request.GetType().GetProperty(item.PropertyName).GetValue(request, null);
                        // var value=item.PropertyName.ToString();

                        if (item.IsRequired == true && (value == null || value.ToString() == ""))
                        {
                            result.ErrorCode = item.ErrorCode;
                            result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                            result.Status = false;
                            return result;
                        }
                        if (!string.IsNullOrEmpty(value.ToString()))
                        {
                            int vartype = 0; float vartypefloat; double vartypedouble; DateTime vartypedateTime; DateFormat vartypedateformat;
                            DateOnly vartypedateonly;

                            var valstr = value.GetType().Name;
                            if (item.Type == "Int")
                            {

                                if (!int.TryParse(value.ToString(), out vartype))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            else if (item.Type == "string")
                            {
                                if (item.Type != valstr)
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }

                            }
                            else if (item.Type == "Float")
                            {
                                if (!float.TryParse(value.ToString(), out vartypefloat))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            else if (item.Type == "Double")
                            {
                                if (!double.TryParse(value.ToString(), out vartypedouble))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            else if (item.Type == "DateTime")
                            {
                                if (!DateTime.TryParse(value.ToString(), out vartypedateTime))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            else if (item.Type == "DateFormat")
                            {
                                if (!DateFormat.TryParse(value.ToString(), out vartypedateformat))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            else if (item.Type == "DateOnly")
                            {
                                if (!DateOnly.TryParse(value.ToString(), out vartypedateonly))
                                {
                                    result.ErrorCode = item.ErrorCode;
                                    result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                    result.Status = false;
                                }
                            }
                            int val = Convert.ToInt32(value.ToString());
                            if (val > item.MaxLength)
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            if (val < item.MinLength)
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            var regex = new Regex(item.Regx);
                            if (!regex.IsMatch(value.ToString()))
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }
                            if (!string.IsNullOrEmpty(item.ErrorCode))
                            {
                                result.ErrorCode = item.ErrorCode;
                                result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                                result.Status = false;
                            }

                        }
                        else
                        {
                            result.ErrorCode = item.ErrorCode;
                            result.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, item.ErrorCode, request.GetType().GetProperty("Language").GetValue(request, null).ToString());
                            result.Status = false;
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error(ex.StackTrace);
                result.Message = ex.Message;
                result.ErrorCode = "Err";
                result.Status = false;
            }
            return result;

        }

        public async Task<ApiResult<bool>> ValidateOTP(OTPRequestModel requestModel)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<bool> responseModel = new ApiResult<bool>();
            //string otp = (string)RedisHelper.GetData(!String.IsNullOrEmpty(requestModel.UserId.ToString()) ? requestModel.UserId.ToString() : "" + requestModel.OTPFor + requestModel.OTPReferenceId);
            requestModel.UserId = 0;
            string otp = (string)RedisHelper.GetData(requestModel.OTPFor + requestModel.OTPReferenceId);
            if (requestModel.OTP == otp)
            {
                RedisHelper.RemoveData(requestModel.OTPFor + requestModel.OTPReferenceId);

                responseModel.ResponseData = true;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00024, requestModel.Language); ;
                responseModel.Status = true;

            }
            else
            {

                responseModel.ResponseData = false;
                responseModel.ErrorCode = ErrorCodes.Err00063;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00063, requestModel.Language);
                responseModel.Status = false;
            }
            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<CaptchaResponseModel>> GenerateCaptcha(HeaderHelperRequest headerHelperRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();
            try
            {
                CaptchaResponseModel data = new CaptchaResponseModel();
                CaptchaGenerator generatedcaptcha = new CaptchaGenerator();

                string CapchaReferenceId = Guid.NewGuid().ToString();

                var captcha = generatedcaptcha.CaptchaWithCipher();
                if (!string.IsNullOrEmpty(captcha.CaptchaBase64))
                {
                    RedisHelper.SetData(CapchaReferenceId, captcha.Captcha, DateTimeOffset.Now.AddMinutes(AppSettings.Current.OTPExpiry));
                    //var ResCaptcha = RedisHelper.GetData(CapchaReferenceId);
                    data = new CaptchaResponseModel()
                    {
                        Cipher = CapchaReferenceId,
                        CaptchaBase64 = captcha.CaptchaBase64,
                        //Captcha = captcha.Captcha,


                    };
                    //Err00025
                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00000;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, headerHelperRequest.Language);
                    responseModel.Status = true;

                }
                else
                {

                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00023;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, headerHelperRequest.Language); 
                    responseModel.Status = false;
                }

            }
            catch (Exception ex)
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00023;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, headerHelperRequest.Language);  
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<ApiResult<bool>> ValidateCaptcha(CaptchaRequestModel request)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<bool> responseModel = new ApiResult<bool>();

            try
            {
                if (request.Cipher == null || request.Captcha == null)
                {
                    // Handle the case where request.Cipher or request.Captcha is null
                    responseModel.ResponseData = false;
                    responseModel.ErrorCode = ErrorCodes.Err00001; // You might want to use a more appropriate error code
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, request.Language);
                    responseModel.Status = false;
                    return responseModel;
                }

                var ResCaptcha = RedisHelper.GetData(request.Cipher);

                if (string.Equals(ResCaptcha?.ToString(), request.Captcha.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    RedisHelper.RemoveData(request.Cipher);

                    responseModel.ResponseData = true;
                    responseModel.ErrorCode = ErrorCodes.Err00000;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00024, request.Language);
                    responseModel.Status = true;
                }
                else
                {
                    responseModel.ResponseData = false;
                    responseModel.ErrorCode = ErrorCodes.Err00025;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00025, request.Language);
                    responseModel.Status = false;
                }
            }
            catch (Exception ex)
            {
                responseModel.ResponseData = false;
                responseModel.ErrorCode = ErrorCodes.Err00001;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, request.Language);
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        public ApiResult<CaptchaResponseModel> VerifyCaptcha(CaptchaRequestModel request)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<CaptchaResponseModel> responseModel = new ApiResult<CaptchaResponseModel>();

            try
            {
                CaptchaResponseModel data = new CaptchaResponseModel();
                //CaptchaGenerator cg = new CaptchaGenerator();
                //bool captcha = cg.Verify(request.Captcha, request.Cipher);
                var ResCaptcha = RedisHelper.GetData(request.Cipher);

                if (ResCaptcha.ToString() == request.Captcha.ToString())
                {
                    RedisHelper.RemoveData(request.Cipher);

                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00024;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00024, request.Language.IsString()); ;
                    responseModel.Status = true;

                }
                else
                {

                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00025;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00025, request.Language.IsString());
                    responseModel.Status = false;
                }

            }
            catch (Exception ex)
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00025;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00023, request.Language.IsString()); ;
                responseModel.Status = false;
                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<TokenResponseModel> GenerateToken(TokenRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            TokenResponseModel responseModel = new TokenResponseModel();

            // return null if userid ,pass not found
            dynamic user = null;

            // user = this.UserLogin(tokenRequest);
            user = await this.GetLoginUser(tokenRequest);



            if (user.Status && user.ResponseData != null)
            {
                Log.Debug(LoggerMessage.GettingResponse);
                if (true)
                {

                    DateTime? currentLoginDate = null;
                    string plaintext = tokenRequest.Password;
                    //string encryptedData = this.Encryption(plaintext);

                    //List<LoginUserModel> AddUserList = new List<LoginUserModel>();
                    LoginUserModel Loginuser = new LoginUserModel
                    {

                        UserId = user.ResponseData.UserId,
                        UserName = user.ResponseData.UserName,
                        //Password = user.ResponseData.Password,
                        ProfileId = user.ResponseData.ProfileId,
                        FirstName = user.ResponseData.FirstName,
                        LastName = user.ResponseData.LastName,
                        LastLoginDate = user.ResponseData.LastLoginDate,
                        Title = user.ResponseData.Title,
                        EmailId = user.ResponseData.EmailId,
                        MobileNo = user.ResponseData.MobileNo,
                        IsResetPwd = user.ResponseData.IsResetPwd,
                        DepartmentId = user.ResponseData.DepartmentId,
                        OfficeLevelId = user.ResponseData.OfficeLevelId,
                        DivisionId = user.ResponseData.DivisionId,
                        DistrictId = user.ResponseData.DistrictId,
                        SubDivisionId = user.ResponseData.SubDivisionId,
                        TehsilId = user.ResponseData.TehsilId,
                        DesignationId = user.ResponseData.DesignationId,
                        RoleId = user.ResponseData.RoleId,
                        IsEKyc= user.ResponseData.IsEKyc,
                    };

                    //AddUserList.Add(Loginuser);


                    List<UserMenuList> AdduserRoleList = new List<UserMenuList>();
                    foreach (var item in user.ResponseData.UserRoleList)
                    {
                        UserMenuList userRole = new UserMenuList
                        {

                            Id = item.Id,
                            RoleId = item.RoleId,
                            MenuId = item.MenuId,
                            OrderIndex = item.OrderIndex,
                            MenuNameHi = item.MenuNameHi,
                            MenuNameEng = item.MenuNameEng,
                            MenuPath = item.MenuPath,
                            MenuParentId = item.MenuParentId,
                            MenuTypeId = item.MenuTypeId,
                            Class = item.Class,
                            Icon = item.Icon,

                        };

                        AdduserRoleList.Add(userRole);
                    }

                    responseModel.Token = this.GenerateAccessToken(Loginuser, plaintext, "", "", currentLoginDate, out DateTime? issueAt, out DateTime? expires, "", "");
                    responseModel.RefreshToken = this.GenerateRefreshToken(Loginuser, plaintext, out DateTime? refreshExpires);
                    responseModel.IssuedAt = issueAt;
                    responseModel.Expires = expires;
                    responseModel.RefreshTokenExpires = refreshExpires;
                    responseModel.LoginUserModel1 = Loginuser;
                    responseModel.UserMenuList = AdduserRoleList;
                }

            }


            Log.Debug(LoggerMessage.Begin);
            return responseModel;
        }

        #region OTP Varification
        public async Task<OTPmodelResponce> VerifyOTP(OTPmodel smsRequest)
        {
            string actualOTP = "123456";
            int referenceId = 987654321;

            OTPmodelResponce resOTP = new OTPmodelResponce();

            if (smsRequest.Rfrenceid == smsRequest.Rfrenceid)
            {
                string receivedOTP = smsRequest.OTP;
                bool isMatch = receivedOTP == actualOTP;

                if (isMatch)
                {

                    resOTP.Status = true;
                    resOTP.Message = "OTP Verified Successfully!";
                    return resOTP;
                }
                else
                {

                    resOTP.Status = false;
                    resOTP.Message = "Invalid OTP";
                    return resOTP;
                }
            }
            else
            {
                resOTP.Status = false;
                resOTP.Message = "Invalid OTP";
                return resOTP;
            }

        }

        #endregion
        private string GenerateAccessToken(LoginUserModel tokenRequest, string encryptedData, string allowTokenIdentifier, string tokenIdentifierExpiry, DateTime? loginTimeDB, out DateTime? issueAt, out DateTime? expires, string baseTimeZone, string selectedTimeZone)
        {
            Log.Debug(LoggerMessage.Begin);
            if (tokenRequest == null)
            {
                expires = null;
                issueAt = null;
                return null;
            }
            using RSA rsa = RSA.Create();
            SigningCredentials signingCredentials = TokenAuth.GetJwtPrivateKey(rsa);
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            var expiresTime = !string.IsNullOrEmpty(this.appSettings.AccessTokenExpireTime) ? int.Parse(this.appSettings.AccessTokenExpireTime) : 30;


            int AuthToken_TimeSpan_ValidBeforeTime = 5;

            TimeSpan timeSpan = new TimeSpan(0, 0, AuthToken_TimeSpan_ValidBeforeTime);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, tokenRequest.UserName.Trim()),
                    new Claim(APIConstants.AuthHeader, encryptedData),
                    new Claim(APIConstants.TokenType, APIConstants.AccessToken),
                    new Claim(APIConstants.UserId, tokenRequest.UserId.Trim())

                }),
                IssuedAt = DateTime.UtcNow.Subtract(timeSpan),
                NotBefore = DateTime.UtcNow.Subtract(timeSpan),
                Expires = DateTime.UtcNow.AddMinutes(expiresTime),
                SigningCredentials = signingCredentials
            };
            issueAt = DateTime.UtcNow.Subtract(timeSpan);
            expires = DateTime.UtcNow.AddMinutes(expiresTime);


            if (loginTimeDB != null)
                tokenDescriptor.Subject.AddClaim(new Claim(APIConstants.LoginTimeDB, loginTimeDB?.ToString()));


            var token = tokenHandler.CreateToken(tokenDescriptor);
            Log.Debug(LoggerMessage.End);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Generates the refresh token.
        /// </summary>
        /// <param name="tokenRequest">The token request.</param>
        /// <param name="encryptedData">The encrypted data.</param>
        /// <param name="expires">Access Token Expire Time.</param>
        /// <returns>string of random number.</returns>
        private string GenerateRefreshToken(LoginUserModel tokenRequest, string encryptedData, out DateTime? expires)
        {
            Log.Debug(LoggerMessage.Begin);
            if (tokenRequest == null)
            {
                expires = null;
                return null;
            }
            using RSA rsa = RSA.Create();
            SigningCredentials signingCredentials = TokenAuth.GetJwtPrivateKey(rsa);
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.appSettings.Secret);
            var expiresTime = !string.IsNullOrEmpty(this.appSettings.RefreshTokenExpireTime) ? int.Parse(this.appSettings.RefreshTokenExpireTime) : 60;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, tokenRequest.UserName.Trim()),
                    new Claim(APIConstants.AuthHeader, encryptedData),
                    new Claim(APIConstants.TokenType, APIConstants.RefreshToken),
                }),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiresTime),
                SigningCredentials = signingCredentials,
            };
            expires = DateTime.UtcNow.AddMinutes(expiresTime);
            var token = tokenHandler.CreateToken(tokenDescriptor);
            Log.Debug(LoggerMessage.End);
            return tokenHandler.WriteToken(token);
        }

        private async Task<ApiResult<UserLoginResponseModel>> GetLoginUser(TokenRequestModel request)
        {
            //this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<UserLoginResponseModel> responseModel = new ApiResult<UserLoginResponseModel>();
            UserLoginResponseModel data = new UserLoginResponseModel();
            TokenRequest requestmodel = new TokenRequest();
            //Check Menu Permission and Data Permission.
            UserLoginResponse response;
            requestmodel.UserName = request.UserName;
            if (request.GrantType == "RefreshToken" || request.type == "SSOLogin")
            {
                requestmodel.Password = null;
            }
            else
            {
                requestmodel.Password = request.Password;
            }
            requestmodel.type = request.type;

            response = await this.infrastructureServices.User.GetLoginUser(requestmodel);

            if (response.UserName != null && response.Password != null)
            {
                if ((requestmodel.type == "SSOLogin" || requestmodel.GrantType == "RefreshToken"))
                {

                    this._logger.LogWarning(LoggerMessage.ResponseBegin);
                    data = new UserLoginResponseModel()
                    {
                        //UserName = response.UserName,
                        //Password = response.Password,
                        //LastLoginDate = response.LastLoginDate,
                        UserId = response.UserId,
                        UserName = response.UserName,
                        Password = response.Password,
                        ProfileId = response.ProfileId,
                        FirstName = response.FirstName,
                        LastName = response.LastName,
                        LastLoginDate = response.LastLoginDate,
                        Title = response.Title,
                        EmailId = response.EmailId,
                        MobileNo = response.MobileNo,
                        IsResetPwd = response.IsResetPwd,
                        DepartmentId = response.DepartmentId,
                        OfficeLevelId = response.OfficeLevelId,
                        DivisionId = response.DivisionId,
                        DistrictId = response.DistrictId,
                        SubDivisionId = response.SubDivisionId,
                        TehsilId = response.TehsilId,
                        DesignationId = response.DesignationId,
                        RoleId = response.RoleId,
                        IsEKyc= response.IsEKyc,
                        UserRoleList = ConvertArrayListToUserRoleList(response)

                    };

                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00000;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                    responseModel.Status = true;
                    this._logger.LogWarning(LoggerMessage.ResponseEnd);


                }
                else
                {
                    if (requestmodel.Password == response.Password)
                    {

                        this._logger.LogWarning(LoggerMessage.ResponseBegin);
                        data = new UserLoginResponseModel()
                        {
                            UserId = response.UserId,
                            UserName = response.UserName,
                            Password = response.Password,
                            ProfileId = response.ProfileId,
                            FirstName = response.FirstName,
                            LastName = response.LastName,
                            LastLoginDate = response.LastLoginDate,
                            Title = response.Title,
                            EmailId = response.EmailId,
                            MobileNo = response.MobileNo,
                            IsResetPwd = response.IsResetPwd,
                            DepartmentId = response.DepartmentId,
                            OfficeLevelId = response.OfficeLevelId,
                            DivisionId = response.DivisionId,
                            DistrictId = response.DistrictId,
                            SubDivisionId = response.SubDivisionId,
                            TehsilId = response.TehsilId,
                            DesignationId = response.DesignationId,
                            RoleId = response.RoleId,
                            IsEKyc = response.IsEKyc,
                            UserRoleList = ConvertArrayListToUserRoleList(response)

                        };

                        responseModel.ResponseData = data;
                        responseModel.ErrorCode = ErrorCodes.Err00000;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                        responseModel.Status = true;
                        this._logger.LogWarning(LoggerMessage.ResponseEnd);
                    }
                    else if (requestmodel.type == "LoginOTP")
                    {
                        this._logger.LogWarning(LoggerMessage.ResponseBegin);
                        data = new UserLoginResponseModel()
                        {
                            UserId = response.UserId,
                            UserName = response.UserName,
                            Password = response.Password,
                            ProfileId = response.ProfileId,
                            FirstName = response.FirstName,
                            LastName = response.LastName,
                            LastLoginDate = response.LastLoginDate,
                            Title = response.Title,
                            EmailId = response.EmailId,
                            MobileNo = response.MobileNo,
                            IsResetPwd = response.IsResetPwd,
                            DepartmentId = response.DepartmentId,
                            OfficeLevelId = response.OfficeLevelId,
                            DivisionId = response.DivisionId,
                            DistrictId = response.DistrictId,
                            SubDivisionId = response.SubDivisionId,
                            TehsilId = response.TehsilId,
                            DesignationId = response.DesignationId,
                            RoleId = response.RoleId,
                            IsEKyc = response.IsEKyc,
                            UserRoleList = ConvertArrayListToUserRoleList(response)

                        };

                        responseModel.ResponseData = data;
                        responseModel.ErrorCode = ErrorCodes.Err00000;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
                        responseModel.Status = true;
                        this._logger.LogWarning(LoggerMessage.ResponseEnd);
                    }
                    else
                    {
                        responseModel.ResponseData = null;
                        responseModel.ErrorCode = ErrorCodes.Err00010;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00010, request.Language);
                        responseModel.Status = false;
                    }


                }
                //bool passwordsMatch = VerifyPassword(requestmodel.Password, response.salt, response.Password);
                //if (passwordsMatch == true)

            }

            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00010;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00010, request.Language);
                responseModel.Status = false;

            }

            //if (response != null)
            //{
            //    this._logger.LogWarning(LoggerMessage.ResponseBegin);
            //    responseModel.ResponseData = response;
            //    responseModel.ErrorCode = ErrorCodes.Err00000;
            //    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, request.Language);
            //    responseModel.Status = true;
            //    this._logger.LogWarning(LoggerMessage.ResponseEnd);
            //}
            //else
            //{
            //    responseModel.ResponseData = null;
            //    responseModel.ErrorCode = ErrorCodes.Err00001;
            //    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, request.Language);
            //    responseModel.Status = false;
            //}

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }

        private List<UserRoleDetails> ConvertArrayListToUserRoleList(UserLoginResponse response)
        {
            List<UserRoleDetails> userRoleList = new List<UserRoleDetails>();
            if (response.UserRoleList != null)
            {
                foreach (var item in response.UserRoleList)
                {
                    UserRoleDetails userRole = new UserRoleDetails
                    {

                        Id = item.Id,
                        RoleId = item.RoleId,
                        MenuId = item.MenuId,
                        OrderIndex = item.OrderIndex,
                        MenuNameHi = item.MenuNameHi,
                        MenuNameEng = item.MenuNameEng,
                        MenuPath = item.MenuPath,
                        MenuParentId = item.MenuParentId,
                        MenuTypeId = item.MenuTypeId,
                        Class = item.Class,
                        Icon = item.Icon


                    };

                    userRoleList.Add(userRole);
                }
            }

            return userRoleList;

        }
        public T CreateRequest<T>(RequestModelBase model, out bool isValid, out string errorCode)
            where T : DomainRequestModelBase, new()
        {
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);

                isValid = true;
                errorCode = "";

                var request = new T()
                {
                    AuthHeader = model.AuthHeader,
                    UID = model.UserId,
                    UserOfficeId = model.UserOfficeId,
                    UserRoleId = model.UserRoleId,
                    
                };



                this._logger.LogDebug(LoggerMessage.End);
                return request;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage + ex);
                throw;
            }
        }


        public async Task<GSTDataModel> GetGSTDetails(string gstNumber)
        {

            GSTDataModel dataModel = null;
            try
            {
                // Set the security protocol to Tls12
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // Build the URL
                
                string client_id = "10rt110d-5d5f-4a19-a468-3b247bc770ad";
                string client_secret = "p2m38cd1-ad8d-47b0-b1a2-245ce7ff7890";

                string url = $"https://api.mastergst.com/public/search?email=bisen.indra@mapit.gov.in&gstin={gstNumber}";

                // Create the request
                var webrequest = (HttpWebRequest)WebRequest.Create(url);
                webrequest.Headers.Add("client_id", client_id);
                webrequest.Headers.Add("client_secret", client_secret);

                // Get the response
                using (var response = webrequest.GetResponse())
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();

                    // Deserialize the response
                    dataModel = Newtonsoft.Json.JsonConvert.DeserializeObject<GSTDataModel>(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dataModel;

        }

        public async Task<ResultPAN> VerifyPAN(PANRequestModel pANRequestModel)
        {

            try
            {
                var apiUrl = "https://apiseva.mp.gov.in:8243/PANServices/1";
                var apiKey = "eyJ4NXQjUzI1NiI6Ik9EQm1NVFUwWmpKak9ESmtOR1kxWlRobVlqazJZVFl6TnpSall6SXpNVEJsWWpFME1XSmtZMkl4TmpnM09EZGpZV1E1WldaaE5XSTBOREEzWmpNNE5RPT0iLCJraWQiOiJnYXRld2F5X2NlcnRpZmljYXRlX2FsaWFzIiwidHlwIjoiSldUIiwiYWxnIjoiUlMyNTYifQ==.eyJzdWIiOiJOUkVEX1BvcnRhbEBjYXJib24uc3VwZXIiLCJhcHBsaWNhdGlvbiI6eyJvd25lciI6Ik5SRURfUG9ydGFsIiwidGllclF1b3RhVHlwZSI6bnVsbCwidGllciI6IlVubGltaXRlZCIsIm5hbWUiOiJEZWZhdWx0QXBwbGljYXRpb24iLCJpZCI6MTcsInV1aWQiOiJjOGM3NjQyOC1iYzI5LTRlMDktOGFhMS0yYmQxMWQyNTk4NWYifSwiaXNzIjoiaHR0cHM6XC9cL2FwaXNldmEubXAuZ292LmluOjk0NDNcL29hdXRoMlwvdG9rZW4iLCJ0aWVySW5mbyI6eyJVbmxpbWl0ZWQiOnsidGllclF1b3RhVHlwZSI6InJlcXVlc3RDb3VudCIsImdyYXBoUUxNYXhDb21wbGV4aXR5IjowLCJncmFwaFFMTWF4RGVwdGgiOjAsInN0b3BPblF1b3RhUmVhY2giOmZhbHNlLCJzcGlrZUFycmVzdExpbWl0IjowLCJzcGlrZUFycmVzdFVuaXQiOiJzZWMifX0sImtleXR5cGUiOiJQUk9EVUNUSU9OIiwic3Vic2NyaWJlZEFQSXMiOlt7InN1YnNjcmliZXJUZW5hbnREb21haW4iOiJjYXJib24uc3VwZXIiLCJuYW1lIjoiUEFOIFNlcnZpY2VzIiwiY29udGV4dCI6IlwvUEFOU2VydmljZXNcLzEiLCJwdWJsaXNoZXIiOiJhZG1pbiIsInZlcnNpb24iOiIxIiwic3Vic2NyaXB0aW9uVGllciI6IlVubGltaXRlZCJ9XSwidG9rZW5fdHlwZSI6ImFwaUtleSIsImlhdCI6MTc0MDA1MzMyMCwianRpIjoiMDE4Yzk1ODUtM2Q0Mi00OTBhLWE5MDItZWM3MDdlNGQ4OGM4In0=.Os2qtkdaoXEk_ev6plVWC8qBtLFQrOkQSTE4Va6lPEkEYrwPt9K6Cd_vLSr8DrXXG9IUIYlBdfaaXnKnMYqo7xeMz0s31GRRCt34vifyj3VfHTQwEaY0aTJO2Zm6TwCYS90QWw0sDePfuRMe3bfwd8OIPXuQGV0WZ8ClRZCBKCOUKdrwMn9w3ko8nmnhQQfCCeZIuSZ0MykixokPevXrOeHmCLUV_OSOOSeEJ1lUzPJB431p40EwHLRx0_k75qLYI2OrdZ9i2ISowg8L62jj92S97__wqKtI548qFd0iSbql7ViaBjI_Y5diftyGUUTSklpb563zKAyM4cgMnuEvKg==";
                var Data = "";
                string inputDate = "";
                // Define an array of possible formats




                // Convert to desired format
                var requestData = new
                {
                    pan = pANRequestModel.PAN,
                    name = pANRequestModel.Name,
                    fathername = pANRequestModel.Fathername,
                    dob = "01/01/2020"
                };
                var clientRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
                clientRequest.Method = "POST";
                clientRequest.ContentType = "application/json";
                clientRequest.Headers.Add("ApiKey", apiKey);
                string responseString = null;
                var requestJson = JsonConvert.SerializeObject(requestData);
                using (var streamWriter = new StreamWriter(clientRequest.GetRequestStream()))
                {
                    streamWriter.WriteAsync(requestJson);
                }
                using (var clientResponse = clientRequest.GetResponse())
                {
                    using (var reader = new StreamReader(clientResponse.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                        var apiResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
                        string responseCode = apiResponse.response_Code;
                        if (responseCode == "1")
                        {
                            PANOutputData panData = apiResponse.outputData[0].ToObject<PANOutputData>();

                            return new ResultPAN { Status = true, Message = responseCode, data = panData };
                        }
                        else if (responseCode == "2")
                        {
                            return new ResultPAN { Status = false, Message = "System Error" };
                        }
                        else if (responseCode == "3")
                        {
                            return new ResultPAN { Status = false, Message = "Authentication Failure" };
                        }
                        else if (responseCode == "4")
                        {
                            return new ResultPAN { Status = false, Message = "User not authorized " };
                        }
                        else if (responseCode == "5")
                        {
                            return new ResultPAN { Status = false, Message = "No PANs Entered or Number of PANs exceeds the limit" };
                        }
                        else if (responseCode == "6")
                        {
                            return new ResultPAN { Status = false, Message = "User validity has expired" };
                        }
                        else if (responseCode == "8")
                        {
                            return new ResultPAN { Status = false, Message = "Not enough balance." };
                        }
                        else
                        {
                            return new ResultPAN { Status = false, Message = "unable to get the response from API with the status code " + responseCode, data = responseCode };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResultPAN { Status = false, Message = "An error occurred while fetching PAN details.", data = ex.Message };
            }

        }
    }

}
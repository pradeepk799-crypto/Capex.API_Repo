using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Capex.Business.Common;
using Capex.Business.Interfaces;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Microsoft.AspNetCore.Identity;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Services;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;

namespace Capex.Business.Services
{
    public class LoginService : ILoginService
    {
        /// <summary>
        /// The application settings.
        /// </summary>
        private readonly AppSettings appSettings;
        private readonly IInfrastructureServices infrastructureServices;
        private readonly Interfaces.IUser user;
        private readonly ILogger<LoginService> _logger;
        private readonly LdapURL _Ldurl;
        private readonly IBusinessServices businessServices;
        private readonly Interfaces.ICommon _icommon;

        private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

        public LoginService(IOptions<AppSettings> appSettings, IInfrastructureServices infrastructureServices, IBusinessServices businessServices,
           Interfaces.ICommon common, ILogger<LoginService> logger, IOptions<LdapURL> options, IPasswordHasher<ApplicationUser> passwordHasher)
        {
            this.appSettings = appSettings.Value;
            this._logger = logger;
            _Ldurl = options.Value;
            this.infrastructureServices = infrastructureServices;
            this.businessServices = businessServices;
            this._icommon = common;
            this._passwordHasher = passwordHasher;



        }
        /// <summary>
        /// Tokens the specified token request.
        /// </summary>
        /// <param name="tokenRequest">The token request.</param>
        /// <returns>TokenResponseModel.</returns>
        public async Task<ApiResult<TokenResponseModel>> Token(TokenRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            var responseModel = new ApiResult<TokenResponseModel>();
            ApplicationUser applicationUser = null;
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(tokenRequest.UserName) ||
                    string.IsNullOrWhiteSpace(tokenRequest.Password))
                {
                    return BuildErrorResponse(ErrorCodes.Err100001, ErrorCodes.Err100033, tokenRequest?.Language);
                }

                if (string.IsNullOrWhiteSpace(tokenRequest.Captcha))
                {
                    return BuildErrorResponse(ErrorCodes.Err00025, ErrorCodes.Err00025, tokenRequest?.Language);
                }

                // Verify Captcha
                var captchaRequest = new CaptchaRequestModel
                {
                    Captcha = tokenRequest.Captcha,
                    Cipher = tokenRequest.Cipher
                };

                var captchaResponse = _icommon.VerifyCaptcha(captchaRequest);
                if (!captchaResponse.Status)
                {
                    return BuildErrorResponse(ErrorCodes.Err00025, ErrorCodes.Err00025, tokenRequest?.Language);
                }

                // Generate token
                responseModel = await GenerateTokens(tokenRequest);
                var loginUser = responseModel.ResponseData?.LoginUserModel;

                if (loginUser == null || !VerifyPassword(applicationUser, loginUser.PasswordHash, tokenRequest.Password))
                {
                    return BuildErrorResponse(ErrorCodes.Err0065, ErrorCodes.Err0065, tokenRequest.Language);
                }

                responseModel.Status = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
                return BuildErrorResponse(ErrorCodes.Err00001, ErrorCodes.Err00001, tokenRequest?.Language);
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        private ApiResult<TokenResponseModel> BuildErrorResponse(string errorCode, string messageCode, string language)
        {
            return new ApiResult<TokenResponseModel>
            {
                ResponseData = null,
                ErrorCode = errorCode,
                Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, messageCode, language),
                Status = false
            };
        }


        public bool VerifyPassword(ApplicationUser user, string password, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, password, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
        private async Task<ApiResult<TokenResponseModel>> GenerateTokens(TokenRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            ApiResult<TokenResponseModel> responseModel = new ApiResult<TokenResponseModel>();

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


                    UserApplicationInfoRequestModel Loginuser = new UserApplicationInfoRequestModel
                    {

                        LoginId = user.ResponseData.LoginId,
                        ProfileId = user.ResponseData.ProfileId,
                        ApplicationId = user.ResponseData.ApplicationId,
                        ApplicationNumber = user.ResponseData.ApplicationNumber,
                        EmailId = user.ResponseData.EmailId,
                        RoleId = user.ResponseData.RoleId,
                        LoginTypeId = user.ResponseData.LoginTypeId,
                        GSTNumber = user.ResponseData.GSTNumber,
                        PANNumber = user.ResponseData.PANNumber,
                        COEName = user.ResponseData.COEName,
                        NodalOfficerName = user.ResponseData.NodalOfficerName,
                        NodalOfficerDesignation = user.ResponseData.NodalOfficerDesignation,
                        UserTypeId = user.ResponseData.UserTypeId,
                        PasswordHash = user.ResponseData.PasswordHash,
                    };

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


                    TokenResponseModel tokenResponse = new TokenResponseModel
                    {

                        Token = this.GenerateAccessToken(Loginuser, plaintext, "", "", currentLoginDate, out DateTime? issueAt, out DateTime? expires, "", ""),
                        RefreshToken = this.GenerateRefreshToken(Loginuser, plaintext, out DateTime? refreshExpires),
                        IssuedAt = issueAt,
                        Expires = expires,
                        RefreshTokenExpires = refreshExpires,
                        LoginUserModel = Loginuser,
                        UserMenuList = AdduserRoleList,
                    };

                    responseModel.ResponseData = tokenResponse;
                    responseModel.ErrorCode = ErrorCodes.Err00017;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00017, tokenRequest.Language);
                    responseModel.Status = true;

                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = !string.IsNullOrWhiteSpace(user.Info_Code) ? user.Info_Code : ErrorCodes.Err00016;
                    responseModel.Message = !string.IsNullOrWhiteSpace(user.Info_Message) ? user.Info_Message : UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00016, tokenRequest.Language);
                    responseModel.Status = false;
                }
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00010;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00010, tokenRequest.Language);
                responseModel.Status = false;
                Log.Debug(LoggerMessage.GettingErrorResponse);
            }

            Log.Debug(LoggerMessage.Begin);

            return responseModel;
        }

        private ApiResult<TokenResponseModel> CheckConcurrentlogin(string key, string tokenId, string userAgent, string language)
        {
            ApiResult<TokenResponseModel> responseModel = new ApiResult<TokenResponseModel>();
            DeviceInfoResponseModel deviceInfoResponse = new DeviceInfoResponseModel();
            deviceInfoResponse = DeviceInformation.GetDeviceInfo(userAgent);
            deviceInfoResponse.TokenId = tokenId;
            var redislogininfo = RedisHelper.GetData<DeviceInfoResponseModel>(key);
            if (redislogininfo != null)
            {
                if (Convert.ToString(redislogininfo.TokenId) != deviceInfoResponse.TokenId)
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00058;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00058, language);
                    responseModel.Message = responseModel.Message + Environment.NewLine + redislogininfo.DeviceType + '/'
                        + redislogininfo.BrowserName + '/' + redislogininfo.OSName;
                    responseModel.Status = false;
                }
                else
                {
                    if (redislogininfo.IsLoggedIn == true)
                    {
                        if (Convert.ToString(redislogininfo.MacId) != deviceInfoResponse.MacId)
                        {
                            responseModel.ResponseData = null;
                            responseModel.ErrorCode = ErrorCodes.Err00059;
                            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00059, language);
                            responseModel.Message = responseModel.Message + Environment.NewLine + redislogininfo.DeviceType + '/'
                        + redislogininfo.BrowserName + '/' + redislogininfo.OSName;
                            responseModel.Status = false;
                        }
                    }
                    else
                    {
                        deviceInfoResponse.IsLoggedIn = true;
                        RedisHelper.SetData<DeviceInfoResponseModel>(key, deviceInfoResponse, DateTimeOffset.Now.AddMinutes(10));
                        responseModel.ResponseData = null;
                        responseModel.ErrorCode = ErrorCodes.Err00000;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, language);
                        responseModel.Status = true;
                    }
                }
            }
            else
            {
                deviceInfoResponse.IsLoggedIn = true;
                RedisHelper.SetData<DeviceInfoResponseModel>(key, deviceInfoResponse, DateTimeOffset.Now.AddMinutes(10));
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00000;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00000, language);
                responseModel.Status = true;
            }
            return responseModel;
        }
        public string GenerateAccessToken(UserApplicationInfoRequestModel tokenRequest, string encryptedData, string allowTokenIdentifier, string tokenIdentifierExpiry, DateTime? loginTimeDB, out DateTime? issueAt, out DateTime? expires, string baseTimeZone, string selectedTimeZone)
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
                    new Claim(ClaimTypes.Email, tokenRequest.EmailId.Trim()),
                    new Claim(APIConstants.AuthHeader, encryptedData),
                    new Claim(APIConstants.TokenType, APIConstants.AccessToken),
                    new Claim(APIConstants.UserId, tokenRequest.LoginId.ToString().Trim()),
                    new Claim(APIConstants.UserRoleId, tokenRequest.RoleId.ToString().Trim())

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
        public string GenerateRefreshToken(UserApplicationInfoRequestModel tokenRequest, string encryptedData, out DateTime? expires)
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
                    new Claim(ClaimTypes.Email, tokenRequest.EmailId.Trim()),
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
        public string Encryption(string strText)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var sr = new System.IO.StringReader(AppSettings.Current.PublicKey);

                    // we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    // get the object back from the stream
                    var pubKey = (RSAParameters)xs.Deserialize(sr);
                    rsa.ImportParameters(pubKey);

                    // for encryption, always handle bytes...
                    var bytesPlainTextData = System.Text.Encoding.Unicode.GetBytes(strText);

                    // apply pkcs#1.5 padding and encrypt our data.
                    var bytesCypherText = rsa.Encrypt(bytesPlainTextData, false);

                    // we might want a string representation of our cypher text... base64 will do
                    var cypherText = Convert.ToBase64String(bytesCypherText);

                    return cypherText;
                }
                catch (Exception ex)
                {
                    Log.Error(LoggerMessage.ErrorMessage + ex);
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public string Decryption(string strText)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var sr = new System.IO.StringReader(AppSettings.Current.PrivateKey);

                    // we need a deserializer
                    var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

                    // get the object back from the stream
                    var prtKey = (RSAParameters)xs.Deserialize(sr);
                    rsa.ImportParameters(prtKey);

                    // first, get our bytes back from the base64 string ...
                    var bytesCypherText = Convert.FromBase64String(strText);

                    var decryptedBytes = rsa.Decrypt(bytesCypherText, false);
                    var decryptedData = System.Text.Encoding.Unicode.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(LoggerMessage.ErrorMessage + ex);
                    throw;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        //public UserLoginResponseModel UserLogin(TokenRequestModel request)
        //{
        //    Log.Debug(LoggerMessage.Begin);
        //    int messageId = 5002;          
        //     return new UserLoginResponseModel();
        //}
        public async Task<ApiResult<UserApplicationInfoRequestModel>> GetLoginUser(TokenRequestModel request)
        {
            ApiResult<UserApplicationInfoRequestModel> responseModel = new ApiResult<UserApplicationInfoRequestModel>();
            UserApplicationInfoRequestModel data = new UserApplicationInfoRequestModel();
            TokenRequest requestmodel = new TokenRequest();
            //Check Menu Permission and Data Permission.
            UserApplicationInfoDomainRequestModel response;
            requestmodel.UserName = request.UserName;
            requestmodel.Password = request.Password;
            requestmodel.type = Convert.ToString(request.LoginType);

            response = await this.infrastructureServices.User.GetLoginDetails(requestmodel);

            if (response.EmailId != null)
            {
                data = new UserApplicationInfoRequestModel()
                {
                    LoginId = response.LoginId,
                    EmailId = response.EmailId,
                    RoleId = response.RoleId,
                    LoginTypeId = response.LoginTypeId,
                    ProfileId = response.ProfileId,
                    ApplicationId = response.ApplicationId,
                    ApplicationNumber = response.ApplicationNumber,
                    GSTNumber = response.GSTNumber,
                    PANNumber = response.PANNumber,
                    COEName = response.COEName,
                    NodalOfficerName = response.NodalOfficerName,
                    NodalOfficerDesignation = response.NodalOfficerDesignation,
                    UserTypeId = response.UserTypeId,
                    PasswordHash = response.PasswordHash,
                    UserRoleList = ConvertArrayListToUserRoleListNew(response),
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



            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Combine password and salt, then compute the hash
                byte[] combinedBytes = Encoding.UTF8.GetBytes(password + salt);
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);

                // Convert the hash to a hexadecimal string
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
        static bool VerifyPassword(string providedPassword, string salt, string hashedPassword)
        {
            // Hash the provided password with the same salt
            string hashedProvidedPassword = HashPassword(providedPassword, salt);

            // Compare the stored hashed password with the newly hashed provided password
            return string.Equals(hashedProvidedPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
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
                        Icon = item.Icon,
                        IsHiddenAction = item.IsHiddenAction


                    };

                    userRoleList.Add(userRole);
                }
            }

            return userRoleList;

        }
        private List<UserRoleDetails> ConvertArrayListToUserRoleListNew(UserApplicationInfoDomainRequestModel response)
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
                        Icon = item.Icon,
                        IsHiddenAction = item.IsHiddenAction


                    };

                    userRoleList.Add(userRole);
                }
            }

            return userRoleList;

        }
        public async Task<ApiResult<ValidUserResponseModel>> GetValidUser(ValidUserRequestModel request)
        {
            //this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            ValidUserResponseModel data = new ValidUserResponseModel();
            ValidUserRequest requestmodel = new ValidUserRequest();
            ValidUserResponse response;

            requestmodel.UserName = request.UserName;
            requestmodel.Mobno = request.Mobno;
            requestmodel.flag = request.flag;
            requestmodel.TemplateId = request.TemplateId;

            CaptchaRequestModel reqData = new CaptchaRequestModel();

            reqData.Captcha = request.Captcha;
            reqData.Cipher = request.Cipher;
            ApiResult<CaptchaResponseModel> responsedata = new ApiResult<CaptchaResponseModel>();
            responsedata = this._icommon.VerifyCaptcha(reqData);
            if (responsedata.Status)
            {
                response = await this.infrastructureServices.User.GetValidUser(requestmodel);

                if (response.Status == true)
                {
                    this._logger.LogWarning(LoggerMessage.ResponseBegin);

                    SendSMSRequestModel SMSrequest = new SendSMSRequestModel
                    {
                        MobileNumber = request.Mobno,
                        TemplateId = request.TemplateId,

                    };

                    //  SMSResponseModel responseOtp = await businessServices.Masters.SendSMS(SMSrequest);
                    //  data = new ValidUserResponseModel()
                    //  {
                    //      Msg = responseOtp.Message,
                    //      Status = responseOtp.Status,
                    //      Data = responseOtp.Data,


                    //  };
                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00016;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00016, request.Language);
                    responseModel.Status = true;
                    this._logger.LogWarning(LoggerMessage.ResponseEnd);
                }
                else
                {


                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00003;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00003, request.Language);
                    responseModel.Status = false;
                }

            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00025;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00025, request.Language);
                responseModel.Status = false;
            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<ValidUserResponseModel>> ChangeUserPwd(ChangePwsRequestModel request)
        {
            //this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ValidUserResponseModel> responseModel = new ApiResult<ValidUserResponseModel>();
            ValidUserResponseModel data = new ValidUserResponseModel();
            ChangePasswordRequest requestmodel = new ChangePasswordRequest();
            //Check Menu Permission and Data Permission.
            ValidUserResponse response;

            CaptchaRequestModel reqData = new CaptchaRequestModel();

            reqData.Captcha = request.Captcha;
            reqData.Cipher = request.Cipher;
            ApiResult<CaptchaResponseModel> responsedata = new ApiResult<CaptchaResponseModel>();
            responsedata = this._icommon.VerifyCaptcha(reqData);

            if (responsedata.Status)
            {


                if (request.flag == 1)
                {
                    requestmodel.UserName = request.UserName;

                }
                else
                {
                    requestmodel.UserID = request.UID;
                }

                requestmodel.NewPassword = request.NewPassword;
                requestmodel.OldPassword = request.OldPassword;
                requestmodel.flag = request.flag;

                if (request.NewPassword != request.ConfirmPassword)
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00004;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00004, request.Language);
                    responseModel.Status = false;
                }
                else if (request.NewPassword == request.OldPassword)
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00005;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00005, request.Language);
                    responseModel.Status = false;

                }
                else
                {
                    response = await this.infrastructureServices.User.ChangeUserPassword(requestmodel);

                    if (response.Status == true)
                    {
                        this._logger.LogWarning(LoggerMessage.ResponseBegin);
                        responseModel.ResponseData = data;
                        responseModel.ErrorCode = ErrorCodes.Err00041;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00041, request.Language);
                        responseModel.Status = true;
                        this._logger.LogWarning(LoggerMessage.ResponseEnd);
                    }
                    else
                    {
                        if (response.Msg == "Err00007")
                        {
                            responseModel.ResponseData = null;
                            responseModel.ErrorCode = ErrorCodes.Err00007;
                            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00007, request.Language);
                            responseModel.Status = false;
                        }
                        else if (response.Msg == "Err00006")
                        {
                            responseModel.ResponseData = null;
                            responseModel.ErrorCode = ErrorCodes.Err00007;
                            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00006, request.Language);
                            responseModel.Status = false;
                        }

                    }
                }
            }
            else
            {
                responseModel.ResponseData = null;
                responseModel.ErrorCode = ErrorCodes.Err00025;
                responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00025, request.Language);
                responseModel.Status = false;

            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<ForgotPasswordResponseModel>> ForGotUserPwd(CitizenForgotPwdModel request)
        {
            //this._logger.LogDebug(LoggerMessage.Begin);
            ApiResult<ForgotPasswordResponseModel> responseModel = new ApiResult<ForgotPasswordResponseModel>();
            ForgotPasswordResponseModel data = new ForgotPasswordResponseModel();
            CitizenForgotPwdRequest requestmodel = new CitizenForgotPwdRequest();
            CitizenForgotPwdResponse response;
            requestmodel.UserName = request.UserName;
            requestmodel.type = request.type;

            CaptchaRequestModel reqData = new CaptchaRequestModel();

            reqData.Captcha = request.Captcha;
            reqData.Cipher = request.Cipher;
            ApiResult<CaptchaResponseModel> responsedata = new ApiResult<CaptchaResponseModel>();
            if (!requestmodel.type.Equals("ResendOTP"))
            {
                responsedata = this._icommon.VerifyCaptcha(reqData);
                if (responsedata.Status)
                {
                    response = await this.infrastructureServices.User.ForgotUserPassword(requestmodel);

                    if (response.MobileNo != null && response.MobileNo.Length == 10)
                    {
                        this._logger.LogWarning(LoggerMessage.ResponseBegin);
                        SendSMSRequestModel SMSrequest = new SendSMSRequestModel
                        {
                            MobileNumber = response.MobileNo,
                            TemplateId = 1,
                        };

                        data.RefrenceId = Convert.ToInt64(response.MobileNo);
                        data.UserName = request.UserName;

                        //data = new ValidUserResponseModel()
                        //{
                        //    Msg = responseOtp.Message,
                        //    Status = responseOtp.Status,
                        //    Data = responseOtp.Data,
                        //};
                        responseModel.ResponseData = data;
                        responseModel.ErrorCode = ErrorCodes.Err00016;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00016, request.Language);
                        responseModel.Status = true;
                        this._logger.LogWarning(LoggerMessage.ResponseEnd);
                    }
                    else
                    {
                        if (request.type == "FPwd")
                        {
                            responseModel.ResponseData = null;
                            responseModel.ErrorCode = ErrorCodes.Err00020;
                            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00020, request.Language);
                            responseModel.Status = false;

                        }
                        else
                        {
                            responseModel.ResponseData = null;
                            responseModel.ErrorCode = ErrorCodes.Err00003;
                            responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00003, request.Language);
                            responseModel.Status = false;
                        }
                    }

                }
                else
                {
                    responseModel.ResponseData = null;
                    responseModel.ErrorCode = ErrorCodes.Err00025;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00025, request.Language);
                    responseModel.Status = false;
                }

            }
            else
            {

                response = await this.infrastructureServices.User.ForgotUserPassword(requestmodel);

                if (response.MobileNo != null && response.MobileNo.Length == 10)
                {
                    this._logger.LogWarning(LoggerMessage.ResponseBegin);
                    SendSMSRequestModel SMSrequest = new SendSMSRequestModel
                    {
                        MobileNumber = response.MobileNo,
                        TemplateId = 1,
                    };

                    data.RefrenceId = Convert.ToInt64(response.MobileNo);
                    responseModel.ResponseData = data;
                    responseModel.ErrorCode = ErrorCodes.Err00016;
                    responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00016, request.Language);
                    responseModel.Status = true;
                    this._logger.LogWarning(LoggerMessage.ResponseEnd);
                }
                else
                {
                    if (request.type == "FPwd")
                    {
                        responseModel.ResponseData = null;
                        responseModel.ErrorCode = ErrorCodes.Err00020;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00020, request.Language);
                        responseModel.Status = false;

                    }
                    else
                    {
                        responseModel.ResponseData = null;
                        responseModel.ErrorCode = ErrorCodes.Err00003;
                        responseModel.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00003, request.Language);
                        responseModel.Status = false;
                    }
                }


            }

            this._logger.LogDebug(LoggerMessage.End);
            return responseModel;
        }
        public async Task<ApiResult<UserForgotPasswordResponseModel>> ForgotPassword(ForgotPasswordRequestModel requestModel)
        {
            var responseModel = new ApiResult<UserForgotPasswordResponseModel>
            {
                ResponseData = new UserForgotPasswordResponseModel(),
                Status = false
            };

            if (requestModel != null)
            {
                ApplicationUser applicationUser= null;
                string _passwordHash = _passwordHasher.HashPassword(applicationUser, requestModel.Password);
                var obj = new ForgotPasswordDomainRequestModel
                {
                    Password = _passwordHash,
                    Type = requestModel.Type,
                    MobileNumber = requestModel.MobileNumber,

                };
                ApiResult<UserForgotPasswordDomainResponseModel> apiResult = new ApiResult<UserForgotPasswordDomainResponseModel>();
                apiResult = await this.infrastructureServices.User.ForgotPassword(obj);

                if (apiResult?.ResponseData.Status == true)
                {
                    responseModel.ResponseData.Response = apiResult.ResponseData.Status;
                    responseModel.Status = true;
                }
            }
            return responseModel;
        }

    }


}

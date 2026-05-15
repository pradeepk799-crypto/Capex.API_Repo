using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Capex.Business.Interfaces;
using Capex.DomainModels.DomainRequestModel;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.ResponseModel.Masters;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using YamlDotNet.Core.Tokens;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using static System.Net.WebRequestMethods;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel;

namespace Capex.Business.Services
{
    public class Notification : INotification
    {
        private readonly IInfrastructureServices infrastructureServices;
        private readonly ILogger<Notification> _logger;
        private readonly Capex.Utilities.Common.EmailNotification EmailNotification;
        private readonly Capex.Utilities.Common.SMSNotification SMSNotification;
        private readonly Capex.Utilities.Common.WhatsAppNotification WhatsAppNotification;
        private readonly ServiceConfigSettings _service;
        private List<SMSKeys> _smsKeys;
        private List<EmailKeys> _emailKeys;
        private List<WhatsAppKeys> _whatsAppKeys;

        public Notification(IInfrastructureServices _infrastructureServices, ILogger<Notification> logger,
            Capex.Utilities.Common.EmailNotification emailNotification, SMSNotification sMSNotification, WhatsAppNotification whatsAppNotification)
        {
            this.infrastructureServices = _infrastructureServices;
            this._logger = logger;
            this.EmailNotification = emailNotification;
            this.SMSNotification = sMSNotification;
            this.WhatsAppNotification = whatsAppNotification;
            this._service = ServiceConfiguration.serviceConfigSettings;
            this._smsKeys = _service.Services.Where(x => x.ServiceName == "SMS").FirstOrDefault().SmsKeys;
            this._emailKeys = _service.Services.Where(x => x.ServiceName == "Email").FirstOrDefault().EmailKeys;
            this._whatsAppKeys = _service.Services.Where(x => x.ServiceName == "WhatsApp").FirstOrDefault().WhatsAppKeys;

        }
        public async Task<ApiResult<string>> SendMail(dynamic obj, int templateId)
        {
            Result<string> result = new Result<string>();
            ApiResult<string> response = new ApiResult<string>();
            //var emaiModel = (EmailModel)obj;
            try
            {
                MessagesNotificationModel template = await GetTemplateData(templateId);
                if (template != null)
                {
                    template.Body = ReplaceContent(template.Body, obj);
                    SendEmailRquestModel emailModel = new SendEmailRquestModel
                    {
                        Recipient = _emailKeys[0].EmailId,
                        Message = template.Body,
                        Subject = template.Subject,
                        EnableEmail = _emailKeys[0].EnableEmail,
                        SenderEmail_Id = _emailKeys[0].EmailId,
                        SenderPassword = _emailKeys[0].EmailPassword,
                        MailServer = _emailKeys[0].MailServer,
                        Port = _emailKeys[0].Port
                        //FilePath = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.FullName,
                    };
                    //result = EmailNotification.SendEmail(emailModel);
                    result = EmailNotification.SendEmail(emailModel);

                    if (result != null)
                    {
                        if (result.Status == true)
                        {
                            response.Status = result.Status;
                            response.Message = result.Message[0];
                            response.ErrorCode = ErrorCodes.Err00000;
                            response.ResponseData = null;
                        }
                        else
                        {
                            response.Status = result.Status;
                            response.Message = result.Message[0];
                            response.ErrorCode = ErrorCodes.Err00001;
                            response.ResponseData = null;
                        }
                    }
                    else
                    {
                        response.Status = result.Status;
                        response.Message = result.Message[0];
                        response.ErrorCode = ErrorCodes.Err00001;
                        response.ResponseData = null;
                    }
                }
                else
                {
                    response.Status = false;
                    response.Message = UserMessageUtility.GetMessage(MessagesEnum.ErrorMessage, ErrorCodes.Err00001, "");
                    response.ErrorCode = ErrorCodes.Err00001;
                    response.ResponseData = null;
                }
            }
            catch (Exception ex)
            {
                response.ResponseData = null;
                response.ErrorCode = ErrorCodes.Err00001;
                response.Message = ex.Message.ToString();
                response.Status = false;
            }
            return response;
        }

        public async Task<Result<dynamic>> SendWhatsApp(dynamic obj, int templateId)
        {
            Result<dynamic> result = new Result<dynamic>();

            try
            {
                MessagesNotificationModel template = await GetTemplateData(templateId);
                if (template != null)
                {
                    template.Body = ReplaceContent(template.Body, obj);

                    try
                    {
                        WhatsAppModel WhatsAppTo = new WhatsAppModel
                        {
                            Mobile = obj.MobileNumber,
                            Message = template.Body,
                            WhatsappURL = _whatsAppKeys[0].WhatsappURL,
                            WhatsAppUserid = _whatsAppKeys[0].WhatsAppUserid,
                            WhatsAppPwd = _whatsAppKeys[0].WhatsAppPwd
                        };
                        result = WhatsAppNotification.SendWhatsApp(WhatsAppTo);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<Result<dynamic>> SendWhatsAppOptInOut(string MobileNo, string Type)
        {
            Result<dynamic> result = new Result<dynamic>();
            try
            {
                WhatsAppModelOptINOUT WhatsAppTo = new WhatsAppModelOptINOUT
                {
                    Mobile = MobileNo,
                    WhatsappURL = _whatsAppKeys[0].WhatsappURL,
                    WhatsAppUserid = _whatsAppKeys[0].WhatsAppUserid,
                    WhatsAppPwd = _whatsAppKeys[0].WhatsAppPwd,
                    Type = Type
                };
                result = WhatsAppNotification.SendWhatsAppOptInOut(WhatsAppTo);
            }
            catch (Exception)
            {
            }
            return result;
        }
        public async Task<int> PreParePushMessageNotification(dynamic obj, int templateId)
        {
            int task = 0;
            try
            {
                MessagesNotificationModel templates = await GetTemplateData(templateId);
                if (templates != null)
                {
                    if (templates.Query != null)
                    {
                        //string ss = "{\"ApplicantsDetails\":[{\"ApplicationId\":358,\"ApplicantName\":\"Vipul Chaudhary Jaat\",\"MobileNo\":\"9999637462\",\"Email\":\"Vipul1234@gmail.com\"},{\"ApplicationId\":358,\"ApplicantName\":\"Sudhir Singh\",\"MobileNo\":\"9999637462\",\"Email\":\"Sudhir1234@gmail.com\"}]}";
                        //dynamic jsonData = await this.infrastructureServices.MessageNotification.GetTemplateQueryData(obj.ApplicationId, templates.Query);

                        dynamic jsonData = await this.infrastructureServices.MessageNotification.GetTemplateQueryData(obj.ApplicationId, templates.Query);

                        //jsonData = "{\"ApplicantsDetails\":[{\"ApplicationId\":358,\"ApplicantName\":\"VipulChaudhary\",\"MobileNo\":\"9999637468\",\"Email\":\"Sudhir1234@gmail.com\"},{\"ApplicationId\":358,\"ApplicantName\":\"SudhirSingh\",\"MobileNo\":\"9999637464\",\"Email\":\"Sudhir@gmail.com\"}],\"NonApplicantsDetails\":[{\"ApplicationId\":358,\"ApplicantName\":\"BiliJaat\",\"MobileNo\":\"9999637460\",\"Email\":\"Bili1234@gmail.com\"},{\"ApplicationId\":358,\"ApplicantName\":\"RamSingh\",\"MobileNo\":\"9999637467\",\"Email\":\"R@gmail.com\"}]}";
                        JObject data = JObject.Parse(jsonData);
                        if (data != null)
                        {
                            task = await PreParePushMessageNotificationWithQuery(templates, data);
                        }
                    }
                    else
                    {
                        templates.Body = ReplaceContent(templates.Body, obj);
                        PushMessageNotificationModel pushMessageNotificationModel = new PushMessageNotificationModel();
                        pushMessageNotificationModel.Subject = templates.Subject;
                        pushMessageNotificationModel.Body = templates.Body;
                        pushMessageNotificationModel.RecipientPhoneNumber = obj.MobileNo;
                        pushMessageNotificationModel.RecipientEmail = obj.Email;
                        pushMessageNotificationModel.TemplateTypeId = templates.TemplateTypeId;
                        pushMessageNotificationModel.CreateBy = obj.CreateBy;
                        task = await PushMessageNotification(pushMessageNotificationModel);
                    }
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                task = 0;
            }
            return task;
        }
        private async Task<int> PreParePushMessageNotificationWithQuery(MessagesNotificationModel templates, JObject data)
        {
            int task = 0;
            string templatebody = templates.Body;
            try
            {
                var properties = data.Properties();
                for (int i = 0; i < properties.Count(); i++)
                {
                    JProperty property = properties.ElementAt(i);
                    if (data[property.Name] != null && data[property.Name].Type == JTokenType.Array)
                    {
                        var arrayKey = property.Name;
                        var arrayValue = property.Value;
                        foreach (var itemarray in arrayValue)
                        {
                            var item = itemarray as JObject;
                            if (item != null)
                            {
                                var itemprop = item.Properties();
                                foreach (var itemp in itemprop)
                                {
                                    var k = "{{" + itemp.Name + "}}";
                                    var v = itemp.Value;
                                    templates.Body = templates.Body.Replace(Convert.ToString(k), Convert.ToString(v));
                                    Console.WriteLine($"{itemp.Name}: {itemp.Value}");
                                }
                                PushMessageNotificationModel pushMessageNotificationModel = new PushMessageNotificationModel();
                                pushMessageNotificationModel.Subject = templates.Subject;
                                pushMessageNotificationModel.Body = templates.Body;
                                pushMessageNotificationModel.RecipientPhoneNumber = (string)item.SelectToken("MobileNo");
                                pushMessageNotificationModel.RecipientEmail = (string)item.SelectToken("Email");
                                pushMessageNotificationModel.TemplateTypeId = templates.TemplateTypeId;
                                pushMessageNotificationModel.CreateBy = 1;
                                task = await PushMessageNotification(pushMessageNotificationModel);
                                templates.Body = templatebody;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error replacing placeholders: {ex.Message}");
            }
            return task;
        }
        public async Task<int> PushMessageNotification(PushMessageNotificationModel requestModel)
        {
            int task = 0;
            this._logger.LogDebug(LoggerMessage.Begin);
            if (requestModel != null)
            {
                PushMessageNotification request = new PushMessageNotification();
                request.Subject = requestModel.Subject;
                request.Body = requestModel.Body;
                request.RecipientPhoneNumber = requestModel.RecipientPhoneNumber;
                request.RecipientEmail = requestModel.RecipientEmail;
                request.TemplateTypeId = requestModel.TemplateTypeId;
                request.CreateBy = requestModel.CreateBy;
                task = await this.infrastructureServices.MessageNotification.PushMessageNotification(request);
            }
            this._logger.LogDebug(LoggerMessage.End);
            return task;
        }

        //private string ReplaceContent(string body, object obj)
        //{
        //    try
        //    {
        //        foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
        //        {
        //            var key = "{{" + propertyInfo.Name + "}}";
        //            var value = propertyInfo.GetValue(obj, null);
        //            body = body.Replace(Convert.ToString(key), Convert.ToString(value));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error replacing placeholders: {ex.Message}");
        //    }
        //    return body;
        //}
        private string ReplaceContent(string body, object obj)
        {
            if (string.IsNullOrEmpty(body) || obj == null)
                return body;

            try
            {
                foreach (PropertyInfo propertyInfo in obj.GetType().GetProperties())
                {
                    var key = $"{{{{{propertyInfo.Name}}}}}"; // this becomes {{PropertyName}}
                    var value = propertyInfo.GetValue(obj, null)?.ToString() ?? string.Empty;

                    body = body.Replace(key, value);
                }
            }
            catch (Exception ex)
            {
                // Log it instead of just console in real apps
                Console.WriteLine($"Error replacing placeholders: {ex.Message}");
            }

            return body;
        }

        #region sendsms
        public async Task<Result<string>> SendSMS(dynamic obj, int templateId)
        {
            var result = new Result<string>();

            try
            {
                // 1. Fetch UserName and Template
                string userName = await infrastructureServices.MessageNotification.GetUserNameAsync(obj.MobileNumber, obj.OTPFor);
                var template = await GetTemplateData(templateId);



                obj.UserName = userName.ToString();
                string messagebody= GetTemplateMessage(templateId, obj);
                // 2. Push Notification (log/save)
                var pushMessage = new PushMessageNotificationModel
                {
                    Subject = Convert.ToString(template.Subject),
                    Body = Convert.ToString(messagebody),
                    RecipientPhoneNumber = Convert.ToString(obj.MobileNumber),
                    RecipientEmail = "",
                    TemplateTypeId =obj.TemplateTypeId,
                    CreateBy = obj.UserId
                };

                await PushMessageNotification(pushMessage);

                // 3. Replace template placeholders
                //string message = ReplaceContent(template.Body, obj);

                // 4. SMS config
                var smsKey = _smsKeys.FirstOrDefault();
                if (smsKey == null)
                {
                    result.Message.Add("SMS configuration not found.");
                    return result;
                }

                // 5. Prepare SMS payload
                var sms = new SmsModel
                {
                    Mobile = obj.MobileNumber,
                    Message = messagebody,
                    SMSTemplateId = template.TemplateCode,
                    SMSURL = smsKey.SMSURL,
                    SmsUser = smsKey.SmsUser,
                    SmsPassword = smsKey.SmsPassword,
                    SmsSenderId = smsKey.SmsSenderId,
                    SmsSecureKey = smsKey.SmsSecureKey,
                    EnableSms = smsKey.EnableSms
                };

                // 6. Send SMS
                var smsResult = SMSNotification.Send(sms);
                if (smsResult.Status)
                {
                    result.Status = true;
                    result.Data = messagebody;
                    result.Message.Add("SMS sent successfully.");
                }
                else
                {
                    result.Message.AddRange(smsResult.Message);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add($"An error occurred while sending SMS: {ex.Message}");
                // Optionally log the error here
            }

            return result;
        }
        public async Task<Result<string>> SendSMSUser1(dynamic obj, int templateId)
        {
            var result = new Result<string>();

            try
            {
                // 1. Fetch UserName and Template
              
                var template = await GetTemplateData(templateId);


                //string pwd = EncDnc.DecryptString(obj.PasswordHash.ToString());
             

                string messagebody = GetTemplateMessage(templateId, obj, template.ActiveLink);
                // 2. Push Notification (log/save)
                var pushMessage = new PushMessageNotificationModel
                {
                    Subject = Convert.ToString(template.Subject),
                    Body = Convert.ToString(messagebody),
                    RecipientPhoneNumber = Convert.ToString(obj.MobileNumber),
                    RecipientEmail = "",
                    TemplateTypeId = obj.TemplateTypeId,
                    CreateBy = obj.UserId
                };

                await PushMessageNotification(pushMessage);

                // 3. Replace template placeholders
                //string message = ReplaceContent(template.Body, obj);

                // 4. SMS config
                var smsKey = _smsKeys.FirstOrDefault();
                if (smsKey == null)
                {
                    result.Message.Add("SMS configuration not found.");
                    return result;
                }

                // 5. Prepare SMS payload
                var sms = new SmsModel
                {
                    Mobile = obj.MobileNumber,
                    Message = messagebody,
                    SMSTemplateId = template.TemplateCode,
                    SMSURL = smsKey.SMSURL,
                    SmsUser = smsKey.SmsUser,
                    SmsPassword = smsKey.SmsPassword,
                    SmsSenderId = smsKey.SmsSenderId,
                    SmsSecureKey = smsKey.SmsSecureKey,
                    EnableSms = smsKey.EnableSms
                };

                // 6. Send SMS
                var smsResult = SMSNotification.Send(sms);


                if (smsResult.Status)
                {
                    result.Status = true;
                    result.Data = messagebody;
                    result.Message.Add("SMS sent successfully.");
                }
                else
                {
                    result.Message.AddRange(smsResult.Message);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add($"An error occurred while sending SMS: {ex.Message}");
                // Optionally log the error here
            }

            return result;
        }


        public async Task<Result<string>> SendSMSUser(dynamic obj, int templateId)
        {
            var result = new Result<string>();

            try
            {
                // 1. Fetch Template
                var template = await GetTemplateData(templateId);
                if (template == null)
                {
                    result.Message.Add("Template not found.");
                    return result;
                }

                // 2. Build SMS Message
                string messageBody = GetTemplateMessage(templateId, obj, template.ActiveLink);

                // 3. Get SMS Configuration
                var smsKey = _smsKeys.FirstOrDefault();
                if (smsKey == null)
                {
                    result.Message.Add("SMS configuration not found.");
                    return result;
                }

                // 4. Prepare SMS Model
                var sms = new SmsModel
                {
                    Mobile = obj.MobileNumber,
                    Message = messageBody,
                    SMSTemplateId = template.TemplateCode,
                    SMSURL = smsKey.SMSURL,
                    SmsUser = smsKey.SmsUser,
                    SmsPassword = smsKey.SmsPassword,
                    SmsSenderId = smsKey.SmsSenderId,
                    SmsSecureKey = smsKey.SmsSecureKey,
                    EnableSms = smsKey.EnableSms
                };

                // 5. Send SMS
                var smsResult = SMSNotification.SendMobileSms(sms); // returns Result<SmsLogModel>

                // 6. Prepare and insert SMS log (always)
                var log = new SmsLogModel
                {
                    Url = sms.SMSURL,
                    Mobile = sms.Mobile,
                    TemplateId = sms.SMSTemplateId,
                    QueryString = smsResult.Data?.QueryString ?? "",
                    Response = smsResult.Data?.Response ?? "",
                    Exception = string.Join(", ", smsResult.Message),
                    Timestamp = DateTime.Now
                };

                string jsonLog = JsonConvert.SerializeObject(log);
                int task = await this.infrastructureServices.MessageNotification.InsertSmsLogAsync(jsonLog);

                //await InsertSmsLogAsync(log); // ✅ Always insert SMS log

                // 7. Handle success
                if (smsResult.Status)
                {
                    var pushMessage = new PushMessageNotificationModel
                    {
                        Subject = Convert.ToString(template.Subject),
                        Body = messageBody,
                        RecipientPhoneNumber = Convert.ToString(obj.MobileNumber),
                        RecipientEmail = "",
                        TemplateTypeId = obj.TemplateTypeId,
                        CreateBy = obj.UserId
                    };

                    await PushMessageNotification(pushMessage); // ✅ Only on success

                    result.Status = true;
                    result.Data = messageBody;
                    result.Message.Add("SMS sent successfully.");
                }
                else
                {
                    result.Message.AddRange(smsResult.Message); // ❌ Failure case
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.Message.Add($"An error occurred while sending SMS: {ex.Message}");
                // Optionally log exception
            }

            return result;
        }


        private string GetTemplateMessage(int templateId, dynamic obj,string? ActiveLink=null) 
        {

            string passwordHash = "123456";
            string message = "";
            if (templateId==3)
            {
                 message = $"Dear {obj.UserName}, Your OTP to reset the password for the Resco Capex Portal is {obj.OTP}. This code is valid for 10 minutes. Do not share it with anyone. MPURJA";

            }
            if (templateId == 4)
            {
                
                message = $"Dear {obj.UserName},Your Resco Capex Portal access has been created.Link: {ActiveLink} User ID: {obj.Email} Password: {passwordHash} Please log in and change your password immediately. MPURJA";

            }
            if (templateId == 5)
            {
              
                message = $"Dear {obj.UserName}, Your Resco Capex Portal access is now active.Link: {ActiveLink} User ID: {obj.Email} Password:  {passwordHash}  Please log in and change your password immediately.MPURJA";

            }
            if (templateId == 6)
            {

                message = $"Dear {obj.VendorName},You have been mapped with{obj.DistrictName} in the Resco Capex Portal.Please log in to your account to check more details. MPURJA";

            }
            if (templateId == 7)
            {

                message = $"Dear {obj.DDOName}, The building with IVRS No: {obj.IVRSNO} has been successfully mapped in the Resco Capex Portal.Please log in to view the details and take necessary action.MPURJA";

            }
            if (templateId == 8)
            {

                message = $"Dear {obj.DDOName}, The consolidated bill for all mapped buildings has been generated by the [Vendor Name] for {obj.CurrentDate} {obj.VendorName} in the Resco Capex Portal.Building IVRS No's: {obj.IVRSNO} Please log in to check and proceed with the bill payment.MPURJA";

            }
            return message;
        }
        

        private async Task<MessagesNotificationModel> GetTemplateData(int templateId)
        {
            this._logger.LogDebug(LoggerMessage.Begin);
            MessagesNotificationModel templates = await this.infrastructureServices.MessageNotification.GetTemplateData(templateId);
            if (templates == null)
            {
                return new MessagesNotificationModel();
            }
            this._logger.LogDebug(LoggerMessage.End);
            return templates;
        }

        #endregion
    }
}

//    dynamic jsonObject = JObject.Parse(str);

//    var applicants = jsonObject.ApplicantsDetails;

//    foreach (var applicant in applicants)
//    {
//        Console.WriteLine("Applicant Details:");
//        var applicantObj = applicant as JObject;
//        if (applicantObj != null)
//        {
//            var prope = applicantObj.Properties();
//            foreach (var property in prope)
//            {
//                Console.WriteLine($"{property.Name}: {property.Value}");
//            }
//        }
//        Console.WriteLine();
//    }
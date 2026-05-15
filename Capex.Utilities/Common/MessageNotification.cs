using RCMS4._0.Models.Common;
using RCMS4._0.Models.ResponseModel;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using RCMS4._0.Models.ResponseModel.Masters;

namespace RCMS4._0.Utilities.Common
{
    public class MessageNotification
    {
        #region Credencial
        private static string SmsUser = AppSettings.Current.SmsUser;
        private static string SmsPassword = AppSettings.Current.SmsPassword;
        private static string SmsSenderId = AppSettings.Current.SmsSenderId;
        private static string secureKey = AppSettings.Current.SmsSecureKey;
        private static bool EnableSms = AppSettings.Current.EnableSms;
        private static string SMSURL = AppSettings.Current.SMSURL;
        //private static string TemplateId_CDAC = AppSettings.Current.TemplateId_CDAC;
        private static bool Airtel_EnableSms = AppSettings.Current.Airtel_EnableSms;
        private static string Airtel_SMSURL = AppSettings.Current.Airtel_SMSURL;
        private static string Login_ID = AppSettings.Current.Airtel_SMS_Login_ID;
        private static string Password = AppSettings.Current.Airtel_SMS_Password;
        private static string SENDER_ID = AppSettings.Current.Airtel_SMS_SENDER_ID;
        private static string PRINCIPAL_ENTITY_ID = AppSettings.Current.Airtel_SMS_PRINCIPAL_ENTITY_ID;
        private static string DLT_GOVT = AppSettings.Current.Airtel_SMS_DLT_GOVT;
        private static string CAMPAIGN_NAME = AppSettings.Current.Airtel_SMS_CAMPAIGN_NAME;
        private static string Airtel_SMS_DLT_TM_ID = AppSettings.Current.Airtel_SMS_DLT_TM_ID;
        //Email Utitlity
        private static string EmailId = AppSettings.Current.EmailId;
        private static string EmailPassword = AppSettings.Current.EmailPassword;
        private static string MailServer = AppSettings.Current.MailServer;
        private static bool EnableEmail = AppSettings.Current.EnableEmail;
        #endregion

        public SMSResponseModel Send(SmsModel sms)
        {
            SMSResponseModel response = new SMSResponseModel { Status = false, Message = "" };
            try
            {
                if (!EnableSms)
                {
                    response.Message = "SMS Service is currently disabled. Please enable it from Configuration Settings.";
                    return response;
                }

                if (sms == null)
                {
                    response.Message = "Invalid Parameter";
                    return response;
                }

                if (sms.Mobile.Length != 10)
                {
                    response.Message = "Invalid Mobile";
                    return response;
                }

                if (string.IsNullOrEmpty(sms.Message))
                {
                    response.Message = "SMS Message cannot be empty.";
                    return response;
                }

                HttpWebRequest request;
                Stream dataStream;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                request = (HttpWebRequest)WebRequest.Create(SMSURL);
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                request.Method = "POST";

                String finalmessage = "";
                String sss = "";
                foreach (char c in sms.Message)
                {
                    int j = (int)c;
                    sss = "&#" + j + ";";
                    finalmessage = finalmessage + sss;
                }

                string username = SmsUser;
                string password = SmsPassword;
                string senderid = SmsSenderId;
                string SKEY = secureKey;
                String encryptedPassword = EncryptedPasswod(password);
                String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), finalmessage.Trim(), SKEY.Trim());
                String smsservicetype = "unicodemsg"; // for unicode msg

                String query = "username=" + HttpUtility.UrlEncode(username) +
                    "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                    "&content=" + HttpUtility.UrlEncode(finalmessage.Trim()) +
                    "&mobileno=" + HttpUtility.UrlEncode(sms.Mobile) +
                    "&senderid=" + HttpUtility.UrlEncode(senderid) +
                    "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
                    "&templateid=" + HttpUtility.UrlEncode(sms.SMSTemplateId.ToString());

                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse webResponse = request.GetResponse();
                String Status = ((HttpWebResponse)webResponse).StatusDescription;

                dataStream = webResponse.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();

                if (!string.IsNullOrEmpty(responseFromServer))
                {
                    string[] authorsList = responseFromServer.Split(',');
                    if (authorsList[0].ToString() != "402")
                    {
                        // If the response indicates success (replace with your condition)
                        Task.Factory.StartNew(() => SendSms_Aitel(sms));
                    }
                }
                else
                {
                    Task.Factory.StartNew(() => SendSms_Aitel(sms));
                }

                reader.Close();
                dataStream.Close();
                webResponse.Close();

                // Logging
                string logMessage = sms.Mobile + " - " + sms.Message + ">>" + responseFromServer.ToString();
                // LogManager.WriteLog("SmsUtility_Send", LogType.InformationLog, logMessage);

                response.Status = true;
                response.Message = "success";
                response.Data = responseFromServer;
            }
            catch (Exception ex)
            {
                // LogManager.WriteLog("SmsUtility_Send", LogType.ErrorLog, ex);
                response.Message = "Something went wrong. Please contact Administration!";
            }

            return response;
        }

     

        // Helper methods (replace with your actual logic)
        private string EncryptedPasswod(string password)
        {
            return "Encrypted_" + password; // Replace with your encryption logic
        }

        private string hashGenerator(string username, string senderid, string message, string skey)
        {
            return "Hash_" + username + senderid + message + skey; // Replace with your hash generation logic
        }
    
        public static Result<string> SendSms_Aitel(SmsModel sms)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            if (Airtel_EnableSms == true)
            {
                if (sms != null)
                {
                    if (sms.Mobile.Length == 10)
                    {
                        if (sms.Message.Length > 0)
                        {
                            try
                            {
                                string SMSResponse = string.Empty;
                                string MobileNumber = sms.Mobile;//"9826462755";
                                string CONTENT_TEMPLATE_ID = sms.SMSTemplateId.ToString(); //"1007470402279779899";
                                string TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE = sms.Message;// "Your OTP is:0000-Urban Department, M.P.";

                                string URL = Airtel_SMSURL + "?" +
                                             "loginID=" + Login_ID + "&" +
                                             "password=" + Password + "&" +
                                             "mobile=" + MobileNumber + "&" +
                                             "text=" + TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE + "&" +
                                             "senderid=" + SENDER_ID + "&" +
                                             "DLT_TM_ID=" + Airtel_SMS_DLT_TM_ID + "&" +
                                             "DLT_CT_ID=" + CONTENT_TEMPLATE_ID + "&" +
                                             "DLT_PE_ID=" + PRINCIPAL_ENTITY_ID + "&" +
                                             "route_id=" + DLT_GOVT + "&" +
                                             "Unicode=0&" +
                                             "camp_name=" + CAMPAIGN_NAME;

                                using (WebClient webClient = new WebClient())
                                {
                                    SMSResponse = webClient.DownloadString(URL);
                                }

                                r.Status = true;
                                r.Data = SMSResponse;
                                r.Message.Add("Message Send Successfully");

                                string str = sms.Mobile + " - " + sms.Message + ">>" + SMSResponse;
                                // LogManager.WriteLog("SmsUtilityAirtel#Send", LogType.InformationLog, str);

                            }
                            catch (Exception ex)
                            {
                                //  LogManager.WriteLog("SmsUtilityAirtel#Send", LogType.ErrorLog, ex);
                                r.Message.Add("Something went wrong, Please contact Administration!");
                            }
                        }
                        else
                        {
                            r.Message.Add("SMS Messgae cannot be empty.");
                        }
                    }
                    else
                    {
                        r.Message.Add("Invalid Mobile");
                    }
                }
                else
                {
                    r.Message.Add("Invalid Parameter");
                }
            }
            else
            {
                r.Message.Add("Sms Service is currently disabled, Please enable it from Configuration Setting.");
            }


            return r;
        }
        public static Result<string> SendUnicode(SmsModel sms)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            if (Airtel_EnableSms == true)
            {
                if (sms != null)
                {
                    if (sms.Mobile.Length == 10)
                    {
                        if (sms.Message.Length > 0)
                        {
                            try
                            {
                                string SMSResponse = string.Empty;
                                string MobileNumber = sms.Mobile;//"9826462755";
                                string CONTENT_TEMPLATE_ID = sms.SMSTemplateId.ToString(); //"1007470402279779899";
                                string TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE = sms.Message;// "Your OTP is:0000-Urban Department, M.P.";

                                string URL = Airtel_SMSURL + "?" +
                                             "loginID=" + Login_ID + "&" +
                                             "password=" + Password + "&" +
                                             "mobile=" + MobileNumber + "&" +
                                             "text=" + TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE + "&" +
                                             "senderid=" + SENDER_ID + "&" +
                                             "DLT_TM_ID=" + Airtel_SMS_DLT_TM_ID + "&" +
                                             "DLT_CT_ID=" + CONTENT_TEMPLATE_ID + "&" +
                                             "DLT_PE_ID=" + PRINCIPAL_ENTITY_ID + "&" +
                                             "route_id=" + DLT_GOVT + "&" +
                                             "Unicode=1&" +
                                             "camp_name=" + CAMPAIGN_NAME;


                                using (WebClient webClient = new WebClient())
                                {
                                    SMSResponse = webClient.DownloadString(URL);
                                }

                                r.Status = true;
                                r.Data = SMSResponse;
                                r.Message.Add("Message Send Successfully");

                                string str = sms.Mobile + " - " + sms.Message + ">>" + SMSResponse;
                                // LogManager.WriteLog("SmsUtilityAirtel#SendUnicode", LogType.InformationLog, str);

                            }
                            catch (Exception ex)
                            {
                                /// LogManager.WriteLog("SmsUtilityAirtel#SendUnicode", LogType.ErrorLog, ex);
                                r.Message.Add("Something went wrong, Please contact Administration!");
                            }
                        }
                        else
                        {
                            r.Message.Add("SMS Messgae cannot be empty.");
                        }
                    }
                    else
                    {
                        r.Message.Add("Invalid Mobile");
                    }
                }
                else
                {
                    r.Message.Add("Invalid Parameter");
                }
            }
            else
            {
                r.Message.Add("Sms Service is currently disabled, Please enable it from Configuration Setting.");
            }


            return r;
        }


        #region Email Utility
        public Result<string> SendEmail(EmailModel e)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };

            if (EnableEmail == true)
            {
                var ee = GetEmailAttributes(e);
                if (ee != null)
                {

                    try
                    {
                        string smtp_host = Convert.ToString(ee.MailServer);
                        string smpt_mailid_from = ee.SenderEmail_Id.Trim();

                        string smpt_username = ee.SenderEmail_Id;
                        string smpt_password = ee.SenderPassword;
                        int smpt_port = Convert.ToInt16(ee.MailServerPort);
                        using (System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage())
                        {
                            _mail.Subject = ee.Subject;
                            _mail.Body = ee.Message;
                            _mail.From = new System.Net.Mail.MailAddress(smpt_mailid_from);
                            //foreach (var address in EmailIds)
                            //{
                            //    _mail.To.Add(address);
                            //}
                            _mail.To.Add(e.Recipient);
                            _mail.IsBodyHtml = false;
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                            smtp.Host = smtp_host;
                            smtp.EnableSsl = true;
                            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(smpt_username, smpt_password);
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = smpt_port;
                            smtp.Send(_mail);
                            r.Status = true;
                            r.Message.Add("Email send Successfully!");

                            // Writing Log
                            var str = "EmailSent - " + r.Status + " " + e.Message;
                            //LogManager.WriteLog("SendEmailToDistrictUser", LogType.InformationLog, str);
                        }
                    }

                    catch (Exception ex)
                    {
                        r.Message.Add("Something went wrong, Please contact Administration!");
                        // LogManager.WriteLog("SendEmailToDistrictUser", LogType.ErrorLog, ex);
                    }

                }
                else
                {
                    r.Message.Add("Email Service configuration not found.");
                }
            }
            else
            {
                r.Message.Add("Email Service is currently disabled, Please enable it from Configuration Setting.");
            }
            return r;
        }
        public static Result<string> Send(EmailModel e)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            r.Status = true;
            r.Message.Add("Email send Successfully!");
            return r;
        }
        public static Result<string> SendWithAttachement(EmailModel e)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            r.Status = true;
            r.Message.Add("Email send Successfully!");
            return r;

            //if (EnableEmail == true)
            //{
            //    var ee = GetEmailAttributes(e);
            //    if (ee != null)
            //    {
            //        System.Web.Mail.MailMessage Email = new System.Web.Mail.MailMessage();
            //        Email.To = ee.Recipient.Trim();
            //        Email.BodyFormat = System.Web.Mail.MailFormat.Html;
            //        Email.From = ee.SenderEmail_Id.Trim();
            //        Email.Body = ee.Message;
            //        Email.BodyEncoding = Encoding.UTF8;
            //        Email.Subject = e.Subject;

            //        if (e.FilePathList.Count > 0)
            //        {
            //            e.FilePathList.ForEach(f =>
            //            {
            //                if (File.Exists(f))
            //                {
            //                    Email.Attachments.Add(new System.Web.Mail.MailAttachment(f));
            //                }
            //            });
            //        }

            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", ee.MailServer);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", Convert.ToInt16(ee.MailServerPort));
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ee.SenderEmail_Id);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ee.SenderPassword);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", 1);
            //        System.Web.Mail.SmtpMail.SmtpServer = e.MailServer;
            //        try
            //        {
            //            System.Web.Mail.SmtpMail.Send(Email);
            //            r.Status = true; r.Message.Add("Email send Successfully!");

            //            // Writing Log
            //            var str = "EmailSent - " + r.Status + " " + e.Message;
            //            LogManager.WriteLog("EmailUtility_Send", LogType.InformationLog, str);
            //        }
            //        catch (Exception ex)
            //        {
            //            r.Message.Add("Something went wrong, Please contact Administration!");
            //            LogManager.WriteLog("EmailUtility_Send", LogType.ErrorLog, ex);
            //        }
            //    }
            //    else
            //    {
            //        r.Message.Add("Email Service configuration not found.");
            //    }
            //}
            //else
            //{
            //    r.Message.Add("Email Service is currently disabled, Please enable it from Configuration Setting.");
            //}
            //return r;
        }
        public static Result<string> SendWithAttachementByte(EmailModel e)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            r.Status = true;
            r.Message.Add("Email send Successfully!");
            return r;

            //if (EnableEmail == true)
            //{
            //    var ee = GetEmailAttributes(e);
            //    if (ee != null)
            //    {
            //        System.Web.Mail.MailMessage Email = new System.Web.Mail.MailMessage();
            //        Email.To = ee.Recipient.Trim();
            //        Email.BodyFormat = System.Web.Mail.MailFormat.Html;
            //        Email.From = ee.SenderEmail_Id.Trim();
            //        Email.Body = ee.Message;
            //        Email.BodyEncoding = Encoding.UTF8;
            //        Email.Subject = e.Subject;

            //        e.FilePathList.ForEach(f =>
            //        {
            //            if (File.Exists(f))
            //            {
            //                var fileBytes = File.ReadAllBytes(f);
            //                using (MemoryStream ms = new MemoryStream(fileBytes))
            //                {
            //                    Email.Attachments.Add(new System.Net.Mail.Attachment(ms, "FileName", "application/pdf"));
            //                }
            //            }
            //        });
            //        //System.Web.Mail.MailAttachment attachment = new System.Web.Mail.MailAttachment();
            //        //Email.Attachments.Add(attachment);


            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", ee.MailServer);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", Convert.ToInt16(ee.MailServerPort));
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", ee.SenderEmail_Id);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", ee.SenderPassword);
            //        Email.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", 1);
            //        System.Web.Mail.SmtpMail.SmtpServer = e.MailServer;
            //        try
            //        {
            //            System.Web.Mail.SmtpMail.Send(Email);
            //            r.Status = true; r.Message.Add("Email send Successfully!");

            //            // Writing Log
            //            var str = "EmailSent - " + r.Status + " " + e.Message;
            //            LogManager.WriteLog("EmailUtility_Send", LogType.InformationLog, str);
            //        }
            //        catch (Exception ex)
            //        {
            //            r.Message.Add("Something went wrong, Please contact Administration!");
            //            LogManager.WriteLog("EmailUtility_Send", LogType.ErrorLog, ex);
            //        }
            //    }
            //    else
            //    {
            //        r.Message.Add("Email Service configuration not found.");
            //    }
            //}
            //else
            //{
            //    r.Message.Add("Email Service is currently disabled, Please enable it from Configuration Setting.");
            //}
            //return r;
        }
        private static EmailModel GetEmailAttributes(EmailModel e)
        {
            e.MailServer = MailServer.Split(',')[0];
            e.MailServerPort = MailServer.Split(',')[1];
            e.SenderEmail_Id = EmailId;
            e.SenderPassword = EmailPassword;
            return (e == null) ? null : e;
        }
    }
    #endregion

    //class MyPolicy : ICertificatePolicy
    //{
    //    public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
    //    {
    //        return true;
    //    }
    //}
   
    public class KeyValueModel
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class SmsTemplateModel
    {
        public string Mobile { get; set; }
        public int SMSTemplateId { get; set; }
        public List<KeyValueModel> Values { get; set; }
    }
    //EmailModel
    public class EmailModel : EmailSendModel
    {
        public string MailServer { get; set; }
        public string SenderEmail_Id { get; set; }
        public string SenderPassword { get; set; }
        public string MailServerPort { get; set; }
        public List<Guid> FileUploadList { get; set; }
        public List<string> FilePathList { get; set; }
    }
    public class EmailSendModel
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}





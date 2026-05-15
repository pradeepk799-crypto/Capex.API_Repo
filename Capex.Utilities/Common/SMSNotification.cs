using Capex.Models.Common;
using Capex.Models.ResponseModel;
using Capex.Models.ResponseModel.Masters;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace Capex.Utilities.Common
{
    public class SMSNotification
    {

        private readonly ILogger<SMSNotification> _logger;
        public SMSNotification(ILogger<SMSNotification> logger)
        {
            _logger = logger;
        }


        //public Result<string> Send(SmsModel sms)
        //{

        //    this._logger.LogDebug(LoggerMessage.End);
        //    Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
        //    SMSResponseModel response = new SMSResponseModel { Status = false, Message = "" };

        //    try
        //    {
        //        // Set Security Protocol
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        //        // Use HttpClient with Custom Certificate Validation
        //        HttpClientHandler handler = new HttpClientHandler
        //        {
        //            ServerCertificateCustomValidationCallback = (HttpRequestMessage request, X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) =>
        //            {
        //                // Accept all certificates (For production, implement proper validation)
        //                return true;
        //            }
        //        };

        //        using (HttpClient client = new HttpClient(handler))
        //        {
        //            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)");

        //            // Construct the SMS message
        //            String finalMessage = "";
        //            foreach (char c in sms.Message)
        //            {
        //                finalMessage += $"&#{(int)c};";
        //            }

        //            string username = sms.SmsUser;
        //            string password = sms.SmsPassword;
        //            string senderId = sms.SmsSenderId;
        //            string secureKey = sms.SmsSecureKey;
        //            string encryptedPassword = EncryptedPasswod(password);
        //            string newSecureKey = hashGenerator(username.Trim(), senderId.Trim(), finalMessage.Trim(), secureKey.Trim());
        //            string smsServiceType = "singlemsg"; // for Unicode messages

        //            // Create query parameters
        //            var query = new Dictionary<string, string>
        //    {
        //        { "username", username },
        //        { "password", encryptedPassword },
        //        { "smsservicetype", smsServiceType },
        //        { "content", finalMessage.Trim() },
        //        { "mobileno", sms.Mobile },
        //        { "senderid", senderId },
        //        { "key", newSecureKey.Trim() },
        //        { "templateid", sms.SMSTemplateId.ToString() }
        //    };

        //            foreach (var param in query)
        //            {
        //                this._logger.LogInformation("Query Parameter - {Key}: {Value}", param.Key, param.Value);
        //            }


        //            // Send POST request
        //            HttpResponseMessage responseMessage = client.PostAsync(sms.SMSURL, new FormUrlEncodedContent(query)).Result;
        //            string responseFromServer = responseMessage.Content.ReadAsStringAsync().Result;


        //            this._logger.LogInformation("Response from Server: {Response}", responseFromServer);

        //            if (!string.IsNullOrEmpty(responseFromServer))
        //            {
        //                string[] authorsList = responseFromServer.Split(',');
        //                if (authorsList[0] != "402")
        //                {
        //                    // If the response indicates success (replace with your condition)
        //                    Task.Factory.StartNew(() => SendSms_Aitel(sms));
        //                }
        //            }
        //            else
        //            {
        //                Task.Factory.StartNew(() => SendSms_Aitel(sms));
        //            }

        //            // Logging
        //            string logMessage = $"{sms.Mobile} - {sms.Message} >> {responseFromServer}";
        //            Console.WriteLine(logMessage);

        //            result.Status = true;
        //            result.Message.Add("success");
        //            result.Data = responseFromServer;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogManager.WriteLog("SmsUtility_Send", LogType.ErrorLog, ex);
        //        result.Message.Add("Something went wrong. Please contact Administration!");
        //    }
        //    return result;
        //}
        //public Result<string> Send(SmsModel sms)
        //{
        //    Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
        //    SMSResponseModel response = new SMSResponseModel { Status = false, Message = "" };
        //    try
        //    {
        //        //if ((bool)!sms.EnableSms)
        //        //{
        //        //    result.Message.Add("SMS Service is currently disabled. Please enable it from Configuration Settings.");
        //        //    return result;
        //        //}

        //        if (sms == null)
        //        {
        //            result.Message.Add("Invalid Parameter");
        //            return result;
        //        }

        //        if (sms.Mobile.Length != 10)
        //        {
        //            result.Message.Add("Invalid Mobile");
        //            return result;
        //        }

        //        if (string.IsNullOrEmpty(sms.Message))
        //        {
        //            result.Message.Add("SMS Message cannot be empty.");
        //            return result;
        //        }

        //        HttpWebRequest request;
        //        Stream dataStream;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

        //        request = (HttpWebRequest)WebRequest.Create(sms.SMSURL);
        //        request.ProtocolVersion = HttpVersion.Version10;
        //        request.KeepAlive = false;
        //        request.ServicePoint.ConnectionLimit = 1;
        //        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
        //        request.Method = "POST";

        //        String finalmessage = "";
        //        String sss = "";
        //        foreach (char c in sms.Message)
        //        {
        //            int j = (int)c;
        //            sss = "&#" + j + ";";
        //            finalmessage = finalmessage + sss;
        //        }

        //        string username = "DITMP-MPURJA";
        //        string password = "MPURJA#123";
        //        string senderid = "MPURJA";
        //        string SKEY = "c9f3ac85-7ebe-467a-9b9e-17e43d075574";
        //        String encryptedPassword = EncryptedPasswod(password);
        //        String NewsecureKey = hashGenerator(username.Trim(), senderid.Trim(), finalmessage.Trim(), SKEY.Trim());
        //        String smsservicetype = "unicodemsg"; // for unicode msg






        //        String query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
        //            "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
        //            "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
        //            "&content=" + HttpUtility.UrlEncode(finalmessage.Trim()) +
        //            "&bulkmobno=" + HttpUtility.UrlEncode("8827343904") +
        //            "&senderid=" + HttpUtility.UrlEncode(senderid.Trim()) +
        //            "&key=" + HttpUtility.UrlEncode(NewsecureKey.Trim()) +
        //    "&templateid=" + HttpUtility.UrlEncode("1307162306607858651");

        //        byte[] byteArray = Encoding.ASCII.GetBytes(query);
        //        request.ContentType = "application/x-www-form-urlencoded";
        //        request.ContentLength = byteArray.Length;

        //        dataStream = request.GetRequestStream();
        //        dataStream.Write(byteArray, 0, byteArray.Length);
        //        dataStream.Close();

        //        WebResponse webResponse = request.GetResponse();
        //        String Status = ((HttpWebResponse)webResponse).StatusDescription;

        //        dataStream = webResponse.GetResponseStream();
        //        StreamReader reader = new StreamReader(dataStream);
        //        string responseFromServer = reader.ReadToEnd();

        //        if (!string.IsNullOrEmpty(responseFromServer))
        //        {
        //            string[] authorsList = responseFromServer.Split(',');
        //            if (authorsList[0].ToString() != "402")
        //            {
        //                // If the response indicates success (replace with your condition)
        //                Task.Factory.StartNew(() => SendSms_Aitel(sms));
        //            }
        //        }
        //        else
        //        {
        //            Task.Factory.StartNew(() => SendSms_Aitel(sms));
        //        }

        //        reader.Close();
        //        dataStream.Close();
        //        webResponse.Close();
        //        // Logging
        //        string logMessage = sms.Mobile + " - " + sms.Message + ">>" + responseFromServer.ToString();
        //        // LogManager.WriteLog("SmsUtility_Send", LogType.InformationLog, logMessage);

        //        result.Status = true;
        //        result.Message.Add("success");
        //        result.Data = responseFromServer;
        //    }
        //    catch (Exception ex)
        //    {
        //        // LogManager.WriteLog("SmsUtility_Send", LogType.ErrorLog, ex);
        //        result.Message.Add("Something went wrong. Please contact Administration!");
        //    }
        //    return result;
        //}


        public Result<string> Send(SmsModel sms)
        {
            //string responseFromServer = SendUnicodeSMS(sms);
            Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
            string responseFromServer = string.Empty;



            try
            {
                Stream dataStream;

                // Force TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // ⚠️ Only for DEV/TEST: Bypass SSL Certificate Validation (Don't use in Production)
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true;

                // Create HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sms.SMSURL);
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                request.Method = "POST";

                // Convert to unicode message
                StringBuilder U_Convertedmessage = new StringBuilder();
                foreach (char c in sms.Message)
                {
                    int j = (int)c;
                    U_Convertedmessage.Append("&#" + j + ";");
                }

                // Replace with your actual values
                //string username = "DITMP-MPURJA";
                //string password = "MPURJA#123";
                //string senderId = "MPURJA";
                //string secureKey = "c9f3ac85-7ebe-467a-9b9e-17e43d075574";
                //string tmpid = "1307162306607858651";


                string username = sms.SmsUser;
                string password = sms.SmsPassword;
                string senderId = sms.SmsSenderId;
                string secureKey = "c9f3ac85-7ebe-467a-9b9e-17e43d075574";
                string tmpid = sms.SMSTemplateId;

                string encryptedPassword = EncryptedPasswod(password);
                string newSecureKey = hashGenerator(username.Trim(), senderId.Trim(), U_Convertedmessage.ToString().Trim(), secureKey.Trim());

                string smsservicetype = "unicodemsg"; // for unicode msg

                string query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                    "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                    "&content=" + HttpUtility.UrlEncode(U_Convertedmessage.ToString().Trim()) +
                    "&bulkmobno=" + HttpUtility.UrlEncode(sms.Mobile) +
                    "&senderid=" + HttpUtility.UrlEncode(senderId.Trim()) +
                    "&key=" + HttpUtility.UrlEncode(newSecureKey.Trim()) +
                    "&templateid=" + HttpUtility.UrlEncode(tmpid.Trim());


                this._logger.LogInformation("Response from Server in the SendUnicodeSMS query", query);


                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                // Write data to request
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Get response
                using WebResponse response = request.GetResponse();
                using Stream responseStream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(responseStream);

                responseFromServer = reader.ReadToEnd();
                this._logger.LogInformation("Response from Server in the SendUnicodeSMS responseFromServer: {0}", responseFromServer);
                result.Status = false;
                result.Message.Add("success");
                result.Data = responseFromServer;

                return result;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                this._logger.LogInformation("Response from Server in the SendUnicodeSMS :{0}", ex.InnerException);
                result.Status = false;
                result.Message.Add("success");
                result.Data = responseFromServer;

                return result;

            }
        }

        public Result<SmsLogModel> SendMobileSms(SmsModel sms)
        {
            Result<SmsLogModel> result = new Result<SmsLogModel>() { Status = false, Message = new List<string>() };
            string responseFromServer = string.Empty;
            string query = string.Empty;
            Exception exception = null;

            try
            {
                Stream dataStream;
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                System.Net.ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sms.SMSURL);
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;
                request.UserAgent = "Mozilla/4.0";
                request.Method = "POST";

                // Convert message to unicode
                StringBuilder U_Convertedmessage = new StringBuilder();
                foreach (char c in sms.Message)
                    U_Convertedmessage.Append("&#" + (int)c + ";");

                string username = sms.SmsUser;
                string password = sms.SmsPassword;
                string senderId = sms.SmsSenderId;
                string secureKey = "c9f3ac85-7ebe-467a-9b9e-17e43d075574";
                string tmpid = sms.SMSTemplateId;
                string encryptedPassword = EncryptedPasswod(password);
                string newSecureKey = hashGenerator(username.Trim(), senderId.Trim(), U_Convertedmessage.ToString().Trim(), secureKey.Trim());
                string smsservicetype = "unicodemsg";

                query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                        "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                        "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                        "&content=" + HttpUtility.UrlEncode(U_Convertedmessage.ToString().Trim()) +
                        "&bulkmobno=" + HttpUtility.UrlEncode(sms.Mobile) +
                        "&senderid=" + HttpUtility.UrlEncode(senderId.Trim()) +
                        "&key=" + HttpUtility.UrlEncode(newSecureKey.Trim()) +
                        "&templateid=" + HttpUtility.UrlEncode(tmpid.Trim());

                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                using WebResponse response = request.GetResponse();
                using Stream responseStream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(responseStream);
                responseFromServer = reader.ReadToEnd();

                result.Status = true;
                result.Message.Add("success");
            }
            catch (Exception ex)
            {
                exception = ex;
                result.Message.Add("error");
            }

            // Prepare log
            SmsLogModel log = new SmsLogModel
            {
                Url = sms.SMSURL,
                Mobile = sms.Mobile,
                TemplateId = sms.SMSTemplateId,
                QueryString = query,
                Response = responseFromServer,
                Exception = exception?.ToString(),
                Timestamp = DateTime.Now
            };

            result.Data = log;           

            return result;
        }


        public string SendUnicodeSMS(SmsModel sms)
        {
            try
            {
                Stream dataStream;

                // Force TLS 1.2
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // ⚠️ Only for DEV/TEST: Bypass SSL Certificate Validation (Don't use in Production)
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
                    (sender, cert, chain, sslPolicyErrors) => true;

                // Create HttpWebRequest
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sms.SMSURL);
                request.ProtocolVersion = HttpVersion.Version10;
                request.KeepAlive = false;
                request.ServicePoint.ConnectionLimit = 1;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.0; Windows 98; DigExt)";
                request.Method = "POST";

                // Convert to unicode message
                StringBuilder U_Convertedmessage = new StringBuilder();
                foreach (char c in sms.Message)
                {
                    int j = (int)c;
                    U_Convertedmessage.Append("&#" + j + ";");
                }

                // Replace with your actual values
                //string username = "DITMP-MPURJA";
                //string password = "MPURJA#123";
                //string senderId = "MPURJA";
                //string secureKey = "c9f3ac85-7ebe-467a-9b9e-17e43d075574";
                //string tmpid = "1307162306607858651";


                string username = sms.SmsUser;
                string password = sms.SmsPassword;
                string senderId = sms.SmsSenderId;
                string secureKey = sms.SmsSecureKey;
                string tmpid = sms.SMSTemplateId;

                string encryptedPassword = EncryptedPasswod(password);
                string newSecureKey = hashGenerator(username.Trim(), senderId.Trim(), U_Convertedmessage.ToString().Trim(), secureKey.Trim());

                string smsservicetype = "unicodemsg"; // for unicode msg

                string query = "username=" + HttpUtility.UrlEncode(username.Trim()) +
                    "&password=" + HttpUtility.UrlEncode(encryptedPassword) +
                    "&smsservicetype=" + HttpUtility.UrlEncode(smsservicetype) +
                    "&content=" + HttpUtility.UrlEncode(U_Convertedmessage.ToString().Trim()) +
                    "&bulkmobno=" + HttpUtility.UrlEncode(sms.Mobile) +
                    "&senderid=" + HttpUtility.UrlEncode(senderId.Trim()) +
                    "&key=" + HttpUtility.UrlEncode(newSecureKey.Trim()) +
                    "&templateid=" + HttpUtility.UrlEncode(tmpid.Trim());


                this._logger.LogInformation("Response from Server in the SendUnicodeSMS query", query);


                byte[] byteArray = Encoding.ASCII.GetBytes(query);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                // Write data to request
                dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                // Get response
                using WebResponse response = request.GetResponse();
                using Stream responseStream = response.GetResponseStream();
                using StreamReader reader = new StreamReader(responseStream);

                string responseFromServer = reader.ReadToEnd();
                this._logger.LogInformation("Response from Server in the SendUnicodeSMS responseFromServer", responseFromServer);
                return responseFromServer;
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                this._logger.LogInformation("Response from Server in the SendUnicodeSMS", ex.InnerException);
                return $"Error sending SMS: {ex.Message}";
            }
        }

        public static Result<string> SendSms_Aitel(SmsModel sms)
        {
            Result<string> r = new Result<string>() { Status = false, Message = new List<string> { } };
            if (sms.Airtel_EnableSms == true)
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
                                string CONTENT_TEMPLATE_ID = sms.Airtel_SMS_DLT_TM_ID.ToString(); //"1007470402279779899";
                                string TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE = sms.Message;// "Your OTP is:0000-Urban Department, M.P.";

                                string URL = sms.Airtel_SMSURL + "?" +
                                             "loginID=" + sms.Airtel_SMS_Login_ID + "&" +
                                             "password=" + sms.Airtel_SMS_Password + "&" +
                                             "mobile=" + MobileNumber + "&" +
                                             "text=" + TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE + "&" +
                                             "senderid=" + sms.Airtel_SMS_SENDER_ID + "&" +
                                             "DLT_TM_ID=" + sms.Airtel_SMS_DLT_TM_ID + "&" +
                                             "DLT_CT_ID=" + sms.Airtel_SMS_DLT_CT_ID + "&" +
                                             "DLT_PE_ID=" + sms.Airtel_SMS_PRINCIPAL_ENTITY_ID + "&" +
                                             "route_id=" + sms.Airtel_SMS_DLT_GOVT + "&" +
                                             "Unicode=" + sms.Airtel_SMS_Unicode + "&" +
                                             "camp_name=" + sms.Airtel_SMS_CAMPAIGN_NAME;

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
            if (sms.Airtel_EnableSms == true)
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

                                string URL = sms.Airtel_SMSURL + "?" +
                                             "loginID=" + sms.Airtel_SMS_Login_ID + "&" +
                                             "password=" + sms.Airtel_SMS_Password + "&" +
                                             "mobile=" + MobileNumber + "&" +
                                             "text=" + TEST_MESSAGE_AS_PER_THE_CONTENT_TEMPLATE + "&" +
                                             "senderid=" + sms.Airtel_SMS_SENDER_ID + "&" +
                                             "DLT_TM_ID=" + sms.Airtel_SMS_DLT_TM_ID + "&" +
                                             "DLT_CT_ID=" + CONTENT_TEMPLATE_ID + "&" +
                                             "DLT_PE_ID=" + sms.Airtel_SMS_PRINCIPAL_ENTITY_ID + "&" +
                                             "route_id=" + sms.Airtel_SMS_DLT_GOVT + "&" +
                                             "Unicode=1&" +
                                             "camp_name=" + sms.Airtel_SMS_CAMPAIGN_NAME;


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

        protected String EncryptedPasswod(String password)
        {

            byte[] encPwd = Encoding.UTF8.GetBytes(password);
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA1");
            byte[] pp = sha1.ComputeHash(encPwd);
            // static string result = System.Text.Encoding.UTF8.GetString(pp);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in pp)
            {

                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();

        }



        protected String hashGenerator(String Username, String sender_id, String message, String secure_key)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Username).Append(sender_id).Append(message).Append(secure_key);
            byte[] genkey = Encoding.UTF8.GetBytes(sb.ToString());
            //static byte[] pwd = new byte[encPwd.Length];
            HashAlgorithm sha1 = HashAlgorithm.Create("SHA512");
            byte[] sec_key = sha1.ComputeHash(genkey);

            StringBuilder sb1 = new StringBuilder();
            for (int i = 0; i < sec_key.Length; i++)
            {
                sb1.Append(sec_key[i].ToString("x2"));
            }
            return sb1.ToString();
        }

    }
}



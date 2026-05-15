using Capex.Models.Common;
using Capex.Models.ResponseModel.Masters;
using System.Net;

namespace Capex.Utilities.Common
{
    public class EmailNotification
    {
        //Email Utitlity
        //Email Utitlity
        //private static string EmailId = AppSettings.Current.EmailId;
        //private static string EmailPassword = AppSettings.Current.EmailPassword;
        //private static string MailServer = AppSettings.Current.MailServer;
        //private static bool EnableEmail = AppSettings.Current.EnableEmail;
        //public Result<string> SendEmail(SendEmailRquestModel e)
        //{
        //    Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
        //    if (EnableEmail == true)
        //    {
        //        var ee = GetEmailAttributes(e);
        //        if (ee != null)
        //        {
        //            try
        //            {
        //                string smtp_host = Convert.ToString(ee.MailServer);  // smtp.gmial.com
        //                string smpt_mailid_from = ee.SenderEmail_Id.Trim();
        //                string smpt_username = ee.SenderEmail_Id;
        //                string smpt_password = ee.SenderPassword;
        //                int smpt_port = Convert.ToInt16(ee.MailServerPort);  //587
        //                using (System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage())
        //                {
        //                    _mail.Subject = ee.Subject;
        //                    _mail.Body = ee.Message;
        //                    _mail.From = new System.Net.Mail.MailAddress(smpt_mailid_from);
        //                    _mail.To.Add(e.Recipient);
        //                    _mail.IsBodyHtml = false;
        //                    if (e.FilePathList.Count > 0)
        //                    {
        //                        e.FilePathList.ForEach(f =>
        //                        {
        //                            if (File.Exists(f))
        //                            {
        //                                _mail.Attachments.Add(new System.Net.Mail.Attachment(f));
        //                            }
        //                        });
        //                    }
        //                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //                    smtp.Host = smtp_host;
        //                    smtp.EnableSsl = true;
        //                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(smpt_username, smpt_password);
        //                    smtp.UseDefaultCredentials = true;
        //                    smtp.Credentials = NetworkCred;
        //                    smtp.Port = smpt_port;
        //                    smtp.Send(_mail);
        //                    result.Status = true;
        //                    result.Message.Add("Email send Successfully!");
        //                    // Writing Log
        //                    var str = "EmailSent - " + result.Status + " " + e.Message;
        //                    //LogManager.WriteLog("SendEmailToDistrictUser", LogType.InformationLog, str);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                result.Message.Add("Something went wrong, Please contact Administration!");
        //                // LogManager.WriteLog("SendEmailToDistrictUser", LogType.ErrorLog, ex);
        //            }
        //        }
        //        else
        //        {
        //            result.Message.Add("Email Service configuration not found.");
        //        }
        //    }
        //    else
        //    {
        //        result.Message.Add("Email Service is currently disabled, Please enable it from Configuration Setting.");
        //    }
        //    return result;
        //}
        public Result<string> SendEmail(SendEmailRquestModel e)
        {
            Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
            if (e.EnableEmail == true)
            {
                if (e != null)
                {
                    //var ee = GetEmailAttributes(e);
                    try
                    {
                        /*string smtp_host = Convert.ToString(ee.MailServer);*/  // smtp.gmial.com
                        string smtp_host = Convert.ToString(e.MailServer);
                        string smpt_mailid_from = e.SenderEmail_Id.Trim();
                        string smpt_username = e.SenderEmail_Id;
                        string smpt_password = e.SenderPassword;
                        //int smpt_port= Convert.ToInt16(ee.MailServerPort);
                        int smpt_port = Convert.ToInt16(e.Port);  //587
                        using (System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage())
                        {
                            _mail.Subject = e.Subject;
                            _mail.Body = e.Message;
                            _mail.From = new System.Net.Mail.MailAddress(smpt_mailid_from);
                            _mail.To.Add(e.Recipient);
                            _mail.IsBodyHtml = false;
                            if (e.FilePathList != null)
                            {
                                if (e.FilePathList.Count > 0)
                                {
                                    e.FilePathList.ForEach(f =>
                                    {
                                        if (File.Exists(f))
                                        {
                                            _mail.Attachments.Add(new System.Net.Mail.Attachment(f));
                                        }
                                    });
                                }
                            }
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                            smtp.Host = smtp_host;
                            smtp.EnableSsl = true;
                            System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(smpt_username, smpt_password);
                            smtp.UseDefaultCredentials = false;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = smpt_port;
                            smtp.Send(_mail);
                            result.Status = true;
                            result.Message.Add("Email send Successfully!");
                            // Writing Log
                            var str = "EmailSent - " + result.Status + " " + e.Message;
                            //LogManager.WriteLog("SendEmailToDistrictUser", LogType.InformationLog, str);
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Message.Add(ex.Message.ToString());
;                        result.Message.Add("Something went wrong, Please contact Administration!");
                        // LogManager.WriteLog("SendEmailToDistrictUser", LogType.ErrorLog, ex);
                    }
                }
                else
                {
                    result.Message.Add(result.Message.ToString());
                    result.Message.Add("Email Service configuration not found.");
                }
            }
            else
            {
                result.Message.Add("Email Service is currently disabled, Please enable it from Configuration Setting.");
            }
            return result;
        }
        //private static SendEmailRquestModel GetEmailAttributes(SendEmailRquestModel e)
        //{
        //    e.MailServer = MailServer.Split(',')[0];
        //    e.MailServerPort = MailServer.Split(',')[1];
        //    e.SenderEmail_Id = EmailId;
        //    e.SenderPassword = EmailPassword;
        //    return (e == null) ? null : e;
        //}
        public Result<string> SendEmailTest(SendEmailRquestModel e)
        {
            Result<string> result = new Result<string>() { Status = false, Message = new List<string> { } };
            try
            {
                string smtp_host = "smtp.gmail.com";
                int smpt_port = 587;
                using (System.Net.Mail.MailMessage _mail = new System.Net.Mail.MailMessage())
                {
                    _mail.Subject = e.Subject;
                    _mail.Body = e.Message;
                    _mail.To.Add("chaudharyvipul2010@gmail.com");
                    _mail.From = new System.Net.Mail.MailAddress("chaudharyvipul2010@gmail.com");
                    _mail.IsBodyHtml = false;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = smtp_host;
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("chaudharyvipul2010@gmail.com", "qrsfecuaxzapybmt");
                    smtp.Port = smpt_port;
                    smtp.Send(_mail);
                    result.Status = true;
                    result.Message.Add("Email send Successfully!");
                    // Writing Log
                    var str = "EmailSent - " + result.Status + " " + e.Message;
                    //LogManager.WriteLog("SendEmailToDistrictUser", LogType.InformationLog, str);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add("Something went wrong, Please contact Administration!");
                // LogManager.WriteLog("SendEmailToDistrictUser", LogType.ErrorLog, ex);
            }
            return result;
        }
    }
}

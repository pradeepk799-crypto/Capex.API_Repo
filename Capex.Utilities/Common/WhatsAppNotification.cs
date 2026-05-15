using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Capex.Models.Common;
using Capex.Models.ResponseModel.Masters;
using System.Net;
namespace Capex.Utilities.Common
{
    public class WhatsAppNotification
    {
        public WhatsAppNotification() { }
        public Result<dynamic> SendWhatsApp(WhatsAppModel whatsapp)
        {
            Result<dynamic> r = new Result<dynamic>();
            try
            {
                string APIResponse = string.Empty;
                HttpClient client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                var URL = whatsapp.WhatsappURL + "?method=SendMessage&format=json&userid=" + whatsapp.WhatsAppUserid + "&password=" + whatsapp.WhatsAppPwd + "&send_to=" + whatsapp.Mobile + "&v=1.1&auth_scheme=plain&msg_type=Text&msg=" + whatsapp.Message;
                HttpResponseMessage response = client.GetAsync(URL).Result;
                //Http Status code 200
                if (response.IsSuccessStatusCode)
                {
                    if (whatsapp == null)
                    {
                        r.Message.Add("Invalid Parameter");
                        return r;
                    }
                    if (whatsapp.Mobile.Length != 10)
                    {
                        r.Message.Add("Invalid Mobile");
                        return r;
                    }
                    if (string.IsNullOrEmpty(whatsapp.Message))
                    {
                        r.Message.Add("WhatsApp Message cannot be empty.");
                        return r;
                    }
                    //Read response content result into string variable
                    string JSON = response.Content.ReadAsStringAsync().Result;
                    //Deserialize the string(JSON) object
                    var jObj = (JObject)JsonConvert.DeserializeObject(JSON);
                    var _json = JObject.Parse(JSON.ToString());
                    APIResponse = _json.ToString();
                    var Response = _json["response"]["status"].ToString();
                    if (Response.ToString() != "success")
                    {
                        r.Status = false;
                        r.Message.Add(_json["response"]["details"].ToString());
                    }
                    else
                    {
                        r.Status = true;
                        r.Message.Add(Response);
                    }
                }
                else
                {
                    r.Status = false;
                    r.Message.Add(response.IsSuccessStatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                r.Message.Add("Something went wrong. Please contact Administration!");
            }
            return r;
        }
        public Result<dynamic> SendWhatsAppOptInOut(WhatsAppModelOptINOUT whatsappOptInOut)
        {
            Result<dynamic> r = new Result<dynamic>();
            try
            {
                string APIResponse = string.Empty;
                var URL = "";
                HttpClient client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                if (whatsappOptInOut.Type == WhatsAppConsentType.OPTIN)
                {
                    URL = whatsappOptInOut.WhatsappURL + "?method=" + WhatsAppConsentType.OPTIN + "&format=json&userid=" + whatsappOptInOut.WhatsAppUserid + "&password=" + whatsappOptInOut.WhatsAppPwd + "&phone_number=" + whatsappOptInOut.Mobile + "&v=1.1&auth_scheme=plain&channel=WHATSAPP";
                }
                else
                {
                    URL = whatsappOptInOut.WhatsappURL + "?method=" + WhatsAppConsentType.OPTOUT + "&format=json&userid=" + whatsappOptInOut.WhatsAppUserid + "&password=" + whatsappOptInOut.WhatsAppPwd + "&phone_number=" + whatsappOptInOut.Mobile + "&v=1.1&auth_scheme=plain&channel=WHATSAPP";
                }
                HttpResponseMessage response = client.GetAsync(URL).Result;
                //Http Status code 200
                if (response.IsSuccessStatusCode)
                {
                    if (whatsappOptInOut == null)
                    {
                        r.Message.Add("Invalid Parameter");
                        return r;
                    }

                    if (whatsappOptInOut.Mobile.Length != 10)
                    {
                        r.Message.Add("Invalid Mobile");
                        return r;
                    }
                    //Read response content result into string variable
                    string JSON = response.Content.ReadAsStringAsync().Result;
                    //Deserialize the string(JSON) object
                    var jObj = (JObject)JsonConvert.DeserializeObject(JSON);
                    var _json = JObject.Parse(JSON.ToString());
                    APIResponse = _json.ToString();
                    var Response = _json["response"]["status"].ToString();
                    if (Response.ToString() != "success")
                    {
                        r.Status = false;
                        r.Message.Add(_json["response"]["details"].ToString());

                    }
                    else
                    {
                        r.Status = true;
                        r.Message.Add(Response);
                    }
                }
                else
                {
                    r.Status = false;
                    r.Message.Add(response.IsSuccessStatusCode.ToString());
                }
            }
            catch (Exception ex)
            {

                r.Message.Add("Something went wrong. Please contact Administration!");
            }
            return r;
        }
    }
}

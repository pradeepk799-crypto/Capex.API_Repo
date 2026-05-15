namespace Capex.Models.ResponseModel
{
    public class CaptchaResponseModel:ResponseModelBase
    {
        public string Cipher { get; set; }
        public string CaptchaBase64 { get; set; }
        public string Captcha { get; set; }

    }
}

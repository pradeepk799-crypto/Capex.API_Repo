namespace Capex.Models.RequestModel
{
    public class ValidUserRequestModel : RequestModelBase
    {
             
        public string UserName { get; set; } 
        public string Mobno { get; set; }
        public int flag { get; set; }
        public int TemplateId { get; set; }
        public string Cipher { get; set; }
        public string Captcha { get; set; }
    }
}

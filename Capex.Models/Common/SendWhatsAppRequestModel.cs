namespace Capex.Models.Common
{
    public class WhatsAppModel
    {
        public string? Mobile { get; set; }
        public string? Message { get; set; }
        public string? WhatsappURL { get; set; }
        public string? WhatsAppUserid { get; set; }
        public string? WhatsAppPwd { get; set; }

    }
    public class WhatsAppModelOptINOUT
    {
        public string? Mobile { get; set; }
        public string? WhatsappURL { get; set; }
        public string? WhatsAppUserid { get; set; }
        public string? WhatsAppPwd { get; set; }
        public string ? Type { get; set; }

    }
}

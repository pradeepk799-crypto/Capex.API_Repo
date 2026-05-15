namespace Capex.Models.Common
{
    public class SendSMSRequestModel
    {
        public string? MobileNumber { get; set; }
        public int TemplateId { get; set; }
    }
    public class SmsModel
    {
        public string? Mobile { get; set; }
        public string? Message { get; set; }
        public string? SMSTemplateId { get; set; }

        public string? SMSURL { get; set; }
        public string? SmsUser { get; set; }
        public string? SmsPassword { get; set; }
        public string? SmsSenderId { get; set; }
        public string? SmsSecureKey { get; set; }
        public bool? EnableSms { get; set; }
        public bool? Airtel_EnableSms { get; set; }
        public string? Airtel_SMSURL { get; set; }
        public string? Airtel_SMS_Login_ID { get; set; }
        public string? Airtel_SMS_Password { get; set; }
        public string? Airtel_SMS_SENDER_ID { get; set; }
        public string? Airtel_SMS_PRINCIPAL_ENTITY_ID { get; set; }
        public string? Airtel_SMS_DLT_GOVT { get; set; }
        public string? Airtel_SMS_CAMPAIGN_NAME { get; set; }
        public string? Airtel_SMS_DLT_TM_ID { get; set; }
        public string? Airtel_SMS_DLT_CT_ID { get; set; }
        public string? Airtel_SMS_Unicode { get; set; }
        public string? OTPExpiry { get; set; }

        public string? FUsKey { get; set; }

    }

}

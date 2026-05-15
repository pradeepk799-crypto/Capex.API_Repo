using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.Common
{
    public class ServiceConfigSettings
    {
        public List<ServiceDetails>? Services { get; set; }

    }
    public class ServiceDetails
    {
        public string? ServiceName { get; set; }
        public List<MethodDetails>? Methods { get; set; }
        public List<AadharCreadential>? AadharCreadential { get; set; }
        public List<AuthenticationKey>? Authkey { get; set; }
        public List<SMSKeys>? SmsKeys { get; set; }
        public List<EmailKeys>? EmailKeys { get; set; }
        public List<WhatsAppKeys>? WhatsAppKeys { get; set; }
        public List<AuthenticationKeyIGRS>? AuthkeyIGRS { get; set; }

    }
    public class MethodDetails
    {
        public string? MethodName { get; set; }
        public string? MethodURL { get; set; }
        public string? FamilyUrl { get; set; }
        public string? encryptionKey { get; set; }
        public string? TokenKey { get; set; }
        public string? serviceCode { get; set; }
        public string? deptCode { get; set; }
        public string? applicationCode { get; set; }
    }
    public class AadharCreadential
    {
        public string ProdOTPGen { get; set; }
        public string ProdOTPAUTH { get; set; }
        public string ProdEkyc { get; set; }
        public string PreProdOTPGen { get; set; }
        public string PreProdOTPAUTH { get; set; }
        public string PreProdEkyc { get; set; }
        public string Tid { get; set; }
        public string SA { get; set; }
        public string DomainId { get; set; }
        public string LK { get; set; }
        public string DeptCode { get; set; }
        public string DisclosureInfo { get; set; }
    }
    public class AuthenticationKey
    {
        public string? Key { get; set; }

    }

    public class AuthenticationKeyIGRS
    {
        public string? Key { get; set; }

    }
    public class SMSKeys
    {
        public string? SMSURL { get; set; }
        public string? SmsUser { get; set; }
        public string? SmsPassword { get; set; }
        public string? SmsSenderId { get; set; }
        public string? SmsSecureKey { get; set; }
        public bool? EnableSms { get; set; }
        public string? TemplateId_CDAC { get; set; }
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
    public class EmailKeys
    {
        public bool? EnableEmail { get; set; }
        public string? EmailId { get; set; }
        public string? EmailPassword { get; set; }
        public string? MailServer { get; set; }
        public string? Port { get; set; }
    }
    public class WhatsAppKeys
    {
        public bool? EnableWhatsApp { get; set; }
        public string? WhatsappURL { get; set; }
        public string? WhatsAppUserid { get; set; }
        public string? WhatsAppPwd { get; set; }
    }
}
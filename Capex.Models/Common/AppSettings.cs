using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Models.Common
{
    public class AppSettings
    {
        /// <summary>
        /// It is used for static class.
        /// </summary>
        public static AppSettings Current;

        /// <summary>
        /// In static classes we use Current.
        /// </summary>
        public AppSettings()
        {
            Current = this;
        }
        /// <summary>
        /// Gets or sets the Secret.
        /// </summary>
        /// <value>
        /// The Secret.
        /// </value>
        public string Secret { get; set; }
        /// <summary>
        /// Gets or sets the APITookTime.
        /// </summary>
        /// <value>
        /// APITookTime.
        /// </value>       
        public string APITookTime { get; set; }
        /// <summary>
        /// Gets or sets the SQLCommandTimeout.
        /// </summary>
        /// <value>
        /// SQLCommandTimeout.
        /// </value>
        public string SQLCommandTimeout { get; set; }
        /// <summary>
        /// Gets or sets the IsMultiConnect.
        /// </summary>
        /// <value>
        /// IsMultiConnect.
        /// </value>
        
        public string IsMultiConnect { get; set; }
        public Int16 OTPExpiry { get; set; }
        public  string AccessTokenExpireTime { get; set; }
        public string RefreshTokenExpireTime { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
        /// <summary>
        /// List of SPTookTime
        /// </summary>
        public List<SPTookTime> SPTookTime { get; set; }
        //gov SMS
        public string SmsUser { get; set; }
        public bool EnableSms { get; set; }
        public string SMSURL { get; set; }
        public string SmsPassword { get; set; }
        public string SmsSenderId { get; set; }
        public string SmsSecureKey { get; set; }
        public string TemplateId_CDAC { get; set; }

        //Airtel SMS
        public bool Airtel_EnableSms { get; set; }
        public string Airtel_SMSURL { get; set; }
        public string Airtel_SMS_Login_ID { get; set; }
        public string Airtel_SMS_Password { get; set; }
        public string Airtel_SMS_SENDER_ID { get; set; }
        public string Airtel_SMS_PRINCIPAL_ENTITY_ID { get; set; }
        public string Airtel_SMS_DLT_GOVT { get; set; }
        public string Airtel_SMS_CAMPAIGN_NAME { get; set; }
        public string Airtel_SMS_DLT_TM_ID { get; set; }

        //Email Utility
     
        public string DMSUrl { get; set; }
        
        public string FUsKey { get; set; }   
        public string EmailId { get; set; }
        public string EmailPassword { get; set; }
        public string MailServer { get; set; }
        public bool EnableEmail{ get; set; }
        public string IvValue { get; set; }
        public string EncKey { get; set; }
        public string ReturnURLForUniPay { get; set; }
        public string ReturnUniPayAngularURL { get; set; }
        public string UniPayKey { get; set; }
        public string UniPayStatusCheckURL { get; set; }
        public string DefaultOTP { get; set; }
        public string DefaultCaptcha { get; set; }

    }
    /// <summary>
    /// JwtKeyType
    /// </summary>
    public enum JwtKeyType
    {
        /*
         Symmetric key: The same key is used for both encryption (when the JWT is created) and decryption (Client Together Server uses the key to verify the JWT).
        Asymmetric keys is based on two keys, a public key, and a private key. The public key is used to validate, in this case, the JWT Token. And the private key is used to sign the Token.
         */
        SymmetricKey = 0,
        RSAJSonKey = 1,
        RSAXMLKey = 2,
    }
    /// <summary>
    /// JwtPrivateKey
    /// </summary>
    public class JwtPrivateKey
    {
        /// <summary>
        /// Jwt kty Key
        /// </summary>
        public string kty { get; set; }
        /// <summary>
        /// Jwt n Key
        /// </summary>
        public string n { get; set; }
        /// <summary>
        /// Jwt e Key
        /// </summary>
        public string e { get; set; }
        /// <summary>
        /// Jwt d Key
        /// </summary>
        public string d { get; set; }
        /// <summary>
        /// Jwt p Key
        /// </summary>
        public string p { get; set; }
        /// <summary>
        /// Jwt q Key
        /// </summary>
        public string q { get; set; }
        /// <summary>
        /// Jwt dp Key
        /// </summary>
        public string dp { get; set; }
        /// <summary>
        /// Jwt dq Key
        /// </summary>
        public string dq { get; set; }
        /// <summary>
        /// Jwt qi Key
        /// </summary>
        public string qi { get; set; }
        /// <summary>
        /// Jwt alg Key
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// Jwt kid Key
        /// </summary>
        public string kid { get; set; }
        /// <summary>
        /// Jwt use Key
        /// </summary>
        public string use { get; set; }

    }
    /// <summary>
    /// Jwt JwtPublicKey Key
    /// </summary>
    public class JwtPublicKey
    {
        /// <summary>
        /// Jwt kty Key
        /// </summary>
        public string kty { get; set; }
        /// <summary>
        /// Jwt e Key
        /// </summary>
        public string e { get; set; }
        /// <summary>
        /// Jwt use Key
        /// </summary>
        public string use { get; set; }
        /// <summary>
        /// Jwt kid Key
        /// </summary>
        public string kid { get; set; }
        /// <summary>
        /// Jwt alg Key
        /// </summary>
        public string alg { get; set; }
        /// <summary>
        /// Jwt n Key
        /// </summary>
        public string n { get; set; }
        /// <summary>
        /// Jwt x5c Key
        /// </summary>
        public string[] x5c { get; set; }
       

    } 
}

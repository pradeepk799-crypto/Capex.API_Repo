using Capex.DomainModels.DomainRequestModel;
using System.Security.Cryptography.X509Certificates;

namespace Capex.DomainModels.Common
{
    public class AadharlogModel: DomainRequestModelBase
    {      
        public string ReferenceKey { get; set; }
        public string TxnID { get; set; }
        public string TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string DeviceID { get; set; }
        public string RequestID { get; set; }
        public int? RecordOfConsent { get; set; }
        public string TextOfConsent { get; set; }
        public decimal? ConsentVersion { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
        public string AuthType { get; set; }
        public string OperatorID { get; set; }
        public string UIDToken { get; set; }
        public string? AuaErrorCode { get; set; }
        public string? SrdhErrorCode { get; set; }
        public string? Ret { get; set; }       
        public int CreatedBy { get; set; }      
        public string? IPAddress { get; set; }
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
        public string?  ResName { get; set; }
        public string  ResidentPhoto { get; set; }
        public int FileUploadId { get; set; }
        public string?  DOB { get; set; }
        public string? Gender { get; set; }
        public string? GuardianName { get; set; }
        public string?  GuardianRelationType { get; set; }
        public string?  Building { get; set; }
        public string?  Street { get; set; }
        public string?  Landmark { get; set; }
        public string?  Locality { get; set; }
        public string?  VTC { get; set; }
        public string? Subdistrict { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }
    }

    public class AadharObject
    {
        public string referenceKey { get; set; }
      
        public string deviceID { get; set; }
        public string requestID { get; set; }
        public int? recordOfConsent { get; set; }
        public string textOfConsent { get; set; }
        public decimal? consentVersion { get; set; }
        public DateTime? requestTimestamp { get; set; }
        public DateTime? responseTimestamp { get; set; }
        public string authType { get; set; }
        public string operatorID { get; set; }
        public string uidToken { get; set; }
        public string? auaErrorCode { get; set; }
        public string? srdhErrorCode { get; set; }
        public int createdBy { get; set; }
        public string? iPAddress { get; set; }
        public string? requestData { get; set; }
        public string? responseData { get; set; }
        public string? resName { get; set; }
        public string residentPhoto { get; set; }
        public int fileUploadId { get; set; }
        public string? dob { get; set; }
        public string? gender { get; set; }
        public string? guardianName { get; set; }
        public string? guardianRelationType { get; set; }
        public string? building { get; set; }
        public string? street { get; set; }
        public string? landmark { get; set; }
        public string? locality { get; set; }
        public string? vtc { get; set; }
        public string? subdistrict { get; set; }
        public string? district { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? pincode { get; set; }
    }

    public class PrnInfo
    {
        public string prnValue { get; set; }
        public string type { get; set; }
    }

    public class AadharObjectList
    {
        public string auaErrorCode { get; set; }
        public string srdhErrorCode { get; set; }
        public string ret { get; set; }
        public string txn { get; set; }
        public string transactionCode { get; set; }
        public string refKey { get; set; }
        public AadharObject residentDetails { get; set; }
        public string ttl { get; set; }
        public string transactionId { get; set; }
    }

    class Program
    {
        static void Main()
        {
            // You can deserialize your JSON string into this C# class using a JSON deserializer library, such as Newtonsoft.Json.
            // Example:
            // string jsonString = "your JSON string here";
            // ModelProperty model = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelProperty>(jsonString);
        }
    }
    public class DomainAadharlogModel : DomainRequestModelBase
    {
        public string ReferenceKey { get; set; }
        public string TxnID { get; set; }
        public string TransactionId { get; set; }
        public string TransactionCode { get; set; }
        public string DeviceID { get; set; }
        public string RequestID { get; set; }
        public int? RecordOfConsent { get; set; }
        public string TextOfConsent { get; set; }
        public decimal? ConsentVersion { get; set; }
        public DateTime? RequestTimestamp { get; set; }
        public DateTime? ResponseTimestamp { get; set; }
        public string AuthType { get; set; }
        public string OperatorID { get; set; }
        public string UIDToken { get; set; }
        public string? AuaErrorCode { get; set; }
        public string? SrdhErrorCode { get; set; }
        public string? Ret { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
     
    }


    public class DomainAadharDetailsModel : DomainRequestModelBase
    {
        public string LogId { get; set; }
        public string ReferenceKey { get; set; }
        public string? Ret { get; set; }
        public int CreatedBy { get; set; }
        public string? IPAddress { get; set; }
        public string? ResName { get; set; }
        public int FileUploadId { get; set; }
        public string? DOB { get; set; }
        public string? Gender { get; set; }
        public string? GuardianName { get; set; }
        public string? GuardianRelationType { get; set; }
        public string? Building { get; set; }
        public string? Street { get; set; }
        public string? Landmark { get; set; }
        public string? Locality { get; set; }
        public string? VTC { get; set; }
        public string? Subdistrict { get; set; }
        public string? District { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Pincode { get; set; }


    }
}

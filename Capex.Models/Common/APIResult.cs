using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Capex.Models.Common
{
    public class APIResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ApiResult<T>
        {
            /// <summary>
            /// API Response Message
            /// </summary>
            [StringLength(200)]
            public string? Message { get; set; }

            /// <summary>
            /// API Status 
            /// Example: Success - true, Error - false
            /// </summary>
            public bool Status { get; set; }

           
            /// <summary>
            /// Response Message Error Code
            /// </summary>
            [StringLength(10)]
            public string? ErrorCode { get; set; }

            /// <summary>
            /// Contains all Response properties
            /// </summary>
            public T? ResponseData { get; set; }

            /// <summary>
            /// Error
            /// </summary>
            [JsonIgnore]
            [IgnoreDataMember]
            public CustomException? Error { get; set; }

    }
        /// <summary>
        /// CustomException
        /// </summary>
        [Serializable]
        public class CustomException : Exception
        {
            /// <summary>
            /// 
            /// </summary>
            public CustomException()
                : base() { }
            /// <summary>
            /// CustomException
            /// </summary>
            public CustomException(string message)
              : base(message) { }
            /// <summary>
            /// CustomException
            /// </summary>
            public CustomException(string format, params object[] args)
                : base(string.Format(format, args)) { }
            /// <summary>
            /// CustomException
            /// </summary>
            public CustomException(string message, Exception innerException)
                : base(message, innerException) { }
            /// <summary>
            /// CustomException
            /// </summary>
            public CustomException(string format, Exception innerException, params object[] args)
                : base(string.Format(format, args), innerException) { }
            /// <summary>
            /// CustomException
            /// </summary>
            protected CustomException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }
        }
    }
}

namespace Capex.Models.Common
{
    public class JWTTokenResponseEntity
    {
        public class ResponseModel
        {
            public string Status { get; set; }
            public int? StatusCode { get; set; }
            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class ErrorResponse
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class Users
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}

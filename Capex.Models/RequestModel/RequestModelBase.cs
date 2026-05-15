namespace Capex.Models.RequestModel
{
    public class RequestModelBase
    {
        public string? Language { get; set; }
        public string? AuthHeader { get; set; }
        public string? Area { get; set; } 
        public string? Controller { get; set; }
        public string? ActionName { get; set; }
        public int? UserId { get; set; }
        public int? UserRoleId { get; set; }
        public int? UserOfficeId { get; set; }

    }
}

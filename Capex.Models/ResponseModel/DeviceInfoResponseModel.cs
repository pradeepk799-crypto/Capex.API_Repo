namespace Capex.Models.ResponseModel
{
    public class DeviceInfoResponseModel : ResponseModelBase
    {
        public string? MacId { get; set; }
        public string? TokenId { get; set; }
        public bool? IsLoggedIn { get; set; }
        public string? IpAddress { get; set; }
        public string? HostName { get; set; }
        public string? DeviceName { get; set; }
        public string? OSName { get; set; }
        public string? OSVersion { get; set; }
        public string? BrowserName { get; set; }
        public string? BrowserVersion { get; set; }
        public string? BrandName { get; set; }
        public string? ModelName { get; set; }
        public string? DeviceType { get; set; }

    }
}

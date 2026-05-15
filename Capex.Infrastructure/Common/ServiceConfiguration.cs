using Newtonsoft.Json;
using Capex.Models.Common;

namespace Capex.Infrastructure.Common
{
    public static class ServiceConfiguration
    {
        static ServiceConfiguration()
        {
            string Path = Directory.GetCurrentDirectory() + "\\ServiceConfiguration.json";
            StreamReader file = File.OpenText(Path);
            dynamic GetServiceConfData = file.ReadToEnd();
            serviceConfigSettings = JsonConvert.DeserializeObject<ServiceConfigSettings>(GetServiceConfData);

        }

        public static ServiceConfigSettings serviceConfigSettings { get; set; }         
    }
}

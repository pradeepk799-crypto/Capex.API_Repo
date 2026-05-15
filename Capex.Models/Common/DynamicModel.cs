namespace Capex.Models.Common
{

    public class DynamicModel
    {
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public T? GetProperty<T>(string propertyName)
        {
            if (Properties.ContainsKey(propertyName) && Properties[propertyName] is T value)
            {
                return value;
            }
            return default;
        }
        public string MobileNumber
        {
            get => GetProperty<string>("MobileNumber");
            set => Properties["MobileNumber"] = value;
        }
       
    }

}

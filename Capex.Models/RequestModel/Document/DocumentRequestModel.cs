namespace Capex.Models.RequestModel.Document
{
    public class HTMLDocumentRequestModel:RequestModelBase
    {
        public int ApplicationId { get; set; }
        public string? EncApplicationId { get; set; }

        // Modify to accept a list of key-value pairs (since you're receiving an array)
        public List<KeyValuePair<string, object>>? KeyValuePairs { get; set; }

        public int TemplateId { get; set; }

        // Constructor to initialize the list
        public HTMLDocumentRequestModel()
        {
            KeyValuePairs = new List<KeyValuePair<string, object>>();
        }
    }
}

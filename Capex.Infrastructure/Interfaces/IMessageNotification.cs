using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Common;
using Capex.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Interfaces
{
    public interface IMessageNotification
    {
        Task<MessagesNotificationModel> GetTemplateData(int templateId);
        Task<int> PushMessageNotification(PushMessageNotification request);
        Task<dynamic> GetTemplateQueryData(int applicationId,string query);
        Task<List<TemplateResponse>> GetTemplates(TemplateRequest request);
        Task<dynamic> GetTemplateQueryDataNew(int applicationId, string query, List<KeyValuePair<string, object>>? keyValuePairs = null);
        Task<string> GetUserNameAsync(string mobile, string type);

        Task<int> InsertSmsLogAsync(string request);
    }
}

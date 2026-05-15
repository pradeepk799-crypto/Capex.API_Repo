using Capex.Models.ResponseModel.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;

namespace Capex.Business.Interfaces
{
    public interface INotification
    {
        Task<Result<string>> SendSMS(dynamic obj, int templateId);
        Task<ApiResult<string>> SendMail(dynamic obj, int templateId);
        Task<Result<dynamic>> SendWhatsApp(dynamic obj, int templateId);
        Task<Result<dynamic>> SendWhatsAppOptInOut(string MobileNo, string Type);
        Task<Result<string>> SendSMSUser(dynamic obj, int templateId);
    }
}

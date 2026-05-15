using Capex.Infrastructure.Services;

namespace Capex.Infrastructure.Interfaces
{
    public interface IInfrastructureServices
    {
        /// <summary>
        /// Gets the User.
        /// </summary>
        IUser User { get; }
        IMasters Masters { get; }
        IDBLogger DBLogger { get; }

        IDashboard Dashboard { get; }
        IMessageNotification MessageNotification { get; }
       
        IUploadFileService UploadFileService { get; }

        IBillGenerationInfra BillGenerationInfra { get; }
    }
}

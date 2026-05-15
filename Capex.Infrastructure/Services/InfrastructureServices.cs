using Capex.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Services
{
    public class InfrastructureServices : IInfrastructureServices
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InfrastructureServices" /> class.
        /// </summary>
        /// <seealso cref="IInfrastructureServices" />
        /// <param name="user">IUser.</param>


        public InfrastructureServices(IUser user, IMasters masters, 
            IDBLogger dBLogger, IDashboard dashboard, IMessageNotification messageNotification, IUploadFileService uploadfileservice)
            
        {
            this.User = user;
            this.Masters = masters;
            this.DBLogger = dBLogger;
            this.Dashboard = dashboard;
            this.MessageNotification = messageNotification;
           
            this.UploadFileService = uploadfileservice;


        }
        /// <summary>
        /// Gets the User.
        /// </summary>
        public IUser User { get; }
        /// <summary>
        /// Gets the Masters.
        /// </summary>
        public IMasters Masters { get; }
        public IDBLogger DBLogger { get; }
        public IDashboard Dashboard { get; }
        public IMessageNotification MessageNotification { get; }
       
        public IUploadFileService UploadFileService { get; }

        public IBillGenerationInfra BillGenerationInfra { get; }
    }
}

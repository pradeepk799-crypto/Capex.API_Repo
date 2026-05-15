using Capex.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Services
{
    public class BusinessServices : IBusinessServices
    {
        public BusinessServices(IUser user, IMasters masters,INotification notification, IDBLogger dBLogger)
        {
            this.User = user;
            this.Masters = masters;
            this.DBLogger = dBLogger;
            this.Notification = notification;
        }
        public IUser User { get; }
        public IMasters Masters { get; }
        public IDBLogger DBLogger { get; }
        public INotification Notification { get; }

        public IBillGeneration BillGeneration { get; }
   

    }
}

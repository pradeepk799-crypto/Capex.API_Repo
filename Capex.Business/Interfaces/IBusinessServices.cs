using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Interfaces
{
    public interface IBusinessServices
    {
        /// <summary>
        /// Gets the UserBLL.
        /// </summary>
        IUser User { get; }
        /// <summary>
        /// Gets the MastersBLL.
        /// </summary>
        IMasters Masters { get; }
        /// <summary>
        /// Save error log
        /// </summary>
        /// <param name="errorLog"></param>
        IDBLogger DBLogger { get; }

        INotification Notification { get; }
   
        IBillGeneration BillGeneration { get; }
    }
}

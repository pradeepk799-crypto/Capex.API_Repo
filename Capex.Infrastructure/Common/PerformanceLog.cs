using Newtonsoft.Json.Linq;
using Capex.DomainModels.Common;
using Capex.Models.Common;
using Capex.Utilities.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Common
{
    public static class PerformanceLog
    {
        /// <summary>
        /// Function used to log SP took time.
        /// </summary>
        /// <param name="spName">spName.</param>
        /// <param name="ms">ms.</param>
        public static void LogSPTime(string spName, long ms)
        {
                 

            
            AppSettings.Current.SPTookTime = new List<SPTookTime>();
            

            AppSettings.Current.SPTookTime.Add(new SPTookTime { USPName = spName, USPTime = ms });

           

            if (!string.IsNullOrWhiteSpace(Capex.Models.Common.AppSettings.Current.APITookTime) && Convert.ToInt64(Capex.Models.Common.AppSettings.Current.APITookTime) < ms)
            {
                Log.Debug(string.Format(spName + LoggerMessage.SPTookTIME, ms));
            }

          
        }

        public static void LogSPTime(string sessionid, string spName, long ms)
        {

            #region Add USP TookTime Sarvgya Jain | Aug 2023

            if (Capex.Models.Common.AppSettings.Current.SPTookTime == null)
            {
                Capex.Models.Common.AppSettings.Current.SPTookTime = new List<Models.Common.SPTookTime>();
            }

            Capex.Models.Common.AppSettings.Current.SPTookTime.Add(new Models.Common.SPTookTime { USPName = spName, USPTime = ms, SessionID = sessionid });

            #endregion

            if (!string.IsNullOrWhiteSpace(Capex.Models.Common.AppSettings.Current.APITookTime) && Convert.ToInt64(Capex.Models.Common.AppSettings.Current.APITookTime) < ms)
            {
                Log.Debug(string.Format(spName + LoggerMessage.SPTookTIME, ms));
            }

            
        }
    }
}

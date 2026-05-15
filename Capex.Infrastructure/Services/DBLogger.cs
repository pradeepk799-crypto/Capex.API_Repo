using Capex.Infrastructure.Common;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;

using Capex.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Capex.Infrastructure.Common.DBConstants;

namespace Capex.Infrastructure.Services
{
    public class DBLogger : IDBLogger
    {
        public DBType DataBase => DBType.MasterDB;
        public async void AddErrorLog(string errorLog)
        {
            //DBManager.ParameterList param = new DBManager.ParameterList();
            //param.Add(new DBManager.SQLParameter("@ErrorLog", errorLog));
            //DbHelper.ExecuteQuery.ExecuteReader(RCMSSP.SP_ADDAPPLICATIONERRORLOG, param, DBType.RCMS);

            SqlCommand dbCommand = null;
            try
            {
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.RCMSSP.SP_ADDAPPLICATIONERRORLOG, this.DataBase);

                #region Pass Arguments to Stored Procedure
                dbCommand.Parameters.AddWithValue("@ErrorLog", errorLog);
                int i = dbCommand.ExecuteNonQuery();
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

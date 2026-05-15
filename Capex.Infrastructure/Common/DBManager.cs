using Newtonsoft.Json;
using Capex.Models.Common;
using Capex.Utilities.Common;
using Serilog;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Capex.Infrastructure.Common
{
    public static class DBManager
    {
        #region Private Variables

        private static readonly string MasterDBConnectionString = string.Empty;

        #endregion

        static DBManager()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(ConnectionStrings.ConnectionStringMasterDB))
                {
                    MasterDBConnectionString = ConnectionStrings.ConnectionStringMasterDB;
                }
                 
            }
            catch (Exception ex)
            {
                MasterDBConnectionString = string.Empty;
                Log.Error(LoggerMessage.ErrorMessage + ErrorMessage.UnableToLoadConnectionStringMsg, ex);
            }
        }

        #region Public Static Methods

        /// <summary>
        /// Gets the stored proc command.
        /// </summary>
        /// <param name="storedProcName">Name of the stored proc.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="enableAuditLog">if set to <c>true</c> [enable audit log].</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>SqlCommand.</returns>
        public static SqlCommand GetStoredProcCommand(string storedProcName, DBType dbType, bool enableAuditLog = true, bool readOnly = false)
        {
            Log.Debug(LoggerMessage.Begin);

            SqlConnection objSqlConnection = null;
            SqlCommand objSqlCommand = null;


            try
            {
                if (dbType == DBType.MasterDB)
                    objSqlConnection = GetMasterDBConnection();

            }
            catch (Exception ex)
            {
                Log.Error(string.Format(ErrorMessage.FailedToOpenConnection, dbType.ToString()), ex);
                throw;
            }

            if (objSqlConnection != null)
            {
                objSqlCommand = new SqlCommand(storedProcName, objSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                Log.Warning(ErrorMessage.ConnectedtoDataBaseMsg + objSqlConnection.ConnectionString);
                try
                {
                    objSqlCommand.CommandTimeout = Convert.ToInt32(AppSettings.Current.SQLCommandTimeout);
                }
                catch (Exception ex1)
                {
                    Log.Error(ErrorMessage.ErrorFoundInAppsettingSection + ex1);
                }
            }

            if (objSqlCommand == null || objSqlCommand.Connection.State != ConnectionState.Open)
            {
                Log.Error(string.Format(ErrorMessage.ErrorFoundInAllocatingDatabase, dbType.ToString()));
            }

            //LoggerUtil.Current.Debug(LoggerMessage.End);
            return objSqlCommand; // Will never be null in any way
        }

        /// <summary>
        /// Gets the SQL command.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <returns>SqlCommand.</returns>
        public static SqlCommand GetSqlCommand(string sqlCommand, DBType dbType)
        {
            SqlConnection objSqlConnection = null;
            SqlCommand objSqlCommand = null;

            try
            {
                if (dbType == DBType.MasterDB)
                {
                    objSqlConnection = GetMasterDBConnection();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ErrorMessage.FailedToOpenConnection);
                throw;
            }

            if (objSqlConnection != null)
            {
                objSqlCommand = new SqlCommand(sqlCommand, objSqlConnection)
                {
                    CommandType = CommandType.Text,
                };

                try
                {
                    objSqlCommand.CommandTimeout = Convert.ToInt32(AppSettings.Current.SQLCommandTimeout);
                }
                catch (Exception ex1)
                {
                    Log.Warning(ErrorMessage.ErrorFoundInAppsettingSectionZeroBrace + ex1);
                    Log.Error(ErrorMessage.CommandTimeOut, ex1);
                }
            }

            return objSqlCommand; // Will never be null in any way
        }
        /// <summary>
        /// Gets the SQL command.
        /// </summary>
        /// <param name="sqlCommand">The SQL command.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <returns>SqlCommand.</returns>
        public static async Task<SqlCommand> GetSqlCommandAsync(string sqlCommand, DBType dbType)
        {
            SqlConnection objSqlConnection = null;
            SqlCommand objSqlCommand = null;

            try
            {
                if (dbType == DBType.MasterDB)
                {
                    objSqlConnection = await GetMasterDBConnectionAsync();
                }

            }
            catch (Exception ex)
            {
                Log.Error(ErrorMessage.FailedToOpenConnection);
                throw;
            }

            if (objSqlConnection != null)
            {
                objSqlCommand = new SqlCommand(sqlCommand, objSqlConnection)
                {
                    CommandType = CommandType.Text,
                };

                try
                {
                    objSqlCommand.CommandTimeout = Convert.ToInt32(AppSettings.Current.SQLCommandTimeout);
                }
                catch (Exception ex1)
                {
                    Log.Warning(ErrorMessage.ErrorFoundInAppsettingSectionZeroBrace + ex1);
                    Log.Error(ErrorMessage.CommandTimeOut, ex1);
                }
            }

            return objSqlCommand; // Will never be null in any way
        }

        /// <summary>
        /// Gets Master Database Connection.
        /// </summary>
        /// <returns>SqlConnection.</returns>

        public static SqlConnection GetMasterDBConnection()
        {
            Log.Debug(LoggerMessage.MasterDBBegin);
            SqlConnection conn = null;
            if (!string.IsNullOrWhiteSpace(MasterDBConnectionString))
            {
                conn = new SqlConnection(MasterDBConnectionString);
                conn.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                conn.Open();
            }

            Log.Debug(LoggerMessage.MasterDBEnd);

            return conn;
        }


        public static void CloseConnection(DbCommand oCommandObject, bool enableAuditLog = true)
        {
            //LoggerUtil.Current.Debug(LoggerMessage.CloseDBBegin);
            if (oCommandObject.Connection != null)
            {
                oCommandObject.Connection.Close();
            }

            //LoggerUtil.Current.Debug(LoggerMessage.CloseDBEnd);
        }
        internal static void InfoMessageHandler(object sender, SqlInfoMessageEventArgs e)
        {

        }

        /// <summary>
        /// Sets the database nullsfor defaults.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        public static void SetDBNullsforDefaults(ref SqlCommand dbCommand)
        {
            switch (dbCommand.CommandType)
            {
                case CommandType.StoredProcedure:

                    for (int i = 0; i < dbCommand.Parameters.Count; i++)
                    {
                        if (dbCommand.Parameters[i].Value == null)
                        {
                            dbCommand.Parameters[i].Value = DBNull.Value;
                        }
                    }

                    break;

                case CommandType.Text:

                    for (int i = 0; i < dbCommand.Parameters.Count; i++)
                    {
                        if (dbCommand.Parameters[i].Value == null)
                        {
                            dbCommand.Parameters[i].Value = DBNull.Value;
                        }
                    }

                    break;
            }
        }


        /// <summary>
        /// Gets CoreIssue Database Connection.
        /// </summary>
        /// <returns>SqlConnection.</returns>
        /// remarks
        /// Abhishek Singh: Changed to use string.IsNullOrWhiteSpace() to check connection string rather than string.Empty
        public static async Task<SqlConnection> GetMasterDBConnectionAsync()
        {
            //LoggerUtil.Current.Debug(LoggerMessage.Begin);
            SqlConnection conn = null;
            if (!string.IsNullOrWhiteSpace(MasterDBConnectionString))
            {
                conn = new SqlConnection(MasterDBConnectionString);
                conn.InfoMessage += new SqlInfoMessageEventHandler(InfoMessageHandler);
                await conn.OpenAsync();
            }

            //LoggerUtil.Current.Debug(LoggerMessage.End);
            return conn;
        }

         
        /// <summary>
        /// Gets the stored proc command.
        /// </summary>
        /// <param name="storedProcName">Name of the stored proc.</param>
        /// <param name="dbType">Type of the database.</param>
        /// <param name="enableAuditLog">if set to <c>true</c> [enable audit log].</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>SqlCommand.</returns>
        public static async Task<SqlCommand> GetStoredProcCommandAsync(string storedProcName, DBType dbType, bool enableAuditLog = true, bool readOnly = false)
        {
            //LoggerUtil.Current.Debug(LoggerMessage.Begin);

            SqlConnection objSqlConnection = null;
            SqlCommand objSqlCommand = null;


            try
            {
                //LoggerUtil.Current.Warn(Value.ReadOnly + readOnly.ToString());
                if (dbType == DBType.MasterDB)
                {
                    objSqlConnection = await GetMasterDBConnectionAsync();
                }
                else
                {
                    objSqlConnection = null;
                }


            }
            catch (Exception ex)
            {
                //LoggerUtil.Current.Error(string.Format(ErrorMessage.FailedToOpenConnection, dbType.ToString(Value.F)), ex);
                throw;
            }

            if (objSqlConnection != null)
            {
                objSqlCommand = new SqlCommand(storedProcName, objSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                };

                //LoggerUtil.Current.Warn(ErrorMessage.ConnectedtoDataBaseMsg + objSqlConnection.ConnectionString);
                try
                {
                    objSqlCommand.CommandTimeout = Convert.ToInt32(AppSettings.Current.SQLCommandTimeout);
                }
                catch (Exception ex1)
                {
                    //LoggerUtil.Current.Error(ErrorMessage.ErrorFoundInAppsettingSection + ex1);
                }
            }

            if (objSqlCommand == null || objSqlCommand.Connection.State != ConnectionState.Open)
            {
                //LoggerUtil.Current.Error(string.Format(ErrorMessage.ErrorFoundInAllocatingDatabase, dbType.ToString(Value.F)));
            }

            //LoggerUtil.Current.Debug(LoggerMessage.End);
            return objSqlCommand; // Will never be null in any way
        }

        /// <summary>
        /// GetStoredProcCommandAsync.
        /// </summary>
        /// <param name="getCollateralIDHistory">object of getCollateralIDHistory.</param>
        /// <param name="dataBase">Type of the database.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>SqlCommand.</returns>
        internal static Task<SqlCommand> GetStoredProcCommandAsync(object getCollateralIDHistory, DBType dataBase, bool readOnly)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Traces the database command.
        /// </summary>
        /// <param name="dbCommand">The database command.</param>
        [Conditional("TRACE")]
        public static void TraceDbCommand(DbCommand dbCommand)
        {
            Log.Debug(LoggerMessage.TraceDBBegin);

            StringBuilder sqlText = new StringBuilder();

            switch (dbCommand.CommandType)
            {
                case CommandType.StoredProcedure:
                    {
                        sqlText.AppendLine(string.Format("EXEC {0}", dbCommand.CommandText));

                        for (int i = 0; i < dbCommand.Parameters.Count; i++)
                        {
                            if (i != dbCommand.Parameters.Count - 1)
                            {
                                if (dbCommand.Parameters[i].Value != null)
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} ,--Direction:{2}", dbCommand.Parameters[i].ParameterName, dbCommand.Parameters[i].Value.ToString(), dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                                else
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} ,--Direction:{2}", dbCommand.Parameters[i].ParameterName, "NULL", dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                            }
                            else
                            {
                                if (dbCommand.Parameters[i].Value != null)
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} --Direction:{2}", dbCommand.Parameters[i].ParameterName, dbCommand.Parameters[i].Value.ToString(), dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                                else
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} --Direction:{2}", dbCommand.Parameters[i].ParameterName, "NULL", dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                            }
                        }

                        Log.Debug(sqlText.ToString());

                        break;
                    }

                case CommandType.Text:
                    {
                        sqlText.AppendLine(string.Format("EXEC {0}", dbCommand.CommandText));

                        for (int i = 0; i < dbCommand.Parameters.Count; i++)
                        {
                            if (i != dbCommand.Parameters.Count - 1)
                            {
                                if (dbCommand.Parameters[i].Value != null)
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} ,--Direction:{2}", dbCommand.Parameters[i].ParameterName, dbCommand.Parameters[i].Value.ToString(), dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                                else
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} ,--Direction:{2}", dbCommand.Parameters[i].ParameterName, "NULL", dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                            }
                            else
                            {
                                if (dbCommand.Parameters[i].Value != null)
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} --Direction:{2}", dbCommand.Parameters[i].ParameterName, dbCommand.Parameters[i].Value.ToString(), dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                                else
                                {
                                    sqlText.AppendLine(string.Format(" {0} = {1} --Direction:{2}", dbCommand.Parameters[i].ParameterName, "NULL", dbCommand.Parameters[i].Direction.ToString("F")));
                                }
                            }
                        }

                        Log.Error(string.Format(sqlText.GetType().Name + " :{0}", JsonConvert.SerializeObject(sqlText)));

                        break;
                    }
            }

            Log.Debug(LoggerMessage.TraceDBEnd);
        }

        #endregion
        public static async Task<SqlCommand> GetTuppleStoredProcCommandAsync(string storedProcName, DBType dbType, bool enableAuditLog = true, bool readOnly = false)
        {
            SqlConnection objSqlConnection = null;
            SqlCommand objSqlCommand = null;

            try
            {
                // Get the database connection
                if (dbType == DBType.MasterDB)
                {
                    objSqlConnection = await GetMasterDBConnectionAsync();
                }
                else
                {
                    throw new Exception("Invalid DBType. Connection cannot be null.");
                }

                if (objSqlConnection == null)
                {
                    throw new Exception("Failed to retrieve database connection.");
                }

                // Ensure the connection is open
                if (objSqlConnection.State != ConnectionState.Open)
                {
                    await objSqlConnection.OpenAsync();
                }

                // Create the command with the open connection
                objSqlCommand = new SqlCommand(storedProcName, objSqlConnection)
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = Convert.ToInt32(AppSettings.Current.SQLCommandTimeout) // Read from config
                };

                return objSqlCommand;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating stored procedure command: {storedProcName}", ex);
            }
        }

    }
}

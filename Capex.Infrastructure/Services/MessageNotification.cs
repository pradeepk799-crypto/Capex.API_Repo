using Microsoft.Extensions.Logging;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using System.Data.SqlClient;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using System.Text.Json;
using Capex.DomainModels.DomainResponseModel;
using Capex.Models.ResponseModel;
using Newtonsoft.Json;
using static Capex.Models.Common.APIResult;
using System.Data;


namespace Capex.Infrastructure.Services
{
    public class MessageNotification : IMessageNotification
    {
        public ILogger<MessageNotification> _logger { get; }
        public DBType DataBase => DBType.MasterDB;
        public MessageNotification(ILogger<MessageNotification> logger)
        {
            this._logger = logger;
        }
        public async Task<MessagesNotificationModel> GetTemplateData(int templateId)
        {
            SqlCommand dbCommand = null;
            try
            {
                MessagesNotificationModel response = new MessagesNotificationModel();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetTemplate, this.DataBase);
                dbCommand.Parameters.AddWithValue("@TemplateId", templateId);
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetTemplate, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    if (dataReader.Read())
                    {
                        response.ModuleId = DbHelper.CheckDbNullInt(dataReader["ModuleId"].ToString());
                        response.TemplateId = DbHelper.CheckDbNullInt(dataReader["TemplateId"].ToString());
                        response.TemplateDesription = DbHelper.CheckDbNullString(dataReader["TemplateDesription"].ToString());
                        response.TemplateType = DbHelper.CheckDbNullString(dataReader["TemplateType"].ToString());
                        response.Body = DbHelper.CheckDbNullString(dataReader["Body"].ToString());
                        response.Query = DbHelper.CheckDbNullString(dataReader["Query"].ToString());
                        response.CreatedOn = DbHelper.CheckDbNullString(dataReader["CreatedOn"].ToString());
                        response.CreatedBy = DbHelper.CheckDbNullString(dataReader["CreatedBy"].ToString());
                        response.IsActive = DbHelper.CheckDbNullString(dataReader["IsActive"].ToString());
                        response.Subject = DbHelper.CheckDbNullString(dataReader["Subject"].ToString());
                        response.TemplateTypeId = DbHelper.CheckDbNullInt(dataReader["TemplateTypeId"].ToString());
                        response.TemplateCode = DbHelper.CheckDbNullString(dataReader["TemplateCode"].ToString());
                        response.ActiveLink = DbHelper.CheckDbNullString(dataReader["ActiveLink"].ToString());

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }
        public async Task<List<TemplateResponse>> GetTemplates(TemplateRequest request)
        {
            SqlCommand dbCommand = null;
            try
            {
                int iOrdinal = 0;
                List<TemplateResponse> response = new List<TemplateResponse>();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetTemplate, this.DataBase);
                SqlParameter[] parameters = DbHelper.AddSQLParameters<TemplateRequest>(request);
                dbCommand.Parameters.AddRange(parameters);
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetOffice, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        TemplateResponse _office = new TemplateResponse();

                        iOrdinal = dataReader.GetOrdinal("TemplateId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.TemplateId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("TemplateDesription");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.TemplateDesription = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("TemplateType");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.TemplateType = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Body");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.Body = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Query");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.Query = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Subject");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.Subject = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("TemplateTypeId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            _office.TemplateTypeId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        response.Add(_office);
                    }
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }
        public async Task<int> PushMessageNotification(PushMessageNotification request)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.PostMessageNotification, this.DataBase);
                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<Capex.Infrastructure.Common.PushMessageNotification>(request);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                int task = Convert.ToInt32(dbCommand.ExecuteNonQuery());
                DBManager.TraceDbCommand(dbCommand);
                if (task > 0)
                {
                    swatch.Stop();
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.PostMessageNotification, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                    this._logger.LogDebug(LoggerMessage.End);
                    return task;
                }
                else
                {
                    this._logger.LogDebug(LoggerMessage.End);
                    return task;
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }
        public async Task<int> InsertSmsLogAsync(string request)
        {
            int insertedId = 0;
            SqlCommand dbCommand = null;

            this._logger.LogDebug("Starting InsertSmsLogAsync process.");

            try
            {
                dbCommand = await DBManager.GetStoredProcCommandAsync("InsertSmsLogJson", this.DataBase);

                dbCommand.Parameters.AddWithValue("@JsonData", request);

                // Output parameter for inserted ID
                SqlParameter outputParam = new SqlParameter("@InsertedId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                dbCommand.Parameters.Add(outputParam);

                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                await dbCommand.ExecuteNonQueryAsync(); // Async execution
                DBManager.TraceDbCommand(dbCommand);    // Optional: Log trace info

                stopwatch.Stop();

                insertedId = Convert.ToInt32(outputParam.Value);

                this._logger.LogDebug("InsertSmsLogAsync completed successfully. InsertedId: {InsertedId}", insertedId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in InsertSmsLogAsync.");
            }

            return insertedId;
        }


     
        public async Task<dynamic?> GetTemplateQueryData(int applicationId, string query)
        {
            SqlCommand dbCommand = null;
            try
            {
                dynamic? response = null;
                dbCommand = await DBManager.GetSqlCommandAsync(query, this.DataBase);
                dbCommand.Parameters.AddWithValue("@Id", applicationId);



                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                int iOrdinal = 0;
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetTemplate, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    if (dataReader.Read())
                    {

                        response = dataReader.GetValue(0).ToString();
                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }
        public async Task<dynamic?> GetTemplateQueryDataNew(int applicationId, string query, List<KeyValuePair<string, object>>? keyValuePairs)
        {
            SqlCommand dbCommand = null;
            try
            {
                dynamic? response = null;
                dbCommand = await DBManager.GetSqlCommandAsync(query, this.DataBase);
                dbCommand.Parameters.AddWithValue("@Id", applicationId);

                bool isDefault = keyValuePairs.Count == 1 &&
                     keyValuePairs[0].Key == "string" &&
                     keyValuePairs[0].Value.ToString() == "string";


                if (!isDefault)
                {
                    foreach (var kvp in keyValuePairs)
                    {
                        string parameterName = $"@{kvp.Key}"; // This creates a parameter like @Year, @Month, etc.

                        // Handle JsonElement type and convert it to a proper SQL type (string in this case)
                        object parameterValue = kvp.Value;

                        // Check if the value is a JsonElement and handle it
                        if (parameterValue is JsonElement jsonElement)
                        {
                            if (jsonElement.ValueKind == JsonValueKind.String)
                            {
                                parameterValue = jsonElement.GetString(); // Convert to string
                            }
                            else if (jsonElement.ValueKind == JsonValueKind.Number)
                            {
                                parameterValue = jsonElement.GetDecimal(); // Convert to decimal (or use GetInt32, GetDouble, etc.)
                            }
                            else
                            {
                                parameterValue = jsonElement.ToString(); // Convert the whole JSON to a string
                            }
                        }

                        // Add the parameter to the dbCommand
                        dbCommand.Parameters.AddWithValue(parameterName, parameterValue);
                    }
                }

                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                int iOrdinal = 0;
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetTemplate, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    if (dataReader.Read())
                    {

                        response = dataReader.GetValue(0).ToString();
                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return response;
            }
            catch (Exception ex)
            {
                this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    this._logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }

        #region MyRegion
        public async Task<string> GetUserNameAsync(string mobile, string type)
        {
            string userName = string.Empty;
            SqlCommand dbCommand = null;    

            try
            {
                _logger.LogDebug(LoggerMessage.Begin);

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetUserNameByMobile, this.DataBase);

                #region Pass Arguments to Stored Procedure
                dbCommand.Parameters.AddWithValue("@Mobile", mobile);
                dbCommand.Parameters.AddWithValue("@type", type);

                #endregion

                var stopwatch = new System.Diagnostics.Stopwatch();
                stopwatch.Start();

                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await dbCommand.ExecuteReaderAsync())
                {
                    stopwatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetUserNameByMobile, stopwatch.ElapsedMilliseconds);

                    _logger.LogWarning(LoggerMessage.StoredProcedureBegin);

                    if (dataReader.Read())
                    {
                        int iOrdinal = dataReader.GetOrdinal("FullName");
                        if (!dataReader.IsDBNull(iOrdinal))
                        {
                            userName = dataReader.GetString(iOrdinal);
                        }
                    }

                    _logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                _logger.LogDebug(LoggerMessage.End);
                return userName;
            }
            catch (Exception ex)
            {
                _logger.LogError(LoggerMessage.ErrorMessage, ex);
                throw;
            }
            finally
            {
                try
                {
                    DBManager.CloseConnection(dbCommand);
                }
                catch (Exception ex)
                {
                    _logger.LogError(LoggerMessage.ErrorMessage, ex);
                }
            }
        }

        #endregion

    }
}

using Capex.Infrastructure.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Capex.DomainModels.Common;
using Capex.DomainModels.DomainRequestModel;

using Capex.Infrastructure.Interfaces;
using System.Data.SqlClient;
using static Capex.Models.Common.JWTTokenResponseEntity;
using System.Threading.Tasks;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using static Capex.Models.Common.APIResult;
using Capex.Models.RequestModel;

namespace Capex.Infrastructure.Services
{
    public class Common : ICommon
    {
        public DBType DataBase => DBType.MasterDB;
        public ILogger<Common> _logger { get; }
        public Common(ILogger<Common> logger)
        {
            this._logger = logger;
        }
        public async Task<List<ModelValidateDetailResponse>> GetModelValidation(ModelValidateRequest request)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<ModelValidateDetailResponse> responseList = new List<ModelValidateDetailResponse>();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetModelValidationDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<ModelValidateRequest>(request);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetModelValidationDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    
                    while (dataReader.Read())
                    {
                        string propertiesdetails="";
                        
                        iOrdinal = dataReader.GetOrdinal("PropertiesDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            propertiesdetails = dataReader.GetString(iOrdinal).ToString();
                        
                        responseList= JsonConvert.DeserializeObject<List<ModelValidateDetailResponse>>(propertiesdetails);
                    }
                    

                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
                this._logger.LogDebug(LoggerMessage.End);
                return responseList;
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
        public async Task<string> InsertAPILogStatus(APILogStatusDomainRequestModel request)
        {
            SqlCommand dbCommand = null;
            string responseList = string.Empty;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.SaveAPILogStatus, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<APILogStatusRequestModel>(request);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                var task = await dbCommand.ExecuteScalarAsync();
                DBManager.TraceDbCommand(dbCommand);
                swatch.Stop();
                PerformanceLog.LogSPTime(DBConstants.MastersSP.SaveAPILogStatus, swatch.ElapsedMilliseconds);
                this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                responseList = task.ToString();
                this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                this._logger.LogDebug(LoggerMessage.End);

                return responseList;
                
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
        
        
    }
}

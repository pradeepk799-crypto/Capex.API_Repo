using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
namespace Capex.Infrastructure.Services
{
    public class BillGenerationInfra : IBillGenerationInfra
    {
        public DBType DataBase => DBType.MasterDB;
        public ILogger<BillGenerationInfra> _logger { get; }
        public BillGenerationInfra(ILogger<BillGenerationInfra> logger)
        {
            this._logger = logger;
        }
        public async Task<APIResult.ApiResult<BillGenerationDomainResponse>> SaveBillGeneration1(BillGenerationBuildingDetailsByVendorDomainRequest requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }
            this._logger.LogDebug("Starting SaveBillGeneration process.");

            var responseModel = new ApiResult<BillGenerationDomainResponse>
            {
                ResponseData = new BillGenerationDomainResponse()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveBillGeneration, this.DataBase);

            dbCommand.Parameters.AddWithValue("@json", jsonData);
            dbCommand.Parameters.AddWithValue("@CreatedBy", requestModel.UID);


            // Define the output parameter for success
            SqlParameter successParameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            dbCommand.Parameters.Add(successParameter);
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            try
            {
                await dbCommand.ExecuteNonQueryAsync(); // Ensure async execution
                DBManager.TraceDbCommand(dbCommand);

                // Retrieve the output parameter value
                bool success = Convert.ToBoolean(successParameter.Value);
                responseModel.ResponseData.Result = success;


                this._logger.LogDebug("SaveBillGeneration process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveBillGeneration process.");
                responseModel.ResponseData.Result = false;
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }
        public async Task<ApiResult<SaveDataDomainResponseModel>> SaveBillGeneration(BillGenerationBuildingDetailsByVendorDomainRequest requestModel)
        {
            SqlCommand dbCommand = null;
            int iOrdinal = 0;
            try
            {

                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                string jsonData = null;

                if (requestModel != null)
                {
                    jsonData = JsonConvert.SerializeObject(requestModel);
                }
                this._logger.LogDebug("Starting SaveBillGeneration process.");

                var responseModel = new ApiResult<SaveDataDomainResponseModel>
                {
                    ResponseData = new SaveDataDomainResponseModel()
                };

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveBillGeneration, this.DataBase);

                dbCommand.Parameters.AddWithValue("@json", jsonData);
                dbCommand.Parameters.AddWithValue("@CreatedBy", requestModel.UID);




                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetVendorData, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {


                        SaveDataDomainResponseModel SaveDataDomainResponseModel = new SaveDataDomainResponseModel();
                        iOrdinal = dataReader.GetOrdinal("UserName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.UserName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Email");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.Email = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("MobileNumber");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.MobileNumber = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("PasswordHash");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.PasswordHash = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsSuccess");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.IsSuccess = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("TemplateTypeId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.TemplateTypeId = dataReader.GetInt32(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("UserId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.UserId = dataReader.GetInt32(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DistrictName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.DistrictName = dataReader.GetString(iOrdinal);


                        iOrdinal = dataReader.GetOrdinal("VendorName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.VendorName = dataReader.GetString(iOrdinal);


                        iOrdinal = dataReader.GetOrdinal("DDOName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.DDOName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IVRSNO");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.IVRSNO = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("CurrentDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            SaveDataDomainResponseModel.CurrentDate = dataReader.GetString(iOrdinal);


                        responseModel.ResponseData = SaveDataDomainResponseModel;

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return responseModel;
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

        public async Task<GetBillGenerationDomainResponse> GetBillGenerationData(GetBillDetailsDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                GetBillGenerationDomainResponse billData = new GetBillGenerationDomainResponse();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetBillGenerationDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<GetBillDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddWithValue("@UID", requestModel.UID);
                //dbCommand.Parameters.AddWithValue("@MeterNo", requestModel.MeterNo);
                //dbCommand.Parameters.AddWithValue("@BuildingName", requestModel.BuildingName);
                //dbCommand.Parameters.AddWithValue("@StartReadingDate", requestModel.StartReadingDate);

                //dbCommand.Parameters.AddWithValue("@EndReadingDate", requestModel.EndReadingDate);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetBillGenerationDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        GetBillGenerationDomainResponse response = new GetBillGenerationDomainResponse();
                        iOrdinal = dataReader.GetOrdinal("BillDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            billData.BillDetails = dataReader.GetString(iOrdinal);
                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return billData;
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
        public async Task<GetBillGenerationDomainResponse> BuildingDetailsByDDO(BuildingDetailsByDDODomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                GetBillGenerationDomainResponse billData = new GetBillGenerationDomainResponse();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.BillGeneration_GetBuildingDetailsByVendor, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<GetBillDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddWithValue("@UID", requestModel.UID);
                dbCommand.Parameters.AddWithValue("@BuildingId", requestModel.BuildingId);
                dbCommand.Parameters.AddWithValue("@Month", requestModel.Month);
                dbCommand.Parameters.AddWithValue("@Year", requestModel.Year);

                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.BillGeneration_GetBuildingDetailsByVendor, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        GetBillGenerationDomainResponse response = new GetBillGenerationDomainResponse();
                        iOrdinal = dataReader.GetOrdinal("BuildingDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            billData.BuildingDetails = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsCombinedDateInvalid");
                        if (!dataReader.IsDBNull(iOrdinal))
                            billData.IsCombinedDateInvalid = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsBillAlreadyGenerated");
                        if (!dataReader.IsDBNull(iOrdinal))
                            billData.IsBillAlreadyGenerated = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsPreviousBillAlreadyGenerated");
                        if (!dataReader.IsDBNull(iOrdinal))
                            billData.IsPreviousBillAlreadyGenerated = dataReader.GetBoolean(iOrdinal);


                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }
                this._logger.LogDebug(LoggerMessage.End);
                return billData;
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

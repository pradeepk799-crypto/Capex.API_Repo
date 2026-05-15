using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using static Capex.Infrastructure.Common.PropertyConstants;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;


namespace Capex.Infrastructure.Services
{
    public class Masters : IMasters
    {
        /// <summary>
        /// Gets the data base.
        /// </summary>
        /// <value>
        /// The data base.
        /// </value>
        public DBType DataBase => DBType.MasterDB;
        public ILogger<Masters> _logger { get; }
        public Masters(ILogger<Masters> logger)
        {
            this._logger = logger;
        }
        public async Task<DemographyResponse> GetDemography(DemographyRequest request)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);

                int iOrdinal = 0;
                DemographyResponse response = new DemographyResponse();
                List<Demography> list = new List<Demography>();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetDemography, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DemographyRequest>(request);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetDemography, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        Demography demography = new Demography();
                        iOrdinal = dataReader.GetOrdinal("DemographyId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.DemographyId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("DemographyTypeId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.DemographyTypeId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("DemographyNameEng");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.Demography_Name_Eng = dataReader.GetString(iOrdinal).ToString();
                        iOrdinal = dataReader.GetOrdinal("DemographyNameHi");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.Demography_Name_Hi = dataReader.GetString(iOrdinal).ToString();
                        iOrdinal = dataReader.GetOrdinal("LGDCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.LGDCode = DbHelper.CheckDbNullLong(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("DemographyType");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.DemographyType = dataReader.GetString(iOrdinal).ToString();

                        iOrdinal = dataReader.GetOrdinal("PatwariHalkaNumber");
                        if (!dataReader.IsDBNull(iOrdinal))
                            demography.PatwariHalkaNumber = dataReader.GetString(iOrdinal).ToString();

                        list.Add(demography);
                    }
                    response.DemographyList = list;

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

        public async Task<string> GetStates()
        {

            string item = "";
            SqlCommand dbCommand = null;
            try
            {
                dbCommand = DBManager.GetStoredProcCommand("Test", this.DataBase);

                #region Pass Arguments to Stored Procedure

                #endregion

                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();

                    while (dataReader.Read())
                    {


                        int iOrdinal = dataReader.GetOrdinal("Test");
                        if (!dataReader.IsDBNull(iOrdinal))
                            item = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal)).Trim();



                    }


                    return item;
                }
            }
            catch (Exception ex)
            {
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
                }
            }
        }

        public async Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveOrUpdateDDO(DDODomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }


            this._logger.LogDebug("Starting SaveOrUpdateMstDDO process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel() // Ensure ResponseData is initialized
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.Proc_SaveOrUpdateMstDDO, this.DataBase);

            dbCommand.Parameters.AddWithValue("@JsonData", jsonData);

            // Define the output parameter for success
            SqlParameter successParameter = new SqlParameter("@UserId", SqlDbType.Int)
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
                int success = Convert.ToInt32(successParameter.Value);
                responseModel.ResponseData.UserId = success;

                this._logger.LogDebug("SaveOrUpdateMstDDO process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveOrUpdateMstDDO process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }

        public async Task<APIResult.ApiResult<SaveDataDomainResponseModel>> GetDDODetailForSendSMS(string userId)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                

                var responseModel = new ApiResult<SaveDataDomainResponseModel>
                {
                    ResponseData = new SaveDataDomainResponseModel()
                };


                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetDDODetailForSendSMS, this.DataBase);

                dbCommand.Parameters.AddWithValue("@UserId", userId);


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
        public async Task<List<DDODetailsDomainResponseModel>> GetDOODetails(DDODetailsDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DDODetailsDomainResponseModel> dDODetailsDomainResponseModel = new List<DDODetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetOrUpdateDDODetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DDODetailsRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetOrUpdateDDODetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DDODetailsDomainResponseModel response = new DDODetailsDomainResponseModel();

                        iOrdinal = dataReader.GetOrdinal("DDOID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DDOCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOCode = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDONameEn = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("DDOName_Hi");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOName_Hi = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("NodalPersonName_En");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.NodalPersonName_En = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("ContactDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.ContactDetails = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("EmailID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.EmailID = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("District");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DistrictId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("Address");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Address = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("CreatedBy");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedBy = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("IsActive");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsActive = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("TrsId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.TrsId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DeptId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DeptId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("TrsName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.TrsName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());



                        iOrdinal = dataReader.GetOrdinal("DeptName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DeptName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("CreatedDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedDate = DbHelper.CheckDbNullDate(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("IsPasswordChanged");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsPasswordChanged = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));




                        dDODetailsDomainResponseModel.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return dDODetailsDomainResponseModel;
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

        public async Task<List<DDODetailsDomainResponseModel>> GetDOOByDistricts(DistrictsDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("DistrictId", typeof(string));
                foreach (var id in requestModel.DistrictIds)
                {
                    dt.Rows.Add(id);
                }


                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DDODetailsDomainResponseModel> dDODetailsDomainResponseModel = new List<DDODetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetDDOByDistrict, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DDODetailsRequestModel>(requestModel);
                dbCommand.Parameters.AddWithValue("@DistrictIds", dt);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetDDOByDistrict, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DDODetailsDomainResponseModel response = new DDODetailsDomainResponseModel();

                        iOrdinal = dataReader.GetOrdinal("DDOID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DDOCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOCode = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDONameEn = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("DDOName_Hi");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOName_Hi = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("NodalPersonName_En");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.NodalPersonName_En = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("ContactDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.ContactDetails = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("EmailID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.EmailID = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("District");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("DistrictId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());


                        iOrdinal = dataReader.GetOrdinal("Address");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Address = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("CreatedBy");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedBy = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("IsActive");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsActive = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        dDODetailsDomainResponseModel.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return dDODetailsDomainResponseModel;
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


        public async Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveBankDetails(BankDetailsDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }


            this._logger.LogDebug("Starting SaveBankDetails process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveBankDetails, this.DataBase);

            dbCommand.Parameters.AddWithValue("@JsonData", jsonData);

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
                responseModel.ResponseData.response = success;

                this._logger.LogDebug("SaveBankDetails process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveBankDetails process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }


        public async Task<List<BankDetailsDomainResponseModel>> GetBankDetails(BankSearchDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<BankDetailsDomainResponseModel> bankList = new List<BankDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetBankDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<BankSearchDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetBankDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        BankDetailsDomainResponseModel response = new BankDetailsDomainResponseModel();


                        iOrdinal = dataReader.GetOrdinal("BankId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BankId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("IFSCCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IFSCCode = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BankName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BankName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BranchName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BranchName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Centre");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Centre = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Address");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Address = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("District");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.District = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("State");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.State = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("City");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.City = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("MICR");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MICR = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BankCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BankCode = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("SWIFT");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SWIFT = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Contact");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Contact = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("RTGS");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.RTGS = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IMPS");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IMPS = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("UPI");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.UPI = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("NEFT");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.NEFT = dataReader.GetBoolean(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("CreatedBy");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedBy = dataReader.GetValue(iOrdinal).ToString();


                        iOrdinal = dataReader.GetOrdinal("CreatedDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedDate = dataReader.GetValue(iOrdinal).ToString();

                        iOrdinal = dataReader.GetOrdinal("IsActive");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsActive = Convert.ToInt32(dataReader.GetValue(iOrdinal));

                        bankList.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return bankList;
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

        public async Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveOrUpdateBuildingDetails(BuildingRegistrationDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }


            this._logger.LogDebug("Starting SaveOrUpdateBuildingDetails process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveOrUpdateBuildingRegistration, this.DataBase);

            dbCommand.Parameters.AddWithValue("@JsonData", jsonData);

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
                responseModel.ResponseData.response = success;

                this._logger.LogDebug("SaveOrUpdateBuildingDetails process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveOrUpdateBuildingDetails process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }
        public async Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveOrUpdateBuildingDetails1(BuildingDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }


            this._logger.LogDebug("Starting SaveOrUpdateBuildingDetails process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveBuildingDetails, this.DataBase);

            dbCommand.Parameters.AddWithValue("@BuildingJson", requestModel.Building);
            dbCommand.Parameters.AddWithValue("@BuildingMappingJson", requestModel.BuildingMapping);
            dbCommand.Parameters.AddWithValue("@GenerationMeterJson", requestModel.GenerationMeter);
            dbCommand.Parameters.AddWithValue("@OtherBuildingDetailsJson", requestModel.OtherBuildingDetails);
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
                responseModel.ResponseData.response = success;

                this._logger.LogDebug("SaveOrUpdateBuildingDetails process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveOrUpdateBuildingDetails process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }

        public async Task<ApiResult<SaveDataDomainResponseModel>> SaveOrUpdateBuildingDetails(BuildingDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;

                this._logger.LogDebug("Starting SaveOrUpdateBuildingDetails process.");

                var responseModel = new ApiResult<SaveDataDomainResponseModel>
                {
                    ResponseData = new SaveDataDomainResponseModel()
                };

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveBuildingDetails, this.DataBase);

                dbCommand.Parameters.AddWithValue("@BuildingJson", requestModel.Building);
                dbCommand.Parameters.AddWithValue("@BuildingMappingJson", requestModel.BuildingMapping);
                dbCommand.Parameters.AddWithValue("@GenerationMeterJson", requestModel.GenerationMeter);
                dbCommand.Parameters.AddWithValue("@OtherBuildingDetailsJson", requestModel.OtherBuildingDetails);
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

        public async Task<GetBuildingResponseModel> GetBuildingById(BuildingDetailsSearchDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                GetBuildingResponseModel vendorData = new GetBuildingResponseModel();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetBuildingById, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<BuildingDetailsSearchDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetBuildingById, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {


                        iOrdinal = dataReader.GetOrdinal("Building");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.Building = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BuildingMapping");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.BuildingMapping = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("GenerationMeter");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.GenerationMeter = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("OtherBuildingDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.OtherBuildingDetails = dataReader.GetString(iOrdinal);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return vendorData;
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


        public async Task<List<BuildingRegistrationDomainResponseModel>> GetBuildingDetails(BuildingDetailsSearchDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<BuildingRegistrationDomainResponseModel> buildingList = new List<BuildingRegistrationDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetBuildingDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<BuildingDetailsSearchDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.GetBuildingDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        BuildingRegistrationDomainResponseModel response = new BuildingRegistrationDomainResponseModel();

                        iOrdinal = dataReader.GetOrdinal("BuildingId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingId = dataReader.GetInt32(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BuildingIdNumber");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingIdNumber = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("MeterSerialNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MeterSerialNo = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("SiteAddress");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SiteAddress = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("BeneficiaryName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BeneficiaryName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("SanctionedLoad");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SanctionedLoad = dataReader.GetInt64(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("HESName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.HESName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Phase");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Phase = dataReader.GetInt32(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("MeterMaker");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MeterMaker = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("TariffCategory");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.TariffCategory = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("FeederName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.FeederName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DTRName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DTRName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("PhoneNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.PhoneNo = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("EmailID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.EmailID = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Region");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Region = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Circle");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Circle = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("Division");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Division = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("District");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.District = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DDOId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOId = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("CircleId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CircleId = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DivisionId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DivisionId = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DistrictId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictId = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("CreatedBy");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedBy = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("CreatedDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedDate = dataReader.GetDateTime(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsActive");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsActive = dataReader.GetInt32(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("PhaseName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.PhaseName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDONameEn = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("DeptName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DeptName = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("ProposedCapacity_KW");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.ProposedCapacity_KW = dataReader.GetDecimal(iOrdinal);

                        buildingList.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return buildingList;
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


        //public async Task<ApiResult<MasterDomainResponseModel>> SaveOrUpdateVendorData(VendorDataDomainRequestModel requestModel)
        //{
        //    SqlCommand dbCommand = null;


        //    this._logger.LogDebug("Starting SaveOrUpdateVendorData process.");

        //    var responseModel = new ApiResult<MasterDomainResponseModel>
        //    {
        //        ResponseData = new MasterDomainResponseModel()
        //    };

        //    dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveOrUpdateVendorData, this.DataBase);

        //    dbCommand.Parameters.AddWithValue("@VendorJson", requestModel.Vendors);
        //    dbCommand.Parameters.AddWithValue("@VendorNodalPersonsJson", requestModel.VendorNodalPersons);
        //    dbCommand.Parameters.AddWithValue("@VendorAccountsJson", requestModel.VendorAccounts);

        //    dbCommand.Parameters.AddWithValue("@CreatedBy", requestModel.UID);



        //    SqlParameter successParameter = new SqlParameter("@IsSuccess", SqlDbType.Bit)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    dbCommand.Parameters.Add(successParameter);

        //    var stopwatch = new System.Diagnostics.Stopwatch();
        //    stopwatch.Start();

        //    try
        //    {
        //        await dbCommand.ExecuteNonQueryAsync(); // Ensure async execution
        //        DBManager.TraceDbCommand(dbCommand);

        //        // Retrieve the output parameter value
        //        bool success = Convert.ToBoolean(successParameter.Value);
        //        responseModel.ResponseData.response = success;

        //        this._logger.LogDebug("SaveOrUpdateVendorData process completed successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        this._logger.LogError(ex, "Error occurred in SaveOrUpdateVendorData process.");
        //        responseModel.ResponseData.response = false; // Handle error gracefully
        //    }
        //    finally
        //    {
        //        stopwatch.Stop();
        //    }
        //    return responseModel;
        //}




        public async Task<ApiResult<MasterDomainResponseModel>> SaveOrUpdateVendorData1(VendorDataDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            this._logger.LogDebug("Starting SaveOrUpdateVendorData process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveOrUpdateVendorData, this.DataBase);

            dbCommand.Parameters.AddWithValue("@VendorJson", requestModel.Vendors);
            dbCommand.Parameters.AddWithValue("@VendorNodalPersonsJson", requestModel.VendorNodalPersons);
            dbCommand.Parameters.AddWithValue("@VendorAccountsJson", requestModel.VendorAccounts);

            dbCommand.Parameters.AddWithValue("@CreatedBy", requestModel.UID);



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
                responseModel.ResponseData.response = success;

                this._logger.LogDebug("SaveOrUpdateVendorData process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveOrUpdateVendorData process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }
        public async Task<ApiResult<SaveDataDomainResponseModel>> SaveOrUpdateVendorData(VendorDataDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;

                var responseModel = new ApiResult<SaveDataDomainResponseModel>
                {
                    ResponseData = new SaveDataDomainResponseModel()
                };

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.SaveOrUpdateVendorData, this.DataBase);

                dbCommand.Parameters.AddWithValue("@VendorJson", requestModel.Vendors);
                dbCommand.Parameters.AddWithValue("@VendorNodalPersonsJson", requestModel.VendorNodalPersons);
                dbCommand.Parameters.AddWithValue("@VendorAccountsJson", requestModel.VendorAccounts);

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
        public async Task<VendorDataDomainResponseModel> GetVendorData(VendorSearchDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                VendorDataDomainResponseModel vendorData = new VendorDataDomainResponseModel();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.GetVendorData, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<VendorSearchDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
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

                        BuildingRegistrationDomainResponseModel response = new BuildingRegistrationDomainResponseModel();

                        iOrdinal = dataReader.GetOrdinal("Vendors");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.Vendors = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("VendorNodalPersons");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.VendorNodalPersons = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("VendorAccounts");
                        if (!dataReader.IsDBNull(iOrdinal))
                            vendorData.VendorAccounts = dataReader.GetString(iOrdinal);



                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return vendorData;
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


        public async Task<APIResult.ApiResult<MasterDomainResponseModel>> SaveUnitPriceDetails1(UnitPriceDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            string jsonData = null;

            if (requestModel != null)
            {
                jsonData = JsonConvert.SerializeObject(requestModel);
            }


            this._logger.LogDebug("Starting SaveUnitPriceDetails process.");

            var responseModel = new ApiResult<MasterDomainResponseModel>
            {
                ResponseData = new MasterDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.usp_SaveUnitPrice, this.DataBase);

            dbCommand.Parameters.AddWithValue("@JsonData", jsonData);
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
                responseModel.ResponseData.response = success;

                this._logger.LogDebug("SaveUnitPriceDetails process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in SaveUnitPriceDetails process.");
                responseModel.ResponseData.response = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }



        public async Task<ApiResult<SaveDataDomainResponseModel>> SaveUnitPriceDetails(UnitPriceDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;

                string jsonData = null;

                if (requestModel != null)
                {
                    jsonData = JsonConvert.SerializeObject(requestModel);
                }


                this._logger.LogDebug("Starting SaveUnitPriceDetails process.");

                var responseModel = new ApiResult<SaveDataDomainResponseModel>
                {
                    ResponseData = new SaveDataDomainResponseModel()
                };

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.usp_SaveUnitPrice, this.DataBase);

                dbCommand.Parameters.AddWithValue("@JsonData", jsonData);
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

        public async Task<List<UnitPriceDomainResponseModel>> GetUnitPriceDetails(UnitPriceDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<UnitPriceDomainResponseModel> bankList = new List<UnitPriceDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.usp_GetUnitPriceDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<UnitPriceDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.usp_GetUnitPriceDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        UnitPriceDomainResponseModel response = new UnitPriceDomainResponseModel();


                        iOrdinal = dataReader.GetOrdinal("UnitId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.UnitId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("PriceId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.PriceId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("Price");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Price = DbHelper.CheckDbNullDecimal(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("Unit");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Unit = dataReader.GetString(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("VendorId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.VendorId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("DistrictId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("DistrictName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictName = dataReader.GetString(iOrdinal);


                        iOrdinal = dataReader.GetOrdinal("Name");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.VendorName = dataReader.GetString(iOrdinal);


                        iOrdinal = dataReader.GetOrdinal("CreatedDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.CreatedDate = dataReader.GetDateTime(iOrdinal);


                        iOrdinal = dataReader.GetOrdinal("UpdatedDate");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.UpdatedDate = dataReader.GetDateTime(iOrdinal);

                        iOrdinal = dataReader.GetOrdinal("IsActive");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.IsActive = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        bankList.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return bankList;
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

        public async Task<ApiResult<ValidateIVRSAndMeterExistDomainResponseModel>> ValidateIVRSAndMeterExist(ValidateIVRSAndMeterExistDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


            this._logger.LogDebug("Starting ValidateIVRSAndMeterExist process.");

            var responseModel = new ApiResult<ValidateIVRSAndMeterExistDomainResponseModel>
            {
                ResponseData = new ValidateIVRSAndMeterExistDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.usp_ValidateIVRSAndMeterExist, this.DataBase);

            dbCommand.Parameters.AddWithValue("@Flag", requestModel.Flag);
            dbCommand.Parameters.AddWithValue("@ConsumerNo", requestModel.ConsumerNo);
            dbCommand.Parameters.AddWithValue("@MeterSerialNo", requestModel.MeterSerialNo);

            SqlParameter isConsumerExistsParam = new SqlParameter("@IsConsumerExists", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            SqlParameter isMeterSerialExistsParam = new SqlParameter("@IsMeterSerialExists", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };

            dbCommand.Parameters.Add(isConsumerExistsParam);
            dbCommand.Parameters.Add(isMeterSerialExistsParam);

            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            try
            {
                await dbCommand.ExecuteNonQueryAsync(); // Ensure async execution
                DBManager.TraceDbCommand(dbCommand);

                // Retrieve the output parameter value
                bool isConsumerExists = Convert.ToBoolean(isConsumerExistsParam.Value);
                bool isMeterSerialExists = Convert.ToBoolean(isMeterSerialExistsParam.Value);

                responseModel.ResponseData.IsConsumerExists = isConsumerExists;
                responseModel.ResponseData.IsMeterSerialExists = isMeterSerialExists;

                this._logger.LogDebug("ValidateIVRSAndMeterExist process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in ValidateIVRSAndMeterExist process.");

            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }
    }
}


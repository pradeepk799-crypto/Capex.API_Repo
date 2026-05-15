using Microsoft.Extensions.Logging;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainResponseModel;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Serilog;
using System.Data.SqlClient;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;
using Capex.DomainModels.DomainResponseModel.Masters;
using Newtonsoft.Json;
using static Capex.Models.Common.APIResult;
using System.Data;
using Capex.Models.RequestModel;

namespace Capex.Infrastructure.Services
{
    public class User : IUser
    {
        public DBType DataBase => DBType.MasterDB;
        public ILogger<Masters> _logger { get; }
        public User(ILogger<Masters> logger)
        {
            this._logger = logger;
        }

        public string GetName()
        {
            Log.Information("Infrastructure");
            return "";
        }

        public async Task<UserLoginResponse> GetLoginUser(TokenRequest requestmodel)
        {
            SqlCommand dbCommand = null;
            try
            {
                // this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                UserLoginResponse response = new UserLoginResponse();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetUser, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<TokenRequest>(requestmodel);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetUser, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    List<UserRoleModel> userRoleList = new List<UserRoleModel>();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {


                            iOrdinal = dataReader.GetOrdinal("UserId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.UserId = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                            iOrdinal = dataReader.GetOrdinal("UserName");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.UserName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("Password");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.Password = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                            iOrdinal = dataReader.GetOrdinal("LastLoginDate");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.LastLoginDate = dataReader.GetDateTime(iOrdinal);
                            iOrdinal = dataReader.GetOrdinal("ProfileId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.ProfileId = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("Title");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.Title = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("FirstName");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.FirstName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("LastName");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.LastName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("MobileNo");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.MobileNo = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("EmailId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.EmailId = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                            iOrdinal = dataReader.GetOrdinal("IsResetPwd");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.IsResetPwd = DbHelper.CheckDbNullBool(Convert.ToBoolean(dataReader.GetValue(iOrdinal)));
                            iOrdinal = dataReader.GetOrdinal("DepartmentId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.DepartmentId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("OfficeLevelId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.OfficeLevelId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("DivisionId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.DivisionId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("DistrictId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.DistrictId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("SubDivisionId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.SubDivisionId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("TehsilId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.TehsilId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("DesignationId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.DesignationId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("RoleId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.RoleId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("IsEKyc");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.IsEKyc = DbHelper.CheckDbNullBool(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("OfficeId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.OfficeId = DbHelper.CheckDbNullLong(dataReader.GetValue(iOrdinal));
                            iOrdinal = dataReader.GetOrdinal("UserType");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.UserType = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        }




                    }

                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
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

        public async Task<UserApplicationInfoDomainRequestModel> GetLoginDetails(TokenRequest requestmodel)
        {
            SqlCommand dbCommand = null;
            try
            {
                // this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                UserApplicationInfoDomainRequestModel response = new UserApplicationInfoDomainRequestModel();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.proc_Login, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<TokenRequest>(requestmodel);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MutationSSP.proc_Login, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    List<UserRoleModel> userRoleList = new List<UserRoleModel>();
                    if (dataReader.HasRows)
                    {

                        while (dataReader.Read())
                        {


                            iOrdinal = dataReader.GetOrdinal("LoginId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.LoginId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());



                            iOrdinal = dataReader.GetOrdinal("ProfileId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.ProfileId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());


                            iOrdinal = dataReader.GetOrdinal("EmailId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.EmailId = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


                            iOrdinal = dataReader.GetOrdinal("RoleId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.RoleId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("LoginTypeId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.LoginTypeId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("UserTypeId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.UserTypeId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                            iOrdinal = dataReader.GetOrdinal("ApplicationId");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.ApplicationId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                            iOrdinal = dataReader.GetOrdinal("ApplicationNumber");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.ApplicationNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));


                            iOrdinal = dataReader.GetOrdinal("GSTNumber");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.GSTNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));


                            iOrdinal = dataReader.GetOrdinal("PANNumber");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.PANNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("COEName");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.COEName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("NodalOfficerName");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.NodalOfficerName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("NodalOfficerDesignation");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.NodalOfficerDesignation = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                            iOrdinal = dataReader.GetOrdinal("PasswordHash");
                            if (!dataReader.IsDBNull(iOrdinal))
                                response.PasswordHash = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        }

                        dataReader.NextResult();

                        while (dataReader.Read())
                        {
                            UserRoleModel userRole = new UserRoleModel
                            {
                                Id = dataReader.IsDBNull(dataReader.GetOrdinal("Id")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("Id")),
                                RoleId = dataReader.IsDBNull(dataReader.GetOrdinal("RoleId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("RoleId")),
                                MenuId = dataReader.IsDBNull(dataReader.GetOrdinal("MenuId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MenuId")),
                                OrderIndex = dataReader.IsDBNull(dataReader.GetOrdinal("OrderIndex")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("OrderIndex")),
                                MenuNameHi = dataReader.IsDBNull(dataReader.GetOrdinal("MenuNameHi")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MenuNameHi")),
                                MenuNameEng = dataReader.IsDBNull(dataReader.GetOrdinal("MenuNameEng")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MenuNameEng")),
                                MenuPath = dataReader.IsDBNull(dataReader.GetOrdinal("MenuPath")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("MenuPath")),
                                MenuParentId = dataReader.IsDBNull(dataReader.GetOrdinal("MenuParentId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MenuParentId")),
                                MenuTypeId = dataReader.IsDBNull(dataReader.GetOrdinal("MenuTypeId")) ? 0 : dataReader.GetInt32(dataReader.GetOrdinal("MenuTypeId")),
                                Class = dataReader.IsDBNull(dataReader.GetOrdinal("Class")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("Class")),
                                Icon = dataReader.IsDBNull(dataReader.GetOrdinal("Icon")) ? string.Empty : dataReader.GetString(dataReader.GetOrdinal("Icon")),
                                IsHiddenAction = dataReader.IsDBNull(dataReader.GetOrdinal("IsHiddenAction")) ? false : dataReader.GetBoolean(dataReader.GetOrdinal("IsHiddenAction")),

                            };

                            userRoleList.Add(userRole);
                            response.UserRoleList = userRoleList;

                        }


                    }

                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
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

        public async Task<ValidUserResponse> GetValidUser(ValidUserRequest requestmodel)
        {
            SqlCommand dbCommand = null;
            try
            {
                // this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                ValidUserResponse response = new ValidUserResponse();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.RCMSSP.CheckValidUser, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<ValidUserRequest>(requestmodel);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.RCMSSP.CheckValidUser, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        iOrdinal = dataReader.GetOrdinal("Msg");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Msg = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Status");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Status = DbHelper.CheckDbNullBool(dataReader.GetValue(iOrdinal));
                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
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

        public async Task<ValidUserResponse> ChangeUserPassword(ChangePasswordRequest requestmodel)
        {
            SqlCommand dbCommand = null;
            try
            {
                // this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                ValidUserResponse response = new ValidUserResponse();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.RCMSSP.prochangePwd, this.DataBase);

                #region Pass Arguments to Stored Procedure

                SqlParameter[] parameters = DbHelper.AddSQLParameters<ChangePasswordRequest>(requestmodel);



                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.RCMSSP.prochangePwd, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        iOrdinal = dataReader.GetOrdinal("Msg");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Msg = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Status");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Status = DbHelper.CheckDbNullBool(dataReader.GetValue(iOrdinal));
                    }

                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
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
        public async Task<CitizenForgotPwdResponse> ForgotUserPassword(CitizenForgotPwdRequest requestmodel)
        {
            SqlCommand dbCommand = null;
            try
            {
                // this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                CitizenForgotPwdResponse response = new CitizenForgotPwdResponse();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MastersSP.GetUser, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<CitizenForgotPwdRequest>(requestmodel);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.MastersSP.GetUser, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        iOrdinal = dataReader.GetOrdinal("MobileNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MobileNo = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                    }

                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                //this._logger.LogInformation(response);
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
        public async Task<ApiResult<UserForgotPasswordDomainResponseModel>> ForgotPassword(ForgotPasswordDomainRequestModel requestModel)
        {
            SqlCommand dbCommand = null;


           
            this._logger.LogDebug("Starting ForgotPassword process.");

            var responseModel = new ApiResult<UserForgotPasswordDomainResponseModel>
            {
                ResponseData = new UserForgotPasswordDomainResponseModel()
            };

            dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.MutationSSP.ForgotPassword, this.DataBase);

            dbCommand.Parameters.AddWithValue("@Password", requestModel.Password);
            dbCommand.Parameters.AddWithValue("@Type", requestModel.Type);
            dbCommand.Parameters.AddWithValue("@MobileNo", requestModel.MobileNumber);
          


            // Define the output parameter for success
            SqlParameter successParameter = new SqlParameter("@IsUpdated", SqlDbType.Bit)
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
                responseModel.ResponseData.Status = success;

                this._logger.LogDebug("ForgotPassword process completed successfully.");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Error occurred in ForgotPassword process.");
                responseModel.ResponseData.Status = false; // Handle error gracefully
            }
            finally
            {
                stopwatch.Stop();
            }
            return responseModel;
        }
    }
}

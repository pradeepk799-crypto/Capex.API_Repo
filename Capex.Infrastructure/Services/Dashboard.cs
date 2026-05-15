using Capex.DomainModels.DomainRequestModel.Dashboard;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel.Dashboard;
using Capex.DomainModels.DomainResponseModel.Masters;
using Capex.Infrastructure.Common;
using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel.Masters;
using Capex.Models.ResponseModel.Masters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Data;
using System.Data.SqlClient;
using static Capex.Infrastructure.Common.PropertyConstants;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;


namespace Capex.Infrastructure.Services
{
    public class Dashboard : IDashboard
    {
        public DBType DataBase => DBType.MasterDB;
        public ILogger<Masters> _logger { get; }
        public Dashboard(ILogger<Masters> logger)
        {
            this._logger = logger;
        }

        public async Task<List<DashboardDomainResponseModel>> GetDashboardCountList(DashboardDomainRequestModel requestModel)
        {
            string storePocName = "";
            if (requestModel.RoleID == 1)
            {

                storePocName = DBConstants.OfficeProfileSSP.ProcGetDashboardCountList;
            }
            else if (requestModel.RoleID == 2) 
            {
                storePocName = DBConstants.OfficeProfileSSP.Procusp_GetDashboardVendorReports;
            }
            else if (requestModel.RoleID == 3)
            {
                storePocName = DBConstants.OfficeProfileSSP.Procusp_GetDashboardDDOReportsCount;
            }
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardDomainResponseModel> List = new List<DashboardDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(storePocName, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();

                
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(storePocName, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardDomainResponseModel response = new DashboardDomainResponseModel();


                        iOrdinal = dataReader.GetOrdinal("RoleID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DRoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("TotalBuildingRegistered");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DTotalBuildingRegistered = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("TotalVendorRegistered");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DTotalVendorRegistered = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("TotalNumberOfDDO");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DTotalNumberOfDDO = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("TotalPendingPayment");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DTotalPendingPayment = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("totalDistrict");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.totalDistrict = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("totalDDOs");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.totalDDOs = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("totalBuilding");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.totalBuilding = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("totalMeter");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.totalMeter = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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

        public async Task<List<DashboardVenderDistrictDetailsDomainResponseModel>> GetDashboardVenderDistrictList(DashboardVenderDistrictDetailsDomainRequestModel requestModel)
        {
            //string storePocName = "";
            //if (requestModel.RoleID == 1)
            //{

            //    storePocName = DBConstants.OfficeProfileSSP.ProcGetDashboardCountList;
            //}
            //else if (requestModel.RoleID == 2)
            //{
            //    storePocName = DBConstants.OfficeProfileSSP.Procusp_GetDashboardVendorReports;
            //}
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardVenderDistrictDetailsDomainResponseModel> List = new List<DashboardVenderDistrictDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardVenderDistrictDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();

                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();


                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardVenderDistrictDetailsDomainResponseModel response = new DashboardVenderDistrictDetailsDomainResponseModel();


                        //iOrdinal = dataReader.GetOrdinal("RoleID");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.RoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("DistrictName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("DistrictCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DistrictCode = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal)).ToString();
                        //iOrdinal = dataReader.GetOrdinal("Price");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.Price = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal)).ToString();
                        iOrdinal = dataReader.GetOrdinal("Price");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.Price = ((decimal)dataReader.GetValue(iOrdinal)).ToString();

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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
        public async Task<List<DashboardVenderDdoDetailsDomainResponseModel>> GetDashboardVenderDdoList(DashboardVenderDistrictDetailsDomainRequestModel requestModel)
        {
            
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardVenderDdoDetailsDomainResponseModel> List = new List<DashboardVenderDdoDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardVenderDistrictDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();

                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();


                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardVenderDdoDetailsDomainResponseModel response = new DashboardVenderDdoDetailsDomainResponseModel();


                        //iOrdinal = dataReader.GetOrdinal("RoleID");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.RoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("DDOCode");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOCode = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal)).ToString();
                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDONameEn = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("EmailID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.EmailID = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("NodalPersonName_En");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.NodalPersonName_En = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("ContactDetails");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.ContactDetails = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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
        public async Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardVenderBuildingList(DashboardVenderDistrictDetailsDomainRequestModel requestModel)
        {

            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardVenderBuildingDetailsDomainResponseModel> List = new List<DashboardVenderBuildingDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardVenderDistrictDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();

                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();


                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardVenderBuildingDetailsDomainResponseModel response = new DashboardVenderBuildingDetailsDomainResponseModel();


                        //iOrdinal = dataReader.GetOrdinal("RoleID");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.RoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("BuildingId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("BuildingIdNumber");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingIdNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("MeterSerialNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MeterSerialNo = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("SiteAddress");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SiteAddress = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("BeneficiaryName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BeneficiaryName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("SanctionedLoad");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SanctionedLoad = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("District");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.District = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("DDOName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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


        public async Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardDDOBuildingList(DashboardVenderDistrictDetailsDomainRequestModel requestModel)
        {

            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardVenderBuildingDetailsDomainResponseModel> List = new List<DashboardVenderBuildingDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.OfficeProfileSSP.ProcGetDashboardDDODetailsReports, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardVenderDistrictDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();

                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();


                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardVenderBuildingDetailsDomainResponseModel response = new DashboardVenderBuildingDetailsDomainResponseModel();


                        //iOrdinal = dataReader.GetOrdinal("RoleID");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.RoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("BuildingId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                        //iOrdinal = dataReader.GetOrdinal("BuildingIdNumber");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.BuildingIdNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("MeterSerialNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MeterSerialNo = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        //iOrdinal = dataReader.GetOrdinal("SiteAddress");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.SiteAddress = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("BuildingName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BeneficiaryName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("SanctionedLoad");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SanctionedLoad = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("BuildingDistrict");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.District = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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
        public async Task<List<DashboardVenderBuildingDetailsDomainResponseModel>> GetDashboardDDOMeterList(DashboardVenderDistrictDetailsDomainRequestModel requestModel)
        {

            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                List<DashboardVenderBuildingDetailsDomainResponseModel> List = new List<DashboardVenderBuildingDetailsDomainResponseModel>();

                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.OfficeProfileSSP.ProcGetDashboardDDODetailsReports, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DashboardVenderDistrictDetailsDomainRequestModel>(requestModel);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();

                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();


                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.OfficeProfileSSP.ProcGetDashboardVendorDetailsReports, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        DashboardVenderBuildingDetailsDomainResponseModel response = new DashboardVenderBuildingDetailsDomainResponseModel();


                        //iOrdinal = dataReader.GetOrdinal("RoleID");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.RoleID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));

                        iOrdinal = dataReader.GetOrdinal("BuildingId");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BuildingId = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));
                        //iOrdinal = dataReader.GetOrdinal("BuildingIdNumber");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.BuildingIdNumber = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("MeterSerialNo");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.MeterSerialNo = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        //iOrdinal = dataReader.GetOrdinal("SiteAddress");
                        //if (!dataReader.IsDBNull(iOrdinal))
                        //    response.SiteAddress = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("BuildingName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.BeneficiaryName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));
                        iOrdinal = dataReader.GetOrdinal("SanctionedLoad");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.SanctionedLoad = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("BuildingDistrict");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.District = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));


                        iOrdinal = dataReader.GetOrdinal("DDONameEn");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.DDOName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal));

                        List.Add(response);

                    }
                    this._logger.LogWarning(LoggerMessage.StoredProcedureEnd);
                }

                this._logger.LogDebug(LoggerMessage.End);
                return List;
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


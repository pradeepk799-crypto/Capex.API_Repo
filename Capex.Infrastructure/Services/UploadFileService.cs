using Capex.Infrastructure.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Capex.DomainModels.DomainRequestModel;
using Capex.DomainModels.DomainRequestModel.Masters;
using Capex.DomainModels.DomainResponseModel;
using Capex.DomainModels.DomainResponseModel.Masters;

using Capex.Infrastructure.Interfaces;
using Capex.Models.Common;
using Capex.Models.RequestModel;
using Capex.Models.ResponseModel;
using Capex.Utilities.Common;
using Capex.Utilities.Resource;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Capex.Models.Common.APIResult;
using LoggerMessage = Capex.Utilities.Common.LoggerMessage;

namespace Capex.Infrastructure.Services
{
    public class UploadFileService : IUploadFileService
    {
        public DBType DataBase => DBType.MasterDB;
        public ILogger<UploadFileService> _logger { get; }
        private readonly AppSettings appSettings;
        public UploadFileService(ILogger<UploadFileService> logger, IOptions<AppSettings> appSettings)
        {
            this._logger = logger;
            this.appSettings = appSettings.Value;
        }

        public async Task<DResponseUploadID> FileUploadDMS(DomainFileUploadRequestModel tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            DResponseUploadID responseModel = new DResponseUploadID();
            DResponseUploadID data = new DResponseUploadID();
            //DResponseUploadID response;
            try
            {
                // Stream str = tokenRequest.files.InputStream;
                FileDetails fsDetail = new FileDetails();
                Stream str = tokenRequest.files.OpenReadStream();
                string sKey = this.appSettings.FUsKey;
                var encyptedPdfContent = Encrypt(str, sKey);
                fsDetail.FileName = tokenRequest.files.FileName.ToString();
                fsDetail.FilePath = tokenRequest.Filepath;
                fsDetail.FileMsArray = encyptedPdfContent.ToArray();
                var client = new HttpClient
                {
                    BaseAddress = new Uri(this.appSettings.DMSUrl)
                };
                // Set the Accept header for JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                  new MediaTypeWithQualityHeaderValue("application/json"));

                // POST using the JSON formatter.
                MediaTypeFormatter jonFormatter = new JsonMediaTypeFormatter();
                var result = await client.PostAsync("api/fileupload", fsDetail, jonFormatter);
                if (result.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await result.Content.ReadAsStringAsync();

                    var responseData = JsonConvert.DeserializeObject<FileUploadResponseModel>(responseContent);

                    if (responseData.status)
                    {
                        DFileUploadRequestModel requestmodel = new DFileUploadRequestModel();
                        requestmodel.FileUpload_Id = responseData.FileUpload_Id.ToString();
                        requestmodel.FilePath = responseData.FilePath;
                        requestmodel.FolderName = responseData.FolderName;


                        if (responseData.File_Name == null || responseData.File_Name == "")
                        {
                            requestmodel.File_Name = fsDetail.FileName;
                        }
                        else
                        {
                            requestmodel.File_Name = responseData.File_Name;
                        }
                        if (responseData.FileContentType == null || responseData.FileContentType == "")
                        {
                            requestmodel.FileContentType = "." + System.IO.Path.GetExtension(tokenRequest.files.FileName).Substring(1);
                        }
                        else
                        {
                            requestmodel.FileContentType = responseData.FileContentType;

                        }
                        responseModel = await this.SaveUploadFile(requestmodel);


                    }
                    else
                    {

                    }

                }
                else
                {


                }



            }
            catch (Exception ex)
            {

            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<DomainResponseDownloadFile> FileDownloadDMS(DomainFilePath tokenRequest)
        {
            Log.Debug(LoggerMessage.Begin);
            DomainResponseDownloadFile responseModel = new DomainResponseDownloadFile();
            DomainResponseDownloadFile resmodel = new DomainResponseDownloadFile();

            ResponseUploadID data = new ResponseUploadID();
            DResponseUploadID response;
            try
            {
                DFileUploadRequestModel domainFileDownload = await this.GetFileDetails(tokenRequest.FileId);
                // Stream str = tokenRequest.files.InputStream;
                FileDownload filerequest = new FileDownload();

                filerequest.FileName = domainFileDownload.File_Name;
                filerequest.FolderName = domainFileDownload.FolderName;
                filerequest.FileType = domainFileDownload.FileContentType;

                var client = new HttpClient
                {
                    BaseAddress = new Uri(this.appSettings.DMSUrl)
                };
                // Set the Accept header for JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                  new MediaTypeWithQualityHeaderValue("application/json"));

                // POST using the JSON formatter.
                MediaTypeFormatter jonFormatter = new JsonMediaTypeFormatter();
                var result = await client.PostAsync("api/filedownload", filerequest, jonFormatter);
                if (result.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await result.Content.ReadAsStringAsync();

                    var responseData = JsonConvert.DeserializeObject<ResponseDownloadFile>(responseContent);
                    if (responseData.status)
                    {
                        responseModel.status = responseData.status;
                        responseModel.msg = responseData.msg;
                        responseModel.FilebyteArray = responseData.FilebyteArray;
                        responseModel.Base64String = responseData.FilebyteArray.ToString();
                        responseModel.FileName= domainFileDownload.File_Name;
                        responseModel.FileType= domainFileDownload.FileContentType;
                    }
                    else
                    {

                        responseModel.status = false;
                    }

                }
                else
                {

                    responseModel.status = false;

                }



            }
            catch (Exception ex)
            {

                Log.Error(Convert.ToString(ex));
            }

            Log.Debug(LoggerMessage.End);
            return responseModel;
        }

        public async Task<DResponseUploadID> SaveUploadFile(DFileUploadRequestModel request)
        {

            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(requestmodel);
                int iOrdinal = 0;
                DResponseUploadID response = new DResponseUploadID();
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.RCMSSP.ProcInsertUploadFile, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DFileUploadRequestModel>(request);
                dbCommand.Parameters.AddRange(parameters);
                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.RCMSSP.ProcInsertUploadFile, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {
                        iOrdinal = dataReader.GetOrdinal("UploadID");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.UploadID = DbHelper.CheckDbNullInt(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("Status");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.status = DbHelper.CheckDbNullBool(dataReader.GetValue(iOrdinal));
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


        public static MemoryStream Encrypt(Stream fsInput, string sKey)
        {

            var fsEncrypted = new MemoryStream();
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            var desencrypt = des.CreateEncryptor();
            var cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
            var bytearrayinput = new byte[fsInput.Length];
            fsInput.Position = 0;
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.FlushFinalBlock();
            fsInput.Close();
            fsEncrypted.Flush();
            fsEncrypted.Position = 0;
            return fsEncrypted;
        }
        public static MemoryStream Decrypt(Stream fsread, string sKey)
        {
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(sKey),
                IV = Encoding.ASCII.GetBytes(sKey)
            };
            var desdecrypt = des.CreateDecryptor();
            var cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            MemoryStream decryptedStream = new MemoryStream();
            int bytesRead;
            byte[] buffer = new byte[4096];
            while ((bytesRead = cryptostreamDecr.Read(buffer, 0, buffer.Length)) > 0)
            {
                decryptedStream.Write(buffer, 0, bytesRead);
            }
            decryptedStream.Seek(0, SeekOrigin.Begin);
            return decryptedStream;
        }


        public async Task<DFileUploadRequestModel> GetFileDetails(string fileId)
        {
            SqlCommand dbCommand = null;
            try
            {
                this._logger.LogDebug(LoggerMessage.Begin);
                //this._logger.LogInformation(request);
                int iOrdinal = 0;
                DFileUploadRequestModel response = new DFileUploadRequestModel();
                DomainFilePath domainFilePath = new DomainFilePath();
                domainFilePath.FileId = fileId;


             
                dbCommand = await DBManager.GetStoredProcCommandAsync(DBConstants.RCMSSP.GetFileDetails, this.DataBase);

                #region Pass Arguments to Stored Procedure
                SqlParameter[] parameters = DbHelper.AddSQLParameters<DomainFilePath>(domainFilePath);
                dbCommand.Parameters.AddRange(parameters);

                #endregion
                var swatch = new System.Diagnostics.Stopwatch();
                swatch.Start();
                Task<SqlDataReader> task = dbCommand.ExecuteReaderAsync();
                DBManager.TraceDbCommand(dbCommand);
                using (SqlDataReader dataReader = await task)
                {
                    swatch.Stop();
                    PerformanceLog.LogSPTime(DBConstants.RCMSSP.GetFileDetails, swatch.ElapsedMilliseconds);
                    this._logger.LogWarning(LoggerMessage.StoredProcedureBegin);
                    while (dataReader.Read())
                    {

                        iOrdinal = dataReader.GetOrdinal("FileName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.File_Name = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());
                        iOrdinal = dataReader.GetOrdinal("FolderName");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.FolderName = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());

                        iOrdinal = dataReader.GetOrdinal("FileType");
                        if (!dataReader.IsDBNull(iOrdinal))
                            response.FileContentType = DbHelper.CheckDbNullString(dataReader.GetValue(iOrdinal).ToString());


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


    }
}

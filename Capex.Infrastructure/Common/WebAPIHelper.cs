// <copyright file="WebAPIHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Capex.Models.Common;
using Capex.Utilities.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Capex.Infrastructure.Common
{
    /// <summary>
    /// This class is used to handle request response.
    /// </summary>
    public static class WebAPIHelper<T> where T : class
    {

        
        static WebAPIHelper()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ServiceConfiguration.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();          
            ServicesAPIList = root.GetSection("Services").AsEnumerable();
            ServicesWebGISList = root.GetSection("ServiceWebGIS").AsEnumerable();           

        }
        public static async Task<T> ReadJsonFile()
        {
            T result = null;
            try
            {
                string Path = Directory.GetCurrentDirectory() + "\\ServiceConfiguration.json";
                StreamReader file = File.OpenText(Path);                
                dynamic GetServiceConfData= file.ReadToEnd();
                result = JsonConvert.DeserializeObject<MainRoot>(GetServiceConfData);               
            }
            catch (Exception Exc)
            {

                throw;
            }

            return result;

        }
        /// <summary>
        /// Gets the connection string MasterDB.
        /// </summary>
        /// <value>
        /// The connection string MasterDB.
        /// </value>
        public static string ServicesAPI { get; private set; } = string.Empty;
        public static IEnumerable<KeyValuePair<string, string>> ServicesAPIList;
        public static IEnumerable<KeyValuePair<string, string>> ServicesWebGISList;

        //public static dynamic ServicesList;
        /// <summary>
        /// For getting the resources from a web api
        /// </summary>
        /// <param name="url">API Url</param>
        /// <returns>A Task with result object of type T</returns>
        public static async Task<T> Get(string url)
        {
            T result = null;
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(new Uri(url)).Result;

                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public static async Task<string> PostRequest(string serviceName,string methodName, T postObject)
        {
            string result = null;
            string service = JObject.Parse(ServicesAPI).Root.Where(x => x["ServiceName"].Value<string>() == serviceName).Where(y => y["MethodName"].Value<string>() == methodName).FirstOrDefault()["MethodURL"].Value<string>();
            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(service, postObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;
                    result = x.Result;
                });
            }

            return result;
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public static async Task PutRequest(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var response = await client.PutAsync(apiUrl, putObject, new JsonMediaTypeFormatter()).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }
    }

    public class MainRoot
    {
        public List<Service> Services { get; set; }

     

    }

    public class Service
    {
        public string ServiceName { get; set; }
        public List<Method> Methods { get; set; }

     
    }
    public class Method
    {
        public string MethodName { get; set; }
        public string MethodURL { get; set; }
        public string FamilyUrl { get; set; }
        public string encryptionKey { get; set; }
        public string TokenKey { get; set; }
        public string serviceCode { get; set; }
        public string deptCode { get; set; }
        public string applicationCode { get; set; }
    }
    
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Capex.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Common.DBLogger
{
    public class DbLoggerProvider : ILoggerProvider
    {
        public readonly IConfiguration _configuration;
        //private readonly IDBLogger _iDBLogger;
        public DbLoggerProvider(IConfiguration configuration)
        {
            _configuration = configuration; // Stores all the options.
            //_iDBLogger = dBLogger;
        }


        /// <summary>
        /// Creates a new instance of the db logger.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        public ILogger CreateLogger(string CategoryName)
        {
            return new DbLogger(_configuration);
        }
        public void Dispose()
        {
        }
    }
}

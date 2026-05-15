using Capex.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDBLogger = Capex.Business.Interfaces.IDBLogger;

namespace Capex.Business.Services
{
    public class DBLogger : IDBLogger
    {
        private readonly IInfrastructureServices _infrastructureServices;
        public DBLogger(IInfrastructureServices infrastructureServices)
        {
            _infrastructureServices = infrastructureServices;
        }
        public void AddErrorLog(string errorLog)
        {
            this._infrastructureServices.DBLogger.AddErrorLog(errorLog);
        }
    }
}

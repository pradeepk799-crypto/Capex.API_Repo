using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Capex.Infrastructure.Common
{
    public static class ConnectionStrings
    {
        static ConnectionStrings()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "ConnectionStrings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            ConnectionStringMasterDB = root.GetSection("ConnectionStrings").GetSection("MasterDBConnectionString").Value;
            ConnectionStringSAARADB = root.GetSection("ConnectionStrings").GetSection("SAARADBConnectionString").Value;

        }
        /// <summary>
        /// Gets the connection string MasterDB.
        /// </summary>
        /// <value>
        /// The connection string MasterDB.
        /// </value>
        public static string ConnectionStringMasterDB { get; private set; } = string.Empty;
        /// <summary>
        /// Gets the connection string SAARADB.
        /// </summary>
        /// <value>
        /// The connection string SAARADB.
        /// </value>
        public static string ConnectionStringSAARADB { get; private set; } = string.Empty;
       
    }
}

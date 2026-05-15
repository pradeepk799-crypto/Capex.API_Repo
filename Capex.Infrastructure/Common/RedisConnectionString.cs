using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Common
{
    public class RedisConnectionString
    {
        public static IConfiguration AppSetting
        {
            get;
        }
        static RedisConnectionString()
        {
            AppSetting = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("ConnectionStrings.json").Build();
            try
            {
                RedisConnectionString.lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                {
                    return ConnectionMultiplexer.Connect(AppSetting["RedisURL"] + ",password=" + AppSetting["RedisPassword"]);
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection;
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}

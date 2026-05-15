using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Infrastructure.Common
{
    public static class RedisHelper
    {
        private static IDatabase _db;
        static RedisHelper()
        {
            ConfigureRedis();
        }
        public static void ConfigureRedis()
        {
            _db = RedisConnectionString.Connection.GetDatabase();
        }

        /// <summary>
        /// Get Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        /// <summary>
        /// Set Data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expirationTime"></param>
        /// <returns></returns>
        public static Task<bool> SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return Task.FromResult(isSet);
        }

        /// <summary>
        /// Remove data
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }

        public static object GetData(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                string data = Convert.ToString(JsonConvert.DeserializeObject(value));
                data = data.Replace("\n  ", "").Replace("\n", "").Replace(@"{\", "{").Replace(@"\", "");
                return data;
            }
            return default;

        }

        public static Task<bool> SetData(string key, object value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(Convert.ToString(value)), expiryTime);
            return Task.FromResult(isSet);

        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Business.Common.DBLogger
{
    public static class DbLoggerExtensions
    {
        public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, IConfiguration configure)
        {
            //builder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            builder.Services.TryAdd(ServiceDescriptor.Singleton<ILoggerProvider, DbLoggerProvider>());
            return builder;
        }
    }
}

using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capex.Utilities
{
    public static class LogHelper
    {
        
        public static Logger GetInstance()
        {
            return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    
                    .WriteTo.File(@"d:\log-.txt")
                    .CreateLogger();

        }

        public static void LogText(string text)
        {
            Log.Information(text);
        }

        public static void LogException(Exception ex)
        {
            Log.Fatal(ex.Message + ";" + ex.InnerException + "Stacktrace:" + ex.StackTrace, Encoding.Default);
        }

        public static void End()
        {
            Log.CloseAndFlush();
        }

    }
}

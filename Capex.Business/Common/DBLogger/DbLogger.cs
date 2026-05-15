using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Capex.Models.RequestModel.DBLogger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capex.Business.Interfaces;

namespace Capex.Business.Common.DBLogger
{
    public class DbLogger : Attribute, ILogger
    {
        /// <summary>
        /// Instance of <see cref="_configuration" />.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Creates a new instance of <see cref="FileLogger" />.
        /// </summary>
        /// <param name="fileLoggerProvider">Instance of <see cref="FileLoggerProvider" />.</param>
        //private readonly IDBLogger _iDBLogger;
        public DbLogger(IConfiguration configuration)
        {
            _configuration = configuration;
            //_iDBLogger= dBLogger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Whether to log the entry.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>
        /// Used to log the entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
        /// <param name="state">The event's state.</param>
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
        /// <param name="formatter">A delegate that formats </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            //if (_configuration["IsProd:Key"] == "1")
            //{
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.
                return;
            }

            var threadId = Thread.CurrentThread.ManagedThreadId;
            // Get the current thread ID to use in the log file. 
            // Store record.
            // Add to database.
            // LogLevel
            // ThreadId
            // EventId
            // Exception Message (use formatter)
            // Exception Stack Trace
            // Exception Source
            var values = new JObject();
            DBLoggerModel dBLogger = new DBLoggerModel();

            if (dBLogger?.LogFields?.Any() ?? false)
            {
                foreach (var logField in dBLogger.LogFields)
                {
                    switch (logField)
                    {
                        case "LogLevel":
                            if (!string.IsNullOrWhiteSpace(logLevel.ToString()))
                            {
                                values["LogLevel"] = logLevel.ToString();
                            }
                            break;
                        case "ThreadId":
                            values["ThreadId"] = threadId;
                            break;
                        case "EventId":
                            values["EventId"] = eventId.Id;
                            break;
                        case "EventName":
                            if (!string.IsNullOrWhiteSpace(eventId.Name))
                            {
                                values["EventName"] = eventId.Name;
                            }
                            break;
                        case "Message":
                            if (!string.IsNullOrWhiteSpace(formatter(state, exception)))
                            {
                                values["Message"] = formatter(state, exception);
                            }
                            break;
                        case "ExceptionMessage":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.Message))
                            {
                                values["ExceptionMessage"] = exception?.Message;
                            }
                            break;
                        case "ExceptionStackTrace":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.StackTrace))
                            {
                                values["ExceptionStackTrace"] = exception?.StackTrace;
                            }
                            break;
                        case "ExceptionSource":
                            if (exception != null && !string.IsNullOrWhiteSpace(exception.Source))
                            {
                                values["ExceptionSource"] = exception?.Source;
                            }
                            break;
                    }
                }
            }
            if (exception != null)
            {
                var errorLog = JsonConvert.SerializeObject(values, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    Formatting = Formatting.None
                }).ToString();
                //_iDBLogger.AddErrorLog(errorLog);
            }
            //}
        }
    }
}

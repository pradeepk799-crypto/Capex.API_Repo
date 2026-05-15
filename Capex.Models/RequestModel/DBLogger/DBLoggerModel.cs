namespace Capex.Models.RequestModel.DBLogger
{
    public class DBLoggerModel
    {
        public string[] LogFields { get; set; } = { "LogLevel", "ThreadId", "EventId", "EventName", "ExceptionMessage", "ExceptionStackTrace", "ExceptionSource" };
    }
}

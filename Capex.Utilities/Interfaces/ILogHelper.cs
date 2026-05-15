namespace Capex.Utilities.Interfaces
{
    public interface ILogHelper
    {
        bool IsWarnEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsTraceEnabled { get; }

        bool IsInfoEnabled { get; }

        void Info(object request);

        void Debug(object request);

        void Error(string msg);

        void Error(string msg, Exception ex);

        void Fatal(string msg, Exception ex);

        void Fatal(string msg);

        void Warn(string msg);
    }
}

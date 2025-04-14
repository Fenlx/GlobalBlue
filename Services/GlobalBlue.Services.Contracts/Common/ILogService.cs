namespace GlobalBlue.Services.Contracts.Common
{
    public interface ILogService
    {
        bool IsWarnEnabled { get; }

        bool IsInfoEnabled { get; }

        bool IsFatalEnabled { get; }

        bool IsErrorEnabled { get; }

        bool IsDebugEnabled { get; }

        bool IsTraceEnabled { get; }

        void Debug(string message, params object[] args);

        void Debug(Exception ex);

        void Debug(Exception ex, string message, params object[] args);

        void Error(Exception ex, string message, params object[] args);

        void Error(Exception ex);

        void Error(string message, params object[] args);

        void Fatal(string message, params object[] args);

        void Fatal(Exception ex, string message, params object[] args);

        void Fatal(Exception ex);

        void Info(Exception ex);

        void Info(string message, params object[] args);

        void Info(Exception ex, string message, params object[] args);

        void Trace(string message, params object[] args);

        void Trace(Exception ex, string message, params object[] args);

        void Trace(Exception ex);

        void Warn(string message, params object[] args);

        void Warn(Exception ex);

        void Warn(Exception ex, string message, params object[] args);
    }
}
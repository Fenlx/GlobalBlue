using GlobalBlue.Services.Contracts.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace GlobalBlue.Services.Common
{
    public class LogService : ILogService
    {
        public bool IsWarnEnabled { get; }

        public bool IsInfoEnabled { get; }

        public bool IsFatalEnabled { get; }

        public bool IsErrorEnabled { get; }

        public bool IsDebugEnabled { get; }

        public bool IsTraceEnabled { get; }

        private readonly IConfiguration _configuration;

        private ILogger<LogService> _logger;

        public LogService(IConfiguration configuration, ILogger<LogService> logger)
        {
            _configuration = configuration;
            _logger = logger;
            IsDebugEnabled = bool.TryParse(_configuration["IsDebugEnabled"], out bool isDebugEnabled) && isDebugEnabled;
            IsErrorEnabled = bool.TryParse(_configuration["IsErrorEnabled"], out bool isErrorEnabled) && isErrorEnabled;
            IsFatalEnabled = bool.TryParse(_configuration["IsFatalEnabled"], out bool isFatalEnabled) && isFatalEnabled;
            IsInfoEnabled = bool.TryParse(_configuration["IsInfoEnabled"], out bool isInfoEnabled) && isInfoEnabled;
            IsTraceEnabled = bool.TryParse(_configuration["IsTraceEnabled"], out bool isTraceEnabled) && isTraceEnabled;
            IsWarnEnabled = bool.TryParse(_configuration["IsWarnEnabled"], out bool isWarnEnabled) && isWarnEnabled;
        }

        public void Trace(string message, params object[] args)
        {
            if (IsTraceEnabled)
            {
                _logger.Log(LogLevel.Trace, message, args);
            }
        }

        public void Trace(Exception ex)
        {
            if (IsTraceEnabled)
            {
                _logger.Log(LogLevel.Trace, ex, null, null);
            }
        }

        public void Trace(Exception ex, string message, params object[] args)
        {
            if (IsTraceEnabled)
            {
                _logger.Log(LogLevel.Trace, ex, message, args);
            }
        }

        public void Debug(string message, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _logger.Log(LogLevel.Debug, message, args);
            }
        }

        public void Debug(Exception ex)
        {
            if (IsDebugEnabled)
            {
                _logger.Log(LogLevel.Debug, ex, null, null);
            }
        }

        public void Debug(Exception ex, string message, params object[] args)
        {
            if (IsDebugEnabled)
            {
                _logger.Log(LogLevel.Debug, ex, message, args);
            }
        }

        public void Info(string message, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _logger.Log(LogLevel.Information, message, args);
            }
        }

        public void Info(Exception ex)
        {
            if (IsInfoEnabled)
            {
                _logger.Log(LogLevel.Information, ex, null, null);
            }
        }

        public void Info(Exception ex, string message, params object[] args)
        {
            if (IsInfoEnabled)
            {
                _logger.Log(LogLevel.Information, ex, message, args);
            }
        }

        public void Warn(string message, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _logger.Log(LogLevel.Warning, message, args);
            }
        }

        public void Warn(Exception ex)
        {
            if (IsWarnEnabled)
            {
                _logger.Log(LogLevel.Warning, ex, null, null);
            }
        }

        public void Warn(Exception ex, string message, params object[] args)
        {
            if (IsWarnEnabled)
            {
                _logger.Log(LogLevel.Warning, ex, message, args);
            }
        }

        public void Error(string message, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _logger.Log(LogLevel.Error, message, args);
            }
        }

        public void Error(Exception ex)
        {
            if (IsErrorEnabled)
            {
                _logger.Log(LogLevel.Error, ex, null, null);
            }
        }

        public void Error(Exception ex, string message, params object[] args)
        {
            if (IsErrorEnabled)
            {
                _logger.Log(LogLevel.Error, ex, message, args);
            }
        }

        public void Fatal(string message, params object[] args)
        {
            if (IsFatalEnabled)
            {
                _logger.Log(LogLevel.Critical, message, args);
            }
        }

        public void Fatal(Exception ex)
        {
            if (IsFatalEnabled)
            {
                _logger.Log(LogLevel.Critical, ex, null, null);
            }
        }

        public void Fatal(Exception ex, string message, params object[] args)
        {
            if (IsFatalEnabled)
            {
                _logger.Log(LogLevel.Critical, ex, message, args);
            }
        }
    }
}
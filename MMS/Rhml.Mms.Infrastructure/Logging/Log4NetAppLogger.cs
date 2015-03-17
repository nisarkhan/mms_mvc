using log4net;
using Republic.Core.Interfaces;

namespace Rhml.Mms.Infrastructure.Logging
{
    /// <summary> Application logging using Log4Net.  This logger can be 
    /// configured using Web.config (level of logging, logging destination(s), logging details, ...)
    /// </summary>
    public class Log4NetAppLogger : ILogger
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(Log4NetAppLogger));

 
        /// <summary> Log program information at the default Info level
        /// </summary>
        /// <param name="message">The message information to log</param>
        public void Log(string message)
        {
            _log.Info(message);
        }

        /// <summary> Log program information at the specified logging level
        /// </summary>
        /// <param name="logType">The logging level specified (LoggerTypeEnum)</param>
        /// <param name="message">The information to write to the configured log</param>
        public void Log(LoggerType logType, string message)
        {
            switch (logType)
            {
                case LoggerType.Debug:
                    _log.Debug(message);
                    break;

                case LoggerType.Info:
                    _log.Info(message);
                    break;

                case LoggerType.Warn:
                    _log.Warn(message);
                    break;
            }
        }
    }
}
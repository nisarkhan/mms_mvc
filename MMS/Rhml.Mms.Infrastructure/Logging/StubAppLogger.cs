using Republic.Core.Interfaces;

namespace Rhml.Mms.Infrastructure.Logging
{
    /// <summary> empty logger instance - does not log anything.
    /// Note: there is no documentation for the methods ... they do nothing,
    /// and the application uses the documented interface for information
    /// </summary>
    public class StubAppLogger : ILogger
    {
        /// <summary> Log program information at the default Info level
        /// </summary>
        /// <param name="message">The message information to log</param>
        public void Log(string message)
        {
            // Stub logger does not log anything
        }

        /// <summary> Log program information at the specified logging level
        /// </summary>
        /// <param name="logType">The logging level specified (LoggerTypeEnum)</param>
        /// <param name="message">The information to write to the configured log</param>
        public void Log(LoggerType logType, string message)
        {
            // Stub logger does not log anything
        }
    }
}

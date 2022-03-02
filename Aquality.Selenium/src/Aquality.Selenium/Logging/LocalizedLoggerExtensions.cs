using Aquality.Selenium.Core.Localization;
using System;

namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// Extensions for localized logger.
    /// </summary>
    public static class LocalizedLoggerExtensions
    {
        /// <summary>
        /// Logging of localized messages with a specific log level.
        /// </summary>
        /// <param name="logger">Current instance of logger.</param>
        /// <param name="logLevel">Logging level.</param>
        /// <param name="messageKey">Localized message key.</param>
        /// <param name="args">Arguments for the localized message.</param>
        /// <exception cref="NotSupportedException">Thrown when specified log level is not supported.</exception>
        public static void LogByLevel(this ILocalizedLogger logger, LogLevel logLevel, string messageKey, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    logger.Debug(messageKey, args: args);
                    break;
                case LogLevel.Info:
                    logger.Info(messageKey, args);
                    break;
                case LogLevel.Warn:
                    logger.Warn(messageKey, args);
                    break;
                case LogLevel.Error:
                    logger.Error(messageKey, args);
                    break;
                case LogLevel.Fatal:
                    logger.Fatal(messageKey, args: args);
                    break;
                default:
                    throw new NotSupportedException($"Localized logging at level [{logLevel}] is not supported.");
            }
        }
    }
}

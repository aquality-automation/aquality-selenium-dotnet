using System;
using System.IO;
using System.Threading;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// This class is using for a creating extended log. It implements a Singleton pattern.
    /// </summary>
    public sealed class Logger
    {
        private static readonly Lazy<Logger> LazyInstance = new Lazy<Logger>(() => new Logger());
        private static readonly ThreadLocal<ILogger> Log = new ThreadLocal<ILogger>(() => LogManager.GetLogger(Thread.CurrentThread.ManagedThreadId.ToString()));

        private Logger()
        {
            try
            {
                LogManager.LoadConfiguration("NLog.config");
            }
            catch (FileNotFoundException)
            {
                LogManager.Configuration = GetConfiguration();
            }
        }

        private LoggingConfiguration GetConfiguration()
        {
            var layout = "${date:format=yyyy-MM-dd HH:mm:ss} ${level:uppercase=true} - ${message}";
            var config = new LoggingConfiguration();
            config.AddRule(LogLevel.Info, LogLevel.Fatal, new ConsoleTarget("logconsole")
            {
                Layout = layout
            });
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, new FileTarget("logfile")
            {
                FileName = "Log/log.log",
                Layout = layout
            });
            return config;
        }

        /// <summary>
        /// Gets Logger instance.
        /// </summary>
        public static Logger Instance => LazyInstance.Value;

        /// <summary>
        /// Adds configuration (target).
        /// </summary>
        /// <param name="target">Target configuration to add.</param>
        /// <returns>Logger instance.</returns>
        public Logger AddTarget(Target target)
        {
            LogManager.Configuration.AddRuleForAllLevels(target, Log.Value.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>
        /// Removes configuration (target).
        /// </summary>
        /// <param name="target">Target configuratio to remove.</param>
        /// <returns>Logger instance.</returns>
        public Logger RemoveTarget(Target target)
        {
            LogManager.Configuration.RemoveTarget(target.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>
        /// Log debug message and optional exception.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Debug(string message, Exception exception = null)
        {
            Log.Value.Debug(exception, message);
        }

        /// <summary>
        /// Log info message.
        /// </summary>
        /// <param name="message">Message</param>
        public void Info(string message)
        {
            Log.Value.Info(message);
        }

        /// <summary>
        /// Log warning message.
        /// </summary>
        /// <param name="message">Message</param>
        public void Warn(string message)
        {
            Log.Value.Warn(message);
        }

        /// <summary>
        /// Log error message.
        /// </summary>
        /// <param name="message">Message</param>
        public void Error(string message)
        {
            Log.Value.Error(message);
        }

        /// <summary>
        /// Log fatal message and exception.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Fatal(string message, Exception exception)
        {
            Log.Value.Fatal(exception, message);
        }
    }
}

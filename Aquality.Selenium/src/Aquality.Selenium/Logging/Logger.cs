using System;
using System.Threading;
using NLog;
using NLog.Targets;

namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// This class is using for a creating extended log. It implements a Singleton pattern.
    /// </summary>
    public sealed class Logger
    {
        private const string DefLocale = "en";
        private const string LocaleKey = "logger.lang";
        private static readonly Lazy<Logger> LazyInstance = new Lazy<Logger>(() => new Logger());
        private static readonly ThreadLocal<ILogger> Log = new ThreadLocal<ILogger>(() => LogManager.GetLogger(Thread.CurrentThread.ManagedThreadId.ToString()));

        private Logger()
        {
            LogManager.LoadConfiguration("nlog.config");
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

        /// <summary>
        /// Log localized info message.
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources.</param>
        /// <param name="args">Arguments to be pasted into message via string.Format.</param>
        internal void InfoLoc(string messageKey, params object[] args)
        {
            Info(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Log localized debug message with optional exception.
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources.</param>
        /// <param name="exception">Occurred exception.</param>
        /// <param name="args">Arguments to be pasted into message via string.Format.</param>
        internal void DebugLoc(string messageKey, Exception exception = null, params object[] args)
        {
            Debug(string.Format(GetLocalizedMessage(messageKey), args), exception);
        }

        /// <summary>
        /// Log localized warning message.
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources.</param>
        /// <param name="args">Arguments to be pasted into message via string.Format.</param>
        internal void WarnLoc(string messageKey, params object[] args)
        {
            Warn(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Log localized error message.
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources.</param>
        /// <param name="args">Arguments to be pasted into message via string.Format</param>
        internal void ErrorLoc(string messageKey, params object[] args)
        {
            Error(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Log localized fatal message with exception.
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources.</param>
        /// <param name="exception">Occurred exception.</param>
        /// <param name="args">Arguments to be pasted into message via string.Format.</param>
        internal void FatalLoc(string messageKey, Exception exception = null, params object[] args)
        {
            Fatal(string.Format(GetLocalizedMessage(messageKey), args), exception);
        }

        /// <summary>
        /// Get localized message from resources by its key.
        /// </summary>
        /// <param name="messageKey">Key in resources file.</param>
        /// <returns>Template of message.</returns>
        private static string GetLocalizedMessage(string messageKey)
        {
            throw new NotImplementedException();
        }
    }
}

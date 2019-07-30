using System;
using System.Threading;
using NLog;
using NLog.Targets;

namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// This class is using for a creating extended log. It implements a Singleton pattern
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
        /// Gets Logger instance
        /// </summary>
        public static Logger Instance => LazyInstance.Value;

        /// <summary>
        /// Adds configuration (target)
        /// </summary>
        /// <param name="target"></param>
        /// <returns>Logger instance</returns>
        public Logger AddTarget(TargetWithLayoutHeaderAndFooter target)
        {
            LogManager.Configuration.AddRuleForAllLevels(target, Log.Value.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>
        /// Removes configuration (target)
        /// </summary>
        /// <param name="target"></param>
        /// <returns>Logger instance</returns>
        public Logger RemoveTarget(TargetWithLayoutHeaderAndFooter target)
        {
            LogManager.Configuration.RemoveTarget(target.Name);
            LogManager.ReconfigExistingLoggers();
            return Instance;
        }

        /// <summary>
        /// Debug log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Debug(string message, Exception exception = null)
        {
            Log.Value.Debug(exception, message);
        }

        /// <summary>
        /// Info log
        /// </summary>
        /// <param name="message">Message</param>
        public void Info(object message)
        {
            Log.Value.Info(message);
        }

        /// <summary>
        /// Warn log
        /// </summary>
        /// <param name="message">Message</param>
        public void Warn(object message)
        {
            Log.Value.Warn(message);
        }

        /// <summary>
        /// Error log
        /// </summary>
        /// <param name="message">Message</param>
        public void Error(object message)
        {
            Log.Value.Error(message);
        }

        /// <summary>
        /// Fatal log
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="exception">Exception</param>
        public void Fatal(string message, Exception exception)
        {
            Log.Value.Fatal(exception, message);
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Info log
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void InfoLoc(string messageKey, params object[] args)
        {
            Info(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Debug log
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources</param>
        /// <param name="exception">Exception</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void DebugLoc(string messageKey, Exception exception = null, params object[] args)
        {
            Debug(string.Format(GetLocalizedMessage(messageKey), args), exception);
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Warn log
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void WarnLoc(string messageKey, params object[] args)
        {
            Warn(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Error log
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void ErrorLoc(string messageKey, params object[] args)
        {
            Error(string.Format(GetLocalizedMessage(messageKey), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Fatal log
        /// </summary>
        /// <param name="messageKey">Message messageKey from localization resources</param>
        /// <param name="exception">Exception</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void FatalLoc(string messageKey, Exception exception = null, params object[] args)
        {
            Fatal(string.Format(GetLocalizedMessage(messageKey), args), exception);
        }

        /// <summary>
        /// Gets locale (localized template of message from resources)
        /// </summary>
        /// <param name="messageKey">Key in resources file</param>
        /// <returns>Template of message</returns>
        private static string GetLocalizedMessage(string messageKey)
        {
            throw new NotImplementedException();
        }
    }
}

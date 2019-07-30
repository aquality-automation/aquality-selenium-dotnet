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
        public void Debug(object message, Exception exception = null)
        {
            if (exception != null)
            {
                message = $"{message}: {exception.GetType()}: {exception.Message}";
            }

            Log.Value.Debug(message);
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
            Log.Value.Fatal($"{message}: {exception}");
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Info log
        /// </summary>
        /// <param name="key">Message key from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void InfoLoc(string key, params object[] args)
        {
            Info(string.Format(GetLoc(key), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Debug log
        /// </summary>
        /// <param name="key">Message key from localization resources</param>
        /// <param name="exception">Exception</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void DebugLoc(string key, Exception exception = null, params object[] args)
        {
            Debug(string.Format(GetLoc(key), args), exception);
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Warn log
        /// </summary>
        /// <param name="key">Message key from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void WarnLoc(string key, params object[] args)
        {
            Warn(string.Format(GetLoc(key), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Error log
        /// </summary>
        /// <param name="key">Message key from localization resources</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void ErrorLoc(string key, params object[] args)
        {
            Error(string.Format(GetLoc(key), args));
        }

        /// <summary>
        /// Gets locale (localized template of message from resources) and Fatal log
        /// </summary>
        /// <param name="key">Message key from localization resources</param>
        /// <param name="exception">Exception</param>
        /// <param name="args">Arguments to be pasted into message via String.format</param>
        internal void FatalLoc(string key, Exception exception = null, params object[] args)
        {
            Fatal(string.Format(GetLoc(key), args), exception);
        }


        /// <summary>
        /// Gets locale (localized template of message from resources)
        /// </summary>
        /// <param name="key">Key in resources file</param>
        /// <returns>Template of message</returns>
        private static string GetLoc(string key)
        {
            throw new NotImplementedException();
        }
    }
}

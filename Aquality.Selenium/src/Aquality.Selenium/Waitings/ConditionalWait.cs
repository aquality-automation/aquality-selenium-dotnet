using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Waitings
{
    /// <summary>
    /// This class is using for waiting any conditions.
    /// </summary>
    public static class ConditionalWait
    {
        private static readonly IConfiguration Configuration = Configurations.Configuration.Instance;

        /// <summary>
        /// Wait for some object from condition with timeout using Selenium WebDriver.
        /// </summary>
        /// <typeparam name="T">Type of object which is waiting for</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeout">Condition timeout. Default value is <see cref="ITimeoutConfiguration.Condition"/></param>
        /// <param name="pollingInterval">Condition check interval. Default value is <see cref="ITimeoutConfiguration.PollingInterval"/></param>
        /// <param name="message">Part of error message in case of Timeout exception</param>
        /// <param name="exceptionsToIgnore">Possible exceptions that have to be ignored. Handles <see cref="StaleElementReferenceException"/> by default.</param>
        /// <returns>Condition result which is waiting for.</returns>
        /// <exception cref="WebDriverTimeoutException">Throws when timeout exceeded and condition not satisfied.</exception>
        public static T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null, IList<Type> exceptionsToIgnore = null)
        {
            BrowserManager.Browser.SetImplicitWaitTimeout(TimeSpan.Zero);
            var waitTimeout = ResolveConditionTimeout(timeout);
            var checkInterval = ResolvePollingInterval(pollingInterval);
            var wait = new WebDriverWait(BrowserManager.Browser.Driver, waitTimeout)
            {
                Message = message,
                PollingInterval = checkInterval
            };
            var ignoreExceptions = exceptionsToIgnore ?? new List<Type> { typeof(StaleElementReferenceException) };
            wait.IgnoreExceptionTypes(ignoreExceptions.ToArray());
            var result = wait.Until(condition);
            BrowserManager.Browser.SetImplicitWaitTimeout(Configuration.TimeoutConfiguration.Implicit);
            return result;
        }

        /// <summary>
        /// Wait for some condition within timeout.
        /// </summary>
        /// <param name="condition">Predicate for waiting</param>
        /// <param name="timeout">Condition timeout. Default value is <see cref="ITimeoutConfiguration.Condition"/></param>
        /// <param name="pollingInterval">Condition check interval. Default value is <see cref="ITimeoutConfiguration.PollingInterval"/></param>
        /// <returns>True if condition satisfied and false otherwise.</returns>
        public static bool WaitFor(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null)
        {          
            try
            {
                WaitForTrue(condition, timeout, pollingInterval);
                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Wait for some condition within timeout.
        /// </summary>
        /// <param name="condition">Predicate for waiting</param>
        /// <param name="timeout">Condition timeout. Default value is <see cref="ITimeoutConfiguration.Condition"/></param>
        /// <param name="pollingInterval">Condition check interval. Default value is <see cref="ITimeoutConfiguration.PollingInterval"/></param>
        /// <param name="message">Part of error message in case of Timeout exception</param>
        /// <exception cref="TimeoutException">Throws when timeout exceeded and condition not satisfied.</exception>
        public static void WaitForTrue(Func<bool> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null)
        {
            if (condition == null)
            {
                throw new ArgumentNullException(nameof(condition), "condition cannot be null");
            }

            var waitTimeout = ResolveConditionTimeout(timeout);
            var checkInterval = ResolvePollingInterval(pollingInterval);
            var stopwatch = Stopwatch.StartNew();
            while (true)
            {
                if (condition())
                {
                    return;
                }

                if (stopwatch.Elapsed > waitTimeout)
                {
                    var exceptionMessage = $"Timed out after {waitTimeout.Seconds} seconds";
                    if (!string.IsNullOrEmpty(message))
                    {
                        exceptionMessage += $": {message}";
                    }

                    throw new TimeoutException(exceptionMessage);
                }

                Thread.Sleep(checkInterval);
            }
        }

        private static TimeSpan ResolveConditionTimeout(TimeSpan? timeout)
        {
            return timeout ?? Configuration.TimeoutConfiguration.Condition;
        }

        private static TimeSpan ResolvePollingInterval(TimeSpan? pollingInterval)
        {
            return pollingInterval ?? Configuration.TimeoutConfiguration.PollingInterval;
        }
    }
}
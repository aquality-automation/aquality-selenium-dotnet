using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace Aquality.Selenium.Waitings
{
    /// <summary>
    /// This class is using for waiting any conditions.
    /// </summary>
    public static class ConditionalWait
    {
        private static readonly IConfiguration Configuration = Configurations.Configuration.Instance;
        private static readonly Browser Browser = BrowserManager.Browser;

        /// <summary>
        /// Wait for some object from condition with timeout using Selenium WebDriver.
        /// </summary>
        /// <typeparam name="T">Type of object which is waiting for</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeout">Condition timeout. Default value is <see cref="ITimeoutConfiguration.Condition"/></param>
        /// <param name="exceptionsToIgnore">Possible exceptions that have to be ignored.
        /// Handles <see cref="StaleElementReferenceException"/> by default.</param>
        /// <returns>Condition result which is waiting for.</returns>
        /// <exception cref="WebDriverTimeoutException">Throws when timeout exceeded and condition not satisfied.</exception>
        public static T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeout = null, params Type[] exceptionsToIgnore)
        {
            Browser.ImplicitWaitTimeout = TimeSpan.Zero;
            var waitTimeout = ResolveConditionTimeout(timeout);
            var wait = new WebDriverWait(Browser.Driver, waitTimeout);
            var ignoreExceptions = exceptionsToIgnore.Concat(new Type[] { typeof(StaleElementReferenceException) }).ToArray();
            wait.IgnoreExceptionTypes(ignoreExceptions);
            var result = wait.Until(condition);
            Browser.ImplicitWaitTimeout = Configuration.TimeoutConfiguration.Implicit;
            return result;
        }

        /// <summary>
        /// Wait for some object from condition with timeout.
        /// </summary>
        /// <typeparam name="T">Type of object which is waiting for.</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeout">Condition timeout. Default value is <see cref="ITimeoutConfiguration.Condition"/></param>
        /// <param name="pollingInterval">Condition check interval. Default value is <see cref="ITimeoutConfiguration.PollingInterval"/></param>
        /// <param name="message">Message in case of Timeout exception</param>
        /// <returns>Condition result which is waiting for.</returns>
        /// <exception cref="TimeoutException">Throws when timeout exceeded and condition not satisfied.</exception>
        public static T WaitFor<T>(Func<T> condition, TimeSpan? timeout = null, TimeSpan? pollingInterval = null, string message = null)
        {
            var waitTimeout = ResolveConditionTimeout(timeout);
            var checkInterval = pollingInterval ?? Configuration.TimeoutConfiguration.PollingInterval;

            if (condition == null)
            {
                throw new ArgumentNullException("condition", "condition cannot be null");
            }

            var resultType = typeof(T);
            if ((resultType.IsValueType && resultType != typeof(bool)) || !typeof(object).IsAssignableFrom(resultType))
            {
                throw new ArgumentException($"Can only wait on an object or boolean response, tried to use type: {resultType}", "condition");
            }

            var stopwatch = Stopwatch.StartNew();
            while (true)
            {
                var result = condition();
                if (resultType == typeof(bool))
                {
                    var boolResult = result as bool?;
                    if (boolResult.HasValue && boolResult.Value)
                    {
                        return result;
                    }
                }
                else
                {
                    if (result != null)
                    {
                        return result;
                    }
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
    }
}
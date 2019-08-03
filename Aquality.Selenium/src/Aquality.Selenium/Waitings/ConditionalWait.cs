using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;

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
        /// Wait for condition and return true if waiting successful or false - otherwise.
        /// Default timeout(<see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>True if waiting successful or false - otherwise.</returns>
        public static bool WaitForTrue(Func<IWebDriver, bool> condition, TimeSpan? timeOut = null)
        {
            return WaitFor(condition, timeOut);
        }

        /// <summary>
        /// Wait for some object from condition with timeout.
        /// Default timeout(<see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <typeparam name="T">Type of object which is waiting</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>Object which waiting for or default of a T class - is exceptions occured</returns>
        public static T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeOut = null)
        {
            Browser.ImplicitWaitTimeout = TimeSpan.Zero;
            var exceptionsToIgnore = new Type[] { typeof(StaleElementReferenceException), typeof(NoSuchElementException) };
            var result = WaitFor(condition, Browser.Driver, timeOut, exceptionsToIgnore);
            Browser.ImplicitWaitTimeout = Configuration.TimeoutConfiguration.Implicit;
            return result;
        }

        /// <summary>
        /// For waiting without WebDriver: Wait for function will be true or return some except false.
        /// Default timeout(<see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <typeparam name="T">Type of waitWith param <see cref="DefaultWait{T}"/></typeparam>
        /// <typeparam name="TResult">Type of object which is waiting</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="waitWith">Object who will helping to wait (which will be passed to <see cref="DefaultWait{T}"/>)</param>
        /// <param name="timeOut">Time-out</param>
        /// <param name="exceptionsToIgnore">Possible exceptions that have to be ignored</param>
        /// <returns>Object which waiting for or default of a TResult class - is exceptions occured</returns>
        public static TResult WaitFor<T, TResult>(Func<T, TResult> condition, T waitWith, TimeSpan? timeOut = null, params Type[] exceptionsToIgnore)
        {
            var wait = new DefaultWait<T>(waitWith)
            {
                Timeout = ResolveConditionTimeOut(timeOut),
                PollingInterval = Configuration.TimeoutConfiguration.PollingInterval
            };
            wait.IgnoreExceptionTypes(exceptionsToIgnore);

            try
            {
                return wait.Until(condition);
            }
            catch (Exception e)
            {
                Logger.Instance.Debug("Aquality.Selenium.Waitings.ConditionalWait.WaitFor", e);
            }
            return default;
        }

        private static TimeSpan ResolveConditionTimeOut(TimeSpan? timeOut)
        {
            return timeOut ?? Configuration.TimeoutConfiguration.Condition;
        }
    }
}
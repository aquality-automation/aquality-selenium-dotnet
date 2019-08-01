using OpenQA.Selenium;
using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;
using OpenQA.Selenium.Support.UI;

namespace Aquality.Selenium.Waitings
{
    /// <summary>
    /// This class is using for waiting any conditions.
    /// </summary>
    public static class ConditionalWait
    {
        private static readonly IConfiguration Configuration = Configurations.Configuration.Instance;

        /// <summary>
        /// Wait for some object from condition with timeout.
        /// Default timeout(<see cref="ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <typeparam name="T">Type of object which is waiting</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>Object which waiting for or default of a T class - is exceptions occured</returns>
        public static T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan? timeOut = null)
        {
            BrowserManager.Browser.ImplicitWaitTimeout = TimeSpan.Zero;
            var wait = new DefaultWait<IWebDriver>(BrowserManager.Browser.Driver)
            {
                Timeout = ResolveConditionTimeOut(timeOut),
                PollingInterval = Configuration.TimeoutConfiguration.PollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

            try
            {
                return wait.Until(condition);
            }
            catch (Exception e)
            {
                Logger.Instance.Debug("Aquality.Selenium.Waitings.ConditionalWait.WaitFor", e);
            }
            finally
            {
                BrowserManager.Browser.ImplicitWaitTimeout = Configuration.TimeoutConfiguration.Implicit;
            }
            return default(T);
        }

        /// <summary>
        /// Wait for condition and return true if waiting successful or false - otherwise.
        /// Default timeout(<see cref="ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>True if waiting successful or false - otherwise.</returns>
        public static bool WaitForTrue(Func<IWebDriver, bool> condition, TimeSpan? timeOut = null)
        {
            try
            {
                return WaitFor(condition, ResolveConditionTimeOut(timeOut));
            }
            catch (Exception e)
            {
                Logger.Instance.Debug("Aquality.Selenium.Waitings.ConditionalWait.WaitForTrue", e);
                return false;
            }
        }

        /// <summary>
        /// For waiting without WebDriver: Wait for function will be true or return some except false.
        /// Default timeout(<see cref="ITimeoutConfiguration.Condition"/>) is using.
        /// </summary>
        /// <typeparam name="T">Type of waitWith param <see cref="DefaultWait{T}"/></typeparam>
        /// <typeparam name="TResult">Type of object which is waiting</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="waitWith">Object who will helping to wait (which will be passed to <see cref="DefaultWait{T}"/>)</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>Object which waiting for or default of a TResult class - is exceptions occured</returns>
        public static TResult WaitFor<T, TResult>(Func<T, TResult> condition, T waitWith, TimeSpan? timeOut = null)
        {
            var wait = new DefaultWait<T>(waitWith)
            {
                Timeout = ResolveConditionTimeOut(timeOut),
                PollingInterval = Configuration.TimeoutConfiguration.PollingInterval
            };

            try
            {
                return wait.Until(condition);
            }
            catch (Exception e)
            {
                Logger.Instance.Debug("Aquality.Selenium.Waitings.ConditionalWait.WaitFor", e);
            }
            return default(TResult);
        }

        private static TimeSpan ResolveConditionTimeOut(TimeSpan? timeOut)
        {
            return timeOut ?? Configuration.TimeoutConfiguration.Condition;
        }
    }
}
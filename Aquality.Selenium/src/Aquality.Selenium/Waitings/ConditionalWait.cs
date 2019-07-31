using OpenQA.Selenium;
using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;
using OpenQA.Selenium.Support.UI;

namespace Aquality.Selenium.Waitings
{
    public static class ConditionalWait
    {
        private static readonly Logger Logger = Logger.Instance;
        private static readonly Configuration Configuration = Configuration.Instance;

        /// <summary>
        /// Wait for some object from condition with timeout.
        /// Default timeout(property "Configuration.TimeoutConfiguration.Condition") is using.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="condition"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static T WaitFor<T>(Func<IWebDriver, T> condition, TimeSpan timeOut = default(TimeSpan))
        {
            BrowserManager.Browser.ImplicitWaitTimeout = TimeSpan.Zero;
            var wait = new DefaultWait<IWebDriver>(BrowserManager.Browser.Driver)
            {
                Timeout = GetConditionTimeOut(timeOut),
                PollingInterval = Configuration.TimeoutConfiguration.PollingInterval
            };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));

            try
            {
                return wait.Until(condition);
            }
            catch (Exception e)
            {
                Logger.DebugLoc("java.ConditionalWait.waitFor", e);                
            }
            finally
            {
                BrowserManager.Browser.ImplicitWaitTimeout = Configuration.TimeoutConfiguration.Implicit;
            }

            return default(T);
        }

        /// <summary>
        /// Wait for condition and return true if waiting successful or false - otherwise.
        /// Default timeout(property "Configuration.TimeoutConfiguration.Condition") is using.
        /// </summary>
        /// <param name="condition">Function for waiting</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns></returns>
        public static bool WaitForTrue(Func<IWebDriver, bool> condition, TimeSpan timeOut = default(TimeSpan))
        {
            try
            {
                return WaitFor(condition, GetConditionTimeOut(timeOut));
            }
            catch (Exception e)
            {
                Logger.DebugLoc("java.ConditionalWait.waitForTrue", e);
                return false;
            }
        }

        /// <summary>
        /// For waiting without WebDriver: Wait for function will be true or return some except false.
        /// Default timeout(property "Configuration.TimeoutConfiguration.Condition") is using.
        /// </summary>
        /// <typeparam name="T">Type of waitWith param <see cref="DefaultWait{T}"/></typeparam>
        /// <typeparam name="TResult">Type of object which is waiting</typeparam>
        /// <param name="condition">Function for waiting</param>
        /// <param name="waitWith">Object who will helping to wait (which will be passed to <see cref="DefaultWait{T}"/>)</param>
        /// <param name="timeOut">Time-out</param>
        /// <returns>Object which waiting for or default of a TResult class - is exceptions occured</returns>
        public static TResult WaitFor<T, TResult>(Func<T, TResult> condition, T waitWith, TimeSpan timeOut = default(TimeSpan))
        {
            var wait = new DefaultWait<T>(waitWith)
            {
                Timeout = GetConditionTimeOut(timeOut),
                PollingInterval = Configuration.TimeoutConfiguration.PollingInterval
            };

            try
            {
                return wait.Until(condition);
            }
            catch (Exception e)
            {
                Logger.DebugLoc("java.ConditionalWait.waitFor", e);
            }

            return default(TResult); ;
        }

        private static TimeSpan GetConditionTimeOut(TimeSpan timeOut)
        {
            return timeOut == default(TimeSpan) ? Configuration.TimeoutConfiguration.Condition : timeOut;
        }
    }
}

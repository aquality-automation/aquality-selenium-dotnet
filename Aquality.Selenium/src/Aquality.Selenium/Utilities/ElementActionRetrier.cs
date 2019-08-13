using Aquality.Selenium.Configurations;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Aquality.Selenium.Utilities
{
    internal sealed class ElementActionRetrier
    {
        private const int defaultRetryCount = 2;
        
        public static void DoWithRetry(Action action, int retryCount = default, TimeSpan? retryInterval = default)
        {
            DoWithRetry(() =>
            {
                action();
                return true;
            }, retryCount, retryInterval);
        }

        public static T DoWithRetry<T>(Func<T> function, int retryCount = default, TimeSpan? retryInterval = default)
        {
            var timeoutConfiguration = Configuration.Instance.TimeoutConfiguration;
            var retryAttemptsLeft = retryCount == default ? defaultRetryCount : retryCount;
            var actualInterval = retryInterval ?? timeoutConfiguration.PollingInterval;
            var result = default(T);
            while(retryAttemptsLeft >= 0)
            {
                try
                {
                    result = function();
                }
                catch (Exception exception)
                {
                    if (IsExceptionHandled(exception) && retryAttemptsLeft != 0)
                    {
                        Thread.Sleep(actualInterval);
                        retryAttemptsLeft--;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return result;
        }

        private static bool IsExceptionHandled(Exception exception)
        {
            return exception is StaleElementReferenceException;
        }
    }
}

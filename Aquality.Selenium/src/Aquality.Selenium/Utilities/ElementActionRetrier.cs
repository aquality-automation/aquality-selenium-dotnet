using Aquality.Selenium.Configurations;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Aquality.Selenium.Utilities
{
    /// <summary>
    /// Retries an action or function when <see cref="StaleElementReferenceException"/> or <see cref="InvalidElementStateException"/> occures.
    /// </summary>
    internal static class ElementActionRetrier
    {
        /// <summary>
        /// Retries the action when <see cref="StaleElementReferenceException"/> or <see cref="InvalidElementStateException"/> occures.
        /// </summary>
        /// <param name="action">Action to be applied.</param>
        public static void DoWithRetry(Action action)
        {
            DoWithRetry(() =>
            {
                action();
                return true;
            });
        }

        /// <summary>
        /// Retries the function when <see cref="StaleElementReferenceException"/> or <see cref="InvalidElementStateException"/> occures.
        /// </summary>
        /// <typeparam name="T">Return type of function.</typeparam>
        /// <param name="function">Function to be applied.</param>
        /// <returns>Result of the function</returns>
        public static T DoWithRetry<T>(Func<T> function)
        {
            var retryConfiguration = Configuration.Instance.RetryConfiguration;
            var retryAttemptsLeft = retryConfiguration.Number;
            var actualInterval = retryConfiguration.PollingInterval;
            var result = default(T);
            while (retryAttemptsLeft >= 0)
            {
                try
                {
                    result = function();
                    break;
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
            return exception is StaleElementReferenceException || exception is InvalidElementStateException;
        }
    }
}

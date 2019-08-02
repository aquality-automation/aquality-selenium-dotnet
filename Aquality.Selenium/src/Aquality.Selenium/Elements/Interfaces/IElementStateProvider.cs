using System;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Provides ability to define of element's state (whether it is displayed, exist or not)
    /// Also provides respective positive and negative waiting functions
    /// </summary>
    public interface IElementStateProvider
    {
        /// <summary>
        /// Is an element displayed on the page.
        /// </summary>
        bool IsDisplayed { get; }

        /// <summary>
        /// Is an element exist in DOM (without visibility check)
        /// </summary>
        bool IsExist { get; }

        /// <summary>
        /// Waits for is element displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: Configuration.TimeoutConfiguration.Condition</param>
        /// <returns>true if element displayed after waiting, false otherwise</returns>
        bool WaitForDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for is element not displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: Configuration.TimeoutConfiguration.Condition</param>
        /// <returns>true if element does not display after waiting, false otherwise</returns>
        bool WaitForNotDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits until element is exist in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: Configuration.TimeoutConfiguration.Condition</param>
        /// <returns>true if element exist after waiting, false otherwise</returns>
        bool WaitForExist(TimeSpan? timeout = null);

        /// <summary>
        /// Waits until element does not exist in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: Configuration.TimeoutConfiguration.Condition</param>
        /// <returns>true if element does not exist after waiting, false otherwise</returns>
        bool WaitForNotExist(TimeSpan? timeout = null);
    }
}

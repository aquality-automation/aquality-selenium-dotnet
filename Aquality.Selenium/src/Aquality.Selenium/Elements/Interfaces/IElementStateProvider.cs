using System;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Provides ability to define element's state (whether it is displayed, exist or not).
    /// Also provides respective positive and negative waiting methods.
    /// </summary>
    public interface IElementStateProvider
    {
        /// <summary>
        /// Gets element's displayed state.
        /// </summary>
        /// <value>True if displayed and false otherwise.</value>
        bool IsDisplayed { get; }

        /// <summary>
        /// Gets element's exist state.
        /// </summary>
        /// <value>True if element exists in DOM (without visibility check) and false otherwise</value>        
        bool IsExist { get; }

        /// <summary>
        /// Waits for element is displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element displayed after waiting, false otherwise</returns>
        bool WaitForDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element is not displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element does not display after waiting, false otherwise</returns>
        bool WaitForNotDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element exists in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element exist after waiting, false otherwise</returns>
        bool WaitForExist(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element does not exist in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element does not exist after waiting, false otherwise</returns>
        bool WaitForNotExist(TimeSpan? timeout = null);
    }
}

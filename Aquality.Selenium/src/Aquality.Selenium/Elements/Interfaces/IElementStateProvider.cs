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
        /// Gets element's displayed state: true if displayed and false otherwise.
        /// </summary>
        bool IsDisplayed { get; }

        /// <summary>
        /// Gets element's exist state: true if element exists in DOM (without visibility check) and false otherwise.
        /// </summary>
        bool IsExist { get; }

        /// <summary>
        /// Gets element's clickable state, which means element is displayed and enabled: true if element is clickable, false otherwise.
        /// </summary>
        bool IsClickable { get; }

        /// <summary>
        /// Gets element's Enabled state, which means element is Enabled and does not have "disabled" class: true if enabled, false otherwise.
        /// </summary>
        bool IsEnabled { get; }

        /// <summary>
        /// Waits for element is displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element displayed after waiting, false otherwise</returns>
        bool WaitForDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element is not displayed on the page.
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element does not display after waiting, false otherwise</returns>
        bool WaitForNotDisplayed(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element exists in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element exist after waiting, false otherwise</returns>
        bool WaitForExist(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element does not exist in DOM (without visibility check).
        /// </summary>
        /// <param name="timeout">Timeout for waiting. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>true if element does not exist after waiting, false otherwise</returns>
        bool WaitForNotExist(TimeSpan? timeout = null);        

        /// <summary>
        /// Waits for element is enabled state which means element is Enabled and does not have "disabled" class.
        /// </summary>
        /// <param name="timeout">Timeout to get state. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>True if enabled, false otherwise.</returns>
        bool WaitForEnabled(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element is not enabled state which means element is not Enabled or does have "disabled" class.
        /// </summary>
        /// <param name="timeout">Timeout to get state. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>True if not enabled, false otherwise.</returns>
        bool WaitForNotEnabled(TimeSpan? timeout = null);

        /// <summary>
        /// Waits for element to become clickable which means element is displayed and enabled.
        /// </summary>
        /// <param name="timeout">Timeout for wait. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <exception cref="OpenQA.Selenium.WebDriverTimeoutException">Throws when timeout exceeded and element is not clickable.</exception>
        void WaitForClickable(TimeSpan? timeout = null);
    }
}

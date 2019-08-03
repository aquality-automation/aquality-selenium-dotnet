using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations.WebDriverSettings;
using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes browser settings.
    /// </summary>
    public interface IBrowserProfile
    {
        /// <summary>
        /// Gets name of target browser.
        /// </summary>
        /// <value>Name of browser.</value>
        BrowserName BrowserName { get; }

        /// <summary>
        /// Is remote browser or not.
        /// </summary>
        /// <value>True if remote browser and false if local.</value>
        bool IsRemote { get; }

        /// <summary>
        /// Gets remote connection URI is case of remote browser.
        /// </summary>
        /// <value>URI for remote connection.</value>
        Uri RemoteConnectionUrl { get; }

        /// <summary>
        /// Is element hightlight enabled or not.
        /// </summary>
        /// <value>True if element highlight is enabled and false otherwise.</value>
        bool IsElementHighlightEnabled { get; }

        /// <summary>
        /// Gets driver settings for target browser.
        /// </summary>
        /// <value>Instance of driver settings.</value>
        IDriverSettings DriverSettings { get; }
    }
}

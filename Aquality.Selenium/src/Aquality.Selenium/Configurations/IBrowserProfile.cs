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
        BrowserName BrowserName { get; }

        /// <summary>
        /// Is remote browser or not: true if remote browser and false if local.
        /// </summary>
        bool IsRemote { get; }

        /// <summary>
        /// Gets remote connection URI is case of remote browser.
        /// </summary>
        Uri RemoteConnectionUrl { get; }

        /// <summary>
        /// Is element hightlight enabled or not: true if element highlight is enabled and false otherwise
        /// </summary>
        bool IsElementHighlightEnabled { get; }

        /// <summary>
        /// Gets driver settings for target browser.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when there is no assigned behaviour for retrieving DriverSettings for target browser.</exception>
        IDriverSettings DriverSettings { get; }
    }
}

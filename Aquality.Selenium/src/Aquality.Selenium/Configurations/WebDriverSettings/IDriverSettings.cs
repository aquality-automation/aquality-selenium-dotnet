using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Describes web driver settings.
    /// </summary>
    public interface IDriverSettings
    {
        /// <summary>
        /// Gets desired options for web driver.
        /// </summary>
        DriverOptions DriverOptions { get; }

        /// <summary>
        /// WebDriver page load strategy.
        /// </summary>
        PageLoadStrategy PageLoadStrategy { get; }

        /// <summary>
        /// Gets download directory for web driver.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Thrown when browser settings do not contain capability key.</exception>
        string DownloadDir { get; }

        /// <summary>
        /// Gets web driver capability key for download directory.
        /// </summary>
        string DownloadDirCapabilityKey { get; }
    }
}

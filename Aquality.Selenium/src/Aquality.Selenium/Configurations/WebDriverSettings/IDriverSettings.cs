using OpenQA.Selenium;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Describes web driver settings.
    /// </summary>
    public interface IDriverSettings
    {
        /// <summary>
        /// Gets version of web driver for WebDriverManager.
        /// </summary>
        /// <value>Target version of web driver.</value>
        string WebDriverVersion { get; }

        /// <summary>
        /// Gets system architecture for WebDriverManager.
        /// </summary>
        /// <value>Target system architecture.</value>
        Architecture SystemArchitecture { get; }

        /// <summary>
        /// Gets desired options for web driver.
        /// </summary>
        /// <value>Options for web driver.</value>
        DriverOptions DriverOptions { get; }

        /// <summary>
        /// Gets download directory for web driver.
        /// </summary>
        /// <value>Download directory.</value>
        string DownloadDir { get; }

        /// <summary>
        /// Gets web driver capability key for download directory.
        /// </summary>
        /// <value>Capability key.</value>
        string DownloadDirCapabilityKey { get; }
    }
}

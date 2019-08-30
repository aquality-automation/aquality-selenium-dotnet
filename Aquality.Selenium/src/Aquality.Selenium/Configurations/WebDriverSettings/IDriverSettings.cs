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
        string WebDriverVersion { get; }

        /// <summary>
        /// Gets target system architecture for WebDriverManager.
        /// </summary>
        Architecture SystemArchitecture { get; }

        /// <summary>
        /// Gets desired options for web driver.
        /// </summary>
        DriverOptions DriverOptions { get; }

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

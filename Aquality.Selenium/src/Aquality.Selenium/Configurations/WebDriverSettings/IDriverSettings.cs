using OpenQA.Selenium;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public interface IDriverSettings
    {
        string WebDriverVersion { get; }

        Architecture SystemArchitecture { get; }

        DriverOptions DriverOptions { get; }

        string DownloadDir { get; }

        string DownloadDirCapabilityKey { get; }
    }
}

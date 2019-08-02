using Aquality.Selenium.Utilities;
using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public abstract class DriverSettings : IDriverSettings
    {
        public DriverSettings(JsonFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected JsonFile SettingsFile { get; }

        public string WebDriverVersion { get; protected set; } = "Latest";

        public string SystemArchitecture => throw new System.NotImplementedException();

        public abstract DriverOptions DriverOptions { get; }

        public abstract string DownloadDir { get; }

        public abstract string DownloadDirCapabilityKey { get; }
    }
}

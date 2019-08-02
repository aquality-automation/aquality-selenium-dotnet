using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using System;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public abstract class DriverSettings : IDriverSettings
    {
        public DriverSettings(JsonFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected JsonFile SettingsFile { get; }

        public string WebDriverVersion => throw new NotImplementedException();

        public Architecture SystemArchitecture => throw new NotImplementedException();

        public abstract DriverOptions DriverOptions { get; }

        public abstract string DownloadDir { get; }

        public abstract string DownloadDirCapabilityKey { get; }
    }
}

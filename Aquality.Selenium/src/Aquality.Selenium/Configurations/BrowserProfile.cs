﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations.WebDriverSettings;
using Aquality.Selenium.Utilities;
using System;

namespace Aquality.Selenium.Configurations
{
    public class BrowserProfile : IBrowserProfile
    {
        private readonly JsonFile settingsFile;

        public BrowserProfile(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        public BrowserName BrowserName => (BrowserName) Enum.Parse(typeof(BrowserName), settingsFile.GetObject<string>(".browserName"), ignoreCase: true);

        public bool IsElementHighlightEnabled => settingsFile.GetObject<bool>(".isElementHighlightEnabled");

        public bool IsRemote => settingsFile.GetObject<bool>(".isRemote");

        public Uri RemoteConnectionUrl => new Uri(settingsFile.GetObject<string>(".remoteConnectionUrl"));

        public IDriverSettings DriverSettings
        {
            get
            {
                switch (BrowserName)
                {
                    case BrowserName.Chrome:
                        return new ChromeSettings(settingsFile);
                    case BrowserName.Edge:
                        return new EdgeSettings(settingsFile);
                    case BrowserName.Firefox:
                        return new FirefoxSettings(settingsFile);
                    case BrowserName.InternetExplorer:
                        return new InternetExplorerSettings(settingsFile);
                    case BrowserName.Safari:
                        return new SafariSettings(settingsFile);
                    default:
                        throw new ArgumentOutOfRangeException($"There is no assigned behaviour for retrieving driver driversettings for browser {BrowserName}");
                }
            }
        }
    }
}

﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations.WebDriverSettings;
using Aquality.Selenium.Core.Configurations;
using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides target browser profile.
    /// </summary>
    public class BrowserProfile : IBrowserProfile
    {
        private readonly ISettingsFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public BrowserProfile(ISettingsFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        public BrowserName BrowserName
        {
            get
            {
                var dirtyName = settingsFile.GetValue<string>(".browserName");
                if (dirtyName.Equals("edgechromium", StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new NotSupportedException("EdgeChromium is now officially supported in Selenium 4. Please use 'edge' browserName in settings.json");
                }
                if (!Enum.TryParse(dirtyName, ignoreCase: true, out BrowserName browserName))
                {
                    return BrowserName.Other;
                }
                return browserName;
            }
        }

        public bool IsElementHighlightEnabled => settingsFile.GetValue<bool>(".isElementHighlightEnabled");

        public bool IsRemote => settingsFile.GetValue<bool>(".isRemote");

        public Uri RemoteConnectionUrl => new Uri(settingsFile.GetValue<string>(".remoteConnectionUrl"));

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
                    case BrowserName.IExplorer:
                        return new InternetExplorerSettings(settingsFile);
                    case BrowserName.Opera:
                        return new OperaSettings(settingsFile);
                    case BrowserName.Safari:
                        return new SafariSettings(settingsFile);
                    case BrowserName.Yandex:
                        return new YandexSettings(settingsFile);
                    default:
                        throw new InvalidOperationException($"There is no assigned behaviour for retrieving DriverSettings for browser {BrowserName}");
                }
            }
        }
    }
}

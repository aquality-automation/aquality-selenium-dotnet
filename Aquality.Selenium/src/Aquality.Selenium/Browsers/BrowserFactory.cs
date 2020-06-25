using Aquality.Selenium.Core.Localization;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory(ILocalizedLogger localizedLogger)
        {
            LocalizedLogger = localizedLogger;
        }

        public abstract Browser Browser { get; }

        protected ILocalizedLogger LocalizedLogger { get; }

        protected void LogBrowserIsReady(BrowserName browserName)
        {
            LocalizedLogger.Info("loc.browser.ready", browserName);
        }
    }
}

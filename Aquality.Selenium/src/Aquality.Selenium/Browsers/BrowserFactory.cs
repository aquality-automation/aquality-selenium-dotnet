using Aquality.Selenium.Core.Localization;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory()
        {
        }

        public abstract Browser Browser { get; }

        protected ArgumentOutOfRangeException LoggedWrongBrowserNameException
        {
            get
            {
                var message = AqualityServices.Get<ILocalizationManager>().GetLocalizedMessage("loc.browser.name.wrong");
                var exception = new ArgumentOutOfRangeException(message);
                AqualityServices.Logger.Fatal(message, exception);
                return exception;
            }
        }

        protected void LogBrowserIsReady(BrowserName browserName)
        {
            AqualityServices.LocalizedLogger.Info("loc.browser.ready", browserName);
        }
    }
}

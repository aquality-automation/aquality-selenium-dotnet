using Aquality.Selenium.Configurations;
using System.Threading;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public static class BrowserManager
    {
        private static readonly ThreadLocal<Browser> browserContainer = new ThreadLocal<Browser>();
        private static readonly ThreadLocal<IBrowserFactory> browserFactoryContainer = new ThreadLocal<IBrowserFactory>();

        /// <summary>
        /// Gets and sets thread-safe instance of browser.
        /// </summary>
        /// <value>Instance of desired browser.</value>
        public static Browser Browser
        {
            get
            {
                if (!browserContainer.IsValueCreated || browserContainer.Value.Driver.SessionId == null)
                {
                    SetDefaultBrowser();
                }
                return browserContainer.Value;
            }
            set
            {
                browserContainer.Value = value;
            }
        }

        private static void SetDefaultBrowser()
        {
            if (!browserFactoryContainer.IsValueCreated)
            {
                SetDefaultFactory();
            }

            Browser = browserFactoryContainer.Value.Browser;
        }

        /// <summary>
        /// Sets default factory responsible for browser creation.
        /// RemoteBrowserFactory if value set in configuration and LocalBrowserFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            IBrowserFactory browserFactory;
            IConfiguration configuration = Configuration.Instance;

            if (configuration.BrowserProfile.IsRemote)
            {
                browserFactory = new RemoteBrowserFactory(configuration);
            }
            else
            {
                browserFactory = new LocalBrowserFactory(configuration);
            }
            SetFactory(browserFactory);
        }

        /// <summary>
        /// Sets custom browser factory.
        /// </summary>
        /// <param name="browserFactory">Custom implementation of <see cref="Aquality.Selenium.Browsers.IBrowserFactory"/></param>
        public static void SetFactory(IBrowserFactory browserFactory)
        {
            browserFactoryContainer.Value = browserFactory;
        }
    }
}

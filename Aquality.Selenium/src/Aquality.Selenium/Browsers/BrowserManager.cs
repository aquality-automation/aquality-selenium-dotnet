using Aquality.Selenium.Configurations;
using System.Threading;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public static class BrowserManager
    {
        private static readonly ThreadLocal<Browser> BrowserContainer = new ThreadLocal<Browser>();
        private static readonly ThreadLocal<IBrowserFactory> BrowserFactoryContainer = new ThreadLocal<IBrowserFactory>();

        /// <summary>
        /// Gets and sets thread-safe instance of browser.
        /// </summary>
        /// <value>Instance of desired browser.</value>
        public static Browser Browser
        {
            get
            {
                if (!BrowserContainer.IsValueCreated || BrowserContainer.Value.Driver.SessionId == null)
                {
                    SetDefaultBrowser();
                }
                return BrowserContainer.Value;
            }
            set
            {
                BrowserContainer.Value = value;
            }
        }

        private static void SetDefaultBrowser()
        {
            if (!BrowserFactoryContainer.IsValueCreated)
            {
                SetDefaultFactory();
            }

            Browser = BrowserFactoryContainer.Value.Browser;
        }

        /// <summary>
        /// Sets default factory responsible for browser creation.
        /// RemoteBrowserFactory if value set in configuration and LocalBrowserFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            IConfiguration configuration = Configuration.Instance;
            IBrowserFactory browserFactory;
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
        /// <param name="browserFactory">Custom implementation of <see cref="IBrowserFactory"/></param>
        public static void SetFactory(IBrowserFactory browserFactory)
        {
            BrowserFactoryContainer.Value = browserFactory;
        }
    }
}

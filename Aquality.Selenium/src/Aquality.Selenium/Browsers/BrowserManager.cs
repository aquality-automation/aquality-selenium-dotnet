using Aquality.Selenium.Configurations;
using System.Threading;

namespace Aquality.Selenium.Browsers
{
    public static class BrowserManager
    {
        private static readonly ThreadLocal<Browser> browserContainer = new ThreadLocal<Browser>();
        private static readonly ThreadLocal<IBrowserFactory> browserFactoryContainer = new ThreadLocal<IBrowserFactory>();

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

        public static void SetFactory(IBrowserFactory browserFactory)
        {
            browserFactoryContainer.Value = browserFactory;
        }
    }
}

using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Wrap over implementation of Selenium WebDriver INetwork.
    /// </summary>
    public class NetworkHandling : INetwork
    {
        private readonly INetwork network;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkHandling"/> class.
        /// </summary>
        /// <param name="driver">The <see cref="IWebDriver"/> instance on which the network should be monitored.</param>
        public NetworkHandling(IWebDriver driver)
        {
            network = driver.Manage().Network;
        }

        private ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// A network request sent event.
        /// </summary>
        public event EventHandler<NetworkRequestSentEventArgs> NetworkRequestSent
        {
            add
            {
                Logger.Info("loc.browser.network.event.requestsent.add");
                network.NetworkRequestSent += value;
            }
            remove
            {
                Logger.Info("loc.browser.network.event.requestsent.remove");
                network.NetworkRequestSent -= value;
            }
        }

        /// <summary>
        /// A network response received event.
        /// </summary>
        public event EventHandler<NetworkResponseReceivedEventArgs> NetworkResponseReceived
        {
            add
            {
                Logger.Info("loc.browser.network.event.responsereceived.add");
                network.NetworkResponseReceived += value;
            }
            remove
            {
                Logger.Info("loc.browser.network.event.responsereceived.remove");
                network.NetworkResponseReceived -= value;
            }
        }

        /// <summary>
        /// Add basic authentication handler.
        /// </summary>
        /// <param name="handler">Authentication parameters, such as URI matcher and credentials.</param>
        public void AddAuthenticationHandler(NetworkAuthenticationHandler handler)
        {
            Logger.Info("loc.browser.network.handler.authentication.add");
            network.AddAuthenticationHandler(handler);
        }

        /// <summary>
        /// Add request handler.
        /// </summary>
        /// <param name="handler">Handler parameters: request matcher, request transformer and response supplier.</param>
        public void AddRequestHandler(NetworkRequestHandler handler)
        {
            Logger.Info("loc.browser.network.handler.request.add");
            network.AddRequestHandler(handler);
        }

        /// <summary>
        /// Add response handler.
        /// </summary>
        /// <param name="handler">Handler parameters: response matcher and transformer.</param>
        public void AddResponseHandler(NetworkResponseHandler handler)
        {
            Logger.Info("loc.browser.network.handler.response.add");
            network.AddResponseHandler(handler);
        }

        /// <summary>
        /// Clear basic authentication handlers.
        /// </summary>
        public void ClearAuthenticationHandlers()
        {
            Logger.Info("loc.browser.network.handler.authentication.clear");
            network.ClearAuthenticationHandlers();
        }

        /// <summary>
        /// Clear request handlers.
        /// </summary>
        public void ClearRequestHandlers()
        {
            Logger.Info("loc.browser.network.handler.request.clear");
            network.ClearRequestHandlers();
        }

        /// <summary>
        /// Clear response handlers.
        /// </summary>
        public void ClearResponseHandlers()
        {
            Logger.Info("loc.browser.network.handler.response.clear");
            network.ClearResponseHandlers();
        }

        /// <summary>
        /// Awaits to start network monitoring.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        public async Task StartMonitoring()
        {
            Logger.Info("loc.browser.network.monitoring.start");
            await network.StartMonitoring();
        }

        /// <summary>
        /// Awaits to stop network monitoring.
        /// </summary>
        /// <returns>A task to be awaited.</returns>
        public async Task StopMonitoring()
        {
            Logger.Info("loc.browser.network.monitoring.stop");
            await network.StopMonitoring();
        }
    }
}

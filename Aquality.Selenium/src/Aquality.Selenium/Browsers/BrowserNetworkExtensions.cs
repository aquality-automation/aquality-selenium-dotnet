using OpenQA.Selenium;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    public static class BrowserNetworkExtensions
    {
        /// <summary>
        /// Registers authentication handler and awaits to start network monitoring.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="hostPart">Part of the host name for URI matcher.</param>
        /// <param name="username">Username for authentication.</param>
        /// <param name="password">Password for authentication.</param>
        /// <returns>A task to be awaited.</returns>
        public static async Task RegisterBasicAuthenticationAndStartMonitoring(this Browser browser, string hostPart, string username, string password)
        {
            var handler = new NetworkAuthenticationHandler
            {
                UriMatcher = (uri) => uri.Host.Contains(hostPart),
                Credentials = new PasswordCredentials(username, password)
            };
            await RegisterBasicAuthenticationAndStartMonitoring(browser, handler);
        }

        /// <summary>
        /// Registers authentication handler and awaits to start network monitoring.
        /// </summary>
        /// <param name="browser">Browser instance.</param>
        /// <param name="handler">Authentication parameters, such as URI matcher and credentials.</param>
        /// <returns>A task to be awaited.</returns>
        public static async Task RegisterBasicAuthenticationAndStartMonitoring(this Browser browser, NetworkAuthenticationHandler handler)
        {
            var networkHandling = browser.Network;
            networkHandling.AddAuthenticationHandler(handler);
            await networkHandling.StartMonitoring();
        }
    }
}

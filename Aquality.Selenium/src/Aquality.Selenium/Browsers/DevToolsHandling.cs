using Aquality.Selenium.Core.Localization;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V85.Emulation;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Wrapper for Selenium <see cref="IDevTools"/> functionality.
    /// </summary>
    public class DevToolsHandling : IDevTools
    {
        private readonly IDevTools devToolsProvider;
        private bool wasDevToolsSessionClosed;

        /// <summary>
        /// Initializes an instance of <see cref="DevToolsHandling"/>.
        /// </summary>
        /// <param name="devToolsProvider">Instance of <see cref="WebDriver"/> which supports CDP Protocol.</param>
        public DevToolsHandling(IDevTools devToolsProvider)
        {
            this.devToolsProvider = devToolsProvider;
            wasDevToolsSessionClosed = false;
        }

        private ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Gets a value indicating whether a DevTools session is active.
        /// Because backing field is not set to null in Selenium <see cref="CloseDevToolsSession"/> implementations 
        /// <see cref="ChromiumDriver"/> and <see cref="FirefoxDriver"/>, we need to rework it with ours backing field.
        /// </summary>
        public bool HasActiveDevToolsSession
        {
            get
            {
                Logger.Info("loc.devtools.session.hasactive");
                var result = devToolsProvider.HasActiveDevToolsSession && !wasDevToolsSessionClosed;
                Logger.Info("loc.devtools.session.hasactive.result", result);
                return result;
            }
        }

        /// <summary>
        /// Closes a DevTools session.
        /// </summary>
        public void CloseDevToolsSession()
        {
            Logger.Info("loc.devtools.session.close");
            devToolsProvider.CloseDevToolsSession();
            wasDevToolsSessionClosed = true;
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// Calls overload <see cref="GetDevToolsSession(int)"/>, where parameter protocolVersion
        /// defaults to autodetect the protocol version for Chromium, V85 for Firefox.
        /// </summary>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public DevToolsSession GetDevToolsSession()
        {
            Logger.Info("loc.devtools.session.get", "default");
            return devToolsProvider.GetDevToolsSession();
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// </summary>
        /// <param name="protocolVersion">The version of the Chromium Developer Tools protocol to use. 
        /// Defaults to autodetect the protocol version for <see cref="ChromiumDriver"/>, V85 for FirefoxDriver.</param>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public DevToolsSession GetDevToolsSession(int protocolVersion)
        {
            Logger.Info("loc.devtools.session.get", protocolVersion);
            return devToolsProvider.GetDevToolsSession(protocolVersion);
        }

        /// <summary>
        /// Executes a custom Chromium Dev Tools Protocol Command.
        /// Note: works only if current driver is instance of <see cref="ChromiumDriver"/>.
        /// </summary>
        /// <param name="commandName">Name of the command to execute.</param>
        /// <param name="commandParameters">Parameters of the command to execute.</param>
        /// <returns>An object representing the result of the command, if applicable.</returns>
        public object ExecuteCdpCommand(string commandName, Dictionary<string, object> commandParameters)
        {
            if (devToolsProvider is ChromiumDriver driver)
            {
                Logger.Info("loc.devtools.command.execute", commandName,
                               string.Join(",", commandParameters.Select(cap => $"{Environment.NewLine}{cap.Key}: {cap.Value}")));
                var result = driver.ExecuteCdpCommand(commandName, commandParameters);
                Logger.Info("loc.devtools.command.execute.result", JToken.FromObject(result).ToString());
                return result;
            }
            else
            {
                throw new NotSupportedException("Execution of CDP command directly is not supported for current browser. Try SendCommand method instead.");
            }
        }

        /// <summary>
        /// Sends the specified command and returns the associated command response.
        /// </summary>
        /// <param name="commandName">The name of the command to send.</param>
        /// <param name="commandParameters">The parameters of the command as a JToken object</param>
        /// <param name="cancellationToken">A CancellationToken object to allow for cancellation of the command.</param>
        /// <param name="millisecondsTimeout">The execution timeout of the command in milliseconds.</param>
        /// <param name="throwExceptionIfResponseNotReceived"><see langword="true"/> to throw an exception if a response is not received; otherwise, <see langword="false"/>.</param>
        /// <returns>A JToken based on a command created with the specified command name and parameters.</returns>
        public async Task<JToken> SendCommand(string commandName, JToken commandParameters, 
            CancellationToken cancellationToken = default, int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true)
        {
            Logger.Info("loc.devtools.command.execute", commandName, commandParameters.ToString());
            var result = await GetDevToolsSession().SendCommand(commandName, commandParameters, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
            Logger.Info("loc.devtools.geolocation.set.result", result.ToString());
            return result;
        }

        /// <summary>
        /// Overrides the GeoLocation Position or Error. Omitting any of the parameters emulates position unavailable.
        /// </summary>
        /// <param name="latitude">Latitude of GeoLocation.</param>
        /// <param name="longitude">Longitude of location.</param>
        /// <param name="accuracy">Accuracy of the geoLocation. By default is set to 1 meaning 100% accuracy.</param>
        /// <exception cref="NotSupportedException"></exception>
        public async Task<JToken> SetGeoLocationOverride(double? latitude, double? longitude, double? accuracy = 1)
        {
            var settings = new SetGeolocationOverrideCommandSettings
            {
                Latitude = latitude,
                Longitude = longitude,
                Accuracy = accuracy
            };
            var settingsJToken = JToken.FromObject(settings);
            return await GetDevToolsSession().SendCommand(new SetGeolocationOverrideCommandSettings().CommandName, settingsJToken);
        }

        /// <summary>
        /// Clears the overridden GeoLocation Position and Error.
        /// </summary>
        /// <returns>A task for asynchronous command.</returns>
        public async Task ClearGeolocationOverride()
        {
            await SendCommand(new ClearGeolocationOverrideCommandSettings().CommandName, new JObject());
        }
    }
}

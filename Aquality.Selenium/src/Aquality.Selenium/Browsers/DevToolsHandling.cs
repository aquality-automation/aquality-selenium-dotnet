using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
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

        private static ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Gets a value indicating whether a DevTools session is active.
        /// Because backing field is not set to null in Selenium <see cref="CloseDevToolsSession"/> implementations 
        /// <see cref="ChromiumDriver"/> and <see cref="FirefoxDriver"/>, we need to rework it with ours backing field.
        /// </summary>
        public bool HasActiveDevToolsSession
        {
            get
            {
                Logger.Info("loc.browser.devtools.session.isactive");
                var result = devToolsProvider.HasActiveDevToolsSession && !wasDevToolsSessionClosed;
                Logger.Info("loc.browser.devtools.session.isactive.result", result);
                return result;
            }
        }

        /// <summary>
        /// Closes a DevTools session.
        /// </summary>
        public void CloseDevToolsSession()
        {
            Logger.Info("loc.browser.devtools.session.close");
            devToolsProvider.CloseDevToolsSession();
            wasDevToolsSessionClosed = true;
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// Calls overload <see cref="GetDevToolsSession(DevToolsOptions)"/>, where parameter protocolVersion
        /// defaults to autodetect the protocol version for Chromium, V85 for Firefox.
        /// </summary>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public DevToolsSession GetDevToolsSession()
        {
            Logger.Info("loc.browser.devtools.session.get", "default");
            var session = devToolsProvider.GetDevToolsSession();
            wasDevToolsSessionClosed = false;
            return session;
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// </summary>
        /// <param name="protocolVersion">The version of the Chromium Developer Tools protocol to use. 
        /// Defaults to autodetect the protocol version for <see cref="ChromiumDriver"/>, V85 for FirefoxDriver.</param>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        [Obsolete("Use GetDevToolsSession(DevToolsOptions options)")]
        public DevToolsSession GetDevToolsSession(int protocolVersion)
        {
            Logger.Info("loc.browser.devtools.session.get", protocolVersion);
            var session = devToolsProvider.GetDevToolsSession(protocolVersion);
            wasDevToolsSessionClosed = false;
            return session;
        }

        /// <summary>
        /// Creates a session to communicate with a browser using the Chromium Developer Tools debugging protocol.
        /// </summary>
        /// <param name="options"> The options for the DevToolsSession to use.
        /// Defaults to autodetect the protocol version for <see cref="ChromiumDriver"/>, V85 for FirefoxDriver.</param>
        /// <returns>The active session to use to communicate with the Chromium Developer Tools debugging protocol.</returns>
        public DevToolsSession GetDevToolsSession(DevToolsOptions options)
        {
            Logger.Info("loc.browser.devtools.session.get", options.ProtocolVersion?.ToString() ?? "default");
            var session = devToolsProvider.GetDevToolsSession(options);
            wasDevToolsSessionClosed = false;
            return session;
        }

        /// <summary>
        /// Executes a custom Chromium Dev Tools Protocol Command.
        /// Note: works only if current driver is instance of <see cref="ChromiumDriver"/>.
        /// </summary>
        /// <param name="commandName">Name of the command to execute.</param>
        /// <param name="commandParameters">Parameters of the command to execute.</param>
        /// <param name="loggingOptions">Logging preferences.</param>
        /// <returns>An object representing the result of the command, if applicable.</returns>
        public object ExecuteCdpCommand(string commandName, Dictionary<string, object> commandParameters, DevToolsCommandLoggingOptions loggingOptions = null)
        {
            if (devToolsProvider is ChromiumDriver driver)
            {
                LogCommand(commandName, JsonSerializer.SerializeToNode(commandParameters), loggingOptions);
                var result = driver.ExecuteCdpCommand(commandName, commandParameters);
                var formattedResult = JsonSerializer.SerializeToElement(result);
                LogCommandResult(formattedResult, loggingOptions);
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
        /// <param name="commandParameters">The parameters of the command as a JsonNode object.</param>
        /// <param name="cancellationToken">A CancellationToken object to allow for cancellation of the command.</param>
        /// <param name="millisecondsTimeout">The execution timeout of the command in milliseconds.</param>
        /// <param name="throwExceptionIfResponseNotReceived"><see langword="true"/> to throw an exception if a response is not received; otherwise, <see langword="false"/>.</param>
        /// <param name="loggingOptions">Logging preferences.</param>
        /// <returns>A JsonElement? based on a command created with the specified command name and parameters.</returns>
        public async Task<JsonElement?> SendCommand(string commandName, JsonNode commandParameters = null, 
            CancellationToken cancellationToken = default, int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true, 
            DevToolsCommandLoggingOptions loggingOptions = null)
        {
            var parameters = commandParameters ?? new JsonObject();
            LogCommand(commandName, parameters, loggingOptions);
            var result = await devToolsProvider.GetDevToolsSession()
                .SendCommand(commandName, parameters, cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived);
            LogCommandResult(result, loggingOptions);            
            return result;
        }

        /// <summary>
        /// Sends the specified command and returns the associated command response.
        /// </summary>
        /// <param name="commandWithParameters">The command to send with parameters.</param>
        /// <param name="cancellationToken">A CancellationToken object to allow for cancellation of the command.</param>
        /// <param name="millisecondsTimeout">The execution timeout of the command in milliseconds.</param>
        /// <param name="throwExceptionIfResponseNotReceived"><see langword="true"/> to throw an exception if a response is not received; otherwise, <see langword="false"/>.</param>
        /// <param name="loggingOptions">Logging preferences.</param>
        /// <returns>A JsonElement? based on a command created with the specified command name and parameters.</returns>
        public async Task<JsonElement?> SendCommand(ICommand commandWithParameters,
            CancellationToken cancellationToken = default, int? millisecondsTimeout = null, bool throwExceptionIfResponseNotReceived = true, 
            DevToolsCommandLoggingOptions loggingOptions = null)
        {
            return await SendCommand(commandWithParameters.CommandName, JsonSerializer.SerializeToNode(commandWithParameters, commandWithParameters.GetType()), 
                cancellationToken, millisecondsTimeout, throwExceptionIfResponseNotReceived, loggingOptions);
        }

        protected virtual void LogCommand(string commandName, JsonNode commandParameters, DevToolsCommandLoggingOptions loggingOptions = null)
        {
            var logging = (loggingOptions ?? new DevToolsCommandLoggingOptions()).Command;
            if (!logging.Enabled)
            {
                return;
            }
            if (IsNotEmpty(commandParameters))
            {
                Logger.LogByLevel(logging.LogLevel, "loc.browser.devtools.command.execute.withparams", commandName, commandParameters.ToString());
            }
            else
            {
                Logger.LogByLevel(logging.LogLevel, "loc.browser.devtools.command.execute", commandName);
            }
        }

        protected virtual void LogCommandResult(JsonElement? result, DevToolsCommandLoggingOptions loggingOptions = null)
        {
            var logging = (loggingOptions ?? new DevToolsCommandLoggingOptions()).Result;
            if (IsNotEmpty(result) && logging.Enabled)
            {
                Logger.LogByLevel(logging.LogLevel, "loc.browser.devtools.command.execute.result", result.ToString());
            }
        }

        private static bool IsNotEmpty(JsonElement? jsonElement)
        {
            if (!jsonElement.HasValue)
            {
                return false;
            }

            var element = jsonElement.Value;
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    return element.EnumerateObject().Any();
                case JsonValueKind.Array:
                    return element.EnumerateArray().Any();
                case JsonValueKind.String:
                    return !string.IsNullOrEmpty(element.GetString());
                case JsonValueKind.Number:
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsNotEmpty(JsonNode jsonNode)
        {
            return jsonNode is JsonArray array ? array.Any() : (jsonNode as JsonObject).Any();
        }
    }
}

using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V135.DOM;
using OpenQA.Selenium.DevTools.V135.Emulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Implementation of version-independent emulation DevTools commands as extensions for <see cref="DevToolsHandling"/>.
    /// Currently only non-experimental extensions are implemented.
    /// For more information, see <see href="https://chromedevtools.github.io/devtools-protocol/tot/Emulation/"/>.
    /// </summary>
    public static class DevToolsEmulationExtensions
    {
        /// <summary>
        /// Tells whether emulation is supported.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command with boolean result.</returns>
        public static async Task<bool> CanEmulate(this DevToolsHandling devTools)
        {
            var response = await devTools.SendCommand(new CanEmulateCommandSettings());
            return response.HasValue && response.Value.TryGetProperty("result", out JsonElement resultElement)
                && (resultElement.ValueKind == JsonValueKind.True ||
                (resultElement.ValueKind == JsonValueKind.String &&
                resultElement.GetString().Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// Overrides the GeoLocation Position or Error. Omitting any of the parameters emulates position unavailable.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="latitude">Latitude of GeoLocation.</param>
        /// <param name="longitude">Longitude of location.</param>
        /// <param name="accuracy">Accuracy of the geoLocation. By default is set to 1 meaning 100% accuracy.</param>
        public static async Task SetGeoLocationOverride(this DevToolsHandling devTools, 
            double? latitude, double? longitude, double? accuracy = 1)
        {
            var settings = new SetGeolocationOverrideCommandSettings
            {
                Latitude = latitude,
                Longitude = longitude,
                Accuracy = accuracy
            };
            await devTools.SendCommand(settings);
        }

        /// <summary>
        /// Clears the overridden GeoLocation Position and Error.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task ClearGeolocationOverride(this DevToolsHandling devTools)
        {
            await devTools.SendCommand(new ClearGeolocationOverrideCommandSettings());
        }

        /// <summary>
        /// Overrides the values of device screen dimensions.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="commandParameters">Version-specific set of parameters. 
        /// For example, take a look at <see cref="SetDeviceMetricsOverrideCommandSettings"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetDeviceMetricsOverride(this DevToolsHandling devTools, ICommand commandParameters)
        {
            GuardCommandParameters<SetDeviceMetricsOverrideCommandSettings>(commandParameters);
            await devTools.SendCommand(commandParameters);
        }

        /// <summary>
        /// Overrides the values of device screen dimensions.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="width">Value to override window.screen.width.</param>
        /// <param name="height">Value to override window.screen.height.</param>
        /// <param name="mobile">Whether to emulate mobile device. This includes viewport meta tag, overlay scrollbars, text autosizing and more.</param>
        /// <param name="deviceScaleFactor">Overriding device scale factor value. 0 disables the override.</param>
        /// <param name="screenOrientationType">Orientation type. 
        /// Allowed Values (in any case): portraitPrimary, portraitSecondary, landscapePrimary, landscapeSecondary.</param>
        /// <param name="screenOrientationAngle">Orientation angle. Set only if orientation type was set.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetDeviceMetricsOverride(this DevToolsHandling devTools,
            long width, long height, bool mobile, 
            double deviceScaleFactor = 0, string screenOrientationType = null, int? screenOrientationAngle = null)
        {
            var deviceModeSetting = new SetDeviceMetricsOverrideCommandSettings()
            {
                Width = width,
                Height = height,
                Mobile = mobile,
                DeviceScaleFactor = deviceScaleFactor
            };
            if (!string.IsNullOrEmpty(screenOrientationType))
            {
                var screenOrientation = new ScreenOrientation
                {
                    Type = screenOrientationType.ToEnum<ScreenOrientationTypeValues>()
                };
                if (screenOrientationAngle != null)
                {
                    screenOrientation.Angle = screenOrientationAngle.Value;
                }
                deviceModeSetting.ScreenOrientation = screenOrientation;
            }
            await SetDeviceMetricsOverride(devTools, deviceModeSetting);
        }

        /// <summary>
        /// Clears the overridden device metrics.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task ClearDeviceMetricsOverride(this DevToolsHandling devTools)
        {
            await devTools.SendCommand(new ClearDeviceMetricsOverrideCommandSettings());
        }

        /// <summary>
        /// Overrides the values of user agent.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="commandParameters">Version-specific set of parameters. 
        /// For example, take a look at <see cref="SetUserAgentOverrideCommandSettings"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetUserAgentOverride(this DevToolsHandling devTools, ICommand commandParameters)
        {
            GuardCommandParameters<SetUserAgentOverrideCommandSettings>(commandParameters);
            await devTools.SendCommand(commandParameters);
        }

        /// <summary>
        /// Overrides the values of user agent.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="userAgent">User agent to use.</param>
        /// <param name="acceptLanguage">Browser language to emulate.</param>
        /// <param name="platform">The platform navigator.platform should return.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetUserAgentOverride(this DevToolsHandling devTools,
            string userAgent, string acceptLanguage = null, string platform = null)
        {
            var settings = new SetUserAgentOverrideCommandSettings()
            {
                UserAgent = userAgent,
                AcceptLanguage = acceptLanguage,
                Platform = platform
            };
            await SetUserAgentOverride(devTools, settings);
        }

        /// <summary>
        /// Switches script execution in the page.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="value">Whether script execution should be disabled in the page.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetScriptExecutionDisabled(this DevToolsHandling devTools, bool value = true)
        {
            await devTools.SendCommand(new SetScriptExecutionDisabledCommandSettings { Value = value });
        }

        /// <summary>
        /// Enables touch on platforms which do not support them.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="enabled">Whether the touch event emulation should be enabled.</param>
        /// <param name="maxTouchPoints">Maximum touch points supported. Defaults to one.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetTouchEmulationEnabled(this DevToolsHandling devTools, bool enabled = true, long? maxTouchPoints = null)
        {
            await devTools.SendCommand(new SetTouchEmulationEnabledCommandSettings { Enabled = enabled, MaxTouchPoints = maxTouchPoints });
        }

        /// <summary>
        /// Enables touch on platforms which do not support them.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="commandParameters">Version-specific set of parameters. 
        /// For example, take a look at <see cref="SetTouchEmulationEnabledCommandSettings"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetTouchEmulationEnabled(this DevToolsHandling devTools, ICommand commandParameters)
        {
            GuardCommandParameters<SetTouchEmulationEnabledCommandSettings>(commandParameters);
            await devTools.SendCommand(commandParameters);
        }

        /// <summary>
        /// Emulates the given media type or media feature for CSS media queries.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="media">Media type to emulate. Empty string disables the override.
        /// Possible values: braille, embossed, handheld, print, projection, screen, speech, tty, tv.</param>
        /// <param name="mediaFeatures">Media features to emulate.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetEmulatedMedia(this DevToolsHandling devTools, string media, IDictionary<string, string> mediaFeatures = null)
        {
            var settings = new SetEmulatedMediaCommandSettings { Media = media };
            if (mediaFeatures != null)
            {
                settings.Features = mediaFeatures.Keys.Select(key => new MediaFeature { Name = key, Value = mediaFeatures[key] }).ToArray();
            }
            await devTools.SendCommand(settings);
        }

        /// <summary>
        /// Emulates the given media type or media feature for CSS media queries.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="commandParameters">Version-specific set of parameters. 
        /// For example, take a look at <see cref="SetEmulatedMediaCommandSettings"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetEmulatedMedia(this DevToolsHandling devTools, ICommand commandParameters)
        {
            GuardCommandParameters<SetEmulatedMediaCommandSettings>(commandParameters);
            await devTools.SendCommand(commandParameters);
        }

        /// <summary>
        /// Disables emulated media override.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task DisableEmulatedMediaOverride(this DevToolsHandling devTools)
        {
            var settings = new SetEmulatedMediaCommandSettings { Media = string.Empty };
            await SetEmulatedMedia(devTools, settings);
        }

        /// <summary>
        /// Sets an override of the default background color of the frame. This override is used if the content does not specify one.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="red">The red component, in the [0-255] range.</param>
        /// <param name="green">The green component, in the [0-255] range.</param>
        /// <param name="blue">The blue component, in the [0-255] range.</param>
        /// <param name="alpha">The alpha component, in the [0-1] range (default: 1).</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task SetDefaultBackgroundColorOverride(this DevToolsHandling devTools,
            long red, long green, long blue, double? alpha = null)
        {
            var settings = new SetDefaultBackgroundColorOverrideCommandSettings { Color = new RGBA { R = red, G = green, B = blue, A = alpha } };
            await devTools.SendCommand(settings);
        }

        /// <summary>
        /// Clears an override of the default background color of the frame.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task ClearDefaultBackgroundColorOverride(this DevToolsHandling devTools)
        {
            await devTools.SendCommand(new SetDefaultBackgroundColorOverrideCommandSettings());
        }

        private static void GuardCommandParameters<T>(ICommand commandParameters) where T : ICommand, new()
        {
            if (commandParameters == null || commandParameters.CommandName != new T().CommandName)
            {
                throw new ArgumentException("Command parameters are null or does not match to command name.", nameof(commandParameters));
            }
        }
    }
}

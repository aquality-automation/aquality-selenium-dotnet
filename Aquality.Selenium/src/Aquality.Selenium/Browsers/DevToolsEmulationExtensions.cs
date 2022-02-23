using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V85.Emulation;
using System;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Implementation of version-independent emulation DevTools commands as extensions for <see cref="DevToolsHandling"/>.
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
            return response["result"]?.ToString().Equals(bool.TrueString, StringComparison.OrdinalIgnoreCase) == true;
        }

        /// <summary>
        /// Overrides the GeoLocation Position or Error. Omitting any of the parameters emulates position unavailable.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="latitude">Latitude of GeoLocation.</param>
        /// <param name="longitude">Longitude of location.</param>
        /// <param name="accuracy">Accuracy of the geoLocation. By default is set to 1 meaning 100% accuracy.</param>
        public static async Task SetGeoLocationOverride(this DevToolsHandling devTools, double? latitude, double? longitude, double? accuracy = 1)
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
            await devTools.SendCommand(deviceModeSetting);
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
    }
}

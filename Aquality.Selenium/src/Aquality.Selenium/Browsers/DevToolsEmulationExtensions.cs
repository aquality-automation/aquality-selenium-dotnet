using Newtonsoft.Json.Linq;
using OpenQA.Selenium.DevTools.V85.Emulation;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Implementation of version-independent emulation DevTools commands as extensions for <see cref="DevToolsHandling"/>.
    /// </summary>
    public static class DevToolsEmulationExtensions
    {
        /// <summary>
        /// Overrides the GeoLocation Position or Error. Omitting any of the parameters emulates position unavailable.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/></param>
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
            var settingsJToken = JToken.FromObject(settings);
            await devTools.SendCommand(settings.CommandName, settingsJToken);
        }

        /// <summary>
        /// Clears the overridden GeoLocation Position and Error.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/></param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task ClearGeolocationOverride(this DevToolsHandling devTools)
        {
            await devTools.SendCommand(new ClearGeolocationOverrideCommandSettings().CommandName, new JObject());
        }
    }
}

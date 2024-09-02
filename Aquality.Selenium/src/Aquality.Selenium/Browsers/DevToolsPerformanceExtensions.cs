using OpenQA.Selenium.DevTools.V85.Performance;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Implementation of version-independent performance DevTools commands as extensions for <see cref="DevToolsHandling"/>.
    /// For more information, see <see href="https://chromedevtools.github.io/devtools-protocol/tot/Performance/"/>.
    /// </summary>
    public static class DevToolsPerformanceExtensions
    {
        /// <summary>
        /// Disable collecting and reporting metrics.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task DisablePerfomanceMonitoring(this DevToolsHandling devTools)
        {
            await devTools.SendCommand(new DisableCommandSettings());
        }

        /// <summary>
        /// Enable collecting and reporting metrics.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <param name="timeDomain">Time domain to use for collecting and reporting duration metrics.
        /// Allowed Values: timeTicks, threadTicks. </param>
        /// <returns>A task for asynchronous command.</returns>
        public static async Task EnablePerfomanceMonitoring(this DevToolsHandling devTools, string timeDomain = null)
        {
            await devTools.SendCommand(new EnableCommandSettings { TimeDomain = timeDomain });
        }

        /// <summary>
        /// Retrieve current values of run-time metrics.
        /// </summary>
        /// <param name="devTools">Current instance of <see cref="DevToolsHandling"/>.</param>
        /// <returns>A task for asynchronous command with current values for run-time metrics as result.</returns>
        public static async Task<IDictionary<string, double>> GetPerformanceMetrics(this DevToolsHandling devTools)
        {
            JsonNode result = await devTools.SendCommand(new GetMetricsCommandSettings());
            return (result["metrics"].AsArray())
                .ToDictionary(item => item["name"].ToString(), item => double.Parse(item["value"].ToString(), CultureInfo.InvariantCulture));
        }
    }
}

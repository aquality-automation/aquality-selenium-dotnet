using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration
{
    internal class DevToolsPerformanceTests : UITest
    {
        private static DevToolsHandling DevTools => AqualityServices.Browser.DevTools;

        [Test]
        public void Should_BePossibleTo_CollectPerformanceMetrics()
        {
            DevTools.EnablePerfomanceMonitoring().GetAwaiter().GetResult();

            AqualityServices.Browser.GoTo("http://www.google.com");
            IDictionary<string, double> metrics = DevTools.GetPerformanceMetrics().GetAwaiter().GetResult();
            Assert.That(metrics, Is.Not.Empty, "Some metrics should be returned");
            
            AqualityServices.Browser.Refresh();
            IDictionary<string, double> otherMetrics = DevTools.GetPerformanceMetrics().GetAwaiter().GetResult();
            Assert.That(otherMetrics, Is.Not.EqualTo(metrics), "Some additional metrics should have been collected");

            DevTools.DisablePerfomanceMonitoring().GetAwaiter().GetResult();
            AqualityServices.Browser.Refresh();
            metrics = DevTools.GetPerformanceMetrics().GetAwaiter().GetResult();
            Assert.That(metrics, Is.Empty, "Metrics should have not been collected after performance monitoring have been disabled");
        }
    }
}

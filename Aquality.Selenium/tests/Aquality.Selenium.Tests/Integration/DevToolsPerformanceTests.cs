﻿using Aquality.Selenium.Browsers;
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
            Assert.DoesNotThrowAsync(async () => await DevTools.EnablePerfomanceMonitoring(), "Should be possible to enable performance monitoring");

            AqualityServices.Browser.GoTo("http://www.google.com");
            IDictionary<string, double> metrics = null;
            Assert.DoesNotThrowAsync(async () => metrics = await DevTools.GetPerformanceMetrics(), "Should be possible to get performance metrics");
            CollectionAssert.IsNotEmpty(metrics, "Some metrics should be returned");
            
            AqualityServices.Browser.Refresh();
            IDictionary<string, double> otherMetrics = DevTools.GetPerformanceMetrics().GetAwaiter().GetResult();
            CollectionAssert.AreNotEqual(otherMetrics, metrics, "Some additional metrics should have been collected");

            Assert.DoesNotThrowAsync(async () => await DevTools.DisablePerfomanceMonitoring(), "Should be possible to disable performance monitoring");
            AqualityServices.Browser.Refresh();
            metrics = DevTools.GetPerformanceMetrics().GetAwaiter().GetResult();
            CollectionAssert.IsEmpty(metrics, "Metrics should have not been collected after performance monitoring have been disabled");
        }
    }
}

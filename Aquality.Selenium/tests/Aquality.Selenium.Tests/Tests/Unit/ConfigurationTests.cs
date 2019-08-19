using Aquality.Selenium.Configurations;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit.JavaScripts
{
    [Parallelizable(ParallelScope.All)]
    internal class ConfigurationTests
    {
        [Test]
        [Parallelizable(ParallelScope.None)]
        public void Should_GetConfiguration_FromCustomConfigurationProfile()
        {
            Environment.SetEnvironmentVariable("profile", "local");
            Assert.IsNotNull(Configuration.Instance);
            Assert.IsFalse(Configuration.Instance.BrowserProfile.IsRemote);
        }

        [Test]
        public void Should_GetInstanceOfConfiguration()
        {
            Assert.IsNotNull(Configuration.Instance);
        }

        [Test]
        public void Should_GetInstanceOfTimeoutConfiguration()
        {
            Assert.IsNotNull(Configuration.Instance.TimeoutConfiguration);
        }

        [Test]
        public void Should_GetInstanceOfBrowserProfile()
        {
            Assert.IsNotNull(Configuration.Instance.BrowserProfile);
            Assert.IsNotNull(Configuration.Instance.BrowserProfile.BrowserName);
            Assert.IsNotNull(Configuration.Instance.BrowserProfile.DriverSettings);
        }
    }
}

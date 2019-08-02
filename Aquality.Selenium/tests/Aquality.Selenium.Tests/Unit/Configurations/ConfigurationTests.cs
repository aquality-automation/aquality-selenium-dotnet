using Aquality.Selenium.Configurations;
using Aquality.Selenium.Utilities;
using NUnit.Framework;
using System.IO;

namespace Aquality.Selenium.Tests.Unit.JavaScripts
{
    [Parallelizable(ParallelScope.All)]
    public class ConfigurationTests
    {
        private const string ConfigFile = "local.settings.json";
        
        [Test]
        public void Should_GetLocalConfigurationFile()
        {
            var fileObject = new JsonFile(new FileInfo(Path.Combine("Resources", ConfigFile))).GetObject<object>("$");
            Assert.IsNotNull(fileObject);
            Assert.IsNotEmpty(fileObject.ToString());
        }

        [Test]
        public void Should_ParseLocalConfigurationFile()
        {
            var settingsFile = new JsonFile(new FileInfo(Path.Combine("Resources", ConfigFile)));
            Assert.IsNotNull(new TimeoutConfiguration(settingsFile));
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

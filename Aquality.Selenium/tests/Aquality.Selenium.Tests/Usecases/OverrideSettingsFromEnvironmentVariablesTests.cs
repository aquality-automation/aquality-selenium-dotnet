using Aquality.Selenium.Configurations;
using Aquality.Selenium.Utilities;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Usecases
{
    [TestFixture]
    internal class OverrideSettingsFromEnvironmentVariablesTests
    {
        [Test]
        public void ShouldBe_PossibleTo_SetTimeoutValueFromEnvironment()
        {
            Environment.SetEnvironmentVariable("timeouts.timeoutCondition", "500");
            var timeoutConfiguration = new TimeoutConfiguration(new JsonFile("settings.json"));
            Assert.AreEqual(TimeSpan.FromSeconds(500), timeoutConfiguration.Condition);
        }

        [Test]
        public void ShouldThrow_ArgumentException_InCaseOfEnvVarIncorrectFormat()
        {
            Environment.SetEnvironmentVariable("timeouts.timeoutPollingInterval", "incorrect_env_var");
            Assert.Throws<ArgumentException>(() => new TimeoutConfiguration(new JsonFile("settings.json")));
        }
    }
}

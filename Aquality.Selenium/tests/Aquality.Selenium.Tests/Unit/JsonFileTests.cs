using Aquality.Selenium.Utilities;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    internal class JsonFileTests
    {
        [Test]
        public void GetValue_ShouldBe_PossibleTo_OverrideValueFromEnvVar()
        {
            Environment.SetEnvironmentVariable("timeouts.timeoutCondition", "500");
            var timeoutConfiguration = new JsonFile("settings.json");
            Assert.AreEqual(500, timeoutConfiguration.GetValue<int>(".timeouts.timeoutCondition"));
        }

        [Test]
        public void GetValue_ShouldThrow_ArgumentException_InCaseOfEnvVarIncorrectFormat()
        {
            Environment.SetEnvironmentVariable("timeouts.timeoutPollingInterval", "incorrect_env_var");
            Assert.Throws<ArgumentException>(() => new JsonFile("settings.json").GetValue<int>(".timeouts.timeoutPollingInterval"));
        }

        [Test]
        public void GetValue_ShouldThrow_ArgumentException_InCaseOfNotValidKey()
        {
            Assert.Throws<ArgumentException>(() => new JsonFile("settings.json").GetValue<int>(".timeouts.invalidKey"));
        }

        [Test]
        public void GetValueList_ShouldThrow_ArgumentException_InCaseOfEnvVarIncorrectFormat()
        {
            Environment.SetEnvironmentVariable("driverSettings.firefox.startArguments", "val1, valu2");
            Assert.Throws<ArgumentException>(() => new JsonFile("settings.json").GetValueList<int>(".driverSettings.firefox.startArguments"));
        }

        [Test]
        public void GetValueList_ShouldThrow_ArgumentException_InCaseOfNotValidKey()
        {
            Assert.Throws<ArgumentException>(() => new JsonFile("settings.json").GetValueList<string>(".driverSettings.args"));
        }
    }
}

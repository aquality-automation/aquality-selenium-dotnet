using System;
using System.IO;
using Aquality.Selenium.Logging;
using NLog.Targets;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Unit.Logging
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        private const string AddTargetLogFile = "AddTargetTestLog.log";
        private const string RemoveTargetLogFile = "RemoveTargetTestLog.log";
        private const string TestMessage = "test message";

        [SetUp]
        public void Setup()
        {
            File.Delete(AddTargetLogFile);
            File.Delete(RemoveTargetLogFile);
        }

        [Test]
        public void TestShouldBePossibleToAddTarget()
        {
            Logger.Instance.AddTarget(GetTarget(AddTargetLogFile)).Info(TestMessage);
            Assert.True(File.Exists(AddTargetLogFile),
                $"Target wasn't added. File '{AddTargetLogFile}' doesn't exist.");
            var log = File.ReadAllText(AddTargetLogFile).Trim();
            Assert.True(log.Equals(TestMessage),
                $"Target wasn't added. File doesn't contain message: '{TestMessage}'.");
        }

        [Test]
        public void TestShouldBePossibleToRemoveTarget()
        {
            var Target = GetTarget(RemoveTargetLogFile);
            Logger.Instance.AddTarget(Target).RemoveTarget(Target).Info(TestMessage);
            Assert.False(File.Exists(RemoveTargetLogFile),
                $"Target wasn't removed. File '{RemoveTargetLogFile}' exists.");
        }

        private static Target GetTarget(string filePath)
        {
            return new FileTarget
            {
                Name = Guid.NewGuid().ToString(),
                FileName = filePath,
                Layout = "${message}"
            };
        }
    }
}
using System;
using System.IO;
using Aquality.Selenium.Logging;
using NLog.Targets;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class LoggerTests
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
        public void Should_BePossibleTo_AddTarget()
        {
            Logger.Instance.AddTarget(GetTarget(AddTargetLogFile)).Info(TestMessage);
            Assert.True(File.Exists(AddTargetLogFile),
                $"Target wasn't added. File '{AddTargetLogFile}' doesn't exist.");
            var log = File.ReadAllText(AddTargetLogFile).Trim();
            Assert.True(log.Equals(TestMessage),
                $"Target wasn't added. File doesn't contain message: '{TestMessage}'.");
        }

        [Test]
        public void Should_BePossibleTo_RemoveTarget()
        {
            var target = GetTarget(RemoveTargetLogFile);
            Logger.Instance.AddTarget(target).RemoveTarget(target).Info(TestMessage);
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
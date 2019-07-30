using System;
using System.IO;
using Aquality.Selenium.Logging;
using NLog.Targets;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Logging
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class Tests
    {
        private const string AddAppenderLogFile = "AddAppenderTestLog.log";
        private const string RemoveAppenderLogFile = "RemoveAppenderTestLog.log";
        private const string TestMessage = "test message";

        [SetUp]
        public void Setup()
        {
            File.Delete(AddAppenderLogFile);
            File.Delete(RemoveAppenderLogFile);
        }

        [Test]
        public void TestShouldBePossibleToAddAppender()
        {
            Logger.Instance.AddTarget(GetTarget(AddAppenderLogFile)).Info(TestMessage);
            Assert.True(File.Exists(AddAppenderLogFile),
                $"Appender wasn't added. File '{AddAppenderLogFile}' doesn't exist.");
            var log = File.ReadAllText(AddAppenderLogFile).Trim();
            Assert.True(log.Equals(TestMessage),
                $"Appender wasn't added. File doesn't contain message: '{TestMessage}'.");
        }

        [Test]
        public void TestShouldBePossibleToRemoveAppender()
        {
            var appender = GetTarget(RemoveAppenderLogFile);
            Logger.Instance.AddTarget(appender).RemoveTarget(appender).Info(TestMessage);
            Assert.False(File.Exists(RemoveAppenderLogFile),
                $"Appender wasn't removed. File '{RemoveAppenderLogFile}' exists.");
        }

        private static TargetWithLayoutHeaderAndFooter GetTarget(string filePath)
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
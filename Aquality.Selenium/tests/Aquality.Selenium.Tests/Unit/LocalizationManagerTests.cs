using System;
using System.IO;
using System.Linq;
using Aquality.Selenium.Browsers;
using NUnit.Framework;
using Aquality.Selenium.Core.Localization;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class LocalizationManagerTests
    {
        private const string LocalizedNavigationMessage = "Navigate to url - 'test'";
        private const string LogPath = "../../../Log/log.log";
        private const string TestUrl = "test";
        private const string NavigationKey = "loc.browser.navigate";
        private static ILocalizedLogger LocalizedLogger => AqualityServices.Get<ILocalizedLogger>();
        private static ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();

        [Parallelizable(ParallelScope.None)]
        [TestCase(LogLevel.Info)]
        [TestCase(LogLevel.Debug)]
        [TestCase(LogLevel.Error)]
        [TestCase(LogLevel.Fatal)]
        [TestCase(LogLevel.Warn)]
        public void Should_BeAble_LogLocalizedMessage(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Info:
                    LocalizedLogger.Info(NavigationKey, TestUrl);
                    break;
                case LogLevel.Debug:
                    LocalizedLogger.Debug(NavigationKey, null, TestUrl);
                    break;
                case LogLevel.Error:
                    LocalizedLogger.Error(NavigationKey, TestUrl);
                    break;
                case LogLevel.Fatal:
                    LocalizedLogger.Fatal(NavigationKey, null, TestUrl);
                    break;
                case LogLevel.Warn:
                    LocalizedLogger.Warn(NavigationKey, TestUrl);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, "Cannot process log level");
            }

            var logMessage = File.ReadAllLines(LogPath).LastOrDefault();
            Assert.IsFalse(string.IsNullOrEmpty(logMessage), "Message should appear in log file");
            Assert.IsTrue(logMessage.Contains(LocalizedNavigationMessage),
                $"Message should be localized. Expected: {LocalizedNavigationMessage}, actual: {logMessage}");
        }

        [Test]
        public void Should_BeAble_ToLocalizeLoggerMessage()
        {
            var message = LocalizationManager.GetLocalizedMessage("loc.browser.navigate", "test");
            Assert.AreEqual(LocalizedNavigationMessage, message, "Message should be localized");
        }

        public enum LogLevel
        {
            Info,
            Debug,
            Error,
            Fatal,
            Warn
        }
    }
}

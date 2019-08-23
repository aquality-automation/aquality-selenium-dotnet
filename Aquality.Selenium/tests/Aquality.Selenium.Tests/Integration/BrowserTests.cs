using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using Aquality.Selenium.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class BrowserTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_StartBrowserAndNavigate()
        {
            var url = new WelcomeForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            Assert.AreEqual(browser.CurrentUrl, url);
        }

        [Test]
        public void Should_BePossibleTo_GetWebDriverInstance()
        {
            var url = new WelcomeForm().Url;
            var browser = BrowserManager.Browser;
            browser.Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(browser.Driver.Url, url);
        }

        [Test]
        public void Should_BePossibleTo_NavigateBackAndForward()
        {
            var firstNavigationUrl = new WelcomeForm().Url;
            var secondNavigationUrl = new CheckBoxesForm().Url;

            var browser = BrowserManager.Browser;
            browser.GoTo(firstNavigationUrl);
            Assert.AreEqual(browser.CurrentUrl, firstNavigationUrl);

            browser.GoTo(secondNavigationUrl);
            Assert.AreEqual(browser.CurrentUrl, secondNavigationUrl);

            browser.GoBack();
            Assert.AreEqual(browser.CurrentUrl, firstNavigationUrl);

            browser.GoForward();
            Assert.AreEqual(browser.CurrentUrl, secondNavigationUrl);
        }

        [Test]
        public void Should_BePossibleTo_OpenNewBrowserAfterQuit()
        {
            var url = new WelcomeForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.Quit();

            Assert.AreNotEqual(url, BrowserManager.Browser.CurrentUrl);
        }

        [Test]
        public void Should_BePossibleTo_RefreshPage()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            var dynamicContentForm = new DynamicContentForm();
            var firstItem = dynamicContentForm.GetContentItem(1).GetText();

            browser.Refresh();
            browser.WaitForPageToLoad();
            var firstItemAfterRefresh = dynamicContentForm.GetContentItem(1).GetText();
            Assert.AreNotEqual(firstItem, firstItemAfterRefresh);
        }

        [Ignore("should be updated")]
        [Test]
        public void Should_BePossibleTo_SetPageLoadTimeout()
        {
            var browser = BrowserManager.Browser;
            browser.PageLoadTimeout = TimeSpan.FromSeconds(1);
            Assert.Throws<WebDriverTimeoutException>(() => browser.GoTo("https://github.com/aquality-automation"));
        }

        [Test]
        public void Should_BePossibleTo_TakeScreenshot()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.WaitForPageToLoad();
            Assert.IsTrue(browser.GetScreenshot().Length > 0);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScript()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.WaitForPageToLoad();
            var currentUrl = browser.ExecuteScript<string>("return window.location.href");
            Assert.AreEqual(url, currentUrl);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteAsyncJavaScript()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.WaitForPageToLoad();
            var expectedDurationInSeconds = 1;
            var operationDurationInSeconds = 1;

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            browser.ExecuteAsyncScript(GetAsyncTimeoutJavaScript(expectedDurationInSeconds));
            stopwatch.Stop();
            var durationSeconds = stopwatch.Elapsed.TotalSeconds;

            Assert.True(durationSeconds < (expectedDurationInSeconds + operationDurationInSeconds) &&
                    durationSeconds >= expectedDurationInSeconds);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteAsyncJavaScript_WithScriptTimeoutException()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.WaitForPageToLoad();

            var expectedDurationInSeconds = Configuration.Instance.TimeoutConfiguration.Script.TotalSeconds + 1;            
            Assert.Throws<WebDriverTimeoutException>(() => browser.ExecuteAsyncScript(GetAsyncTimeoutJavaScript(expectedDurationInSeconds)));
        }

        private string GetAsyncTimeoutJavaScript(double expectedDurationInSeconds)
        {
            return "window.setTimeout(arguments[arguments.length - 1], " + expectedDurationInSeconds * 1000 + ");";
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromFile()
        {
            var url = new DynamicContentForm().Url;
            var browser = BrowserManager.Browser;
            browser.GoTo(url);
            browser.WaitForPageToLoad();
            var currentUrl = browser.ExecuteScriptFromFile<string>("Resources.GetCurrentUrl.js");
            Assert.AreEqual(url, currentUrl);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromPredefinedFile()
        {
            var valueToSet = "username";
            var authForm = new AuthenticationForm();
            authForm.Open();
            BrowserManager.Browser.ExecuteScript(JavaScript.SetValue, authForm.UserNameTextBox.GetElement(), valueToSet);
            Assert.AreEqual(valueToSet, authForm.UserNameTextBox.Value);
        }

        [Ignore("should be updated")]
        [Test]
        public void Should_BePossibleTo_SetWindowSize()
        {
            var defaultSize = new Size(1024, 768);
            var browser = BrowserManager.Browser;
            var initialSize = browser.Driver.Manage().Window.Size;

            var testSize = new Size(600, 600);
            browser.SetWindowSize(testSize.Width, testSize.Height);

            var currentSize = browser.Driver.Manage().Window.Size;
            Assert.Multiple(() => 
            {
                Assert.IsTrue(currentSize.Height < initialSize.Height);
                Assert.IsTrue(currentSize.Width < initialSize.Width);
                Assert.IsTrue(currentSize.Width >= testSize.Width);
            });

            browser.Maximize();
            currentSize = browser.Driver.Manage().Window.Size;
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(currentSize, testSize);
                Assert.IsTrue(currentSize.Height > testSize.Height);
                Assert.IsTrue(currentSize.Width > testSize.Width);
            });

            browser.SetWindowSize(defaultSize.Width, defaultSize.Height);
            Assert.AreEqual(browser.Driver.Manage().Window.Size, defaultSize);
        }

        [Test]
        public void Should_BePossibleTo_ScrollWindowBy()
        {
            var browser = BrowserManager.Browser;
            browser.GoTo(Constants.UrlAutomationPractice);
            var sliderForm = new SliderForm();
            var initialY = sliderForm.FormPointInViewPort.Y;
            var formHeight = sliderForm.Size.Height;
            browser.ScrollWindowBy(0, formHeight);
            Assert.AreEqual(formHeight, initialY - sliderForm.FormPointInViewPort.Y);
        }

        [Test]
        public void Should_BePossibleTo_GetBrowserName()
        {
            var settingsFile = new JsonFile("settings.json");
            var browserName = (BrowserName)Enum.Parse(typeof(BrowserName), settingsFile.GetValue<string>(".browserName"), ignoreCase: true);
            Assert.AreEqual(browserName, BrowserManager.Browser.BrowserName);
        }

        [Test]
        public void Should_BePossibleTo_SetImplicitWait()
        {
            var browser = BrowserManager.Browser;
            browser.GoTo(new WelcomeForm().Url);
            var waitTime = TimeSpan.FromSeconds(5);
            browser.ImplicitWaitTimeout = waitTime;

            var stopwatch = Stopwatch.StartNew();
            var elapsedTime = TimeSpan.Zero;
            try
            {
                browser.Driver.FindElement(By.Id("not_exist_element"));
            } 
            catch (NoSuchElementException)
            {
                elapsedTime = stopwatch.Elapsed;
            }
            Assert.Multiple(() =>
            {
                Assert.IsTrue(elapsedTime < waitTime + TimeSpan.FromSeconds(2));
                Assert.IsTrue(elapsedTime >= waitTime);
            });
        }

        [Test]
        public void Should_BePossibleTo_GetDownloadDir()
        {
            var listOfDownloadDirs = new List<string>
            {
                "//home//selenium//downloads",
                "/Users/username/Downloads",
                "target//downloads",
                "/home/circleci/repo/target/downloads"
            };
            var downloadDir = BrowserManager.Browser.DownloadDirectory;
            Assert.IsTrue(listOfDownloadDirs.Any(dir => downloadDir.ToLower().Contains(dir)));
        }
    }
}

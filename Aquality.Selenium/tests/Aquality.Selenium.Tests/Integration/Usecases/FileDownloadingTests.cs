using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using Aquality.Selenium.Waitings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class FileDownloadingTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_DownloadTextFile()
        {
            var browser = BrowserManager.Browser;
            var downloaderForm = new FileDownloaderForm();
            var fileName = downloaderForm.FileName;
            var filePath = FileUtil.GetTargetFilePath(fileName);
            var file = new FileInfo(filePath);

            if (!Directory.Exists(browser.DownloadDirectory) && !Configuration.Instance.BrowserProfile.IsRemote)
            {
                Directory.CreateDirectory(browser.DownloadDirectory);
            }

            if (file.Exists)
            {
                file.Delete();
            }

            var lblFileContent = new ElementFactory().GetLabel(By.XPath("//pre"), "text file content");
            Assert.False(FileUtil.IsFileDownloaded(filePath, lblFileContent), "file should not exist before downloading");

            browser.ExecuteScriptFromFile("Resources.OpenUrlInNewWindow.js", TheInternetPage.Download);
            var tabs = new List<string>(BrowserManager.Browser.Driver.WindowHandles);

            browser.Driver.SwitchTo().Window(tabs[1]);
            browser.GoTo(TheInternetPage.Download);
            downloaderForm.GetDownloadLink(fileName).JsActions.ClickAndWait();
            browser.Driver.GetScreenshot().SaveAsFile("../../../Log/screen1.png");
            browser.Driver.SwitchTo().Window(tabs[0]);
            bool condition() => FileUtil.IsFileDownloaded(filePath, lblFileContent) || file.Exists;
            try
            {
                ConditionalWait.WaitForTrue(condition);
            }
            catch (TimeoutException)
            {
                browser.Driver.GetScreenshot().SaveAsFile("../../../Log/screen2.png");
                Logger.Instance.Debug(browser.Driver.PageSource);
                browser.Quit();
                Logger.Instance.Debug(lblFileContent.GetElement().Text);
            }
        }
    }
}

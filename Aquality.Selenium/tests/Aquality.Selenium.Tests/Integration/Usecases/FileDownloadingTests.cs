using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class FileDownloadingTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_DownloadTextFile()
        {
            var browser = AqualityServices.Browser;
            var downloaderForm = new FileDownloaderForm();
            var fileName = downloaderForm.FileName;
            var filePath = FileDownloadHelper.GetTargetFilePath(fileName);

            FileDownloadHelper.CreateDownloadDirectoryIfNotExist();
            FileDownloadHelper.DeleteFileIfExist(filePath);

            var lblFileContent = AqualityServices.Get<IElementFactory>().GetLabel(By.XPath("//pre"), "text file content");
            Assert.False(FileDownloadHelper.IsFileDownloaded(filePath, lblFileContent), $"file {filePath} should not exist before downloading");

            browser.ExecuteScriptFromFile("Resources.OpenUrlInNewWindow.js", downloaderForm.Url);
            var tabs = new List<string>(AqualityServices.Browser.Driver.WindowHandles);

            browser.Driver.SwitchTo().Window(tabs[1]);
            downloaderForm.Open();
            downloaderForm.GetDownloadLink(fileName).JsActions.ClickAndWait();
            browser.Driver.SwitchTo().Window(tabs[0]);
            bool condition() => FileDownloadHelper.IsFileDownloaded(filePath, lblFileContent);
            var message = $"file {filePath} was not downloaded";
            try
            {
                AqualityServices.ConditionalWait.WaitForTrue(condition, message: message);
            }
            catch (TimeoutException)
            {
                if(browser.BrowserName == BrowserName.Safari)
                {
                    browser.Quit();
                    AqualityServices.ConditionalWait.WaitForTrue(condition, message: message);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

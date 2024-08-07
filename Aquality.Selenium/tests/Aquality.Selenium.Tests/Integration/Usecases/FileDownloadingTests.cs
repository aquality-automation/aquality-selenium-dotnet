﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

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
            Assert.That(FileDownloadHelper.IsFileDownloaded(filePath, lblFileContent), Is.False, $"file {filePath} should not exist before downloading");

            var oldWindowHandle = browser.Tabs().CurrentHandle;
            browser.ExecuteScriptFromFile("Resources.OpenUrlInNewWindow.js", downloaderForm.Url);

            browser.Tabs().SwitchToLast();
            downloaderForm.Open();
            var downloadLink = downloaderForm.GetDownloadLink(fileName);
            if (downloadLink.State.IsDisplayed)
            {
                downloadLink.JsActions.ClickAndWait();
            }
            else
            {
                browser.GoTo(new Uri($"{downloaderForm.Url}/{fileName}"));
            }

            browser.Tabs().SwitchTo(oldWindowHandle);
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

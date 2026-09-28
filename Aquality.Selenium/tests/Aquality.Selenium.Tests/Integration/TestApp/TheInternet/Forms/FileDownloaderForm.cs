using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class FileDownloaderForm : TheInternetForm
    {
        private const int MaxAlertsCount = 100;
        private const string LinkTemplate = "//a[contains(@href,'{0}')]";

        public FileDownloaderForm() : base(By.Id("content"), "File Downloader")
        {
        }

        public string FileName => "some-file.txt";

        protected override string UrlPart => "download";

        public new void Open()
        {
            try
            {
                base.Open();
            }
            catch (UnhandledAlertException e)
            {
                Logger.Debug("Unhandled alert detected", e);
                for (int i = 0; i < MaxAlertsCount; i++)
                {
                    try
                    {
                        AqualityServices.Browser.HandleAlert(AlertAction.Accept);
                    }
                    catch (NoAlertPresentException)
                    {
                        Logger.Debug("No more alerts present");
                        break;
                    }
                }
                if (AqualityServices.Browser.CurrentUrl != Url)
                {
                    throw new NoSuchWindowException($"{Url} was not opened", e);
                }
                AqualityServices.Browser.WaitForPageToLoad();
            }

        }

        public ILink GetDownloadLink(string fileName)
        {
            return ElementFactory.GetLink(By.XPath(string.Format(LinkTemplate, fileName)), $"Download file {fileName}");
        }
    }
}

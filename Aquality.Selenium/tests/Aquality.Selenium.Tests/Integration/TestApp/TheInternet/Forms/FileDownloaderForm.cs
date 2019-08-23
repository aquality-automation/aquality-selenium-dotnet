using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class FileDownloaderForm : TheInternetForm
    {
        private const string LinkTemplate = "//a[contains(@href,'{0}')]";

        public FileDownloaderForm() : base(By.Id("content"), "File Downloader")
        {
        }

        public string FileName => "some-file.txt";

        protected override string UrlPart => "download";

        public ILink GetDownloadLink(string fileName)
        {
            return ElementFactory.GetLink(By.XPath(string.Format(LinkTemplate, fileName)), $"Download file {fileName}");
        }
    }
}

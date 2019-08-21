using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class FileDownloaderForm : Form
    {
        private const string LinkTemplate = "//a[contains(@href,'{0}')]";

        public FileDownloaderForm() : base(By.Id("content"), "File Downloader")
        {
        }

        public string FileName => "some-file.txt";

        public ILink GetDownloadLink(string fileName)
        {
            return ElementFactory.GetLink(By.XPath(string.Format(LinkTemplate, fileName)), $"Download file {fileName}");
        }
    }
}

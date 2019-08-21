using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal static class FileUtil
    {
        public static bool IsFileDownloaded(string filePath, ILabel lblFileContent)
        {
            try
            {
                BrowserManager.Browser.GoTo($"file://{filePath}");
                return lblFileContent.State.IsDisplayed;
            }
            catch (WebDriverException exception)
            {
                Logger.Instance.Warn(exception.Message);
                return false;
            }
        }

        public static string GetTargetFilePath(string fileName)
        {
            var downloadDirectory = BrowserManager.Browser.DownloadDirectory;

            // below is workaround for case when local FS is different from remote (e.g. local machine runs on Windows but remote runs on Linux)
            if (downloadDirectory.Contains("/") && !downloadDirectory.EndsWith("/"))
            {
                downloadDirectory += '/';
            }
            if (downloadDirectory.Contains("\\") && !downloadDirectory.EndsWith("\\"))
            {
                downloadDirectory += '\\';
            }
            return downloadDirectory + fileName;
        }
    }
}

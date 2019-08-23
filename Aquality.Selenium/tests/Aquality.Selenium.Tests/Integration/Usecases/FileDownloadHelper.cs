using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using System.IO;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal static class FileDownloadHelper
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

        public static void CreateDownloadDirectoryIfNotExist()
        {
            var browserProfile = Configuration.Instance.BrowserProfile;
            var downloadDir = browserProfile.DriverSettings.DownloadDir;
            if (!Directory.Exists(downloadDir) && !browserProfile.IsRemote)
            {
                Directory.CreateDirectory(downloadDir);
            }
        }

        public static void DeleteFileIfExist(string filePath)
        {
            var file = new FileInfo(filePath);
            if (file.Exists && !Configuration.Instance.BrowserProfile.IsRemote)
            {
                file.Delete();
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

using System;
using Aquality.Selenium.Localization;

namespace Aquality.Selenium.Logging
{
    internal static class LoggerExtension
    {
        public static void InfoLoc(this Logger logger, string messageKey, params string[] args)
        {
            logger.Info(LocalizationManager.Instance.GetLocalizedMessage(messageKey, args));
        }

        public static void DebugLoc(this Logger logger, string messageKey, Exception exception = null, params string[] args)
        {
            logger.Debug(LocalizationManager.Instance.GetLocalizedMessage(messageKey, args), exception);
        }

        public static void WarnLoc(this Logger logger, string messageKey, params string[] args)
        {
            logger.Warn(LocalizationManager.Instance.GetLocalizedMessage(messageKey, args));
        }

        public static void ErrorLoc(this Logger logger, string messageKey, params string[] args)
        {
            logger.Error(LocalizationManager.Instance.GetLocalizedMessage(messageKey, args));
        }

        public static void FatalLoc(this Logger logger, string messageKey, Exception exception = null, params string[] args)
        {
            logger.Fatal(LocalizationManager.Instance.GetLocalizedMessage(messageKey, args), exception);
        }
    }
}

using System;
using Aquality.Selenium.Core.Logging;
using Aquality.Selenium.Localization;

namespace Aquality.Selenium.Logging
{
    internal static class LoggerExtension
    {
        public static void InfoLocElementAction(this Logger logger, string elementType, string elementName, string messageKey, params object[] args)
        {
            logger.Info($"{elementType} '{elementName}' :: {GetLocalizedMessage(messageKey, args)}");
        }

        public static void InfoLoc(this Logger logger, string messageKey, params object[] args)
        {
            logger.Info(GetLocalizedMessage(messageKey, args));
        }

        public static void DebugLoc(this Logger logger, string messageKey, Exception exception = null, params object[] args)
        {
            logger.Debug(GetLocalizedMessage(messageKey, args), exception);
        }

        public static void WarnLoc(this Logger logger, string messageKey, params object[] args)
        {
            logger.Warn(GetLocalizedMessage(messageKey, args));
        }

        public static void ErrorLoc(this Logger logger, string messageKey, params object[] args)
        {
            logger.Error(GetLocalizedMessage(messageKey, args));
        }

        public static void FatalLoc(this Logger logger, string messageKey, Exception exception = null, params object[] args)
        {
            logger.Fatal(GetLocalizedMessage(messageKey, args), exception);
        }

        private static string GetLocalizedMessage(string messageKey, params object[] args)
        {
            return LocalizationManager.Instance.GetLocalizedMessage(messageKey, args);
        }
    }
}

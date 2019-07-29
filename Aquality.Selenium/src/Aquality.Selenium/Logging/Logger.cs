using System;

namespace Aquality.Selenium.Logging
{
    public sealed class Logger
    {
        internal void InfoLoc(string messageKey, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        internal string GetLoc(string messageKey)
        {
            throw new NotImplementedException();
        }

        public static Logger Instance => throw new NotImplementedException();
    }
}

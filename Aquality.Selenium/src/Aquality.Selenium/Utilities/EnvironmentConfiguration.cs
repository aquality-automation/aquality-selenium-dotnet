using System;

namespace Aquality.Selenium.Utilities
{
    internal static class EnvironmentConfiguration
    {
        public static string GetVariable(string key) => Environment.GetEnvironmentVariable(key);
    }
}

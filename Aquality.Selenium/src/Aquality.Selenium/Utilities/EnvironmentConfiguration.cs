using System;

namespace Aquality.Selenium.Utilities
{
    /// <summary>
    /// Environment variables reader.
    /// </summary>
    internal static class EnvironmentConfiguration
    {
        /// <summary>
        /// Gets value of environment variable by key.
        /// </summary>
        /// <param name="key">Environment variable key.</param>
        /// <returns>Value of environment variable.</returns>
        public static string GetVariable(string key) => Environment.GetEnvironmentVariable(key);
    }
}

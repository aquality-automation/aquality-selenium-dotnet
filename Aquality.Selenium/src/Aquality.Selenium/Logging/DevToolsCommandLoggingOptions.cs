namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// DevTools Command/Result logging options.
    /// </summary>
    public class DevToolsCommandLoggingOptions
    {
        /// <summary>
        /// Controls logging of command info: name and parameters (if any).
        /// </summary>
        public LoggingParameters Command { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Info };

        /// <summary>
        /// Controls logging of command result (if present).
        /// </summary>
        public LoggingParameters Result { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Info };        
    }
}

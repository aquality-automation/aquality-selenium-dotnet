namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// HTTP Request/Response logging options.
    /// </summary>
    public class HttpExchangeLoggingOptions
    {
        /// <summary>
        /// Controls logging of general request info: Method, URL, Request ID.
        /// </summary>
        public LoggingParameters RequestInfo { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Info };
        
        /// <summary>
        /// Controls logging of request headers.
        /// </summary>
        public LoggingParameters RequestHeaders { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Debug };
        
        /// <summary>
        /// Controls logging of request POST data.
        /// </summary>
        public LoggingParameters RequestPostData { get; set; } = new LoggingParameters { Enabled = false, LogLevel = LogLevel.Debug };

        /// <summary>
        /// Controls logging of general response info: Status code, URL, Resource type, Request ID.
        /// </summary>
        public LoggingParameters ResponseInfo { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Info };

        /// <summary>
        /// Controls logging of response headers.
        /// </summary>
        public LoggingParameters ResponseHeaders { get; set; } = new LoggingParameters { Enabled = true, LogLevel = LogLevel.Debug };

        /// <summary>
        /// Controls logging of response body.
        /// </summary>
        public LoggingParameters ResponseBody { get; set; } = new LoggingParameters { Enabled = false, LogLevel = LogLevel.Debug };
    }
}

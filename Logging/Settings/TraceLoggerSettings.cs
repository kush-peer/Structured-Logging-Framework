namespace Logging.Settings
{
    public class TraceLoggerSettings
    {
        public string MinLogLevel { get; set; }
        public bool EnableFileLogging { get; set; }
        public string LogFilePath { get; set; }
        public int MaxLogItemBufferSize { get; set; }
        public bool EnableConsoleLogging { get; set; }
        public bool EnableApplicationInsightsLogging { get; set; }
        public string ApplicationInsightsKey { get; set; }
    }
}

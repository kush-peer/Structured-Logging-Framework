using Microsoft.ApplicationInsights;
using Logging.Settings;

namespace Logging
{
    public class LoggerManager : ILoggerManager
    {
        private ITraceLogger _defaultTraceLogger;
        private ITelemetryLogger _defaultTelemetryLogger;

        public LoggerManager()
        {
            _defaultTraceLogger = new NullTraceLogger();
            _defaultTelemetryLogger = new NullTelemetryLogger();
        }

        public LoggerManager(TraceLoggerSettings traceSettings, TelemetryLoggerSettings telemetrySettings, TelemetryClient telemetryClient = null, string version = "")
        {
            if (traceSettings != null)
            {
                if (CheckForEnabledTraceLogger(traceSettings))
                    _defaultTraceLogger = new NullTraceLogger();
                else
                    _defaultTraceLogger = new SerilogTraceLogger(traceSettings, version);
            }
            else
            {
                _defaultTraceLogger = new NullTraceLogger();
            }

            if (telemetrySettings != null)
            {
               _defaultTelemetryLogger = new NullTelemetryLogger();
            }

        }

        private bool CheckForEnabledTraceLogger(TraceLoggerSettings traceSettings)
        {
            return traceSettings.EnableApplicationInsightsLogging == false
                   && traceSettings.EnableConsoleLogging == false
                   && traceSettings.EnableFileLogging == false;
        }

        public ITelemetryLogger GetDefaultTelemetryLogger()
        {
            return _defaultTelemetryLogger;
        }

        public ITraceLogger GetDefaultTraceLogger<T>()
        {
            _defaultTraceLogger.SetCategory<T>();
            return _defaultTraceLogger;

        }

        public ITraceLogger GetDefaultTraceLogger(string category)
        {
            _defaultTraceLogger.SetCategory(category);
            return _defaultTraceLogger;

        }

        public void SetDefaultTraceLogger(ITraceLogger logger)
        {
            _defaultTraceLogger = logger;
        }

        public void SetDefaultTelemetryLogger(ITelemetryLogger logger)
        {
            _defaultTelemetryLogger = logger;
        }
    }
}

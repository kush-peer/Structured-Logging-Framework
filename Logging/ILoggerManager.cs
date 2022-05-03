

namespace Logging
{
    public interface ILoggerManager
    {
        ITelemetryLogger GetDefaultTelemetryLogger();
        ITraceLogger GetDefaultTraceLogger<T>();
        ITraceLogger GetDefaultTraceLogger(string category);
        void SetDefaultTraceLogger(ITraceLogger logger);
        void SetDefaultTelemetryLogger(ITelemetryLogger logger);
    }
}

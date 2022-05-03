using System;
using System.Collections.Generic;
using System.Text;

namespace Logging.Extensions
{
    public static class GetContext
    {
        public static TraceContext Trace(ITraceLogger logger = null) { return new TraceContext(logger); }
        public static TelemetryContext Telemetry(ITelemetryLogger logger = null) { return new TelemetryContext(logger); }
    }
}

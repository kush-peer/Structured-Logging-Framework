using System;

namespace Logging.ValueObjects
{
    public class TelemetryTrackRequest : TelemetryBase
    {
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Span { get; set; }
        public string ResponseCode { get; set; }
        public bool Success { get; set; }

    }
}

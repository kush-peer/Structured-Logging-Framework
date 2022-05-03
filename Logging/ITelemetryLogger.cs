
using Logging.ValueObjects;

namespace Logging
{
    public interface ITelemetryLogger
    {
        void TrackEvent(TelemetryEvent telemetryEvent);
        void TrackDependency(TelemetryDependency telemetryDependency);
        void TrackMetric(TelemetryMetric telemetryMetric);
        void TrackRequest(TelemetryTrackRequest telemetryTrackRequest);
    }
}

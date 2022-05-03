
using Logging.ValueObjects;

namespace Logging
{
    public class NullTelemetryLogger : ITelemetryLogger
    {
        public void TrackEvent(TelemetryEvent telemetryEvent)
        {

        }

        public void TrackDependency(TelemetryDependency telemetryDependency)
        {

        }

        public void TrackMetric(TelemetryMetric telemetryMetric)
        {

        }

        public void TrackRequest(TelemetryTrackRequest telemetryTrackRequest)
        {

        }
    }
}

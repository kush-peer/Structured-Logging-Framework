
using System.Collections.Generic;

namespace Logging.ValueObjects
{
    public class TelemetryEvent : TelemetryMetricBase
    {
        public ICollection<TelemetryMetric> Metrics { get; set; }

    }
}

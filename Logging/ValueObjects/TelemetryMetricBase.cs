
using System.Collections.Generic;


namespace Logging.ValueObjects
{
    public abstract class TelemetryMetricBase : TelemetryBase
    {
        public IDictionary<string, string> Properties { get; set; }

    }
}

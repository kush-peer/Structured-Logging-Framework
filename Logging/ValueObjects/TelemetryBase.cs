using System.Collections.Generic;

namespace Logging.ValueObjects
{
    public abstract class TelemetryBase
    {
        public string CorrelationId { get; set; }
        public string Name { get; set; }
        public string Namespace { get; set; }
        public ICollection<string> Labels { get; set; }
    }
}

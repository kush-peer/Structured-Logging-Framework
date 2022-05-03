using System;
using System.Collections.Generic;

namespace Logging.ValueObjects
{
    public class TelemetryDependency
    {
        public string CorrelationId { get; set; }
        public string DependencyName { get; set; }
        public string CommandName { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan Span { get; set; }
        public bool Success { get; set; }
        public string Namespace { get; set; }
        public ICollection<string> Labels { get; set; }
    }
}

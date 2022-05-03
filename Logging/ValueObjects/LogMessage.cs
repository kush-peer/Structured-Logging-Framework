using System.Collections.Generic;

namespace Logging.ValueObjects
{
    public class LogMessage
    {
        public ITraceLogger TraceLogger { get; set; }

        public LogMessage(ITraceLogger logger = null)
        {
            TraceLogger = logger;
        }

        public string Message { get; set; }
        public string CorrelationId { get; set; }
        public string WorkflowItem { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}

using System;

namespace Logging.ValueObjects
{
    public class LogException : LogMessage
    {
        public LogException(ITraceLogger logger = null) : base(logger)
        {
        }
        public Exception Exception { get; set; }
    }
}

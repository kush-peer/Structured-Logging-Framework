using System;
using System.Collections.Generic;
using Logging.ValueObjects;

namespace Logging
{
    public class TraceContext
    {
        private readonly IDictionary<string, string> _properties;
        private string _message;
        private string _correlationId;
        private string _userId;
        private string _workflowItem;
        private string _clientId;

        private ITraceLogger _traceLogger;

        public TraceContext() : this(null)
        {
        }

        public TraceContext(ITraceLogger logger)
        {
            _properties = new Dictionary<string, string>();
            _traceLogger = logger;
        }

        public LogMessage GetLogMessage()
        {
            var logMessage = new LogMessage(_traceLogger)
            {
                Message = _message,
                ClientId = _clientId,
                CorrelationId = _correlationId,
                WorkflowItem = _workflowItem,
                UserId = _userId
            };

            foreach (var property in _properties)
            {
                logMessage.Properties.Add(property.Key, property.Value);
            }

            return logMessage;
        }

        public LogException GetLogException(Exception ex = null)
        {
            var logException = new LogException(_traceLogger)
            {
                Message = _message,
                Exception = ex,
                ClientId = _clientId,
                CorrelationId = _correlationId,
                WorkflowItem = _workflowItem,
                UserId = _userId
            };

            foreach (var property in _properties)
            {
                logException.Properties.Add(property.Key, property.Value);
            }

            return logException;
        }

        public TraceContext Message(string message)
        {
            _message = message;
            return this;
        }
        public TraceContext CorrelationId(string correlationId)
        {
            _correlationId = correlationId;
            return this;
        }

        public TraceContext SecurityEvent()
        {
            _properties.Add("SecurityEvent", true.ToString());
            return this;
        }

        public TraceContext SourceIp(string sourceIp)
        {
            if (!_properties.ContainsKey("SourceIp"))
                _properties.Add("SourceIp", sourceIp);

            return this;
        }

        public TraceContext ClientId(string clientId)
        {
            _clientId = clientId;
            return this;
        }

        public TraceContext UserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public TraceContext DeviceId(string workflowId)
        {
            _workflowItem = workflowId;
            return this;
        }

        public TraceContext AddProperty(string propKey, string propValue)
        {
            if (!_properties.ContainsKey(propKey))
                _properties.Add(propKey, propValue);

            return this;
        }
    }
}

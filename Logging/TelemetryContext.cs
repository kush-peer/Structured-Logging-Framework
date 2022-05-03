using System;
using System.Collections.Generic;
using System.Linq;
using Logging.ValueObjects;

namespace Logging
{
    public class TelemetryContext
    {
        private string _correlationId;
        private ITelemetryLogger _logger;
        private readonly IList<string> _names;
        private readonly IDictionary<string, string> _properties;
        private readonly IList<TelemetryMetric> _metrics;
        private readonly IList<string> _labels;

        public TelemetryContext() : this(null)
        {

        }

        public TelemetryContext(ITelemetryLogger logger)
        {
            _properties = new Dictionary<string, string>();
            _labels = new List<string>();
            _metrics = new List<TelemetryMetric>();
            _names = new List<string>();
            _logger = logger;
        }

        public List<TelemetryEvent> CreateTelemetryEvent()
        {
            return _names.Select(t => new TelemetryEvent()
            {
                CorrelationId = _correlationId,
                Labels = _labels,
                Metrics = _metrics,
                Properties = _properties,
                Name = t
            }).ToList();
        }

        public TelemetryContext CorrelationId(string correlationId)
        {
            _correlationId = correlationId;

            return this;
        }

        public TelemetryContext Name(string name)
        {
            _names.Add(name);

            return this;
        }

        public TelemetryContext Names(params string[] name)
        {
            ((List<string>)_names).AddRange(name);

            return this;
        }

        public TelemetryContext AddProperty(string propKey, string propValue)
        {
            if (!_properties.ContainsKey(propKey))
                _properties.Add(propKey, propValue);

            return this;
        }

        public TelemetryContext AddMetric(TelemetryMetric metric)
        {
            _metrics.Add(metric);

            return this;
        }

        [Obsolete("Use AddLabel(string label)")]
        public TelemetryContext AddMLabel(string label)
        {
            _labels.Add(label);

            return this;
        }

        public TelemetryContext AddLabel(string label)
        {
            _labels.Add(label);

            return this;
        }

        public TelemetryContext AddLabels(params string[] labels)
        {
            ((List<string>)_labels).AddRange(labels);

            return this;
        }    
    }
}

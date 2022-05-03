using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logging.Extensions;
using Logging.ValueObjects;

namespace Logging
{
    public class ApplicationInsightsTelemetryLogger : ITelemetryLogger
    {
        private readonly TelemetryClient _telemetryClient;

        private readonly string _version;

        //For instance, you may have one TelemetryClient instance in your  service to report incoming HTTP requests, 
        //and another in a middleware class to report business logic events. You can set properties such as TelemetryClient.Context.User.Id
        //to track users and sessions, or TelemetryClient.Context.Device.Id to identify the machine. This information is attached to all events 
        //that the instance sends.  https://docs.microsoft.com/en-us/azure/application-insights/app-insights-api-custom-events-metrics
        public ApplicationInsightsTelemetryLogger(TelemetryClient telemetryClient, string version = "")
        {
            Guard.ArgumentIsNotNull(telemetryClient, "telemetryClient can't be null");
            _telemetryClient = telemetryClient;
            _version = version;
        }

        public void TrackEvent(TelemetryEvent telemetryEvent)
        {
            if (telemetryEvent.Properties == null)
                telemetryEvent.Properties = new Dictionary<string, string>() { { "CorrelationId", telemetryEvent.CorrelationId } };
            else if (!telemetryEvent.Properties.ContainsKey("CorrelationId"))
                telemetryEvent.Properties.Add("CorrelationId", telemetryEvent.CorrelationId);

            if (telemetryEvent.Labels != null && telemetryEvent.Labels.Count > 0)
            {
                var indx = 1;
                foreach (var telemetryEventLabel in telemetryEvent.Labels)
                {
                    if (!telemetryEvent.Properties.ContainsKey("Label " + indx))
                        telemetryEvent.Properties.Add("Label " + indx, telemetryEventLabel);
                    indx++;
                }
            }

            if (telemetryEvent.Metrics == null) { telemetryEvent.Metrics = new List<TelemetryMetric>(); }

            _telemetryClient.TrackEvent(telemetryEvent.Name, telemetryEvent.Properties, telemetryEvent.Metrics.ToDictionary(a => a.Name, x => x.Value));
        }

        public void TrackDependency(TelemetryDependency telemetryDependency)
        {
            var trackDependency = new DependencyTelemetry()
            {
                Name = telemetryDependency.Namespace + telemetryDependency.DependencyName,
                Duration = telemetryDependency.Span,
                Success = telemetryDependency.Success,
                Timestamp = telemetryDependency.StartTime,
                Data = telemetryDependency.CommandName,

            };

            trackDependency.Properties.Add("CorrelationId", telemetryDependency.CorrelationId);
            if (telemetryDependency.Labels != null && telemetryDependency.Labels.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var telemetryDependencyLabel in telemetryDependency.Labels)
                {
                    sb.Append(telemetryDependencyLabel + ",");
                }

                trackDependency.Properties.Add("labels", sb.ToString());
            }

            _telemetryClient.TrackDependency(trackDependency);

        }

        public void TrackMetric(TelemetryMetric telemetryMetric)
        {
            if (telemetryMetric.Properties == null)
                telemetryMetric.Properties = new Dictionary<string, string>() { { "CorrelationId", telemetryMetric.CorrelationId } };
            else
                telemetryMetric.Properties.Add("CorrelationId", telemetryMetric.CorrelationId);

            if (telemetryMetric.Labels != null && telemetryMetric.Labels.Count > 0)
            {
                var indx = 1;
                foreach (var telemetryEventLabel in telemetryMetric.Labels)
                {
                    if (!telemetryMetric.Properties.ContainsKey("Label " + indx))
                        telemetryMetric.Properties.Add("Label " + indx, telemetryEventLabel);
                    indx++;
                }
            }

            //they obsoleted this call so som research might be needed
            _telemetryClient.TrackMetric(telemetryMetric.Name, telemetryMetric.Value, telemetryMetric.Properties);

        }

        public void TrackRequest(TelemetryTrackRequest telemetryTrackRequest)
        {
            var requestTelemetry = new RequestTelemetry()
            {
                Duration = telemetryTrackRequest.Span,
                Name = telemetryTrackRequest.Name,
                ResponseCode = telemetryTrackRequest.ResponseCode,
                Success = telemetryTrackRequest.Success,
                Timestamp = telemetryTrackRequest.StartTime,
            };

            requestTelemetry.Properties.Add("CorrelationId", telemetryTrackRequest.CorrelationId);
            if (telemetryTrackRequest.Labels != null && telemetryTrackRequest.Labels.Count > 0)
            {
                var sb = new StringBuilder();
                foreach (var telemetryDependencyLabel in telemetryTrackRequest.Labels)
                {
                    sb.Append(telemetryDependencyLabel + ",");
                }

                requestTelemetry.Properties.Add("labels", sb.ToString());
            }

            _telemetryClient.TrackRequest(telemetryTrackRequest.Name, telemetryTrackRequest.StartTime, telemetryTrackRequest.Span, telemetryTrackRequest.ResponseCode, telemetryTrackRequest.Success);

        }
    }
}

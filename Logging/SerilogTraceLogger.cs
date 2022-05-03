using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using Logging.Extensions;
using Logging.Settings;
using Logging.ValueObjects;

namespace Logging
{
    public class SerilogTraceLogger : ITraceLogger
    {
        protected ILogger Log;
        public const string MessageTemplate = "{category}:{message}, correlationId: {correlationId}, clientId: {clientId}, eventType: {eventType}, userId: {userId}, properties: {properties}";
        private string _category;
        private readonly string _version;

        public SerilogTraceLogger(TraceLoggerSettings settings, string version = "")
        {
            _version = version;
            Init(settings);
        }

        private void Init(TraceLoggerSettings settings)
        {
            Guard.ArgumentIsNotNull(settings, "TraceLoggerSettings can't be null");

            if (CheckForEnabledLogger(settings))
                throw new ArgumentException("SerilogTraceLogger::Constructor-> EnableFileLogging or EnableConsoleLogging or EnableApplicationInsights need to be set to true.");

            Guard.ArgumentIsNotNullOrEmpty(settings.MinLogLevel, "LogLevel is required.  Valid values are...  Debug|Info|Warn|Error|Fatal");

            var logConfig = new LoggerConfiguration();

            ////lets try to do this as fast as possible. https://github.com/serilog/serilog-sinks-async  
            ////Async will log with a worker thread instead of the application thread.
            //if (settings.EnableFileLogging)
            //{
            //    Guard.ArgumentIsNotNullOrEmpty(settings.LogFilePath, "LogFilePath is required when enabling File Logging");
            //    Guard.ArgumentIsGreaterThan(0, settings.MaxLogItemBufferSize, "MaxBufferSize is required when enabling File Logging and must be greater than 0");

            //    if (!settings.LogFilePath.EndsWith(@"\"))
            //    {
            //        settings.LogFilePath = settings.LogFilePath + @"\";
            //    }
                
            //    logConfig.WriteTo.Async(a =>
            //                settings.LogFilePath + "peer-log-{Date}.json",
            //                fileSizeLimitBytes: 1024 * 1024 * 100,
            //                retainedFileCountLimit: 100),
            //        settings.MaxLogItemBufferSize);
            //}
            if (settings.EnableConsoleLogging)
                logConfig.WriteTo.Async(a => a.Console(theme: ConsoleTheme.None));
            if (settings.EnableApplicationInsightsLogging)
            {
                Guard.ArgumentIsNotNullOrEmpty(settings.ApplicationInsightsKey, "ApplicationInsightsKey is required when enabling Application Insights Logging");

                logConfig.WriteTo.Async(a => a.ApplicationInsights(settings.ApplicationInsightsKey, TelemetryConverter.Traces));
            }
            if (settings.MinLogLevel.Equals("Debug", StringComparison.CurrentCultureIgnoreCase))
                logConfig.MinimumLevel.Debug();
            if (settings.MinLogLevel.Equals("Info", StringComparison.CurrentCultureIgnoreCase))
                logConfig.MinimumLevel.Information();
            if (settings.MinLogLevel.Equals("Warn", StringComparison.CurrentCultureIgnoreCase))
                logConfig.MinimumLevel.Warning();
            if (settings.MinLogLevel.Equals("Error", StringComparison.CurrentCultureIgnoreCase))
                logConfig.MinimumLevel.Error();
            if (settings.MinLogLevel.Equals("Fatal", StringComparison.CurrentCultureIgnoreCase))
                logConfig.MinimumLevel.Fatal();

            Log = logConfig.CreateLogger();
            if (!string.IsNullOrEmpty(_version))
                Log = Log.ForContext("version", _version);
        }

        private bool CheckForEnabledLogger(TraceLoggerSettings settings)
        {
            return !settings.EnableConsoleLogging
                   && !settings.EnableFileLogging
                   && !settings.EnableApplicationInsightsLogging;
        }

        public void SetCategory(string category)
        {
            _category = category;
        }

        public void SetCategory<T>()
        {
            _category = typeof(T).FullName;
        }

        public void Debug(LogMessage logMessage)
        {
            Log.Debug(MessageTemplate, _category, logMessage.Message, logMessage.CorrelationId, logMessage.ClientId, logMessage.WorkflowItem, logMessage.UserId, logMessage.Properties);
        }

        public void Info(LogMessage logMessage)
        {
            Log.Information(MessageTemplate, _category, logMessage.Message, logMessage.CorrelationId, logMessage.ClientId, logMessage.WorkflowItem, logMessage.UserId, logMessage.Properties);
        }

        public void Info(LogException logException)
        {
            Log.Information(logException.Exception, MessageTemplate, _category, logException.Message, logException.CorrelationId, logException.ClientId, logException.WorkflowItem, logException.UserId,  logException.Properties);
        }

        public void Warn(LogMessage logMessage)
        {
            Log.Warning(MessageTemplate, _category, logMessage.Message, logMessage.CorrelationId, logMessage.ClientId, logMessage.WorkflowItem, logMessage.UserId,  logMessage.Properties);
        }

        public void Warn(LogException logException)
        {
            Log.Warning(logException.Exception, MessageTemplate, _category, logException.Message, logException.CorrelationId, logException.ClientId, logException.WorkflowItem, logException.UserId,  logException.Properties);
        }

        public void Error(LogMessage logMessage)
        {
            Log.Error(MessageTemplate, _category, logMessage.Message, logMessage.CorrelationId, logMessage.ClientId, logMessage.WorkflowItem, logMessage.UserId,  logMessage.Properties);
        }

        public void Error(LogException logException)
        {
            Log.Error(logException.Exception, MessageTemplate, _category, logException.Message, logException.CorrelationId, logException.ClientId, logException.WorkflowItem, logException.UserId, logException.Properties);
        }

        public void Fatal(LogMessage logMessage)
        {
            Log.Fatal(MessageTemplate, _category, logMessage.Message, logMessage.CorrelationId, logMessage.ClientId, logMessage.WorkflowItem, logMessage.UserId, logMessage.Properties);
        }

        public void Fatal(LogException logException)
        {
            Log.Fatal(logException.Exception, MessageTemplate, _category, logException.Message, logException.CorrelationId, logException.ClientId, logException.WorkflowItem, logException.UserId, logException.Properties);
        }        
    }

}
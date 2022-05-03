
using Logging.ValueObjects;

namespace Logging
{
    public class NullTraceLogger : ITraceLogger
    {

        public string Key { get; set; }

        public void SetCategory(string category)
        {

        }

        public void SetCategory<T>()
        {

        }

        public void Debug(LogMessage logMessage)
        {

        }

        public void Info(LogMessage logMessage)
        {

        }

        public void Info(LogException logException)
        {

        }

        public void Warn(LogMessage logMessage)
        {

        }

        public void Warn(LogException logException)
        {

        }

        public void Error(LogMessage logMessage)
        {

        }

        public void Error(LogException logException)
        {

        }

        public void Fatal(LogMessage logMessage)
        {

        }

        public void Fatal(LogException logException)
        {

        }
    }
}

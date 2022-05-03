
using Logging.ValueObjects;

namespace Logging
{
    public interface ITraceLogger
    {
        void SetCategory(string category);
        void SetCategory<T>();
        void Debug(LogMessage logMessage);
        void Info(LogMessage logMessage);
        void Info(LogException logException);
        void Warn(LogMessage logMessage);
        void Warn(LogException logException);
        void Error(LogMessage logMessage);
        void Error(LogException logException);
        void Fatal(LogMessage logMessage);
        void Fatal(LogException logException);
    }
}

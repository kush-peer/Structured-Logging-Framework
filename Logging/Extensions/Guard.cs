using System;

namespace Logging.Extensions
{
    public static class Guard
    {
        public static void ArgumentIsNotNull(object value, string message)
        {
            if (value == null)
                throw new ArgumentException(message);
        }

        public static void ArgumentIsNotNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(message);
        }

        public static void ArgumentIsGreaterThan(int value, int argValue, string message)
        {
            if (value >= argValue)
                throw new ArgumentException(message);
        }
    }
}

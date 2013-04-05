using System;

namespace TradePlatform.MT4.Core.Exceptions
{
    public class ConsoleTraceListenerException : Exception
    {
        public ConsoleTraceListenerException(object value, string message)
            : base(string.Format("{0} [Passed Value={1}", value, message))
        {
        }
    }
}
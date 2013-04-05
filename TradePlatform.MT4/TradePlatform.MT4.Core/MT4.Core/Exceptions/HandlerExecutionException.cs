using System;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Exceptions
{
    internal class HandlerExecutionException : Exception
    {
        public HandlerExecutionException(ExpertInfo expertInfo, Exception innerException)
            : base(
                string.Format("Handler execution failed with error '{0}'. Failure Context: {1}", innerException.Message,
                              expertInfo), innerException)
        {
        }
    }
}
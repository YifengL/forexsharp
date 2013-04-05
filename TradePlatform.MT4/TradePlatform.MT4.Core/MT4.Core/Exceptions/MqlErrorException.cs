using System;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Exceptions
{
    public class MqlErrorException : Exception
    {
        public MqlErrorException(ExpertInfo expertInfo, MethodCallInfo clientMethodInfo)
            : base(
                string.Format("MQL returned error: '{0}' for method {1}. Failure Context: {2}",
                              clientMethodInfo.ErrorMessage, clientMethodInfo, expertInfo))
        {
        }
    }
}
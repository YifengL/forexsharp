using System;
using System.Collections.Generic;
using TradePlatform.MT4.Core;

namespace TradePlatform.MT4.SDK.Library.Handlers
{
    public abstract class PerformanceCounterBase : MqlHandler
    {
        protected abstract void Begin();
        protected abstract void End();

        protected override string ResolveMethod(string methodName, List<string> parameters)
        {
            switch (methodName)
            {
                case "Begin":
                    Begin();
                    return "";
                case "End":
                    End();
                    return "";
                default:
                    throw new Exception("No method found");
            }
        }
    }
}
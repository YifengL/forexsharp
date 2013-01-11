namespace TradePlatform.MT4.SDK.Library.Handlers
{
    using System.Collections.Generic;
    using TradePlatform.MT4.Core;

    public abstract class PerformanceCounterBase : MqlHandler
    {
        protected abstract void Begin();
        protected abstract void End();

        protected override string ResolveMethod(string methodName, List<string> parameters)
        {
            switch (methodName)
            {
                case "Begin":
                    this.Begin();
                    return "";
                case "End":
                    this.End();
                    return "";
                default:
                    throw new System.Exception("No method found");
            }
        }
    }
}

namespace TradePlatform.MT4.SDK.Library.Common
{
    using System;
    using System.Diagnostics;
    using TradePlatform.MT4.Core.Utils;
    using TradePlatform.MT4.SDK.Library.Handlers;

    public class TickCounter : PerformanceCounterBase
    {
        private DateTime _beginTime;
        private DateTime _endTime;

        protected override void Begin()
        {
            this._beginTime = DateTime.Now;
        }

        protected override void End()
        {
            this._endTime = DateTime.Now;

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Service, message: "Last tick time: " + (this._endTime - this._beginTime).TotalMilliseconds + " ms."));
        }
    }
}

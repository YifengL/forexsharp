using System;
using System.Diagnostics;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.Library.Handlers;

namespace TradePlatform.MT4.SDK.Library.Common
{
    public class TickCounter : PerformanceCounterBase
    {
        private DateTime _beginTime;
        private DateTime _endTime;

        protected override void Begin()
        {
            _beginTime = DateTime.Now;
        }

        protected override void End()
        {
            _endTime = DateTime.Now;

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Service,
                                      message: "Last tick time: " + (_endTime - _beginTime).TotalMilliseconds + " ms."));
        }
    }
}
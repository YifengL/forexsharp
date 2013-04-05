using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.Scripts
{
    public class SmokeTestScript : ExpertAdvisor
    {
        public SmokeTestScript()
        {
            MqlError += OnMqlError;
        }

        public int TestProperty { get; set; }

        private void OnMqlError(MqlErrorException mqlErrorException)
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "MQL Error: " + mqlErrorException.Message));
        }

        protected override int Init()
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Init()"));

            return 1;
        }

        protected override int Start()
        {
            double bid = this.Bid();
            double ask = this.Ask();

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Bid: " + bid));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Ask: " + ask));

            return 2;
        }

        protected override int DeInit()
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "DeInit()"));

            return 3;
        }
    }
}
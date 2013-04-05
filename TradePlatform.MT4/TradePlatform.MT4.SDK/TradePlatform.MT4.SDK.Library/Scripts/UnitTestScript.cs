using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.Library.UnitTests;

namespace TradePlatform.MT4.SDK.Library.Scripts
{
    public class UnitTestScript : ExpertAdvisor
    {
        public UnitTestScript()
        {
            MqlError += OnMqlError;
        }

        private void OnMqlError(MqlErrorException mqlErrorException)
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "MQL Error: " + mqlErrorException.Message));
        }

        protected override int Init()
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Init()"));

            // AccountInformationTests.RunTests(this);
            CommonFunctionsTests.RunTests(this);
            //PredefinedVariablesTests.RunTests(this);
            // TechnicalIndicatorsTests.RunTests(this);
//TradingFunctionsTests.RunTests(this);
            //  WindowFunctionsTests.RunTests(this);

            return 1;
        }

        protected override int Start()
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Start()"));

            return 2;
        }

        protected override int DeInit()
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "DeInit()"));

            return 3;
        }
    }
}
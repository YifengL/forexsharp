using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    public static class PredefinedVariablesTests
    {
        public static void RunTests(MqlHandler script)
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Ask: " + script.Ask() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Bars: " + script.Bars() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Bid: " + script.Bid() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Close: " + script.Close(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Digits: " + script.Digits() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'High: " + script.High(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Low: " + script.Low(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Open: " + script.Open(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Point: " + script.Point() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Time: " + script.Time(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Volume: " + script.Volume() + "'"));
        }
    }
}
using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    public static class WindowFunctionsTests
    {
        public static void RunTests(MqlHandler script)
        {
            script.HideTestIndicators(false);
            script.WindowRedraw();

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'Period: " + script.Period() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'RefreshRates: " + script.RefreshRates() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowBarsPerChart: " + script.WindowBarsPerChart() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowExpertName: " + script.WindowExpertName() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowFind: " + script.WindowFind("Test") + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowFirstVisibleBar: " + script.WindowFirstVisibleBar() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'WindowHandle: " +
                                          script.WindowHandle(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowIsVisible: " + script.WindowIsVisible(0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowOnDropped: " + script.WindowOnDropped() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowPriceMax: " + script.WindowPriceMax() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowPriceMin: " + script.WindowPriceMin() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowPriceOnDropped: " + script.WindowPriceOnDropped() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'WindowScreenShot: " + script.WindowScreenShot("test.bmp", 200, 200) +
                                          "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowTimeOnDropped: " + script.WindowTimeOnDropped() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowsTotal: " + script.WindowsTotal() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowXOnDropped: " + script.WindowXOnDropped() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'WindowYOnDropped: " + script.WindowYOnDropped() + "'"));
        }
    }
}
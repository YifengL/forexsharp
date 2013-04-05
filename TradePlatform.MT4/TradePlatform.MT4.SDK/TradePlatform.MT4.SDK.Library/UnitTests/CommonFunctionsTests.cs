using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    public static class CommonFunctionsTests
    {
        public static void RunTests(MqlHandler script)
        {
            script.Alert("Test Alert");
            script.Comment("Test Comment");

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'GetTickCount: " + script.GetTickCount() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'MarketInfo: " +
                                          script.MarketInfo(SYMBOLS.EURUSD, MARKER_INFO_MODE.MODE_POINT) + "'"));

            script.PlaySound("connect.wav");
            script.Print("Test Print");

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'SendNotification: " + script.SendNotification("Test Notification") +
                                          "'"));
        }
    }
}
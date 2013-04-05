using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    public static class AccountInformationTests
    {
        public static void RunTests(MqlHandler script)
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountBalance: " + script.AccountBalance() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountCredit: " + script.AccountCredit() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountCompany: " + script.AccountCompany() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountCurrency: " + script.AccountCurrency() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountEquity: " + script.AccountEquity() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountFreeMargin: " + script.AccountFreeMargin() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'AccountFreeMarginCheck: " +
                                          script.AccountFreeMarginCheck("EURUSD", ORDER_TYPE.OP_BUY, 1) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountFreeMarginMode: " + script.AccountFreeMarginMode() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountLeverage: " + script.AccountLeverage() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountMargin: " + script.AccountMargin() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountName: " + script.AccountName() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountNumber: " + script.AccountNumber() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountProfit: " + script.AccountProfit() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountServer: " + script.AccountServer() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountStopoutLevel: " + script.AccountStopoutLevel() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'AccountStopoutMode: " + script.AccountStopoutMode() + "'"));
        }
    }
}
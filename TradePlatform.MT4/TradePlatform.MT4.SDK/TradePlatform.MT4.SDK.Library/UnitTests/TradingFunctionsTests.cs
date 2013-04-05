using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;
using TradePlatform.MT4.SDK.API;

namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    public static class TradingFunctionsTests
    {
        public static void RunTests(MqlHandler script)
        {
            int ticket = script.OrderSend(SYMBOLS.EURUSD, ORDER_TYPE.OP_BUY, 0.1, script.Ask(), 3, 0, 0);

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'OrderSend: " + ticket + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'OrderSelect: " + script.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) +
                                          "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'OrderModify: " +
                                          script.OrderModify(ticket, script.OrderOpenPrice(), 0.1112, 2.3645) + "'"));

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderClosePrice: " + script.OrderClosePrice() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderCloseTime: " + script.OrderCloseTime() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderComment: " + script.OrderComment() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderCommission: " + script.OrderCommission() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderExpiration: " + script.OrderExpiration() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderLots: " + script.OrderLots() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderMagicNumber: " + script.OrderMagicNumber() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderOpenPrice: " + script.OrderOpenPrice() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderOpenTime: " + script.OrderOpenTime() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderProfit: " + script.OrderProfit() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderStopLoss: " + script.OrderStopLoss() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderSwap: " + script.OrderSwap() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderSymbol: " + script.OrderSymbol() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderTakeProfit: " + script.OrderTakeProfit() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderTicket: " + script.OrderTicket() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrderType: " + script.OrderType() + "'"));

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'OrdersTotal: " + script.OrdersTotal() + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message: "Test 'HistoryTotal: " + script.HistoryTotal() + "'"));

            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug,
                                      message:
                                          "Test 'OrderClose: " + script.OrderClose(ticket, 0.1, script.Bid(), 3) + "'"));

            // UNCOVERED BY TESTS : OrderPrint(), OrderCloseBy(...), OrderDelete(...) and Pending orders approach
        }
    }
}
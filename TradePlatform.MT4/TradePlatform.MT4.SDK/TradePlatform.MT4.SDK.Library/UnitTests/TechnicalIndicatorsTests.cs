namespace TradePlatform.MT4.SDK.Library.UnitTests
{
    using System.Diagnostics;
    using TradePlatform.MT4.Core;
    using TradePlatform.MT4.Core.Utils;
    using TradePlatform.MT4.SDK.API;

    public static class TechnicalIndicatorsTests
    {
        public static void RunTests(MqlHandler script)
        {
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iAC: " + script.iAC(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iAD: " + script.iAD(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iAlligator: " + script.iAlligator(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 5, 5, 5, 5, 5, 5, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, GATOR_MODE.MODE_GATORJAW, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iADX: " + script.iADX(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 9, APPLY_PRICE.PRICE_CLOSE, ADX_MODE.MODE_MAIN, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iATR: " + script.iATR(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 12, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iAO: " + script.iAO(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iBearsPower: " + script.iBearsPower(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 25, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iBands: " + script.iBands(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, 2, 2, APPLY_PRICE.PRICE_CLOSE, BAND_MODE.MODE_LOWER, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iBullsPower: " + script.iBullsPower(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 23, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iCCI: " + script.iCCI(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iDeMarker: " + script.iDeMarker(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iEnvelopes: " + script.iEnvelopes(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, MA_METHOD.MODE_EMA, 0, APPLY_PRICE.PRICE_CLOSE, 2, BAND_MODE.MODE_LOWER, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iForce: " + script.iForce(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 25, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iFractals: " + script.iFractals(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, BAND_MODE.MODE_LOWER, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iGator: " + script.iGator(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, 0, 23, 0, 21, 0, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, BAND_MODE.MODE_LOWER, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iIchimoku: " + script.iIchimoku(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 4, 2, 6,ICHIMOKU_MODE.MODE_CHINKOUSPAN, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iBWMFI: " + script.iBWMFI(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iMomentum: " + script.iMomentum(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iMFI: " + script.iMFI(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iMA: " + script.iMA(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 25, 0, MA_METHOD.MODE_EMA, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iOsMA: " + script.iOsMA(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 22, 43,23,APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iMACD: " + script.iMACD(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 23, 23, 23,APPLY_PRICE.PRICE_CLOSE,MACD_MODE.MODE_MAIN, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iOBV: " + script.iOBV(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1,APPLY_PRICE.PRICE_CLOSE,  0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iSAR: " + script.iSAR(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 2, 3, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iRSI: " + script.iRSI(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 24, APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iRVI: " + script.iRVI(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1, 25, MACD_MODE.MODE_MAIN, 0)));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iStdDev: " + script.iStdDev(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1,23, 0, MA_METHOD.MODE_EMA,APPLY_PRICE.PRICE_CLOSE, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iStochastic: " + script.iStochastic(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1,23, 12, 2, MA_METHOD.MODE_EMA,APPLY_PRICE.PRICE_CLOSE, MACD_MODE.MODE_MAIN, 0) + "'"));
            Trace.Write(new TraceInfo(BridgeTraceErrorType.Debug, message: "Test 'iWPR: " + script.iWPR(SYMBOLS.EURUSD, TIME_FRAME.PERIOD_H1,23, 0) + "'"));
        }
    }
}

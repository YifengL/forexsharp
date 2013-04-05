using System;
using System.Diagnostics;

namespace TradePlatform.MT4.Core.Utils
{
    public static class Convertor
    {
        public static bool ToBoolean(string value)
        {
            bool flag;
            try
            {
                flag = value == "1";
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
                throw;
            }
            return flag;
        }

        public static DateTime ToDateTime(string value)
        {
            DateTime dateTime;
            try
            {
                dateTime = DateTime.Parse(value);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
                throw;
            }
            return dateTime;
        }

        public static double ToDouble(string value)
        {
            double num;
            try
            {
                num = double.Parse(value);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
                throw;
            }
            return num;
        }

        public static int ToInt(string value)
        {
            int num;
            try
            {
                num = int.Parse(value);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
                throw;
            }
            return num;
        }
    }
}
using System;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;

namespace FXSharp.TradingPlatform.Exts
{
    public static class CheckupFunctions
    {
        public static bool IsTradeContextBusy(this MqlHandler handler)
        {
            string returnValue = handler.CallMqlMethod("IsTradeContextBusy", null);

            return Convertor.ToBoolean(returnValue);
        }

        public static int GetLastError(this MqlHandler handler)
        {
            string returnValue = handler.CallMqlMethod("GetLastError", null);

            return Convertor.ToInt(returnValue);
        }

        public static int OrderReliableLastErr(this MqlHandler handler)
        {
            string returnValue = handler.CallMqlMethod("OrderReliableLastErr", null);

            try
            {
                return Convertor.ToInt(returnValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

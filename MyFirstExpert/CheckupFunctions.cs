using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;

namespace MyFirstExpert
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
    }
}

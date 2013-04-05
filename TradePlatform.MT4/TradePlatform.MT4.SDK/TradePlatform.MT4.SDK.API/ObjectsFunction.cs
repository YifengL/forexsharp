using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.SDK.API
{
    /// <summary>
    ///     General-purpose functions not included into any specialized groups.
    /// </summary>
    public static class ObjectsFunction
    {
        /// <summary>
        ///     Removes all objects of the specified type and in the specified sub-window of the chart.
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static bool ObjectsDeleteAll(this MqlHandler handler)
        {
            string retrunValue = handler.CallMqlMethod("ObjectsDeleteAll", null);

            return Convertor.ToBoolean(retrunValue);
        }
    }
}

using System.Collections.Generic;
namespace FXSharp.EA.NewsBox
{
    public class CurrencyPairRegistry
    {
        private static Dictionary<string, string> currencyToPair = new Dictionary<string, string> 
        {
            {"NZD", "NZDUSD"}, 
            {"AUD", "AUDUSD"}, 
            {"CNY", "AUDUSD"}, 
            {"JPY", "USDJPY"}, 
            {"CHF", "USDCHF"}, 
            {"EUR", "EURUSD"}, 
            {"GBP", "GBPUSD"}, 
            {"CAD", "USDCAD"}, 
            {"USD", "EURUSD"}
        };

        public string RelatedCurrencyPair(string currency)
        {
            return currencyToPair[currency];
        }
    }
}

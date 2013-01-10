using System.Collections.Generic;

namespace FXSharp.EA.NewsBox
{
    public static class CurrencyRegistry
    {
        private static Dictionary<string, string> countryToCurrency = new Dictionary<string, string> 
        {
            {"New Zealand", "NZD"}, 
            {"Australia", "AUD"},
            {"China", "CNY"}, 
            {"Japan", "JPY"},
            {"France", "EUR"},
            {"Spain", "EUR"}, 
            {"Greece", "EUR"}, 
            {"United Kingdom", "GBP"}, 
            {"European Monetary Union", "EUR"},
            {"Canada", "CAD"},
            {"United States", "USD"}
        };
        
        internal static string ForCountry(string country)
        {
            return countryToCurrency[country];
        }
    }
}

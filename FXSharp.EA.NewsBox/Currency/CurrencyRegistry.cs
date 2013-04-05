using System.Collections.Generic;

namespace FXSharp.EA.NewsBox
{
    public static class CurrencyRegistry
    {
        private static readonly Dictionary<string, string> countryToCurrency = new Dictionary<string, string>
            {
                {"New Zealand", "NZD"},
                {"Australia", "AUD"},
                {"China", "CNY"},
                {"Japan", "JPY"},
                {"Switzerland", "CHF"},
                {"France", "EUR"},
                {"Spain", "EUR"},
                {"Italy", "EUR"},
                {"Portugal", "EUR"},
                {"Germany", "EUR"},
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
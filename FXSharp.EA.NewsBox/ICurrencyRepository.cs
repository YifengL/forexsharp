using System.Collections.Generic;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    internal interface ICurrencyRepository
    {
        IEnumerable<string> GetRelatedCurrencyPairs(EExpertAdvisor newsShooterEA, string currency);
    }
}
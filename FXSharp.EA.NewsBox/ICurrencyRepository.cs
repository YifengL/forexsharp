using FXSharp.TradingPlatform.Exts;
using System.Collections.Generic;

namespace FXSharp.EA.NewsBox
{
    interface ICurrencyRepository
    {
        IEnumerable<string> GetRelatedCurrencyPairs(EExpertAdvisor newsShooterEA, string currency);
    }
}

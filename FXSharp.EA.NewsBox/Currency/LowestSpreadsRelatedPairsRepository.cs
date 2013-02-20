using FXSharp.TradingPlatform.Exts;
using System.Collections.Generic;
using System.Linq;

namespace FXSharp.EA.NewsBox
{
    public class LowestSpreadsRelatedPairsRepository : ICurrencyRepository
    {
        public IEnumerable<string> GetRelatedCurrencyPairs(EExpertAdvisor ea, string currency)
        {
            return CurrencyPairRegistry.RelatedCurrencyPairsForMinimalSpread(ea, currency).Take(4);
        }
    }
}

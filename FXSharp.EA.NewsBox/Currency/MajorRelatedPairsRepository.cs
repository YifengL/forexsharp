using FXSharp.TradingPlatform.Exts;
using System.Collections.Generic;
using System.Linq;

namespace FXSharp.EA.NewsBox
{
    public class MajorRelatedPairsRepository : ICurrencyRepository
    {
        public IEnumerable<string> GetRelatedCurrencyPairs(EExpertAdvisor ea, string currency)
        {
            return CurrencyPairRegistry.RelatedMajorCurrencyPairsForMinimalSpread(ea, currency).Take(4);
        }
    }
}

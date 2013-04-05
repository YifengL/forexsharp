using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_generated_currency_pair
    {
        [Test]
        public void Should_retrieve_all_currency_for_code()
        {
            //CurrencyPairRegistry a = new CurrencyPairRegistry();

            IEnumerable<string> lists = CurrencyPairRegistry.RelatedCurrencyPairs("USD").Take(100);
        }
    }
}
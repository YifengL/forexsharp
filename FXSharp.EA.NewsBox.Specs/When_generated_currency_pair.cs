using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_generated_currency_pair
    {
        [Test]
        public void Should_retrieve_all_currency_for_code()
        {
            //CurrencyPairRegistry a = new CurrencyPairRegistry();

            var lists = CurrencyPairRegistry.RelatedCurrencyPairs("USD").Take(100);

        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_creating_magic_box
    {
        [Test]
        public void Should_eliminate_duplicate_currency()
        {
            var decisionProcessor = new OrderDecisionProcessor();
            var result = decisionProcessor.GetTodayMagicBoxOrders().Result;

        }
    }
}

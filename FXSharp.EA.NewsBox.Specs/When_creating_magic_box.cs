using System.Collections.Generic;
using NUnit.Framework;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_creating_magic_box
    {
        [Test]
        public void Should_eliminate_duplicate_currency()
        {
            var decisionProcessor = new OrderDecisionProcessor();
            List<MagicBoxOrder> result = decisionProcessor.GetTodayMagicBoxOrders().Result;
        }
    }
}
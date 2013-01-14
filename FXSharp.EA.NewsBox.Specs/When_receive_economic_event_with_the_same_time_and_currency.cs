using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
namespace FXSharp.EA.NewsBox.Specs
{
    
    [TestFixture]
    public class When_receive_economic_event_with_the_same_time_and_currency
    {
        IList<MagicBoxOrder> mboxList;

        [TestFixtureSetUp]
        public void PrepareEconomicEvents()
        {
            var time = DateTime.Now;

            var evtList = new List<EconomicEvent> 
            {
                new EconomicEvent { Currency = "USD", DateTime = time.AddMinutes(20), Previous = "33" }, 
                new EconomicEvent { Currency = "CHF", DateTime = time.AddMinutes(30), Previous = "33" }, 
                new EconomicEvent { Currency = "USD", DateTime = time.AddMinutes(40), Previous = "33" }, 
                new EconomicEvent { Currency = "USD", DateTime = time.AddMinutes(40), Previous = "33" }
            };

            var creator = new OrderCreator();
            mboxList = creator.CreateOrdersFromEvents(evtList);
        }

        [Test]
        public void Should_create_single_order_for_the_same_currency()
        {
            Assert.AreEqual(3, mboxList.Count);
        }

        [Test]
        public void Should_create_speech_config_for_duplicate_currency()
        {
            var lastEvent = mboxList.Last();
            var config = lastEvent.Config;

            Assert.AreEqual("EURUSD", lastEvent.Symbol);

            Assert.AreEqual(-1, config.MinutePendingExecution);
            Assert.AreEqual(30, config.MinuteExpiracy);
            Assert.AreEqual(250, config.Range);
            Assert.AreEqual(200, config.StopLoss);
            Assert.AreEqual(150, config.TakeProfit);
        }
        // create_speech_config_for_magic_box
    }
}

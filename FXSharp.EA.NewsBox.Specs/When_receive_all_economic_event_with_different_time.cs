using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_receive_all_economic_event_with_different_time
    {
        [Test]
        public void Should_Create_speech_trade()
        {
            DateTime time = DateTime.Now;

            var evtList = new List<EconomicEvent>
                {
                    new EconomicEvent {Currency = "USD", DateTime = time.AddMinutes(20)},
                    new EconomicEvent {Currency = "CHF", DateTime = time.AddMinutes(30)}
                };

            var creator = new OrderCreator();
            IList<MagicBoxOrder> lst = creator.CreateOrdersFromEvents(evtList);

            Assert.IsTrue(evtList.First().IsSpeechOrMeeting);

            MagicBoxConfig config = lst.First().Config;

            Assert.AreEqual(-1, config.MinutePendingExecution);
            Assert.AreEqual(30, config.MinuteExpiracy);
            Assert.AreEqual(250, config.Range);
            Assert.AreEqual(200, config.StopLoss);
            Assert.AreEqual(150, config.TakeProfit);
        }

        [Test]
        public void Should_create_different_order()
        {
            DateTime time = DateTime.Now;

            var evtList = new List<EconomicEvent>
                {
                    new EconomicEvent {Currency = "USD", DateTime = time.AddMinutes(20), Previous = "123"},
                    new EconomicEvent {Currency = "CHF", DateTime = time.AddMinutes(30), Previous = "123"}
                };

            var creator = new OrderCreator();
            IList<MagicBoxOrder> lst = creator.CreateOrdersFromEvents(evtList);

            Assert.AreEqual(2, lst.Count);

            MagicBoxConfig config = lst.First().Config;

            Assert.AreEqual(-1, config.MinutePendingExecution);
            Assert.AreEqual(10, config.MinuteExpiracy);
            Assert.AreEqual(50, config.Range);
            Assert.AreEqual(200, config.StopLoss);
            Assert.AreEqual(150, config.TakeProfit);
        }
    }
}
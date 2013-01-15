using System;
using System.Collections.Generic;
using System.Linq;

namespace FXSharp.EA.NewsBox
{
    public class OrderCreator
    {
        private CurrencyPairRegistry analyzer = new CurrencyPairRegistry();
        
        MagicBoxConfig speechCfg = new MagicBoxConfig
        {
            MinutePendingExecution = -1,
            MinuteExpiracy = 30,
            Range = 250,
            StopLoss = 200,
            TakeProfit = 150
        };
        
        MagicBoxConfig commonCfg = new MagicBoxConfig
        {
            MinutePendingExecution = -1,
            MinuteExpiracy = 10,
            Range = 100,
            StopLoss = 200,
            TakeProfit = 150
        };

        public IList<MagicBoxOrder> CreateOrdersFromEvents(IEnumerable<EconomicEvent> events)
        {
            var mgcBoxOrderList = new List<MagicBoxOrder>();

            var groups = events.GroupBy(x => x.DateTime);

            foreach (var eventGroup in groups)
            {
                if (IsSingleEvent(eventGroup))
                {
                    AddSingleMagicBox(mgcBoxOrderList, eventGroup);
                }
                else // multiple and collide each other
                {
                    AddMagicBoxGroup(mgcBoxOrderList, eventGroup);
                }
            }

            return mgcBoxOrderList;
        }

        private void AddMagicBoxGroup(List<MagicBoxOrder> mgcBoxOrderList, IGrouping<DateTime, EconomicEvent> eventGroup)
        {
            var result = eventGroup.Distinct().Select(eventx => new MagicBoxOrder
                {
                    Symbol = analyzer.RelatedCurrencyPair(eventx.Currency),
                    LotSize = 1,
                    Config = speechCfg,
                    NewsTime = eventx.DateTime
                }).Distinct();

            mgcBoxOrderList.AddRange(result);
        }

        private void AddSingleMagicBox(List<MagicBoxOrder> mgcBoxOrderList, IGrouping<DateTime, EconomicEvent> eventGroup)
        {
            var singleEvent = eventGroup.Single();

            mgcBoxOrderList.Add(new MagicBoxOrder
            {
                Symbol = analyzer.RelatedCurrencyPair(singleEvent.Currency),
                LotSize = 1,
                Config = SelectConfig(singleEvent),
                NewsTime = singleEvent.DateTime
            });
        }

        private static bool IsSingleEvent(IGrouping<DateTime, EconomicEvent> item)
        {
            return item.Count() == 1;
        }

        private MagicBoxConfig SelectConfig(EconomicEvent eventx)
        {
            return eventx.IsSpeechOrMeeting ? speechCfg : commonCfg;
        }
    }
}

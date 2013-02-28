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
            MinutePendingExecution = -1, // test for 2 minutes and change range
            MinuteExpiracy = 30, // 20 - 30 minutes
            Range = 200, // 20-25  minutes
            StopLoss = 200,
            TakeProfit = 300
        };
        
        MagicBoxConfig commonCfg = new MagicBoxConfig
        {
            MinutePendingExecution = -1, // 2-1 minute
            MinuteExpiracy = 10, 
            Range = 100, // 5-10 pips 
            StopLoss = 150,
            TakeProfit = 100
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
            // refactor the this events

            var currencyPairsCandidate = new List<MagicBoxOrder>();

            foreach (var economicEvent in eventGroup.Distinct())
            {
                currencyPairsCandidate.Add(new MagicBoxOrder
                {
                    Symbol = economicEvent.Currency,
                    LotSize = 1,
                    Config = speechCfg,
                    NewsTime = economicEvent.DateTime
                });
            }

            mgcBoxOrderList.AddRange(currencyPairsCandidate.Distinct());
        }

        private void AddSingleMagicBox(List<MagicBoxOrder> mgcBoxOrderList, IGrouping<DateTime, EconomicEvent> eventGroup)
        {
            var singleEvent = eventGroup.Single();

            mgcBoxOrderList.Add(new MagicBoxOrder
            {
                Symbol = singleEvent.Currency,
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

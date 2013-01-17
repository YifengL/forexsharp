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
            // refactor the this events

            var currencyPairsCandidate = new List<MagicBoxOrder>();

            foreach (var economicEvent in eventGroup.Distinct())
            {
                var relatedCurrencies = analyzer.RelatedCurrencyPairs(economicEvent.Currency);

                foreach (var relatedCurrency in relatedCurrencies)
                {
                    currencyPairsCandidate.Add(new MagicBoxOrder
                    {
                        Symbol = relatedCurrency,
                        LotSize = 1,
                        Config = speechCfg,
                        NewsTime = economicEvent.DateTime
                    });
                }
            }

            //var result = eventGroup.Distinct().Select(eventx => new MagicBoxOrder
            //    {
            //        Symbol = analyzer.RelatedCurrencyPair(eventx.Currency),
            //        LotSize = 1,
            //        Config = speechCfg,
            //        NewsTime = eventx.DateTime
            //    }).Distinct();

            mgcBoxOrderList.AddRange(currencyPairsCandidate.Distinct());
        }

        private void AddSingleMagicBox(List<MagicBoxOrder> mgcBoxOrderList, IGrouping<DateTime, EconomicEvent> eventGroup)
        {
            var singleEvent = eventGroup.Single();

            // refactor the this events => duplication

            var relatedCurrencies = analyzer.RelatedCurrencyPairs(singleEvent.Currency);

            foreach (var relatedCurrency in relatedCurrencies)
            {
                mgcBoxOrderList.Add(new MagicBoxOrder
                {
                    Symbol = relatedCurrency,
                    LotSize = 1,
                    Config = SelectConfig(singleEvent),
                    NewsTime = singleEvent.DateTime
                });   
            }
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

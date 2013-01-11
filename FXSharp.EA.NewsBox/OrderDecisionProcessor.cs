using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    class OrderDecisionProcessor
    {
        private IEconomicCalendar fxFactoryCalendar = new ForexFactoryEconomicCalendar();
        private IEconomicCalendar fxStreetCalendar = new FxStreetEconomicCalendar();

        private CurrencyPairRegistry analyzer = new CurrencyPairRegistry();

        internal async Task<List<MagicBoxOrder>> GetTodayMagicBoxOrders()
        {
            var fxFactoryEventsTask = fxFactoryCalendar.GetTodaysNextCriticalEventsAsync();
            var fxStreetEventsTask = fxStreetCalendar.GetTodaysNextCriticalEventsAsync();

            var results = await Task.WhenAll(fxFactoryEventsTask, fxStreetEventsTask);

            var finalResult = MergeResult(results);

            // [x] should contain logic distict the order for the same event
            // should contain logic how long the order will survive -> expired time
            // should contain logic how the order will be processed, tied event, speech 
            // should add distance, stoploss, takeprofit, expired time
            // should contain logic when the order will get place according to the situation
            // should also combine with fxstreet calendar
            // if speech then 
            // if tied with self then 
            // if tied with another currency pair USDCAD => US news and CAD news at the same time

            // group based on time

            var groups = finalResult.GroupBy(x => x.DateTime);

            foreach (var group in groups)
            {
                var time = group.Key;
                var events = group.ToList();
            }

            return finalResult
                .Select(eventx => new MagicBoxOrder 
                {
                    Symbol = analyzer.RelatedCurrencyPair(eventx.Currency), 
                    ExecutingTime = eventx.DateTime.AddMinutes(-1), 
                    LotSize = 1, 
                    Range = 50, 
                    TakeProfit = 150, 
                    StopLoss = 200, 
                    MinuteExpiracy = 10
                }).Distinct().ToList();
        }

        private IEnumerable<EconomicEvent> MergeResult(IList<EconomicEvent>[] results)
        {
            var fxFactResult = results[0];
            var fxStreetResult = results[1];
            return fxFactResult.Concat(fxStreetResult).Distinct();
        }
    }
}

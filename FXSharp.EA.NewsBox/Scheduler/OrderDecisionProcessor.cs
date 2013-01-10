using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    class OrderDecisionProcessor
    {
        private EconomicCalendar calendar = new EconomicCalendar();
        private CurrencyPairRegistry analyzer = new CurrencyPairRegistry();

        internal async Task<List<MagicBoxOrder>> GetTodayMagicBoxOrders()
        {
            var criticalEvents = await calendar.GetTodaysNextCriticalEvents();

            // [x]should contain logic distict the order for the same event
            // should contain logic how long the order will survive -> expired time
            // should contain logic when the order will get place according to the situation

            return criticalEvents
                .Select(eventx => new MagicBoxOrder 
                { 
                    Symbol = analyzer.RelatedCurrencyPair(eventx.Currency), 
                    ExecutingTime = eventx.DateTime.AddMinutes(-3) 
                }).Distinct().ToList();
        }
    }
}

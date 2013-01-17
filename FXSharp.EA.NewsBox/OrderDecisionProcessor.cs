using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    public class OrderDecisionProcessor
    {
        private EconomicCalendarPool calendarPool = new EconomicCalendarPool();

        private CurrencyPairRegistry analyzer = new CurrencyPairRegistry();
        private OrderCreator orderCreator = new OrderCreator();

        public OrderDecisionProcessor()
        {
            calendarPool.Add(new ForexFactoryEconomicCalendar());
            calendarPool.Add(new FxStreetEconomicCalendar());
        }

        public async Task<List<MagicBoxOrder>> GetTodayMagicBoxOrders()
        {
            var finalResult = await calendarPool.AllResultsAsync();

            return orderCreator.CreateOrdersFromEvents(finalResult).ToList();

            // should contain logic how the order will be processed, tied event, speech 
            
            // if tied with another currency pair USDCAD => US news and CAD news at the same time
        }
    }
}

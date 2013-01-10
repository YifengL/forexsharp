using Quartz;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicScheduler
    {
        
        private MagicBoxScheduler scheduler;
        private OrderDecisionProcessor decisionProcessor = new OrderDecisionProcessor(); // currently using polling model, next publish

        public DailyEconomicScheduler(IScheduler scheduler, ConcurrentQueue<MagicBoxOrder> queues)
        {
            this.scheduler = new MagicBoxScheduler(scheduler, queues);
        }

        public async Task PrepareDailyReminder()
        {
            var magicBoxes = await decisionProcessor.GetTodayMagicBoxOrders();

            foreach (var order in magicBoxes)
            {
                scheduler.Schedule(order);
            }
        }
    }
}

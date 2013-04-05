using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicScheduler
    {
        private readonly OrderDecisionProcessor decisionProcessor = new OrderDecisionProcessor();
                                                // currently using polling model, next publish

        private readonly MagicBoxScheduler scheduler;

        public DailyEconomicScheduler(IScheduler scheduler, ConcurrentQueue<MagicBoxOrder> queues)
        {
            this.scheduler = new MagicBoxScheduler(scheduler, queues);
        }

        public async Task PrepareDailyReminder()
        {
            List<MagicBoxOrder> magicBoxes = await decisionProcessor.GetTodayMagicBoxOrders();

            foreach (MagicBoxOrder order in magicBoxes)
            {
                Console.WriteLine("Scheduled : {0} @ {1}", order.Symbol, order.ExecutingTime);
                scheduler.Schedule(order);
            }
        }
    }
}
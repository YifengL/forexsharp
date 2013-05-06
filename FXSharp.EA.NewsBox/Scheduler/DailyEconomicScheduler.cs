using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicScheduler
    {
        private readonly OrderDecisionProcessor _decisionProcessor = new OrderDecisionProcessor();
        // currently using polling model, next publish

        private readonly MagicBoxScheduler _scheduler;

        public DailyEconomicScheduler(IScheduler scheduler, ConcurrentQueue<MagicBoxOrder> queues)
        {
            this._scheduler = new MagicBoxScheduler(scheduler, queues);
        }

        public async Task PrepareDailyReminder()
        {
            var magicBoxes = await _decisionProcessor.GetTodayMagicBoxOrders();

            foreach (var order in magicBoxes)
            {
                Console.WriteLine("Scheduled : {0} @ {1}", order.Symbol, order.ExecutingTime);
                _scheduler.Schedule(order);
            }
        }
    }
}
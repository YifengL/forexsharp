using Quartz;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicScheduler
    {
        private EconomicCalendar calendar = new EconomicCalendar();
        private CurrencyCorrelationAnalyzer analzyer = new CurrencyCorrelationAnalyzer();
        private IScheduler scheduler;
        private ConcurrentQueue<MagicBoxOrder> queues;

        public DailyEconomicScheduler(IScheduler scheduler, ConcurrentQueue<MagicBoxOrder> queues)
        {
            this.scheduler = scheduler;
            this.queues = queues;
        }

        public async Task PrepareDailyReminder()
        {
            var criticalEvents = await calendar.GetTodaysNextCriticalEvents();

            var mbox = new MagicBoxScheduler(scheduler, queues);

            foreach (var eventx in criticalEvents)
            {
                mbox.Schedule(eventx.DateTime.AddMinutes(-3), new MagicBoxOrder { Symbol = analzyer.RelatedCurrencyPair(eventx.Country) });
            }
        }
    }
}

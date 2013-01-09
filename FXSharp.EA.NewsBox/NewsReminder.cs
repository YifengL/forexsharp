using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public class NewsReminder
    {
        private ISchedulerFactory schedFact = new StdSchedulerFactory();
        private ConcurrentQueue<MagicBoxOrder> orderQueue = new ConcurrentQueue<MagicBoxOrder>();
        private IScheduler sched;
        private EconomicCalendar calendar = new EconomicCalendar();

        public NewsReminder()
        {
            sched = schedFact.GetScheduler();
        }

        internal void Start()
        {
            sched.Start();

            ScheduleTodayEconomicCalendarAsync();

            // should create async
        }

        private async Task ScheduleTodayEconomicCalendarAsync()
        {
            var economicEvents = await calendar.GetTodaysCriticalEvents();

            // loop for each economic events and decide the schedule when to create pending order

            ScheduleMagicBox(DateTime.Now.AddSeconds(10), new MagicBoxOrder { Symbol = "EURUSD" });

            ScheduleMagicBox(DateTime.Now.AddSeconds(15), new MagicBoxOrder { Symbol = "USDCAD" });
        }

        private void ScheduleMagicBox(DateTime nexttime, MagicBoxOrder magicBox)
        {
            IJobDetail jobDetail = JobBuilder.Create<MagicBoxOrderJob>()
                .WithIdentity(magicBox.Symbol, "group1")
                .Build();

            //DateTime nexttime = DateTime.Now.AddSeconds(10);

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(magicBox.Symbol, "group1")
                .StartAt(DateBuilder.TodayAt(nexttime.Hour, nexttime.Minute, nexttime.Second))
                .Build();

            // should group this together in one command. just execute when arrived there

            jobDetail.JobDataMap.Add("queue", orderQueue);
            jobDetail.JobDataMap.Add("orders", magicBox);

            sched.ScheduleJob(jobDetail, trigger);
        }

        public ConcurrentQueue<MagicBoxOrder> OrderQueue
        {
            get { return orderQueue; }
        }

        public bool IsAvailable
        {
            get { return orderQueue.Count > 0; }
        }
    }
}

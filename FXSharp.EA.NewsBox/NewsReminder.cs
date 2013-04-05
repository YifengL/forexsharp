using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace FXSharp.EA.NewsBox
{
    public class NewsReminder
    {
        private readonly DailyEconomicScheduler economicScheduler;
        private readonly ConcurrentQueue<MagicBoxOrder> orderQueue = new ConcurrentQueue<MagicBoxOrder>();
        private readonly IScheduler sched;
        private readonly ISchedulerFactory schedFact = new StdSchedulerFactory();

        public NewsReminder()
        {
            sched = schedFact.GetScheduler();
            economicScheduler = new DailyEconomicScheduler(sched, orderQueue);
        }

        public ConcurrentQueue<MagicBoxOrder> OrderQueue
        {
            get { return orderQueue; }
        }

        public bool IsAvailable
        {
            get { return orderQueue.Count > 0; }
        }

        internal void Start()
        {
            sched.Start();

            LoadDailyEconomicCalendar();

            StartDailyEconomicCalendar();
        }

        private void LoadDailyEconomicCalendar()
        {
            Task task = economicScheduler.PrepareDailyReminder();
        }

        private void StartDailyEconomicCalendar()
        {
            IJobDetail jobDetail = JobBuilder.Create<DailyEconomicCalendarUpdateJob>()
                                             .WithIdentity(DateTime.Now.ToString(), "group1")
                                             .Build();

            DateTime nexttime = DateTime.Now.AddSeconds(2);

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity(DateTime.Now.ToString(), "group1")
                                             .StartNow()
                                             .WithCronSchedule("0 01 00 * * ?") // everyday at 00 01
                                             .Build();

            jobDetail.JobDataMap.Add("scheduler", economicScheduler);

            sched.ScheduleJob(jobDetail, trigger);
        }

        internal void Stop()
        {
            sched.Shutdown();
        }
    }
}
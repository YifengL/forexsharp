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
        DailyEconomicScheduler economicScheduler;
        private IScheduler sched;
        
        public NewsReminder()
        {
            sched = schedFact.GetScheduler();
            economicScheduler = new DailyEconomicScheduler(sched, orderQueue);
        }

        internal void Start()
        {
            sched.Start();

            LoadDailyEconomicCalendar();

            StartDailyEconomicCalendar();
        }

        private void LoadDailyEconomicCalendar()
        {
            var task = economicScheduler.PrepareDailyReminder();
        }

        private void StartDailyEconomicCalendar()
        {
            var jobDetail = JobBuilder.Create<DailyEconomicCalendarUpdateJob>()
                .WithIdentity(DateTime.Now.ToString(), "group1")
                .Build();

            var nexttime = DateTime.Now.AddSeconds(2);
            
            var trigger = TriggerBuilder.Create()
                .WithIdentity(DateTime.Now.ToString(), "group1")
                .StartNow()
                .WithCronSchedule("0 28 16 * * ?") // everyday at 00 01
                .Build();

            jobDetail.JobDataMap.Add("scheduler", economicScheduler);

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

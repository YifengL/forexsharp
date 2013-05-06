using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace FXSharp.EA.NewsBox
{
    public class NewsReminder
    {
        private readonly DailyEconomicScheduler _economicScheduler;
        private readonly ConcurrentQueue<MagicBoxOrder> _orderQueue = new ConcurrentQueue<MagicBoxOrder>();
        private readonly IScheduler _sched;
        private readonly ISchedulerFactory _schedFact = new StdSchedulerFactory();

        public NewsReminder()
        {
            _sched = _schedFact.GetScheduler();
            _economicScheduler = new DailyEconomicScheduler(_sched, _orderQueue);
        }

        public ConcurrentQueue<MagicBoxOrder> OrderQueue
        {
            get { return _orderQueue; }
        }

        public bool IsAvailable
        {
            get { return _orderQueue.Count > 0; }
        }

        internal void Start()
        {
            _sched.Start();

            LoadDailyEconomicCalendar();

            StartDailyEconomicCalendar();
        }

        private void LoadDailyEconomicCalendar()
        {
            Task task = _economicScheduler.PrepareDailyReminder();
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

            jobDetail.JobDataMap.Add("scheduler", _economicScheduler);

            _sched.ScheduleJob(jobDetail, trigger);
        }

        internal void Stop()
        {
            _sched.Shutdown();
        }
    }
}
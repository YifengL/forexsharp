using System;
using System.Collections.Concurrent;
using Quartz;

namespace FXSharp.EA.NewsBox
{
    internal class MagicBoxScheduler
    {
        private readonly ConcurrentQueue<MagicBoxOrder> orderQueue;
        private readonly IScheduler scheduler;

        public MagicBoxScheduler(IScheduler scheduler, ConcurrentQueue<MagicBoxOrder> orderQueue)
        {
            this.scheduler = scheduler;
            this.orderQueue = orderQueue;
        }

        public void Schedule(MagicBoxOrder magicBox)
        {
            DateTime nexttime = magicBox.ExecutingTime;

            IJobDetail jobDetail = JobBuilder.Create<MagicBoxOrderJob>()
                                             .WithIdentity(magicBox.Id, "group1")
                                             .Build();

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity(magicBox.Id, "group1")
                                             .StartAt(DateBuilder.TodayAt(nexttime.Hour, nexttime.Minute,
                                                                          nexttime.Second))
                                             .Build();

            // should group this together in one command. just execute when arrived there

            jobDetail.JobDataMap.Add("queue", orderQueue);
            jobDetail.JobDataMap.Add("orders", magicBox);

            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
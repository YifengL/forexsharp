using Quartz;
using System;
using System.Collections.Concurrent;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicCalendarUpdateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var scheduler = (DailyEconomicScheduler)context.JobDetail.JobDataMap["scheduler"];
            var task = scheduler.PrepareDailyReminder();
        }
    }
}

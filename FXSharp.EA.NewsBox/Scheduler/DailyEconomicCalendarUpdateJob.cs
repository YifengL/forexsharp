using System.Threading.Tasks;
using Quartz;

namespace FXSharp.EA.NewsBox
{
    public class DailyEconomicCalendarUpdateJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var scheduler = (DailyEconomicScheduler) context.JobDetail.JobDataMap["scheduler"];
            Task task = scheduler.PrepareDailyReminder();
        }
    }
}
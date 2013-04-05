using System.Collections.Concurrent;
using Quartz;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxOrderJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            var queues = (ConcurrentQueue<MagicBoxOrder>) context.JobDetail.JobDataMap["queue"];
            var mbox = (MagicBoxOrder) context.JobDetail.JobDataMap["orders"];

            queues.Enqueue(mbox);
        }
    }
}
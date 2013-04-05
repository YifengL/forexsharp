using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public class EconomicCalendarPool
    {
        private readonly IList<IEconomicCalendar> calendars = new List<IEconomicCalendar>();

        public void Add(IEconomicCalendar calendar)
        {
            calendars.Add(calendar);
        }

        public async Task<IList<EconomicEvent>> AllResultsAsync()
        {
            IEnumerable<Task<IList<EconomicEvent>>> tasks = from cal in calendars
                                                            select cal.GetTodaysNextCriticalEventsAsync();

            IList<EconomicEvent>[] result = await Task.WhenAll(tasks);

            return MergeResult(result).ToList();
        }

        private IEnumerable<EconomicEvent> MergeResult(IList<EconomicEvent>[] results)
        {
            IEnumerable<EconomicEvent> allEvents = new List<EconomicEvent>();

            foreach (var rs in results)
            {
                allEvents = allEvents.Concat(rs);
            }

            return allEvents.Distinct().OrderBy(x => x.DateTime);
        }
    }
}
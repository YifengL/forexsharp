using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public class EconomicCalendarPool
    {
        IList<IEconomicCalendar> calendars = new List<IEconomicCalendar>();

        public void Add(IEconomicCalendar calendar)
        {
            calendars.Add(calendar);
        }

        public async Task<IList<EconomicEvent>> AllResultsAsync()
        {
            var tasks = from cal in calendars
                         select cal.GetTodaysNextCriticalEventsAsync();

            var result = await Task.WhenAll(tasks);

            return MergeResult(result).ToList();
        }

        private IEnumerable<EconomicEvent> MergeResult(IList<EconomicEvent>[] results)
        {
            IEnumerable<EconomicEvent> allEvents = new List<EconomicEvent>();

            foreach (IList<EconomicEvent> rs in results)
            {
                allEvents = allEvents.Concat(rs);
            }

            return allEvents.Distinct().OrderBy(x => x.DateTime);
        }
    }
}

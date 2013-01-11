using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public abstract class EconomicCalendar : IEconomicCalendar
    {
        public async Task<IList<EconomicEvent>> GetTodaysNextCriticalEventsAsync()
        {
            var incomingNews = await GetTodaysCriticalEventsAsync().ConfigureAwait(false);

            //return incomingNews.Where(x => x.DateTime > DateTime.Now && x.DateTime.Date == DateTime.Now.Date).ToList();
            return incomingNews.Where(x => x.DateTime.Date == DateTime.Now.Date).ToList();
        }

        private async Task<IList<EconomicEvent>> GetTodaysCriticalEventsAsync()
        {
            string rawData = await RequestRawDataToServerAsync().ConfigureAwait(false);

            return await ParseEconomicEventsAsync(rawData).ConfigureAwait(false);
        }

        protected abstract Task<IList<EconomicEvent>> ParseEconomicEventsAsync(string rawData);
        protected abstract Task<string> RequestRawDataToServerAsync();
    }
}

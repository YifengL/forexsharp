using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    interface IEconomicCalendar
    {
        Task<IList<EconomicEvent>> GetTodaysNextCriticalEventsAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    public interface IEconomicCalendar
    {
        Task<IList<EconomicEvent>> GetTodaysNextCriticalEventsAsync();
    }
}

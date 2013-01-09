using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MyFirstExpert
{
    public class EconomicCalendar
    {
        internal IList<EconomicEvent> GetTodaysCriticalEvents()
        {
            var client = new WebClient();

            string todayString = DateTime.Now.ToString();

            client.Headers.Add("Referer", "http://www.fxstreet.com/fundamental/economic-calendar/");
            client.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
            client.Encoding = Encoding.UTF8;
            
            var result = client.DownloadString("http://calendar.fxstreet.com/eventdate/csv?timezone=SE+Asia+Standard+Time&rows=0&view=current&countrycode=AU%2CCA%2CCN%2CEMU%2CDE%2CFR%2CDE%2CGR%2CIT%2CJP%2CNZ%2CPT%2CES%2CCH%2CUK%2CUS&volatility=3&culture=en-US&columns=CountryCurrency");

            return null;
        }
    }
}

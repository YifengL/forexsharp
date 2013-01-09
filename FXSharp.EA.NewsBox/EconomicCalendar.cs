using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public class EconomicCalendar
    {
        internal async Task<IList<EconomicEvent>> GetTodaysCriticalEvents()
        {
            string rawData = await RequestRawDataToServerAsync();

            return await ParseEconomicEventsAsync(rawData);
        }

        private async Task<IList<EconomicEvent>> ParseEconomicEventsAsync(string rawData)
        {
            var reader = new StringReader(rawData);
            var results = new List<EconomicEvent>();
            // read header
            reader.ReadLineAsync();

            string line = string.Empty;

            while ((line = await reader.ReadLineAsync()) != null)
            {
                if (string.IsNullOrEmpty(line)) 
                    break;

                string[] colums = line.Split(',');

                results.Add(new EconomicEvent
                                {
                                    DateTime = ParseDateTime(colums[0].Trim('"')),
                                    Name = colums[1].Trim('"'),
                                    Country = colums[2].Trim('"'),
                                    Volatility = colums[3].Trim('"'),
                                    Actual = colums[4].Trim('"'),
                                    Previous = colums[5].Trim('"'),
                                    Consensus = colums[6].Trim('"')
                                });
            }

            return results;

            //DateTime,Name,Country,Volatility,Actual,Previous,Consensus
            //"20130109 17:00:00","Gross Domestic Product s.a. (QoQ)","European Monetary Union","3","","-0.1%","-0.1%"
        }

        private DateTime ParseDateTime(string p)
        {
            return DateTime.Now;
        }

        private async Task<string> RequestRawDataToServerAsync()
        {
            string rawResult = null;

            using (var client = new WebClient())
            {
                string todayString = DateTime.Now.ToString();

                client.Headers.Add("Referer", "http://www.fxstreet.com/fundamental/economic-calendar/");
                client.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
                client.Encoding = Encoding.UTF8;

                // should create async
                rawResult = await client.DownloadStringTaskAsync("http://calendar.fxstreet.com/eventdate/csv?timezone=SE+Asia+Standard+Time&rows=0&view=range&start=20130109&end=20130109&countrycode=AU%2CCA%2CCN%2CEMU%2CDE%2CFR%2CDE%2CGR%2CIT%2CJP%2CNZ%2CPT%2CES%2CCH%2CUK%2CUS&volatility=3&culture=en-US&columns=CountryCurrency");
            }

            return rawResult;
        }
    }
}

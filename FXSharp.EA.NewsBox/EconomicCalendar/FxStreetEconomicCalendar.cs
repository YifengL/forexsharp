using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    public class FxStreetEconomicCalendar : EconomicCalendar
    {
        protected override async Task<IList<EconomicEvent>> ParseEconomicEventsAsync(string rawData)
        {
            var reader = new StringReader(rawData);
            var results = new List<EconomicEvent>();
            
            string header = await reader.ReadLineAsync().ConfigureAwait(false);

            VerifyHeader(header);

            string line = string.Empty;

            while ((line = await reader.ReadLineAsync().ConfigureAwait(false)) != null)
            {
                if (string.IsNullOrEmpty(line)) 
                    break;

                string[] colums = line.Split(',');
                // be careful about not available data, n/a tentative etc etc
                try
                {
                    results.Add(new EconomicEvent
                    {
                        DateTime = ParseDateTime(colums[0].Trim('"')),
                        Name = colums[1].Trim('"'),
                        Country = colums[2].Trim('"'),
                        Currency = CurrencyRegistry.ForCountry(colums[2].Trim('"')),
                        Volatility = colums[3].Trim('"'),
                        Actual = colums[4].Trim('"'),
                        Previous = colums[5].Trim('"'),
                        Consensus = colums[6].Trim('"')
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return results;
        }

        private void VerifyHeader(string header)
        {
            Console.WriteLine(header);
        }

        private static DateTime ParseDateTime(string date)
        {
            var parts = date.Split(' ');
            string datePart = parts[0];
            string timePart = parts[1];

            int year = Convert.ToInt32(datePart.Substring(0, 4));
            int month = Convert.ToInt32(datePart.Substring(4, 2));
            int day = Convert.ToInt32(datePart.Substring(6, 2));

            var time = DateTime.Parse(timePart);

            return new DateTime(year, month, day, time.Hour, time.Minute, time.Second);
        }

        protected override async Task<string> RequestRawDataToServerAsync()
        {
            string rawResult = null;

            using (var client = new WebClient())
            {
                string todayString = string.Format("{0}{1}{2}", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'), DateTime.Now.Day.ToString().PadLeft(2, '0'));

                client.Headers.Add("Referer", "http://www.fxstreet.com/fundamental/economic-calendar/");
                client.Headers.Add("Accept-Charset", "ISO-8859-1,utf-8;q=0.7,*;q=0.3");
                client.Encoding = Encoding.UTF8;

                int volatility = 1;
                string queryString = string.Format("http://calendar.fxstreet.com/eventdate/csv?timezone=SE+Asia+Standard+Time&rows=0&view=range&start={0}&end={0}&countrycode=AU%2CCA%2CCN%2CEMU%2CDE%2CFR%2CDE%2CGR%2CIT%2CJP%2CNZ%2CPT%2CES%2CCH%2CUK%2CUS&volatility={1}&culture=en-US&columns=CountryCurrency", todayString, volatility);
                rawResult = await client.DownloadStringTaskAsync(queryString).ConfigureAwait(false);
            }

            return rawResult;
        }
    }
}

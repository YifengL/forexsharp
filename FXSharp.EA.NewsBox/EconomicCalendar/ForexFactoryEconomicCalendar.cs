using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
namespace FXSharp.EA.NewsBox
{
    public class ForexFactoryEconomicCalendar : EconomicCalendar
    {
        protected override Task<IList<EconomicEvent>> ParseEconomicEventsAsync(string rawData)
        {
            return Task.Run<IList<EconomicEvent>>(() => 
            {
                var results = new List<EconomicEvent>();

                var doc = new HtmlDocument();
                doc.LoadHtml(rawData);

                foreach (HtmlNode evt in doc.DocumentNode.SelectNodes("//tr[@class='calendar_row']"))
                {
                    var some = evt.ChildNodes;

                    string eventId = evt.Attributes["data-eventid"].Value;

                    //string date = evt.ChildNodes[1].InnerText;

                    string time = Clean(evt.ChildNodes[3].InnerText);
                    string currency = evt.ChildNodes[5].InnerText;

                    var impact = GetImpact(evt);
                    var eventTitle = evt.ChildNodes[9].InnerText;
                    var actual = Clean(evt.ChildNodes[13].InnerText);
                    var effect = GetEffect(evt);
                    var forecast = evt.ChildNodes[15].InnerText;
                    var previous = evt.ChildNodes[17].InnerText;

                    results.Add(new EconomicEvent
                    {
                        DateTime = DateTime.Parse(time),
                        Name = eventTitle,
                        Country = currency,
                        Currency = currency,
                        Volatility = effect,
                        Actual = actual,
                        Previous = previous,
                        Consensus = forecast
                    });
                }

                return results;
            });
        }

        private static string Clean(string p)
        {
            return p.Replace("&nbsp;", "").Trim();
        }

        private static string GetEffect(HtmlNode evt)
        {
            try
            {
                return evt.ChildNodes[13].ChildNodes[1].Attributes["class"].Value.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private static string GetImpact(HtmlNode evt)
        {
            try
            {
                return evt.ChildNodes[7].ChildNodes[1].Attributes["title"].Value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        protected override async Task<string> RequestRawDataToServerAsync()
        {
            string rawResult = null;

            using (var client = new WebClient())
            {
                client.Headers.Add("Origin", "http://www.forexfactory.com");
                client.Headers.Add("Referer", "http://www.forexfactory.com/index.php");
                client.Headers.Add("X-Requested-With", "XMLHttpRequest");
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11");
                client.Headers.Add("Cookie", "__gads=ID=4cb0429a709ae2b1:T=1352673378:S=ALNI_MakhgWOmj0BAHVj_fEPqXcQvJsoAA; fftimezoneoffset=7; dstonoff=0; ffdstonoff=0; fftimezoneoffset=7; ffstartofweek=1; fftimeformat=0; ffverifytimes=1; notice_ffBlog=1357014276; flexHeight_flexBox_flex_minicalendar_=155; ffflextime_flex_minicalendar_=1357838496-0; ffcalendar=93bdedb64d9706a2bebaacef9cdeeb68da7c848ca-2-%7Bs-7-.calyear._i-0_s-8-.calmonth._i-0_%7D; __utma=113005075.2089994478.1352727369.1357842901.1357862156.185; __utmc=113005075; __utmz=113005075.1357455362.160.9.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); ffflex=a%3A1%3A%7Bs%3A16%3A%22Calendar_mainCal%22%3Ba%3A3%3A%7Bs%3A9%3A%22begindate%22%3Bs%3A16%3A%22January+11%2C+2013%22%3Bs%3A7%3A%22enddate%22%3Bs%3A16%3A%22January+11%2C+2013%22%3Bs%3A7%3A%22impacts%22%3Ba%3A1%3A%7Bs%3A4%3A%22high%22%3Bs%3A4%3A%22high%22%3B%7D%7D%7D; ffsessionhash=4e2aacfe2d66b5076c016bf88139ea81; flexHeight_flexBox_flex_calendar_mainCal=227; ffflextime_flex_calendar_mainCal=1357869548-0; fflastvisit=1352673368; fflastactivity=0");

                rawResult = await client.UploadStringTaskAsync("http://www.forexfactory.com/flex.php?", "s=&securitytoken=guest&do=saveoptions&setdefault=no&ignoreinput=no&flex%5BCalendar_mainCal%5D%5BidSuffix%5D=&flex%5BCalendar_mainCal%5D%5B_flexForm_%5D=flexForm&flex%5BCalendar_mainCal%5D%5BmodelData%5D=YTo3OntzOjEyOiJob21lQ2FsZW5kYXIiO2I6MTtzOjEyOiJ2aWV3aW5nVG9kYXkiO2I6MDtzOjExOiJwcmV2Q2FsTGluayI7czoxNDoiZGF5PWphbjEwLjIwMTMiO3M6MTE6Im5leHRDYWxMaW5rIjtzOjE0OiJkYXk9amFuMTIuMjAxMyI7czo3OiJwcmV2QWx0IjtzOjE3OiJZZXN0ZXJkYXk6IEphbiAxMCI7czo3OiJuZXh0QWx0IjtzOjE2OiJUb21vcnJvdzogSmFuIDEyIjtzOjk6InJpZ2h0TGluayI7YjoxO30%3D&flex%5BCalendar_mainCal%5D%5Bbegindate%5D=January+11%2C+2013&flex%5BCalendar_mainCal%5D%5Benddate%5D=January+11%2C+2013&flex%5BCalendar_mainCal%5D%5Bimpacts%5D%5Bhigh%5D=high&flex%5BCalendar_mainCal%5D%5B_cbarray_%5D=1&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Baud%5D=aud&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bcad%5D=cad&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bchf%5D=chf&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bcny%5D=cny&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Beur%5D=eur&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bgbp%5D=gbp&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bjpy%5D=jpy&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Bnzd%5D=nzd&flex%5BCalendar_mainCal%5D%5Bcurrencies%5D%5Busd%5D=usd&flex%5BCalendar_mainCal%5D%5B_cbarray_%5D=1&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bgrowth%5D=growth&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Binflation%5D=inflation&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bemployment%5D=employment&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bcentralbank%5D=centralbank&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bbonds%5D=bonds&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bhousing%5D=housing&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bsentiment%5D=sentiment&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bpmi%5D=pmi&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bspeeches%5D=speeches&flex%5BCalendar_mainCal%5D%5Beventtypes%5D%5Bmisc%5D=misc&flex%5BCalendar_mainCal%5D%5B_cbarray_%5D=1&false").ConfigureAwait(false);
            }

            return rawResult;
        }
    }
}

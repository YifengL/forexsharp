using System;
using FXSharp.EA.NewsBox;
//using TradePlatform.MT4.Data;
using FXSharp.TradingPlatform.Exts;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace FXSharp.EA.FirstTest
{
    public class MyExpertAdvisor : EExpertAdvisor
    {
        Order order = null;
        int count = 0;

        protected override int Init()
        {
            //order = Sell(0.1, Ask + 500 * Point, Bid - 500 * Point);
            
            return 1;
        }

        protected override int DeInit()
        {

            
            return 1;
        }

        protected override int Start()
        {
            for (int i = 1; i <= 5; i++)
            {
                var builder = new StringBuilder();

                for (int j = 31; j >= 0; j--)
                {
                    var idx = j * i;
                    var close = this.Close[i];
                    builder.Append(close);
                    builder.Append(",");
                }

                File.WriteAllText(string.Format("dataset-{0}.csv", i), builder.ToString());
            }

            //TestSpread();
            return 1;
        }

        private void TestSpread()
        {
            var pairs = CurrencyPairRegistry.CurrencyPairs;

            var spreadInfos = new List<SpreadInfo>();

            foreach (var pair in pairs)
            {
                var spread = MarketInfo(pair, TradePlatform.MT4.SDK.API.MARKER_INFO_MODE.MODE_SPREAD);
                spreadInfos.Add(new SpreadInfo {Symbol = pair, Spread = spread});
            }

            var pSp = spreadInfos.OrderBy(x => x.Spread).Select(x => x.ToString());

            CurrencyPairRegistry.FilterCurrencyForMinimalSpread(this);
            var currencies = CurrencyPairRegistry.CurrencyPairs;
            //count++;

            //if (order == null)
            //{
            //    Init();
            //}
            //else
            //{
            //    order.ModifyStopLoss(Bid + (count * 100 * Point));
            //}

            //if (order.CloseInProfit())
            //    order = null;
        }
    }
}

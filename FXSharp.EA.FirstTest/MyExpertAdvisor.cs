using System;
using FXSharp.EA.NewsBox;
//using TradePlatform.MT4.Data;
using FXSharp.TradingPlatform.Exts;
using System.Collections.Generic;
using System.Linq;

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
            var pairs = CurrencyPairRegistry.CurrencyPairs;

            var spreadInfos = new List<SpreadInfo>();

            foreach (var pair in pairs)
            {
                var spread = MarketInfo(pair, TradePlatform.MT4.SDK.API.MARKER_INFO_MODE.MODE_SPREAD);
                spreadInfos.Add(new SpreadInfo {Symbol = pair, Spread = spread });
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
            return 1;
        }
    }
}

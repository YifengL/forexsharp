using TradePlatform.MT4.SDK.API;
using System;
using FXSharp.TradingPlatform.Exts;
//using TradePlatform.MT4.Data;
namespace FXSharp.EA.FirstTest
{   
    public class BuyFunctionTest : EExpertAdvisor
    {
        Order order = null;
        int tickCount = 0;

        protected override int Init()
        {
            order = Buy(0.1, Bid - 100 * Point, Bid + 500 * Point);

            return 1;
        }

        protected override int DeInit()
        {
            return 1;
        }

        protected override int Start()
        {
            tickCount++;

            if (tickCount != 10) return 1;

            if (order == null)
            {
                order = Buy(0.1, Bid - 100 * Point, Bid + 500 * Point);
            }
            else
            {
                order.Close();
                order = null;
            }

            tickCount = 0;
            
            return 1;
        }
    }
}

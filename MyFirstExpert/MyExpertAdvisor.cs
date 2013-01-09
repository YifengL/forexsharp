//using TradePlatform.MT4.Data;
using FXSharp.TradingPlatform.Exts;
namespace MyFirstExpert
{
    public class MyExpertAdvisor : EExpertAdvisor
    {
        Order order = null;
        int count = 0;

        protected override int Init()
        {
            order = Sell(0.1, Ask + 500 * Point, Bid - 500 * Point);
            
            return 1;
        }

        protected override int DeInit()
        {
            
            return 1;
        }

        protected override int Start()
        {
            count++;

            if (order == null)
            {
                Init();
            }
            else
            {
                order.ModifyStopLoss(Bid + (count * 100 * Point));
            }

            //if (order.CloseInProfit())
            //    order = null;
            return 1;
        }
    }
}

//using TradePlatform.MT4.Data;
namespace MyFirstExpert
{
    public class MyExpertAdvisor : EExpertAdvisor
    {
        Order order = null;

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
            if (order == null)
            {
                Init();
            }

            if (order.CloseInProfit())
                order = null;
            return 1;
        }
    }
}

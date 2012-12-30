using System;

namespace MyFirstExpert
{
    public class HappyDayEA : EExpertAdvisor
    {
        private DateTime prevtime = default(DateTime);
        private Order order = null;
        //private bool IsBuy = true;
        private double stopLoss = 100;
        protected override int Init()
        {
            prevtime = Time[0];
            return (0);
        }

        private int deinit()
        {
            return (0);
        }

        protected override int Start()
        {
            if (IsNewBar())
            {
                Print("Hei theres a new bar");

                if (IsThereOpenOrder())
                {
                    CloseOpenOrder();
                }

                if (IsPreviouslyBulishCandle())
                {
                    order = Buy(0.1, BuyClosePrice - (stopLoss * Point), BuyClosePrice + (2 * stopLoss * Point));
                    //IsBuy = true;
                }
                else
                {
                    order = Sell(0.1, SellClosePrice + (stopLoss * Point), SellClosePrice - (2 * stopLoss * Point));
                    //IsBuy = false;
                }
            }
            else
            {
                Print("Wait pal");
            }
            return (0);
        }

        private bool IsNewBar()
        {
            if (prevtime != Time[0])
            {
                prevtime = Time[0];
                return (true);
            }

            return (false);
        }

        private void CloseOpenOrder()
        {
            order.Close();
            order = null;
        }

        //private double CloseOrderPrice()
        //{
        //    if (IsBuy) return (Bid);
        //    else return (Ask);
        //}

        private bool IsThereOpenOrder()
        {
            return (order != null);
        }

        private bool IsPreviouslyBulishCandle()
        {
            return (Close[1] >= Open[1]);
        }

        protected override int DeInit()
        {
            return 0;
        }
    }
}

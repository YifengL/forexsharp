using FXSharp.TradingPlatform.Exts;
using System;

namespace FXSharp.EA.HappyDay
{
    public class HappyDayEA : EExpertAdvisor
    {
        private DateTime prevtime = default(DateTime);
        private Order order = null;
        private double stopLoss = 100;
        private double takeProfit = 200;
        private double lotSize = 1;
        private ITrailingMethod trailingMethod;

        protected override int Init()
        {
            //stopLoss = ATR;
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
                    //order = Buy(0.1, BuyClosePrice - stopLoss, BuyClosePrice + 2 * stopLoss);
                    //order = Buy(0.1, BuyClosePrice - (stopLoss * Point), BuyClosePrice + (4 * stopLoss * Point));
                    double buyStopPrice = BuyOpenPrice + 50 * Point;
                    double buyStopClosePrice = BuyClosePrice + 50 * Point;
                    //order = PendingBuy(0.1, buyStopPrice, buyStopClosePrice - (stopLoss * Point), buyStopClosePrice + (4 * stopLoss * Point));
                    order = PendingBuy(lotSize, buyStopPrice, buyStopClosePrice - (stopLoss * Point), buyStopClosePrice + (takeProfit * Point));
                    trailingMethod = new BuyTrailingMethod(order, this);
                    Print("Buy");
                }
                else
                {
                    //order = Sell(0.1, SellClosePrice + stopLoss, SellClosePrice - 2 * stopLoss);
                    //order = Sell(0.1, SellClosePrice + (stopLoss * Point), SellClosePrice - (4 * stopLoss * Point));
                    double sellStopPrice = SellOpenPrice - 50 * Point;
                    double sellStopClosePrice = SellClosePrice - 50 * Point;
                    order = PendingSell(lotSize, sellStopPrice, sellStopClosePrice + (stopLoss * Point), sellStopClosePrice - (takeProfit * Point));
                    //order = PendingSell(0.1, sellStopPrice, sellStopClosePrice + (stopLoss * Point), sellStopClosePrice - (4 * stopLoss * Point));
                    trailingMethod = new SellTrailingMethod(order, this);
                    Print("Sell");
                }
            }
            else
            {
                //if (IsThereOpenOrder())
                //{
                //    trailingMethod.Trail();
                //}

                if (IsThereRunningOrder())
                {
                    trailingMethod.Trail();
                }

                Print("Wait pal");
            }
            return (0);
        }

        private bool IsThereRunningOrder()
        {
            return IsThereOpenOrder() && order.IsRunning;
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

        private bool IsThereOpenOrder()
        {
            if (order == null) return false;
            
            if (!order.IsOpen)
            {
                order = null;
                return false;
            }

            return true;
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

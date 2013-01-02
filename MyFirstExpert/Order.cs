using System;
using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert
{
    public class Order
    {
        private int ticket;
        private EExpertAdvisor ea;
        private double lots;
        private ORDER_TYPE orderType;
        private DateTime NULL_TIME = new DateTime(621355968000000000);
        private double openPrice;

        public Order(int ticket, double lots, ORDER_TYPE orderType, EExpertAdvisor ea)
        {
            this.ticket = ticket;
            this.ea = ea;
            this.lots = lots;
            this.orderType = orderType;
        }

        public void Close()
        {
            bool success = false;

            if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) && ea.OrderCloseTime() != NULL_TIME)
                return;

            // should refactor to hierarcy

            if (orderType == ORDER_TYPE.OP_BUY)
            {
                success = ea.OrderClose(ticket, lots, ea.BuyClosePrice, 0);
            }
            else if (orderType == ORDER_TYPE.OP_SELL)
            {
                success = ea.OrderClose(ticket, lots, ea.SellClosePrice, 0);
            }

            if (!success)
                ea.ThrowLatestException();
        }

        public double Profit
        {
            get
            {
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderProfit();

                ea.ThrowLatestException();

                return -1;
            }
        }

        public bool CloseInProfit()
        {
            if (Profit < 0.0) return false;

            Close();

            return true;
        }

        public void ModifyStopLoss(double newStopLoss)
        {
            bool success = false;
            // should throw exception for each call
            if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                success = ea.OrderModify(ticket, ea.OrderOpenPrice(), newStopLoss, ea.OrderTakeProfit(),
                                         DateTime.Now.AddDays(100), -1);

            if (!success)
            {
                ea.ThrowLatestException();
            }
        }

        public double OpenPrice
        {
            get
            {
                if (openPrice == default(double))
                {
                    // should throw exception for each call
                    if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                        openPrice = ea.OrderOpenPrice();

                    ea.ThrowLatestException();
                }

                return openPrice;
            }
        }

        public double StopLoss
        {
            get
            {
                // should cache stop loss 

                bool success = false;
                // should throw exception for each call
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderStopLoss();

                if (!success)
                {
                    ea.ThrowLatestException();
                }

                return 0;
            }
        }

        public bool IsOpen
        {
            get { return ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) && ea.OrderCloseTime() == NULL_TIME; }
        }
    }
}

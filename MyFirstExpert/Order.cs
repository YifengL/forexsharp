using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert
{
    public class Order
    {
        private int ticket;
        private EExpertAdvisor ea;
        private double lots;
        ORDER_TYPE orderType;

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
    }
}

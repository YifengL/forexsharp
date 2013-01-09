using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxCreated : IOrderState
    {
        private Order buyOrder;
        private Order sellOrder;
        private OrderManager orderManager;

        public MagicBoxCreated(OrderManager orderManager, Order buyOrder, Order sellOrder)
        {
            this.orderManager = orderManager;
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
        }

        public void Manage()
        {
            // also expired by timer => should attached expired the pending order.. not do it manually. 
            if (buyOrder.IsRunning)
            {
                sellOrder.Close();
                orderManager.OrderRunning(buyOrder);
            }
            else if (sellOrder.IsRunning)
            {
                buyOrder.Close();
                orderManager.OrderRunning(sellOrder);
            }
            else
            {
                // check if it's already expired
            }
        }
    }
}

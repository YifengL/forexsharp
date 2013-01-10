using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxCreated : IOrderState
    {
        private Order buyOrder;
        private Order sellOrder;
        private OrderManager orderManager;
        private AutoCloseOrder autoClose;

        public MagicBoxCreated(OrderManager orderManager, Order buyOrder, Order sellOrder)
        {
            this.orderManager = orderManager;
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
            this.autoClose = new AutoCloseOrder();
        }

        public void Manage()
        {
            // also expired by timer => should attached expired the pending order.. not do it manually. 
            // i think we should make sure the order dissapear

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
            else if (autoClose.IsExpired)
            {
                buyOrder.Close();
                sellOrder.Close();
                orderManager.MagicBoxCompleted(); 
                // should describe event better, so we can publish it to different endpoint
            }
        }
    }
}

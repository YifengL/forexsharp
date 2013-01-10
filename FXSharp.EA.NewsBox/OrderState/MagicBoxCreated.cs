using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxCreated : IOrderState
    {
        private Order buyOrder;
        private Order sellOrder;
        private OrderWatcher orderManager;
        private bool cancel = false;
        //private ExpiracyTimer autoClose;

        public MagicBoxCreated(OrderWatcher orderManager, Order buyOrder, Order sellOrder)
        {
            this.orderManager = orderManager;
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
            //this.autoClose = new ExpiracyTimer();
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
            else if (cancel)
            {
                buyOrder.Close();
                sellOrder.Close();
                orderManager.MagicBoxCompleted(); 
                // should describe event better, so we can publish it to different endpoint
            }
        }


        public void Cancel()
        {
            cancel = true;
        }
    }
}

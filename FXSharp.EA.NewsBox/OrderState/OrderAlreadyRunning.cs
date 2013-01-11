using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class OrderAlreadyRunning : IOrderState
    {
        private OrderWatcher orderManager;
        private Order order;
        private bool cancel = false;

        public OrderAlreadyRunning(OrderWatcher orderManager, Order order)
        {
            this.orderManager = orderManager;
            this.order = order;
        }

        public void Manage()
        {
            // should trail and lock profit

            // should create timer for expired

            if (cancel)
            {
                order.Close();
                orderManager.MagicBoxCompleted();
            }
            else if (!order.IsOpen)
            {
                orderManager.MagicBoxCompleted();
            }
        }

        public void Cancel()
        {
            cancel = true;
        }
    }
}

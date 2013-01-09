using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class OrderAlreadyRunning : IOrderState
    {
        private OrderManager orderManager;
        private Order order;
        private AutoCloseOrder autoClose;

        public OrderAlreadyRunning(OrderManager orderManager, Order order)
        {
            this.orderManager = orderManager;
            this.order = order;
            this.autoClose = new AutoCloseOrder();
        }

        public void Manage()
        {
            // should trail and lock profit

            // should create timer for expired

            if (autoClose.IsExpired)
            {
                order.Close();
                orderManager.MagicBoxCompleted();
            }
        }

    }
}

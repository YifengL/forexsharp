using FXSharp.TradingPlatform.Exts;
using System;

namespace FXSharp.EA.NewsBox
{
    public class OrderAlreadyRunning : IOrderState
    {
        private OrderManager orderManager;
        private Order order;

        public OrderAlreadyRunning(OrderManager orderManager, Order order)
        {
            this.orderManager = orderManager;
            this.order = order;
        }

        public void Manage()
        {
            // should trail and lock profit

            // should create timer for expired

            throw new NotImplementedException();
        }
    }
}

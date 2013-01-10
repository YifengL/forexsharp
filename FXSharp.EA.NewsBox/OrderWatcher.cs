using FXSharp.TradingPlatform.Exts;
using System;

namespace FXSharp.EA.NewsBox
{
    public class OrderWatcher
    {
        IOrderState state;
        public event EventHandler OrderClosed;
        
        public OrderWatcher(Order buyOrder, Order sellOrder)
        {
            AddOneCancelAnother(buyOrder, sellOrder);
        }

        internal void ManageOrder()
        {
            if (state == null) return;

            state.Manage();
        }

        private void AddOneCancelAnother(Order buyOrder, Order sellOrder)
        {
            state = new MagicBoxCreated(this, buyOrder, sellOrder);
        }

        // we should use event based for this
        internal void OrderRunning(Order order)
        {
            state = new OrderAlreadyRunning(this, order);
        }

        // we should use event based for this
        internal void MagicBoxCompleted()
        {
            // should create default state
            state = null;

            OnOrderClosed();
        }

        private void OnOrderClosed()
        {
            if (OrderClosed == null) return;
            OrderClosed(this, EventArgs.Empty);
        }
    }
}

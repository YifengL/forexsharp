using FXSharp.TradingPlatform.Exts;
using System;

namespace FXSharp.EA.NewsBox
{
    public class OrderWatcher
    {
        IOrderState state;
        public event EventHandler OrderClosed;
        private ExpiracyTimer expiracyTimer;
        private double expiredTime;
        
        public OrderWatcher(Order buyOrder, Order sellOrder, double expiredTime)
        {
            AddOneCancelAnother(buyOrder, sellOrder);
            expiracyTimer = new ExpiracyTimer(expiredTime);
            expiracyTimer.Expired += expiracyTimer_Expired;
        }

        void expiracyTimer_Expired(object sender, EventArgs e)
        {
            if (state == null) return;
            
            state.Cancel();
            expiracyTimer.Expired -= expiracyTimer_Expired;
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
            expiracyTimer.Finish();
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

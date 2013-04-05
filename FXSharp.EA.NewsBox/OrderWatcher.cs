using System;
using FXSharp.TradingPlatform.Exts;
using FXSharp.EA.OrderManagements;

namespace FXSharp.EA.NewsBox
{
    public class OrderWatcher
    {
        private readonly double expiredTime;
        private readonly MagicBoxConfig mbConfig;
        private ExpiracyTimer expiracyTimer;
        private IOrderState state;

        public OrderWatcher(Order buyOrder, Order sellOrder, double expiredTime, MagicBoxConfig config)
        {
            mbConfig = config;
            AddOneCancelAnother(buyOrder, sellOrder);
            Symbol = buyOrder.Symbol;

            this.expiredTime = expiredTime;

            expiracyTimer = new ExpiracyTimer(expiredTime/2);
            expiracyTimer.Expired += expiracyTimer_Expired;
        }

        public string Symbol { get; set; }
        public event EventHandler OrderClosed;

        private void expiracyTimer_Expired(object sender, EventArgs e)
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
            state = new MagicBoxCreated(this, buyOrder, sellOrder, mbConfig);
        }

        // we should use event based for this
        internal void OrderRunning(Order order, IProfitProtector trailing)
        {
            ResetExpiracy();

            state = new OrderAlreadyRunning(this, order, trailing);
        }

        private void ResetExpiracy()
        {
            expiracyTimer.Finish();
            expiracyTimer.Expired -= expiracyTimer_Expired;
            expiracyTimer = new ExpiracyTimer(expiredTime);
            expiracyTimer.Expired += expiracyTimer_Expired;
        }

        // we should use event based for this
        internal void MagicBoxCompleted()
        {
            // should create default state
            expiracyTimer.Finish();
            expiracyTimer.Expired -= expiracyTimer_Expired;

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
using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class OrderAlreadyRunning : IOrderState
    {
        private readonly Order order;
        private readonly OrderWatcher orderManager;
        private readonly IProfitProtector trailing;
        private bool cancel;

        public OrderAlreadyRunning(OrderWatcher orderManager, Order order, IProfitProtector trailing)
        {
            this.orderManager = orderManager;
            this.order = order;
            this.trailing = trailing;
        }

        public void Manage()
        {
            if (cancel)
            {
                order.Close();
                orderManager.MagicBoxCompleted();
            }
            else if (!order.IsOpen)
            {
                orderManager.MagicBoxCompleted();
            }
            else
            {
                //trailing.Trail();
                trailing.TryProtectProfit();
            }
        }

        public void Cancel()
        {
            cancel = true;
        }
    }
}
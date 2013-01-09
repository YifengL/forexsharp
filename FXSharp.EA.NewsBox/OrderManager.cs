using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class OrderManager
    {
        IOrderState state;

        internal void ManageOrder()
        {
            if (state == null) return;

            state.Manage();
        }

        internal void AddOneCancelAnother(Order buyOrder, Order sellOrder)
        {
            state = new MagicBoxCreated(this, buyOrder, sellOrder);
        }

        internal void OrderRunning(Order order)
        {
            state = new OrderAlreadyRunning(this, order);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
namespace FXSharp.EA.NewsBox
{
    class OrderWatcherPool
    {
        IList<OrderWatcher> orderWatchers = new List<OrderWatcher>(); // be careful race condition and deadlock

        internal void ManageAllOrder()
        {
            var cloned = orderWatchers.ToList(); // clone to avoid race condition

            foreach (var order in cloned)
            {
                order.ManageOrder();
            }
        }

        internal void Add(OrderWatcher watcher)
        {
            watcher.OrderClosed += watcher_OrderClosed;

            orderWatchers.Add(watcher);
        }

        void watcher_OrderClosed(object sender, System.EventArgs e)
        {
            var watcher = (OrderWatcher)sender;
            orderWatchers.Remove(watcher);
            watcher.OrderClosed -= watcher_OrderClosed;
        }

        internal bool ContainsOrderForSymbol(string currencyPairs)
        {
            return orderWatchers.FirstOrDefault(x => x.Symbol == currencyPairs) != null;
        }
    }
}

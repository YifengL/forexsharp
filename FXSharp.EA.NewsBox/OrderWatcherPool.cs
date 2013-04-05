using System;
using System.Collections.Generic;
using System.Linq;

namespace FXSharp.EA.NewsBox
{
    internal class OrderWatcherPool
    {
        private readonly IList<OrderWatcher> orderWatchers = new List<OrderWatcher>();
                                             // be careful race condition and deadlock

        internal void ManageAllOrder()
        {
            List<OrderWatcher> cloned = orderWatchers.ToList(); // clone to avoid race condition

            foreach (OrderWatcher order in cloned)
            {
                order.ManageOrder();
            }
        }

        internal void Add(OrderWatcher watcher)
        {
            watcher.OrderClosed += watcher_OrderClosed;

            orderWatchers.Add(watcher);
        }

        private void watcher_OrderClosed(object sender, EventArgs e)
        {
            var watcher = (OrderWatcher) sender;
            orderWatchers.Remove(watcher);
            watcher.OrderClosed -= watcher_OrderClosed;
        }

        internal bool ContainsOrderForSymbol(string currencyPairs)
        {
            return orderWatchers.FirstOrDefault(x => x.Symbol == currencyPairs) != null;
        }
    }
}
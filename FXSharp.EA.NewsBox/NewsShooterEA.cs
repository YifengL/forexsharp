using FXSharp.TradingPlatform.Exts;
using System.Collections.Concurrent;
namespace FXSharp.EA.NewsBox
{
    public class NewsShooterEA : EExpertAdvisor
    {
        private double lotSize = 0.1;
        private double range = 100;
        private double takeProfit = 100;
        private double stopLoss = 100;

        private NewsReminder reminder;
        private OrderWatcherPool orderPool;

        protected override int DeInit()
        {
            return 0;
        }

        protected override int Init()
        {
            orderPool = new OrderWatcherPool();

            reminder = new NewsReminder();
            reminder.Start();

            return 0;
        }

        protected override int Start()
        {
            while (reminder.IsAvailable)
            {
                MagicBoxOrder result = null;

                reminder.OrderQueue.TryDequeue(out result);

                if (result == null) continue;

                CreateOrderBox(result); // only manage one news at the time, should manage all
            }

            orderPool.ManageAllOrder();

            //orderManager.ManageOrder();

            // [x]1. one Cancel another
            // [x]3. delete after 10 minutes, Expired

            // 2. trailing stop and Lock Profit
            
            // 4. money management
            return 0;
        }

        private void CreateOrderBox(MagicBoxOrder magicBox)
        {
            var buyOrder = PendingBuy(magicBox.Symbol, lotSize,
                        BuyOpenPriceFor(magicBox.Symbol) + range * PointFor(Symbol),
                        BuyClosePriceFor(magicBox.Symbol) + ((range - stopLoss) * PointFor(Symbol)),
                        BuyClosePriceFor(magicBox.Symbol) + ((range + takeProfit) * PointFor(Symbol)));

            var sellOrder = PendingSell(magicBox.Symbol, lotSize,
                        SellOpenPriceFor(magicBox.Symbol) - range * PointFor(Symbol),
                        SellClosePriceFor(magicBox.Symbol) - ((range - stopLoss) * PointFor(Symbol)),
                        SellClosePriceFor(magicBox.Symbol) - ((range + takeProfit) * PointFor(Symbol)));

            orderPool.Add(new OrderWatcher(buyOrder, sellOrder));
        }
    }
}

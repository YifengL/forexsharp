using FXSharp.TradingPlatform.Exts;
using System.Collections.Concurrent;
namespace FXSharp.EA.NewsBox
{
    public class NewsShooterEA : EExpertAdvisor
    {
        private NewsReminder reminder;
        private OrderWatcherPool orderPool;

        private bool initialized = false;

        protected override int DeInit()
        {
            return 0;
        }

        protected override int Init()
        {
            orderPool = new OrderWatcherPool();

            reminder = new NewsReminder();
            reminder.Start();

            initialized = true;
            return 0;
        }

        protected override int Start()
        {
            MakeSureAlreadyInitialized();

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

        private void MakeSureAlreadyInitialized()
        {
            if (initialized) return;

            Init();
        }

        private void CreateOrderBox(MagicBoxOrder magicBox)
        {
            /// need to refactor this messs into another class
            double lotSize = magicBox.LotSize;
            double range = magicBox.Range;
            double takeProfit = magicBox.TakeProfit;
            double stopLoss = magicBox.StopLoss;
            double expiredTime = magicBox.MinuteExpiracy;

            var buyOrder = PendingBuy(magicBox.Symbol, lotSize,
                        BuyOpenPriceFor(magicBox.Symbol) + range * PointFor(Symbol),
                        BuyClosePriceFor(magicBox.Symbol) + ((range - stopLoss) * PointFor(Symbol)),
                        BuyClosePriceFor(magicBox.Symbol) + ((range + takeProfit) * PointFor(Symbol)));

            var sellOrder = PendingSell(magicBox.Symbol, lotSize,
                        SellOpenPriceFor(magicBox.Symbol) - range * PointFor(Symbol),
                        SellClosePriceFor(magicBox.Symbol) - ((range - stopLoss) * PointFor(Symbol)),
                        SellClosePriceFor(magicBox.Symbol) - ((range + takeProfit) * PointFor(Symbol)));

            orderPool.Add(new OrderWatcher(buyOrder, sellOrder, expiredTime));
        }
    }
}

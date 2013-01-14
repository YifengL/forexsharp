using FXSharp.TradingPlatform.Exts;
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

                CreateOrderBox(result);
            }

            orderPool.ManageAllOrder();
            
            // 2. trailing stop and Lock Profit
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
            
            double range = magicBox.Range;
            double takeProfit = magicBox.TakeProfit;
            double stopLoss = magicBox.StopLoss;
            double expiredTime = magicBox.MinuteExpiracy;
            
            var moneyManagement = new MoneyManagement(2, this.Balance);

            double lotSize = moneyManagement.CalculateLotSize(magicBox);

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

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
            // should filter when the order is created. currently just do this simple things
            CurrencyPairRegistry.FilterCurrencyForMinimalSpread(this);

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
            double takeProfit = 0; // nullify take profit 
            double stopLoss = 0; // nullify stop loss, should set after enter the trade.
            double expiredTime = magicBox.MinuteExpiracy;
            
            var moneyManagement = new MoneyManagement(5, this.Balance);

            double lotSize = moneyManagement.CalculateLotSize(magicBox);

            var buyOrder = PendingBuy(magicBox.Symbol, lotSize,
                        BuyOpenPriceFor(magicBox.Symbol) + range * PointFor(magicBox.Symbol));

            var sellOrder = PendingSell(magicBox.Symbol, lotSize,
                        SellOpenPriceFor(magicBox.Symbol) - range * PointFor(magicBox.Symbol));

            orderPool.Add(new OrderWatcher(buyOrder, sellOrder, expiredTime, magicBox.Config));
        }
    }
}

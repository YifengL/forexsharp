using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class NewsShooterEA : EExpertAdvisor
    {
        private ICurrencyRepository currencyRepository;
        private bool initialized;
        private OrderWatcherPool orderPool;
        private NewsReminder reminder;

        protected override int DeInit()
        {
            reminder.Stop();
            return 0;
        }

        protected override int Init()
        {
            orderPool = new OrderWatcherPool();

            reminder = new NewsReminder();
            reminder.Start();

            //currencyRepository = new LowestSpreadsRelatedPairsRepository();
            currencyRepository = new MajorRelatedPairsRepository();
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
            double takeProfit = magicBox.TakeProfit; // nullify take profit 
            double stopLoss = magicBox.StopLoss; // nullify stop loss, should set after enter the trade.
            double expiredTime = magicBox.MinuteExpiracy;

            var moneyManagement = new MoneyManagement(1, Balance);

            double lotSize = moneyManagement.CalculateLotSize(magicBox.StopLoss);

            foreach (string currencyPairs in currencyRepository.GetRelatedCurrencyPairs(this, magicBox.Symbol))
            {
                // check if the order has been created for this pair
                if (orderPool.ContainsOrderForSymbol(currencyPairs)) continue;

                // refactor to next class and cache the traded value ...(don't let it duplicated like tonight usdcad pairs)
                // the logic should be in orderPool
                Order buyOrder = PendingBuy(currencyPairs, lotSize,
                                            BuyOpenPriceFor(currencyPairs) + range*PointFor(currencyPairs));
                buyOrder.ChangeStopLossInPoints(magicBox.StopLoss);
                buyOrder.ChangeTakeProfitInPoints(magicBox.TakeProfit);

                Order sellOrder = PendingSell(currencyPairs, lotSize,
                                              SellOpenPriceFor(currencyPairs) - range*PointFor(currencyPairs));
                sellOrder.ChangeStopLossInPoints(magicBox.StopLoss);
                sellOrder.ChangeTakeProfitInPoints(magicBox.TakeProfit);

                //var buyOrder = PendingBuy(magicBox.Symbol, lotSize,
                //    BuyOpenPriceFor(magicBox.Symbol) + range * PointFor(magicBox.Symbol),
                //    BuyClosePriceFor(magicBox.Symbol) + ((range - stopLoss) * PointFor(magicBox.Symbol)),
                //    BuyClosePriceFor(magicBox.Symbol) + ((range + takeProfit) * PointFor(magicBox.Symbol)));

                //var sellOrder = PendingSell(magicBox.Symbol, lotSize,
                //    SellOpenPriceFor(magicBox.Symbol) - range * PointFor(magicBox.Symbol),
                //    SellClosePriceFor(magicBox.Symbol) - ((range - stopLoss) * PointFor(magicBox.Symbol)),
                //    SellClosePriceFor(magicBox.Symbol) - ((range + takeProfit) * PointFor(magicBox.Symbol)));

                orderPool.Add(new OrderWatcher(buyOrder, sellOrder, expiredTime, magicBox.Config));
            }
        }
    }
}
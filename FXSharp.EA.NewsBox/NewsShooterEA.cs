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
        private OrderManager orderManager;

        protected override int DeInit()
        {
            return 0;
        }

        protected override int Init()
        {
            reminder = new NewsReminder();
            reminder.Start();

            orderManager = new OrderManager();
            return 0;
        }

        protected override int Start()
        {
            while (reminder.IsAvailable)
            {
                MagicBoxOrder result = null;

                reminder.OrderQueue.TryDequeue(out result);

                if (result == null) continue;

                CreateOrderBox(result);
            }

            orderManager.ManageOrder();

            // 1. one Cancel another
            // 2. trailing stop and Lock Profit
            // 3. delete after 10 minutes, Expired
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

            orderManager.AddOneCancelAnother(buyOrder, sellOrder);
        }
    }
}

using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxCreated : IOrderState
    {
        private Order buyOrder;
        private Order sellOrder;
        private OrderWatcher orderManager;
        private bool cancel = false;
        private MagicBoxConfig mbConfig;

        public MagicBoxCreated(OrderWatcher orderManager, Order buyOrder, Order sellOrder, MagicBoxConfig config)
        {
            this.orderManager = orderManager;
            this.buyOrder = buyOrder;
            this.sellOrder = sellOrder;
            this.mbConfig = config;
        }

        public void Manage()
        {
            // also expired by timer => should attached expired the pending order.. not do it manually. 
            // i think we should make sure the order dissapear

            if (buyOrder.IsRunning)
            {
                buyOrder.ChangeTakeProfitInPoints(mbConfig.TakeProfit);
                buyOrder.ChangeStopLossInPoints(mbConfig.StopLoss);
                sellOrder.Close();
                orderManager.OrderRunning(buyOrder, new BuyTrailingMethod(new OrderTrailingInfo(buyOrder)));
            }
            else if (sellOrder.IsRunning)
            {
                sellOrder.ChangeTakeProfitInPoints(mbConfig.TakeProfit);
                sellOrder.ChangeStopLossInPoints(mbConfig.StopLoss);
                buyOrder.Close();
                orderManager.OrderRunning(sellOrder, new SellTrailingMethod(new OrderTrailingInfo(sellOrder)));
            }
            else if (cancel)
            {
                buyOrder.Close();
                sellOrder.Close();
                orderManager.MagicBoxCompleted(); 
                // should describe event better, so we can publish it to different endpoint
            }
        }


        public void Cancel()
        {
            cancel = true;
        }
    }
}

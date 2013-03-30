using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.HappyDay
{
    internal class OrderRunningManager : IOrderManager
    {
        private readonly HappyDayEA _ea;
        private readonly Order _order;
        private readonly IProfitProtector _profitProtector;
        public OrderRunningManager(Order order, HappyDayEA happyDayEA)
        {
            _order = order;
            _ea = happyDayEA;
            _profitProtector = new ThreeLevelProfitProtector(order);
        }

        public void OnNewBar()
        {
            CloseRunningOrder();
            _ea.CreatedMagicBoxFromPreviousCandle();
            _ea.ChangeStateToBoxAlreadyCreated();
        }

        public void OnTick()
        {
            // do trailing mechanism with multiple strategy

            if (_order.IsRunning && _order.IsOpen)
            {
                _profitProtector.TryProtectProfit();
            }
            else
            {
                _ea.OrderCompleted();
            }

            // change state to NoOrderCreatedManagement
        }

        private void CloseRunningOrder()
        {
            _order.Close();
        }
    }
}
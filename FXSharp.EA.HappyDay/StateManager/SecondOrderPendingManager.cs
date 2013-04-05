using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.HappyDay
{
    internal class SecondOrderPendingManager : IOrderManager
    {
        private readonly HappyDayEA _ea;
        private readonly Order _pendingOrder;
        private readonly IProfitProtector _profitProtector;

        public SecondOrderPendingManager(Order pendingOrder, HappyDayEA happyDayEA)
        {
            _pendingOrder = pendingOrder;
            _ea = happyDayEA;
            _profitProtector = new ThreeLevelProfitProtector(pendingOrder);
        }

        public void OnTick()
        {
            if (_pendingOrder.IsRunning)
            {
                _ea.ChangeStateToSecondOrderRunning(_pendingOrder);
            }
        }

        private void CloseRunningOrder()
        {
            _pendingOrder.Close();
        }


        public void OnLondonOpen()
        {
            
        }

        public void OnNewYorkClose()
        {
            CloseRunningOrder();
            _ea.OrderCompleted();
        }
    }
}
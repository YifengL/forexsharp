using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.HappyDay
{
    internal class FirstOrderRunningManager : IOrderManager
    {
        private readonly HappyDayEA _ea;
        private readonly Order _runningOrder;
        private readonly Order _pendingOrder;
        private readonly IProfitProtector _profitProtector;

        public FirstOrderRunningManager(Order runningOrder, Order pendingOrder, HappyDayEA happyDayEA)
        {
            _runningOrder = runningOrder;
            _pendingOrder = pendingOrder;
            _ea = happyDayEA;
            _profitProtector = new ThreeLevelProfitProtector(_runningOrder);
        }

        public void OnTick()
        {
            if (_runningOrder.IsRunning && _runningOrder.IsOpen)
            {
                //_profitProtector.TryProtectProfit();
            }
            else
            {
                _ea.FirstOrderCompleted(_pendingOrder);
            }
        }

        public void OnLondonOpen()
        {
            
        }

        public void OnNewYorkClose()
        {
            _runningOrder.Close();
            _pendingOrder.Close();
            _ea.OrderCompleted();
            //CloseRunningOrder();
            //_ea.CreatedMagicBoxFromPreviousCandle();
            //_ea.ChangeStateToBoxAlreadyCreated();
        }
    }
}
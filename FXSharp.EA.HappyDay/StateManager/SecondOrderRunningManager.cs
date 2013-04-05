using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.HappyDay
{
    internal class SecondOrderRunningManager : IOrderManager
    {
        private readonly HappyDayEA _ea;
        private readonly Order _runningOrder;
        private readonly IProfitProtector _profitProtector;

        public SecondOrderRunningManager(Order runningOrder, HappyDayEA happyDayEA)
        {
            _runningOrder = runningOrder;
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
                _ea.OrderCompleted();
            }
        }

        private void CloseRunningOrder()
        {
            _runningOrder.Close();
        }


        public void OnLondonOpen()
        {
            //throw new System.NotImplementedException();
        }

        public void OnNewYorkClose()
        {
            CloseRunningOrder();
            _ea.OrderCompleted();
        }
    }
}
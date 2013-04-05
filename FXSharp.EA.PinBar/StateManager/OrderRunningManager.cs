using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.PinBar
{
    internal class OrderRunningManager : IOrderManager
    {
        private readonly PinBarEA _ea;
        private readonly Order _order;
        private readonly IProfitProtector _profitProtector;

        public OrderRunningManager(PinBarEA pinBarEA, Order order)
        {
            _ea = pinBarEA;
            _order = order;
            _profitProtector = new ThreeLevelProfitProtector(order);
        }

        public void OnNewBullishPinBar()
        {
            //if (_order.OrderType == ORDER_TYPE.OP_BUY) return;
            //CloseOrder(); // or lock profit ?

            //var order = _ea.CreatePendingBuyOrder();
            //_ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnNewBearishPinBar()
        {
            //if (_order.OrderType == ORDER_TYPE.OP_SELL) return;
            //CloseOrder(); // or lock profit ?

            //var order = _ea.CreatePendingSellOrder();
            //_ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnTick()
        {
            if (_order.IsRunning && _order.IsOpen)
            {
                //_profitProtector.TryProtectProfit();
            }
            else
            {
                _ea.OrderCompleted();
            }
            // should trail and lock profit ?
        }

        private void CloseOrder()
        {
            _order.Close();
            //_ea.OrderCompleted();
        }
    }
}
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.PinBar
{
    internal class PendingOrderCreatedManager : IOrderManager
    {
        private readonly PinBarEA _ea;
        private readonly Order _order;

        public PendingOrderCreatedManager(PinBarEA pinBarEA, Order order)
        {
            _ea = pinBarEA;
            _order = order;
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
            if (_order.IsRunning)
            {
                _ea.MoveToOrderRunningState(_order);
            }
            else if (!_order.IsValid)
            {
                CloseOrder();
                _ea.OrderCompleted();
            }
        }

        private void CloseOrder()
        {
            _order.Close();
            //_ea.OrderCompleted();
        }
    }
}
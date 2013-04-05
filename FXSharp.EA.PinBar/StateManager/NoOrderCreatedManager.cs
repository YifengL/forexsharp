using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.PinBar
{
    internal class NoOrderCreatedManager : IOrderManager
    {
        private readonly PinBarEA _ea;

        public NoOrderCreatedManager(PinBarEA pinBarEA)
        {
            _ea = pinBarEA;
        }

        public void OnNewBullishPinBar()
        {
            Order order = _ea.CreatePendingBuyOrder();
            _ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnNewBearishPinBar()
        {
            Order order = _ea.CreatePendingSellOrder();
            _ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnTick()
        {
        }
    }
}
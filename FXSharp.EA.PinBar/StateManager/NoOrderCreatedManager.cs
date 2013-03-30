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
            var order = _ea.CreatePendingBuyOrder();
            _ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnNewBearishPinBar()
        {
            var order = _ea.CreatePendingSellOrder();
            _ea.MoveToPendingOrderCreatedState(order);
        }

        public void OnTick()
        {
        }
    }
}
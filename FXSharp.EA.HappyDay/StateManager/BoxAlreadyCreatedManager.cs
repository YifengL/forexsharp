using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.HappyDay
{
    internal class BoxAlreadyCreatedManager : IOrderManager
    {
        private readonly Order _buyOrder;
        private readonly HappyDayEA _ea;
        private readonly Order _sellOrder;

        public BoxAlreadyCreatedManager(Order buyOrder, Order sellOrder, HappyDayEA happyDayEA)
        {
            _buyOrder = buyOrder;
            _sellOrder = sellOrder;
            _ea = happyDayEA;
        }

        public void OnNewBar()
        {
            DeletePreviousMagicBox();
            _ea.CreatedMagicBoxFromPreviousCandle();
            _ea.ChangeStateToBoxAlreadyCreated();
        }

        public void OnTick()
        {
            if (_buyOrder.IsRunning)
            {
                _sellOrder.Close();
                _ea.ChangeStateToOrderRunning(_buyOrder);
            }
            else if (_sellOrder.IsRunning)
            {
                _buyOrder.Close();
                _ea.ChangeStateToOrderRunning(_sellOrder);
            }
        }

        private void DeletePreviousMagicBox()
        {
            _ea.Print("Delete previous magic box");
            _buyOrder.Close();
            _sellOrder.Close();
        }
    }
}
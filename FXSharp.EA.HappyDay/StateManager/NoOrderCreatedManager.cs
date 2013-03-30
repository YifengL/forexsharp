namespace FXSharp.EA.HappyDay
{
    internal class NoOrderCreatedManager : IOrderManager
    {
        private readonly HappyDayEA _ea;

        public NoOrderCreatedManager(HappyDayEA ea)
        {
            _ea = ea;
        }

        public void OnNewBar()
        {
            _ea.CreatedMagicBoxFromPreviousCandle();
            _ea.ChangeStateToBoxAlreadyCreated();
        }

        public void OnTick()
        {
            //throw new NotImplementedException();
        }
    }
}
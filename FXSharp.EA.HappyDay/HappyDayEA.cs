using System;
using FXSharp.TradingPlatform.Exts;
using TradePlatform.MT4.SDK.API;
namespace FXSharp.EA.HappyDay
{
    public class HappyDayEA : EExpertAdvisor
    {
        private IOrderManager _orderManagement;
        private const double LotSize = 1;
        private Order _buyOrder = null;
        private Order _sellOrder = null;
        private DateTime prevtime = default(DateTime);
        private double range = 100;
        private bool isInitialize = false;
        private MoneyManagement _moneyManagement;

        protected override int Init()
        {
            _orderManagement = new NoOrderCreatedManager(this);
            
            prevtime = Time[0];
            isInitialize = true;
            return (0);
        }

        internal void CreatedMagicBoxFromPreviousCandle()
        {
            var slPoints = (High[1] - Low[1]) / Point + 2*range;
            _moneyManagement = new MoneyManagement(2, this.Balance);
            var lotSize = _moneyManagement.CalculateLotSize(slPoints);
            var tpPoints = Math.Max(1000 * Point, 2 * slPoints * Point);
            //var tpPoints = 200 * Point;
            // risk reward ratio = 2 * slPoints

            _buyOrder = PendingBuy(Symbol, lotSize, High[1] + range * Point, Low[1] - range * Point, High[1] + tpPoints);

            _sellOrder = PendingSell(Symbol, lotSize, Low[1] - range * Point, High[1] + range * Point, Low[1] - tpPoints);

            this.ObjectsDeleteAll();
        }

        protected override int Start()
        {
            if (!isInitialize)
            {
                Init();
            }

            if (IsNewBar())
            {
                _orderManagement.OnNewBar();
            }
            else
            {
                _orderManagement.OnTick();
            }
            return (0);
        }

        private bool IsNewBar()
        {
            if (prevtime != Time[0])
            {
                prevtime = Time[0];
                return (true);
            }

            return (false);
        }

        protected override int DeInit()
        {
            return 0;
        }

        internal void ChangeStateToBoxAlreadyCreated()
        {
            _orderManagement = new BoxAlreadyCreatedManager(_buyOrder, _sellOrder, this);
        }

        internal void ChangeStateToOrderRunning(Order order)
        {
            _orderManagement = new OrderRunningManager(order, this);
        }

        internal void OrderCompleted()
        {
            _orderManagement = new NoOrderCreatedManager(this);
        }
    }
}
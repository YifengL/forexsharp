using System;
using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;
using TradePlatform.MT4.SDK.API;

namespace FXSharp.EA.PinBar
{
    public class PinBarEA : EExpertAdvisor
    {
        private const double Range = 100;
        private bool _isInitialize;
        private IOrderManager _orderManager;
        //private double LotSize = 1;
        private DateTime _prevtime = default(DateTime);

        protected override int DeInit()
        {
            return 0;
        }

        protected override int Init()
        {
            _orderManager = new NoOrderCreatedManager(this);

            _prevtime = Time[0];
            _isInitialize = true;
            return 0;
        }

        private bool IsNewBar()
        {
            if (_prevtime != Time[0])
            {
                _prevtime = Time[0];
                return (true);
            }

            return (false);
        }

        private bool IsBearishPinBar()
        {
            // the body is small, the position of the body near open. 
            // still can handle if this fall into trap both shadow is > 50 ??
            return (High[1] - Math.Max(Close[1], Open[1]))/Point > 600;
        }

        private bool IsBullishPinBar()
        {
            return (Math.Min(Close[1], Open[1]) - Low[1])/Point > 600;
        }

        protected override int Start()
        {
            if (!_isInitialize)
            {
                Init();
            }

            if (IsNewBar())
            {
                if (IsBullishPinBar())
                {
                    _orderManager.OnNewBullishPinBar();
                }
                else if (IsBearishPinBar())
                {
                    _orderManager.OnNewBearishPinBar();
                }
            }
            else
            {
                _orderManager.OnTick();
            }
            return 0;
        }

        internal Order CreatePendingBuyOrder()
        {
            double slPoints = (High[1] - Low[1])/Point + 2*Range;
            double tpPoints = 2*slPoints*Point;
            var moneyManagement = new MoneyManagement(2, Balance);
            double lotSize = moneyManagement.CalculateLotSize(slPoints);
            Order order = PendingBuy(Symbol, lotSize, High[1] + Range*Point, Low[1] - Range*Point, High[1] + tpPoints);
            this.ObjectsDeleteAll();
            return order;
        }

        internal Order CreatePendingSellOrder()
        {
            double slPoints = (High[1] - Low[1])/Point + 2*Range;
            double tpPoints = 2*slPoints*Point;
            var moneyManagement = new MoneyManagement(2, Balance);
            double lotSize = moneyManagement.CalculateLotSize(slPoints);
            Order order = PendingSell(Symbol, lotSize, Low[1] - Range*Point, High[1] + Range*Point, Low[1] - tpPoints);
            this.ObjectsDeleteAll();
            return order;
        }

        internal void MoveToPendingOrderCreatedState(Order order)
        {
            _orderManager = new PendingOrderCreatedManager(this, order);
        }

        internal void MoveToOrderRunningState(Order order)
        {
            _orderManager = new OrderRunningManager(this, order);
        }

        public void OrderCompleted()
        {
            _orderManager = new NoOrderCreatedManager(this);
        }
    }
}
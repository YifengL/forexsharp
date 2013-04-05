using System;
using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;
using TradePlatform.MT4.SDK.API;

namespace FXSharp.EA.HappyDay
{
    public class HappyDayEA : EExpertAdvisor
    {
        private const double LotSize = 1;
        private Order _buyOrder;
        //private MoneyManagement _moneyManagement;
        private IOrderManager _orderManagement;
        private Order _sellOrder;
        private bool isInitialize;
        private DateTime prevtime = default(DateTime);
        private double range = 100;

        protected override int Init()
        {
            _orderManagement = new NoOrderCreatedManager(this);

            prevtime = Time[0];

            //var second = Time[10];

            isInitialize = true;
            return (0);
        }

        internal void CreatedMagicBoxFromPreviousCandle()
        {
            double asianSessionHigh = AsianSessionHigh;
            double asianSessionLow = AsianSessionLow;

            double slPoints = (asianSessionHigh - asianSessionLow) / Point + 2 * range;
            var moneyManagement = new MoneyManagement(2, Balance);
            double lotSize = moneyManagement.CalculateLotSize(slPoints);
            double tpPoints = Math.Max(1000 * Point, 2 * slPoints * Point);
            //var tpPoints = 200 * Point;
            // risk reward ratio = 2 * slPoints

            _buyOrder = PendingBuy(Symbol, lotSize, asianSessionHigh + range * Point, asianSessionLow - range * Point, asianSessionHigh + tpPoints);

            _sellOrder = PendingSell(Symbol, lotSize, asianSessionLow - range * Point, asianSessionHigh + range * Point, asianSessionLow - tpPoints);

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
                if (IsLondonOpeningTime)
                {
                    _orderManagement.OnLondonOpen();
                }
                else if (IsCloseNewYorkTime)
                {
                    _orderManagement.OnNewYorkClose();
                }
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

        internal void ChangeStateToOrderRunning(Order runningOrder, Order pendingOrder)
        {
            _orderManagement = new FirstOrderRunningManager(runningOrder, pendingOrder, this);
        }

        internal void ChangeStateToSecondOrderRunning(Order runningOrder)
        {
            _orderManagement = new SecondOrderRunningManager(runningOrder, this);
        }

        internal void FirstOrderCompleted(Order pendingOrder)
        {
            _orderManagement = new SecondOrderPendingManager(pendingOrder, this);
        }

        internal void OrderCompleted()
        {
            _orderManagement = new NoOrderCreatedManager(this);
        }

        public bool IsLondonOpeningTime
        {
            get
            {
                var time = Time[0];
                Console.WriteLine(time.Hour);
                return time.Hour == 10;
            }
        }

        public bool IsCloseNewYorkTime
        {
            get
            {
                var time = Time[0];
                return time.Hour == 01 && time.ToString("tt") == "AM";
            }
        }

        public double AsianSessionHigh
        {
            get
            {
                double max = -1;
                for (int i = 1; i < 11; i++)
                {
                    max = Math.Max(max, High[i]);
                }

                return max;
            }
        }

        public double AsianSessionLow
        {
            get
            {
                double min = 100;
                for (int i = 1; i < 11; i++)
                {
                    min = Math.Min(min, Low[i]);
                }

                return min;
            }
        }

    }
}
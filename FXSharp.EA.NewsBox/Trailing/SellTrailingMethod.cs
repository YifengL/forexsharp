//using FXSharp.TradingPlatform.Exts;

using System;
using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class TrailingMethod : IProfitProtector
    {
        //private const double LimitProtectedLevel = 50;
        //private readonly IOrderDetail _detail;

        //private double _latestProtectedLevel = 50;

        //public TrailingMethod(IOrderDetail detail)
        //{
        //    _detail = detail;
        //}

        //public void Trail()
        //{
        //    double profitPoints = _detail.ProfitPoints;

        //    if (profitPoints >= _latestProtectedLevel*_detail.Point)
        //    {
        //        ProtectProfit(profitPoints);
        //        _latestProtectedLevel = profitPoints/_detail.Point;
        //    }
        //}

        //private void ProtectProfit(double profitPoints)
        //{
        //    double protectPoint = _detail.OpenPrice - (profitPoints - LimitProtectedLevel*_detail.Point);
        //    _detail.ModifyStopLoss(Math.Round(protectPoint, _detail.Digits));
        //}

        //public void TryProtectProfit()
        //{
            
        //}

        private readonly Order _order;

        private readonly double[] _protectLevel = new double[] {100, 200, 300, 10000};
        private readonly double[] _protectValue = new double[] {20, 100, 200, 10000};

        private int _currentProtectedIdx;

        public TrailingMethod(Order order)
        {
            _order = order;
        }

        public double NextProtectedLevel
        {
            get { return _protectLevel[_currentProtectedIdx]; }
        }

        public double NextProtectedValue
        {
            get { return _protectValue[_currentProtectedIdx]; }
        }

        public void TryProtectProfit()
        {
            Console.WriteLine(_order.ProfitPoints);

            if (_order.ProfitPoints >= NextProtectedLevel)
            {
                Console.WriteLine("Try locking profit : next level {0}", NextProtectedLevel);
                _order.ProtectProfit(NextProtectedValue);
                NextLevel();
            }
        }

        private void NextLevel()
        {
            _currentProtectedIdx++;
        }
    }
}
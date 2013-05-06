using System;
using FXSharp.EA.OrderManagements;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.NewsBox
{
    public class TrailingMethod : IProfitProtector
    {
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
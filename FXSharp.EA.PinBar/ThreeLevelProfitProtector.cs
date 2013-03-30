using System;
using FXSharp.TradingPlatform.Exts;

namespace FXSharp.EA.PinBar
{
    internal class ThreeLevelProfitProtector : IProfitProtector
    {
        private readonly Order _order;

        private readonly double[] _protectLevel = new double[] { 300, 600, 800, 10000 };
        private readonly double[] _protectValue = new double[] { 10, 200, 500, 10000 };

        private int _currentProtectedIdx = 0;

        public ThreeLevelProfitProtector(Order order)
        {
            _order = order;
        }

        public void TryProtectProfit()
        {
            Console.WriteLine(_order.ProfitPoints);

            if (_order.ProfitPoints >= NextProtectedLevel)
            {
                Console.WriteLine("Try locking profit : next level {0}", NextProtectedLevel );
                _order.ProtectProfit(NextProtectedValue);
                NextLevel();
            }
        }

        private void NextLevel()
        {
            _currentProtectedIdx++;
        }

        public double NextProtectedLevel
        {
            get { return _protectLevel[_currentProtectedIdx]; }
        }

        public double NextProtectedValue
        {
            get { return _protectValue[_currentProtectedIdx]; }
        }
    }
}
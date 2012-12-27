using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert
{
    public class TradingContext
    {
        private ExpertAdvisor advisor;

        public TradingContext(ExpertAdvisor advisor)
        {
            this.advisor = advisor;
        }

        public string Symbol
        {
            get { return advisor.Symbol(); }
        }

        public int Period
        {
            get { return advisor.Period(); }
        }

        public double MarketInfo(string symbol, MARKER_INFO_MODE mARKER_INFO_MODE)
        {
            return advisor.MarketInfo(symbol, mARKER_INFO_MODE);
        }
    }
}

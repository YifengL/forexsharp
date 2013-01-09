using System;
using TradePlatform.MT4.SDK.API;

namespace FXSharp.TradingPlatform.Exts
{
    public class Times
    {
        private EExpertAdvisor ea;

        public Times(EExpertAdvisor ea)
        {
            this.ea = ea;
        }

        public DateTime this[int i]
        {
            get
            {
                return ea.Time(i);
            }
        }
    }
}

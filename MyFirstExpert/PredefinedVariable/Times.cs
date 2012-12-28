using System;
using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert
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

using TradePlatform.MT4.SDK.API;

namespace FXSharp.TradingPlatform.Exts
{
    public class Highs
    {
        private EExpertAdvisor ea;

        public Highs(EExpertAdvisor ea)
        {
            this.ea = ea;
        }

        public double this[int i]
        {
            get
            {
                return ea.High(i);
            }
        }
    }
}

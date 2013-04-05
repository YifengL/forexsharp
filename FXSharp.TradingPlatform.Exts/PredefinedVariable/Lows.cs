using TradePlatform.MT4.SDK.API;

namespace FXSharp.TradingPlatform.Exts
{
    public class Lows
    {
        private readonly EExpertAdvisor ea;

        public Lows(EExpertAdvisor ea)
        {
            this.ea = ea;
        }

        public double this[int i]
        {
            get { return ea.Low(i); }
        }
    }
}
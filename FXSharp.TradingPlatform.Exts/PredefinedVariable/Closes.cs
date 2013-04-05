using TradePlatform.MT4.SDK.API;

namespace FXSharp.TradingPlatform.Exts
{
    public class Closes
    {
        private readonly EExpertAdvisor ea;

        public Closes(EExpertAdvisor ea)
        {
            this.ea = ea;
        }

        public double this[int i]
        {
            get { return ea.Close(i); }
        }
    }
}
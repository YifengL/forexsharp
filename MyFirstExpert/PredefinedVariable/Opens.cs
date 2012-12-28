using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert.PredefinedVariable
{
    public class Opens
    {
        private EExpertAdvisor ea;

        public Opens(EExpertAdvisor ea)
        {
            this.ea = ea;    
        }

        public double this[int i]
        {
            get
            {
                return ea.Open(i);
            }
        }
    }
}

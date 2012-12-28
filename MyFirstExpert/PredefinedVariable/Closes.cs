using TradePlatform.MT4.SDK.API;

namespace MyFirstExpert.PredefinedVariable
{
    public class Closes
    {
        private EExpertAdvisor ea;

        public Closes(EExpertAdvisor ea)
        {
            this.ea = ea;    
        }

        public double this[int i]
        {
            get
            {
                return ea.Close(i);
            }
        }
    }
}

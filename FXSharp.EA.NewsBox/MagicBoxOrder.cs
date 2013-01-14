
using System;
namespace FXSharp.EA.NewsBox
{
    public class MagicBoxOrder
    {
        private Guid _unique;

        public string Id
        {
            get { return string.Format("{0}-{1}", Symbol, _unique.ToString()); }
        }

        public string Symbol { get; set; }

        public MagicBoxOrder()
        {
            _unique = Guid.NewGuid();
            Config = new MagicBoxConfig();
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null)) return false;

            if (Object.ReferenceEquals(this, obj)) return true;

            if (this.GetType() != obj.GetType()) return false;

            var another = (MagicBoxOrder)obj;

            return another.Symbol == this.Symbol && another.ExecutingTime == this.ExecutingTime;
        }

        public override int GetHashCode()
        {
            return Symbol.Length + ExecutingTime.Hour;
        }

        public double LotSize { get; set; }

        public double Range { get { return Config.Range; } }

        public double TakeProfit { get { return Config.TakeProfit; } }

        public double StopLoss 
        { 
            get { return Config.StopLoss; }
            set { Config.StopLoss = value; }
        }

        public int MinuteExpiracy { get { return Config.MinuteExpiracy; } }

        public DateTime ExecutingTime { get { return NewsTime.AddMinutes(Config.MinutePendingExecution); } }

        public DateTime NewsTime { get; set; }

        public MagicBoxConfig Config { get; set; }

        /*
         Range = 50,
                    TakeProfit = 150,
                    StopLoss = 200,
                    MinuteExpiracy = 10
         */
    }
}

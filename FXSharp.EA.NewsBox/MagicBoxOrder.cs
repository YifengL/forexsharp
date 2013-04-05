using System;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxOrder
    {
        private Guid _unique;

        public MagicBoxOrder()
        {
            _unique = Guid.NewGuid();
            Config = new MagicBoxConfig();
        }

        public string Id
        {
            get { return string.Format("{0}-{1}", Symbol, _unique.ToString()); }
        }

        public string Symbol { get; set; }

        public double LotSize { get; set; }

        public double Range
        {
            get { return Config.Range; }
        }

        public double TakeProfit
        {
            get { return Config.TakeProfit; }
        }

        public double StopLoss
        {
            get { return Config.StopLoss; }
            set { Config.StopLoss = value; }
        }

        public int MinuteExpiracy
        {
            get { return Config.MinuteExpiracy; }
        }

        public DateTime ExecutingTime
        {
            get { return NewsTime.AddMinutes(Config.MinutePendingExecution); }
        }

        public DateTime NewsTime { get; set; }

        public MagicBoxConfig Config { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (GetType() != obj.GetType()) return false;

            var another = (MagicBoxOrder) obj;

            return another.Symbol == Symbol && another.ExecutingTime == ExecutingTime;
        }

        public override int GetHashCode()
        {
            return Symbol.Length + ExecutingTime.Hour;
        }
    }
}
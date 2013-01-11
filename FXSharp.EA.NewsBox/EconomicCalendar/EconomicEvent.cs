using System;

namespace FXSharp.EA.NewsBox
{
    public class EconomicEvent
    {
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Volatility { get; set; }
        public string Actual { get; set; }
        public string Previous { get; set; }
        public string Consensus { get; set; }
        public bool IsSpeechOrMeeting { get { return string.IsNullOrEmpty(Previous); } }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null)) return false;

            if (Object.ReferenceEquals(this, obj)) return true;

            if (this.GetType() != obj.GetType()) return false;

            var another = (EconomicEvent)obj;

            return another.DateTime == this.DateTime 
                && another.Currency == this.Currency
                && another.Actual == this.Actual 
                && another.Previous == this.Previous
                && another.Consensus == this.Consensus;
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode() + Previous.GetHashCode();
        }
    }
}

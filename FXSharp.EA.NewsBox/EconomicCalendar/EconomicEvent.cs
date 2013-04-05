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

        public bool IsSpeechOrMeeting
        {
            get { return string.IsNullOrEmpty(Previous); }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (GetType() != obj.GetType()) return false;

            var another = (EconomicEvent) obj;

            return another.DateTime == DateTime
                   && another.Currency == Currency
                   && another.Actual == Actual
                   && another.Previous == Previous
                   && another.Consensus == Consensus;
        }

        public override int GetHashCode()
        {
            return Currency.GetHashCode() + Previous.GetHashCode();
        }
    }
}
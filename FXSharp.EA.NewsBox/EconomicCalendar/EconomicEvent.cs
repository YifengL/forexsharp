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
    }
}

using System;

namespace FXSharp.EA.FirstTest
{
    internal class QuoteBeat
    {
        private Quote prev;

        public QuoteBeat()
        {
            MaxDown = 1;
            MaxUp = -1;
        }

        public double DeltaAsk { get; set; }

        public double DeltaBid { get; set; }

        public double MaxDown { get; set; }

        public double MaxUp { get; set; }

        public double LastDelta { get; set; }

        internal void Add(Quote quote)
        {
            if (prev == null)
            {
                prev = quote;
                return;
            }

            DeltaAsk = (quote.Ask*100000 - prev.Ask*100000)/10;
            DeltaBid = (quote.Bid*100000 - prev.Bid*100000)/10;

            LastDelta = Math.Max(DeltaAsk, DeltaBid);

            MaxUp = Math.Max(LastDelta, MaxUp);
            MaxDown = Math.Min(LastDelta, MaxDown);

            prev = quote;
        }
    }
}
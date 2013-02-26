namespace FXSharp.EA.FirstTest
{
    internal class Quote
    {
        public Quote(double bid, double ask)
        {
            Bid = bid;
            Ask = ask;
            Spread = (Ask - Bid)*10000;
        }

        public double Bid { get; set; }

        public double Ask { get; set; }

        public double Spread { get; set; }
    }
}
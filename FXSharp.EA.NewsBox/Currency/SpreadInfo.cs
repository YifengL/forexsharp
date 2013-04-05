namespace FXSharp.EA.NewsBox
{
    internal class SpreadInfo
    {
        public string Symbol { get; set; }
        public double Spread { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Symbol, Spread);
        }
    }
}
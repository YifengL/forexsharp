namespace TradePlatform.MT4.SDK.Library.Indicators
{
    using TradePlatform.MT4.Core;
    using TradePlatform.MT4.SDK.API;

    public class PrevDayHighLowIndicator : ExpertAdvisor
    {
        double _dayHigh;
        double _dayLow;
        double _dayHigh1;
        double _dayLow1;

        protected override int Init()
        {
            return 1;
        }

        protected override int Start()
        {
            int counted_bars = this.IndicatorCounted();
            if (counted_bars < 0) return (-1);
            if (counted_bars > 0) counted_bars--;

            int limit = this.Bars() - counted_bars;

            for (int i = limit; i >= 0; i--)
            {
                this.ExtMapBuffer1(i, this._dayHigh1);
                this.ExtMapBuffer2(i, this._dayLow1);

                if (this.High(i + 1) > this._dayHigh) this._dayHigh = this.High(i + 1);
                if (this.Low(i + 1) < this._dayLow) this._dayLow = this.Low(i + 1);


                if (this.Time(i).Day != this.Time(i + 1).Day && this.Time(i + 1).DayOfWeek != 0)
                {
                    this._dayHigh1 = this._dayHigh;
                    this._dayLow1 = this._dayLow;
                    this._dayHigh = this.Open(i);
                    this._dayLow = this.Open(i);
                }
            }

            return 1;
        }

        protected override int DeInit()
        {
            return 1;
        }
    }

    public static class PrevDayHightIndicatorExtensions
    {
        public static void ExtMapBuffer1(this MqlHandler handler, int index, double value)
        {
            string retrunValue = handler.CallMqlMethod("ExtMapBuffer1", index, value);
        }

        public static void ExtMapBuffer2(this MqlHandler handler, int index, double value)
        {
            string retrunValue = handler.CallMqlMethod("ExtMapBuffer2", index, value);
        }
    }
}

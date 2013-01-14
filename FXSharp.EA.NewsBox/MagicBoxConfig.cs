using System;

namespace FXSharp.EA.NewsBox
{
    public class MagicBoxConfig
    {
        public double Range { get; set; }

        public double TakeProfit { get; set; }

        public double StopLoss { get; set; }

        public int MinuteExpiracy { get; set; }

        public int MinutePendingExecution { get; set; }
    }
}

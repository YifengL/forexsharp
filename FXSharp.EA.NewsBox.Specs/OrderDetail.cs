using System;

namespace FXSharp.EA.NewsBox.Specs
{
    public class OrderDetail : IOrderDetail
    {
        //public double Bid { get; set; }

        public double OpenPrice { get; set; }

        public double StopLoss { get; set; }

        public double Point { get; set; }

        public void ModifyStopLoss(double lastProtectedPoint)
        {
            StopLoss = lastProtectedPoint;
        }

        //public double Ask { get; set; }

        public double ProfitPoints { get; set; }


        public int Digits { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox
{
    public interface IOrderDetail
    {
        //double Bid { get; set; }
        double OpenPrice { get; set; }
        double StopLoss { get; set; }
        double Point { get; set; }
        void ModifyStopLoss(double lastProtectedPoint);

        //double Ask { get; set; }

        double ProfitPoints { get; set; }

        int Digits { get; set; }
    }
}

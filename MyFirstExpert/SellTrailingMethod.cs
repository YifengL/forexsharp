using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFirstExpert
{
    public class SellTrailingMethod : ITrailingMethod
    {
        private Order order;
        private double lastProtectedPoint = 0;
        private double minimumProfitToLock = 200;
        private double trailingPoint = 100;
        private EExpertAdvisor ea;

        public SellTrailingMethod(Order order, EExpertAdvisor ea)
        {
            this.order = order;
            this.ea = ea;
        }

        public double UnProtectedProfit
        {
            get
            {
                if (lastProtectedPoint == 0)
                {
                    return order.OpenPrice - ea.Ask;
                }

                return lastProtectedPoint - ea.Ask;
            }
        }

        public void Trail()
        {
            if (UnProtectedProfit > minimumProfitToLock * ea.Point)
            {
                ProtectProfit(trailingPoint);
            }
        }

        private void ProtectProfit(double lockPoints)
        {
            if (lastProtectedPoint == 0)
            {
                lastProtectedPoint = order.OpenPrice - lockPoints * ea.Point;
            }
            else
            {
                lastProtectedPoint = order.StopLoss - lockPoints * ea.Point;
            }

            order.ModifyStopLoss(lastProtectedPoint);
        }
    }
}

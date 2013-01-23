using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_trail_sell_profit
    {
        [Test]
        public void Should_trail_when_reach_20_pips()
        {
            var orderDetail = new OrderDetail
                {
                    //Bid = 0.0010,
                    OpenPrice = 0.0020,
                    Point = 0.00001,
                    StopLoss = 0.0025,
                    Digits = 5
                };

            var trailing = new SellTrailingMethod(orderDetail);
            orderDetail.ProfitPoints = 6 * 0.0001;

            trailing.Trail();
            
            Assert.AreEqual(0.0019, orderDetail.StopLoss);

            orderDetail.ProfitPoints = 7 * 0.0001;

            trailing.Trail();

            Assert.AreEqual(0.0018, orderDetail.StopLoss);

            orderDetail.ProfitPoints = 9 * 0.0001;

            trailing.Trail();

            Assert.AreEqual(0.0016, orderDetail.StopLoss);

            orderDetail.ProfitPoints = 7 * 0.0001;

            trailing.Trail();

            Assert.AreEqual(0.0016, orderDetail.StopLoss);
        }
    }
}

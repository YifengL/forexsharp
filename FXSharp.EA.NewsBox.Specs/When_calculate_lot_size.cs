using FXSharp.EA.OrderManagements;
using NUnit.Framework;

namespace FXSharp.EA.NewsBox.Specs
{
    [TestFixture]
    public class When_calculate_lot_size
    {
        [Test]
        public void Should_contain_risk_one_percent()
        {
            var moneyManagement = new MoneyManagement(1, 10000);
            double lot = moneyManagement.CalculateLotSize(400);
            Assert.AreEqual(0.25, lot);
        }

        [Test]
        public void Should_contain_risk_one_percent_smaller()
        {
            var moneyManagement = new MoneyManagement(1, 2000);
            double lot = moneyManagement.CalculateLotSize(200);

            Assert.AreEqual(0.1, lot);
        }
    }
}
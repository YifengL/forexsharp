////using System;
////using FXSharp.TradingPlatform.Exts;

////namespace FXSharp.EA.NewsBox
////{
////    public class OrderTrailingInfo : IOrderDetail
////    {
////        private readonly Order _order;

////        public OrderTrailingInfo(Order order)
////        {
////            _order = order;
////        }

////        public double OpenPrice
////        {
////            get { return _order.OpenPrice; }
////            set { throw new NotImplementedException(); }
////        }

////        public double StopLoss
////        {
////            get { return _order.StopLoss; }
////            set { throw new NotImplementedException(); }
////        }

////        public void ModifyStopLoss(double lastProtectedPoint)
////        {
////            _order.ModifyStopLoss(lastProtectedPoint);
////        }

////        public double ProfitPoints
////        {
////            get { return _order.ProfitPoints; }
////            set { throw new NotImplementedException(); }
////        }


////        public int Digits
////        {
////            get { return _order.Digits; }
////            set { throw new NotImplementedException(); }
////        }


////        public double Point
////        {
////            get { return _order.Points; }
////            set { throw new NotImplementedException(); }
////        }
////    }
////}
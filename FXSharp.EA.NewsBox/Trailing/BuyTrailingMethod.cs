////using FXSharp.TradingPlatform.Exts;

//using System;

//namespace FXSharp.EA.NewsBox
//{
//    public class BuyTrailingMethod : ITrailingMethod
//    {
//        private const double LimitProtectedLevel = 50;
//        private readonly IOrderDetail _detail;

//        private double _latestProtectedLevel = 50;

//        public BuyTrailingMethod(IOrderDetail detail)
//        {
//            _detail = detail;
//        }

//        public void Trail()
//        {
//            double profitPoints = _detail.ProfitPoints;

//            if (profitPoints >= _latestProtectedLevel*_detail.Point)
//            {
//                ProtectProfit(profitPoints);
//                _latestProtectedLevel = profitPoints/_detail.Point;
//            }
//        }

//        private void ProtectProfit(double profitPoints)
//        {
//            double protectPoint = _detail.OpenPrice + (profitPoints - LimitProtectedLevel*_detail.Point);
//            _detail.ModifyStopLoss(Math.Round(protectPoint, _detail.Digits));
//        }
//    }
//}
using System;
using TradePlatform.MT4.SDK.API;

namespace FXSharp.TradingPlatform.Exts
{
    public class Order
    {
        private readonly DateTime NULL_TIME = new DateTime(621355968000000000);
        private readonly EExpertAdvisor ea;
        private readonly double lots;
        //private ORDER_TYPE orderType;
        private readonly string symbol;
        private readonly int ticket;
        private double openPrice;

        public Order(string symbol, int ticket, double lots, EExpertAdvisor ea)
        {
            this.symbol = symbol;
            this.ticket = ticket;
            this.ea = ea;
            this.lots = lots;
            NULL_TIME = CloseTime;
            //this.orderType = orderType;
        }

        public double Profit
        {
            get
            {
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderProfit();

                ea.ThrowLatestException();

                return -1;
            }
        }

        public double OpenPrice
        {
            get
            {
                if (openPrice != default(double))
                    return openPrice;

                // should throw exception for each call
                if (!ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return openPrice;

                openPrice = ea.OrderOpenPrice();
                return openPrice;
            }
        }

        public double StopLoss
        {
            get
            {
                // should cache stop loss 

                bool success = false;
                // should throw exception for each call
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderStopLoss();

                if (!success)
                {
                    ea.ThrowLatestException();
                }

                return 0;
            }
        }

        public DateTime CloseTime
        {
            get
            {
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderCloseTime();
                return default(DateTime);
                //return ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) && ea.OrderCloseTime() == NULL_TIME;
            }
        }

        public bool IsOpen
        {
            get
            {
                //ea.Print("Time : " + CloseTime);
                //ea.Print("Null Time : " + NULL_TIME);

                return CloseTime == NULL_TIME;
            }
        }

        public bool IsRunning
        {
            get
            {
                return ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) &&
                       (ea.OrderType() == ORDER_TYPE.OP_BUY || ea.OrderType() == ORDER_TYPE.OP_SELL);
            }
        }

        public string Symbol
        {
            get { return symbol; }
        }

        public double ProfitPoints
        {
            get
            {
                ORDER_TYPE orderType = ea.OrderType();

                if (orderType == ORDER_TYPE.OP_BUY)
                {
                    return (ea.BuyClosePriceFor(symbol) - OpenPrice)/ea.PointFor(symbol);
                }
                else if (orderType == ORDER_TYPE.OP_SELL)
                {
                    return (OpenPrice - ea.SellClosePriceFor(symbol))/ea.PointFor(symbol);
                }
                else
                {
                    throw new ApplicationException("Only market order has profit");
                }
            }
        }

        public int Digits
        {
            get { return ea.DigitsFor(symbol); }
        }

        public double Points
        {
            get { return ea.PointFor(symbol); }
        }

        public ORDER_TYPE OrderType
        {
            get
            {
                if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                    return ea.OrderType();

                ea.ThrowLatestException();

                return default(ORDER_TYPE);
            }
        }

        public double Spread
        {
            get { return ea.AskFor(Symbol) - ea.BidFor(Symbol); }
        }

        private bool IsKindOfBuyOrder
        {
            get
            {
                return OrderType == ORDER_TYPE.OP_BUY || OrderType == ORDER_TYPE.OP_BUYLIMIT ||
                       OrderType == ORDER_TYPE.OP_BUYSTOP;
            }
        }

        private bool IsKindOfSellOrder
        {
            get
            {
                return OrderType == ORDER_TYPE.OP_SELL || OrderType == ORDER_TYPE.OP_SELLLIMIT ||
                       OrderType == ORDER_TYPE.OP_SELLSTOP;
            }
        }

        private double CurrentClosingPrice
        {
            get { return IsKindOfBuyOrder ? ea.BuyClosePriceFor(symbol) : ea.SellClosePriceFor(symbol); }
        }

        public bool IsValid
        {
            get { return IsKindOfBuyOrder ? StopLoss < CurrentClosingPrice : StopLoss > CurrentClosingPrice; }
        }

        public void Close()
        {
            while (IsOpen)
            {
                CloseInternal();
            }
        }

        private void CloseInternal()
        {
            bool success = false;

            if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET) && ea.OrderCloseTime() != NULL_TIME)
                return;

            //ea.Print("try to close");

            ORDER_TYPE orderType = ea.OrderType();
            // should refactor to hierarcy

            if (orderType == ORDER_TYPE.OP_BUY)
            {
                success = ea.OrderClose(ticket, lots, ea.BuyClosePriceFor(symbol), 50);
            }
            else if (orderType == ORDER_TYPE.OP_SELL)
            {
                success = ea.OrderClose(ticket, lots, ea.SellClosePriceFor(symbol), 50);
            }
            else
            {
                success = ea.OrderDelete(ticket);
                ea.Print("delete ticket " + success);
            }

            if (!success)
                ea.ThrowLatestException();
        }

        public bool CloseInProfit()
        {
            if (Profit < 0.0) return false;

            Close();

            return true;
        }

        public void ModifyStopLoss(double newStopLoss)
        {
            bool success = false;
            // should throw exception for each call
            if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                success = ea.OrderModify(ticket, ea.OrderOpenPrice(), newStopLoss, ea.OrderTakeProfit(),
                                         DateTime.Now.AddDays(100), -1);

            if (!success)
            {
                ea.ThrowLatestException();
            }
        }

        public void ModifyTakeProfit(double newTakeProfit)
        {
            bool success = false;
            // should throw exception for each call
            if (ea.OrderSelect(ticket, SELECT_BY.SELECT_BY_TICKET))
                success = ea.OrderModify(ticket, ea.OrderOpenPrice(), ea.OrderStopLoss(), newTakeProfit,
                                         DateTime.Now.AddDays(100), -1);

            if (!success)
            {
                ea.ThrowLatestException();
            }
        }

        public void ChangeTakeProfitInPoints(double tpPoints)
        {
            ORDER_TYPE orderType = OrderType;

            double newTp = 0.0;

            if (IsKindOfBuyOrder)
            {
                newTp = OpenPrice + (tpPoints*Points) + Spread;
            }
            else if (IsKindOfSellOrder)
            {
                newTp = OpenPrice - (tpPoints*Points) - Spread;
            }

            ModifyTakeProfit(newTp);
        }

        public void ChangeStopLossInPoints(double slPoints)
        {
            //var orderType = OrderType;

            double newSl = 0.0;

            if (IsKindOfBuyOrder)
            {
                newSl = OpenPrice - (slPoints*Points) - Spread;
            }
            else if (IsKindOfSellOrder)
            {
                newSl = OpenPrice + (slPoints*Points) + Spread;
            }

            ModifyStopLoss(newSl);
        }

        public void ProtectProfit(double pointsValue)
        {
            ORDER_TYPE orderType = OrderType;

            double newSl = 0.0;

            if (orderType == ORDER_TYPE.OP_BUY)
            {
                newSl = OpenPrice + (pointsValue*Points) + Spread;
            }
            else if (orderType == ORDER_TYPE.OP_SELL)
            {
                newSl = OpenPrice - (pointsValue*Points) - Spread;
            }

            ModifyStopLoss(newSl);
        }
    }
}
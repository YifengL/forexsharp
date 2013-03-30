using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.SDK.API;
using TradePlatform.MT4.SDK.Library.Experts;

namespace FXSharp.TradingPlatform.Exts
{
    public abstract class EExpertAdvisor : ExpertAdvisor
    {
        public static readonly MARKER_INFO_MODE MODE_ASK = MARKER_INFO_MODE.MODE_ASK;
        public static readonly MARKER_INFO_MODE MODE_BID = MARKER_INFO_MODE.MODE_BID;
        public static readonly MARKER_INFO_MODE MODE_POINT = MARKER_INFO_MODE.MODE_POINT;
        public static readonly POOL_MODES MODE_TRADES = POOL_MODES.MODE_TRADES;

        #region Special Constant
        public const int CLR_NONE = -1;
        #endregion

        private int lastError;

        public EExpertAdvisor()
        {
            this.MqlError += OnMqlError;
        }

        private void OnMqlError(MqlErrorException mqlErrorException)
        {
            var err = mqlErrorException.Message.Split(' ')[3].Trim('\'');
            var msg = err.Split(':');
            lastError = Convert.ToInt32(msg[0]);
        }

        public bool IsTradeContextBusy()
        {
            return CheckupFunctions.IsTradeContextBusy(this);
        }

        public double NormalizeDouble(double value, double digits)
        {
            return Math.Round(value, (int)digits);
        }

        public int GetLastError()
        {
            int lastErr = lastError;
            lastError = 0;
            return lastErr;
        }

        public int OrdersTotal()
        {
            return TradingFunctions.OrdersTotal(this);
        }

        public string OrderSymbol()
        {
            return TradingFunctions.OrderSymbol(this);
        }

        public int OrderTicket()
        {
            return TradingFunctions.OrderTicket(this);
        }

        public int OrderMagicNumber()
        {
            return TradingFunctions.OrderMagicNumber(this);
        }

        public double OrderOpenPrice()
        {
            return TradingFunctions.OrderOpenPrice(this);
        }

        public ORDER_TYPE OrderType()
        {
            return TradingFunctions.OrderType(this);
        }

        public int OrderSend(string symbol, ORDER_TYPE cmd, double volume, double price, int slippage, double stoploss, double takeprofit, string comment = "", int magic = 0, DateTime expiration = default(DateTime), int arrow_color = -1)
        {
            int ticket = TradingFunctions.OrderSend(this, symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment,
                                              magic, DateTime.Now.AddDays(100), arrow_color);

            if (ticket == -1)
            {
                ThrowLatestOrderException();
            }

            return ticket;
        }

        private void ThrowLatestOrderException()
        {
            int lastError = this.OrderReliableLastErr();

            if (lastError == 0) return;

            throw CreateException(lastError);
        }

        public bool OrderModify(int ticket, double price, double stoploss, double takeprofit, DateTime expiration = default(DateTime), int arrow_color = -1)
        {
            return TradingFunctions.OrderModify(this, ticket, price, stoploss, takeprofit, DateTime.Now.AddDays(100), arrow_color);
        }

        public bool OrderClose(int ticket, double lots, double price, int slippage, int color = 0)
        {
            return TradingFunctions.OrderClose(this, ticket, lots, price, slippage, color);
        }

        public bool OrderSelect(int index, SELECT_BY select, POOL_MODES pool = POOL_MODES.MODE_TRADES)
        {
            if (!TradingFunctions.OrderSelect(this, index, select, pool))
            {
                ThrowLatestException();
                return false;
            }

            return true;
        }

        public void Print(object text)
        {
            CommonFunctions.Print(this, text.ToString());
        }

        public bool RefreshRates()
        {
            return WindowFunctions.RefreshRates(this);
        }

        public string Symbol
        {
            get { return WindowFunctions.Symbol(this); }
        }

        public int Period
        {
            get { return WindowFunctions.Period(this); }
        }

        public double MarketInfo(string symbol, MARKER_INFO_MODE mode)
        {
            return CommonFunctions.MarketInfo(this, symbol, mode);
        }

        public double AskFor(string symbol)
        {
            RefreshRates();
            return MarketInfo(symbol, MARKER_INFO_MODE.MODE_ASK);
        }

        public double BidFor(string symbol)
        {
            RefreshRates();
            return MarketInfo(symbol, MARKER_INFO_MODE.MODE_BID);
        }

        public double BuyOpenPriceFor(string symbol) { return AskFor(symbol); }

        public double BuyClosePriceFor(string symbol) { return BidFor(symbol); }

        public double SellOpenPriceFor(string symbol) { return BidFor(symbol); }

        public double SellClosePriceFor(string symbol) { return AskFor(symbol); }

        public double Bid
        {
            get
            {
                RefreshRates();
                return PredefinedVariables.Bid(this);
            }
        }

        public double Ask
        {
            get
            {
                RefreshRates();
                return PredefinedVariables.Ask(this);
            }
        }

        public double Point
        {
            get { return PredefinedVariables.Point(this); }
        }

        public double PointFor(string symbol)
        {
            return MarketInfo(symbol, MARKER_INFO_MODE.MODE_POINT);
        }

        internal void ThrowLatestException()
        {
            int lastError = GetLastError();

            if (IsIgnorableError(lastError)) return;

            throw CreateException(lastError);
        }

        private bool IsIgnorableError(int lastError)
        {
            return lastError == 0 || lastError == 4008 || lastError == 4002 || lastError == 4009 || lastError == 4021;
        }

        private Exception CreateException(int lastError)
        {
            return ErrorHandler.CreateException(lastError);
        }

        public Order Buy(double size, double stopLoss = 0, double takeProfit = 0)
        {
            return Buy(Symbol, size, stopLoss, takeProfit);
        }

        public Order Buy(string symbol, double size, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy

            if (stopLoss != 0 && stopLoss >= BuyOpenPriceFor(symbol)) throw new ApplicationException("Stop Loss for Buy have to less than Ask");

            if (takeProfit != 0 && takeProfit <= BuyOpenPriceFor(symbol)) throw new ApplicationException("Take profit for Buy have to more than Ask");

            int ticket = OrderSend(symbol, ORDER_TYPE.OP_BUY, size, BuyOpenPriceFor(symbol), 3, stopLoss, takeProfit, "", 12134);

            // check if we can create and order to ecn 

            // should host compatible handler in server

            if (ticket == -1)
            {
                ThrowLatestException();
            }

            return new Order(symbol, ticket, size, this);
        }

        public Order Sell(double size, double stopLoss = 0, double takeProfit = 0)
        {
            return Sell(Symbol, size, stopLoss, takeProfit);
        }

        public Order Sell(string symbol, double size, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy

            if (stopLoss != 0 && stopLoss <= SellOpenPriceFor(symbol)) throw new ApplicationException("Stop Loss for Sell have to more than Bid");

            if (takeProfit != 0 && takeProfit >= SellOpenPriceFor(symbol)) throw new ApplicationException("Take profit for Sell have to less than Bid");

            int ticket = OrderSend(Symbol, ORDER_TYPE.OP_SELL, size, SellOpenPriceFor(symbol), 3, stopLoss, takeProfit, "", 12134);

            // check if we can create and order to ecn 

            // should host compatible handler in server

            if (ticket == -1)
            {
                ThrowLatestException();
            }

            return new Order(symbol, ticket, size, this);
        }

        public double BuyOpenPrice { get { return Ask; } }

        public double BuyClosePrice { get { return Bid; } }

        public double SellOpenPrice { get { return Bid; } }

        public double SellClosePrice { get { return Ask; } }

        public Order PendingBuy(string symbol, double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy
            if (stopLoss != 0 && stopLoss >= entry) throw new ApplicationException("Stop Loss for Buy have to less than entry price");

            if (takeProfit != 0 && takeProfit <= entry) throw new ApplicationException("Take profit for Buy have to more than entry price");

            ORDER_TYPE orderType = default(ORDER_TYPE);

            if (BuyOpenPriceFor(symbol) < entry)
            {
                orderType = ORDER_TYPE.OP_BUYSTOP;
            }
            else
            {
                orderType = ORDER_TYPE.OP_BUYLIMIT;
            }

            int ticket = OrderSend(symbol, orderType, size, entry, 3, stopLoss, takeProfit, "", 12134, DateTime.Now.AddDays(100), CLR_NONE);

            // check if we can create and order to ecn 

            // should host compatible handler in server

            if (ticket == -1)
            {
                ThrowLatestException();
            }

            return new Order(symbol, ticket, size, this);
            //return new Order(
        }

        public Order PendingBuy(double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            return PendingBuy(Symbol, entry, stopLoss, takeProfit);
        }

        public Order PendingSell(double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            return PendingSell(Symbol, size, entry, stopLoss, takeProfit);
        }

        public Order PendingSell(string symbol, double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy
            if (stopLoss != 0 && stopLoss <= entry) throw new ApplicationException("Stop Loss for Sell have to more than entry price");

            if (takeProfit != 0 && takeProfit >= entry) throw new ApplicationException("Take profit for Sell have to less than entry price");

            ORDER_TYPE orderType = default(ORDER_TYPE);

            if (SellOpenPriceFor(symbol) < entry)
            {
                // sell limit
                orderType = ORDER_TYPE.OP_SELLLIMIT;
            }
            else
            {
                // sell stop
                orderType = ORDER_TYPE.OP_SELLSTOP;
            }

            int ticket = OrderSend(symbol, orderType, size, entry, 3, stopLoss, takeProfit, "", 12134, DateTime.Now.AddDays(100), CLR_NONE);

            // check if we can create and order to ecn 

            // should host compatible handler in server

            if (ticket == -1)
            {
                ThrowLatestException();
            }

            return new Order(symbol, ticket, size, this);
            //return new Order(
        }

        public Highs High
        {
            get { return new Highs(this); }
        }

        public Lows Low
        {
            get { return new Lows(this); }
        }

        public Opens Open
        {
            get { return new Opens(this); }
        }

        public Times Time
        {
            get { return new Times(this); }
        }

        public double ATR
        {
            get { return TechnicalIndicators.iATR(this, Symbol, TIME_FRAME.PERIOD_H4, 14, 0); }
        }

        public int Digits
        {
            get { return PredefinedVariables.Digits(this); }
        }

        public double Balance
        {
            get { return AccountInformation.AccountEquity(this); }
        }

        public int DigitsFor(string symbol)
        {
            return (int)MarketInfo(symbol, MARKER_INFO_MODE.MODE_DIGITS);
        }
    }

}


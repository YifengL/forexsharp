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

        #region Error codes returned from trade server
        public const int ERR_NO_ERROR = 0;
        public const int ERR_NO_RESULT = 1;
        public const int ERR_SERVER_BUSY = 4;
        public const int ERR_NO_CONNECTION = 6;
        public const int ERR_INVALID_PRICE = 129;
        public const int ERR_INVALID_STOPS = 130;
        public const int ERR_INVALID_TRADE_VOLUME = 131;
        public const int ERR_MARKET_CLOSED = 132;
        public const int ERR_TRADE_DISABLED = 133;
        public const int ERR_NOT_ENOUGH_MONEY = 134;
        public const int ERR_PRICE_CHANGED = 135;
        public const int ERR_OFF_QUOTES = 136;
        public const int ERR_BROKER_BUSY = 137;
        public const int ERR_REQUOTE = 138;
        public const int ERR_TRADE_CONTEXT_BUSY = 146;
        public const int ERR_TRADE_TOO_MANY_ORDERS = 148;
        #endregion

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
            //return CheckupFunctions.GetLastError(this);
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
            return TradingFunctions.OrderSend(this, symbol, cmd, volume, price, slippage, stoploss, takeprofit, comment,
                                              magic, DateTime.Now.AddDays(100), arrow_color);
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
            return TradingFunctions.OrderSelect(this, index, select, pool);
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
            return MarketInfo(symbol, MARKER_INFO_MODE.MODE_ASK);
        }

        public double BidFor(string symbol)
        {
            return MarketInfo(symbol, MARKER_INFO_MODE.MODE_BID);
        }

        public double BuyOpenPriceFor(string symbol) { return AskFor(symbol); }

        public double BuyClosePriceFor(string symbol) { return BidFor(symbol);}

        public double SellOpenPriceFor(string symbol) { return BidFor(symbol); }

        public double SellClosePriceFor(string symbol) { return AskFor(symbol);  }

        public double Bid
        {
            get { return PredefinedVariables.Bid(this); }
        }

        public double Ask
        {
            get { return PredefinedVariables.Ask(this); }
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

            if (lastError == 0) return;

            throw CreateException(lastError);
        }

        private Exception CreateException(int lastError)
        {
            throw new NotImplementedException();
        }

        protected Order Buy(double size, double stopLoss = 0, double takeProfit = 0)
        {
            //// check if stopLoss and take profit valid for buy

            //if (stopLoss != 0 && stopLoss >= BuyOpenPrice) throw new ApplicationException("Stop Loss for Buy have to less than Ask");

            //if (takeProfit != 0 && takeProfit <= BuyOpenPrice) throw new ApplicationException("Take profit for Buy have to more than Ask");

            //int ticket = OrderSend(Symbol, ORDER_TYPE.OP_BUY, size, BuyOpenPrice, 3, stopLoss, takeProfit, "", 12134);

            //// check if we can create and order to ecn 

            //// should host compatible handler in server

            //if (ticket == -1)
            //{
            //    ThrowLatestException();
            //}

            return Buy(Symbol, size, stopLoss, takeProfit);
        }

        protected Order Buy(string symbol, double size, double stopLoss = 0, double takeProfit = 0)
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

            return new Order(symbol, ticket, size, ORDER_TYPE.OP_BUY, this);
        }

        protected Order Sell(double size, double stopLoss = 0, double takeProfit = 0)
        {
            return Sell(Symbol, size, stopLoss, takeProfit);
        }

        protected Order Sell(string symbol, double size, double stopLoss = 0, double takeProfit = 0)
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

            return new Order(symbol, ticket, size, ORDER_TYPE.OP_SELL, this);
        }

        public double BuyOpenPrice { get { return Ask; } }

        public double BuyClosePrice { get { return Bid; } }

        public double SellOpenPrice { get { return Bid; } }

        public double SellClosePrice { get { return Ask; } }

        protected Order PendingBuy(string symbol, double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy
            if (stopLoss != 0 && stopLoss >= entry) throw new ApplicationException("Stop Loss for Buy have to less than entry price");

            if (takeProfit != 0 && takeProfit <= entry) throw new ApplicationException("Take profit for Buy have to more than entry price");

            ORDER_TYPE orderType = default(ORDER_TYPE);

            if (BuyOpenPriceFor(symbol) < entry)
            {
                orderType = ORDER_TYPE.OP_SELLLIMIT;
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

            return new Order(symbol, ticket, size, orderType, this);
            //return new Order(
        }

        protected Order PendingBuy(double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            return PendingBuy(Symbol, entry, stopLoss, takeProfit);
        }

        protected Order PendingSell(double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            return PendingSell(Symbol, size, entry, stopLoss, takeProfit);
        }

        protected Order PendingSell(string symbol, double size, double entry, double stopLoss = 0, double takeProfit = 0)
        {
            // check if stopLoss and take profit valid for buy
            if (stopLoss != 0 && stopLoss <= entry) throw new ApplicationException("Stop Loss for Sell have to more than entry price");

            if (takeProfit != 0 && takeProfit >= entry) throw new ApplicationException("Take profit for Sell have to less than entry price");

            ORDER_TYPE orderType = default(ORDER_TYPE);

            if (SellOpenPriceFor(symbol) < entry)
            {
                // sell limit
                orderType = ORDER_TYPE.OP_BUYSTOP;
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

            return new Order(symbol, ticket, size, orderType, this);
            //return new Order(
        }

        public Closes Close
        {
            get { return new Closes(this); }
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
    }

}


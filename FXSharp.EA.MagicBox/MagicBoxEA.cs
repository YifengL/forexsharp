using System;
using System.Threading;
using FXSharp.TradingPlatform.Exts;
using TradePlatform.MT4.SDK.API;

namespace FXSharp.EA.MagicBox
{
    public class MagicBoxEA : EExpertAdvisor
    {
        //private OrderContext orderContext;

        //+------------------------------------------------------------------+
        //| Global Variables / Includes                                      |
        //+------------------------------------------------------------------+
        //datetime CurrTime = 0;
        //datetime PrevTime = 0;
        private double Lots = 0.1;
        private int MagicNumberD = 1235;
        private int MagicNumberU = 1237;
        private int Periods = 14;
        private int ProfitTarget = 20;
        private int Shift = 1;
        private int Slippage = 3;
        private int StopLoss = 18;
        private string Sym = "GBPUSD";
        private int SymDigits = 5;
        private double SymPoints = 0.0001;
        private int TimeFrame;
        private bool UseCompletedBars = true;

        //+------------------------------------------------------------------+
        //| Expert initialization function                                   |
        //+------------------------------------------------------------------+
        protected override int Init()
        {
            //this.MqlError += this.OnMqlError;
            Sym = Symbol;

            TimeFrame = Period;

            SymPoints = MarketInfo(Sym, MARKER_INFO_MODE.MODE_POINT);
            SymDigits = (int) MarketInfo(Sym, MARKER_INFO_MODE.MODE_DIGITS);
            //---
            if (SymPoints == 0.001)
            {
                SymPoints = 0.01;
                SymDigits = 3;
            }
            else if (SymPoints == 0.00001)
            {
                SymPoints = 0.0001;
                SymDigits = 5;
            }

            //----
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Expert deinitialization function                                 |
        //+------------------------------------------------------------------+
        protected override int DeInit()
        {
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Expert start function                                            |
        //+------------------------------------------------------------------+
        protected override int Start()
        {
            if (CountAll(Sym, MagicNumberU) == 0)
            {
                EnterLong(Sym, Lots, "");
            }
            else
            {
                UpdateU(Sym, MagicNumberU);
            }

            if (CountAll(Sym, MagicNumberD) == 0)
            {
                EnterShrt(Sym, Lots, "");
            }
            else
            {
                UpdateD(Sym, MagicNumberD);
            }
            //----
            return (0);
        }

        //+------------------------------------------------------------------+
        //| Expert Custom Functions                                          |
        //+------------------------------------------------------------------+
        //| Update for Sell Stop()                                                       |
        //+------------------------------------------------------------------+
        private int UpdateD(string Symbole, int Magic)
        {
            //---- 

            //int Ticket = -1; int err = 0; bool OrderLoop = false; int TryCount = 0; int tic = 0;

            for (int i = OrdersTotal() - 1; i >= 0; i--)
            {
                OrderSelect(i, SELECT_BY.SELECT_BY_POS, POOL_MODES.MODE_TRADES);
                if (OrderMagicNumber() != Magic) continue;
                if (OrderSymbol() != Symbole) continue;


                if ((OrderOpenPrice() + Bid) > 0.007)
                {
                    EnterShrtUpdate(Symbole, Lots, "updated", OrderTicket());
                }
                else if ((OrderOpenPrice() + Bid) < 0.0005)
                {
                    EnterShrtUpdate(Symbole, Lots, "updated", OrderTicket());
                }
            }
            //----
            return (0);
        }

        //| Place Short Update Order                                                 |
        //+------------------------------------------------------------------+
        private int EnterShrtUpdate(string FinalSymbol, double FinalLots, string EA_Comment, int tic)
        {
            int Ticket = -1;
            int err = 0;
            bool OrderLoop = false;
            int TryCount = 0;

            //this.MqlError()
            while (!OrderLoop)
            {
                while (IsTradeContextBusy())
                {
                    Thread.Sleep(10);
                }

                RefreshRates();
                double SymAsk = NormalizeDouble(MarketInfo(FinalSymbol, MODE_ASK), SymDigits);
                double SymBid = NormalizeDouble(MarketInfo(FinalSymbol, MODE_BID), SymDigits);
                double point = MarketInfo(Symbol, MODE_POINT);

                if (OrderSelect(tic, SELECT_BY.SELECT_BY_TICKET))
                    if ((int) OrderType() < 2) continue;
                {
                    OrderModify(tic, SymBid - 100*point, StopShrt(SymAsk - 100*point, StopLoss, SymPoints, SymDigits),
                                TakeShrt(SymBid, ProfitTarget, SymPoints, SymDigits), DateTime.MaxValue, CLR_NONE);
                }

                int Err = GetLastError();

                switch ((MQLError) Err)
                {
                        //---- Success
                    case MQLError.ERR_NO_ERROR:
                        OrderLoop = true;
                        //     if( OrderSelect( Ticket, SELECT_BY_TICKET ) )
                        //      { OrderModify( Ticket, OrderOpenPrice(), StopLong(SymBid,StopLoss, SymPoints,SymDigits), TakeLong(SymAsk,ProfitTarget,SymPoints,SymDigits), 0, CLR_NONE ); }
                        break;

                        //---- Retry Error     
                    case MQLError.ERR_SERVER_BUSY:
                    case MQLError.ERR_NO_CONNECTION:
                    case MQLError.ERR_INVALID_PRICE:
                    case MQLError.ERR_OFF_QUOTES:
                    case MQLError.ERR_BROKER_BUSY:
                    case MQLError.ERR_TRADE_CONTEXT_BUSY:
                        TryCount++;
                        break;
                    case MQLError.ERR_PRICE_CHANGED:
                    case MQLError.ERR_REQUOTE:
                        continue;

                        //---- Fatal known Error 
                    case MQLError.ERR_INVALID_STOPS:
                        OrderLoop = true;
                        Print("Invalid Stops");
                        break;
                    case MQLError.ERR_INVALID_TRADE_VOLUME:
                        OrderLoop = true;
                        Print("Invalid Lots");
                        break;
                    case MQLError.ERR_MARKET_CLOSED:
                        OrderLoop = true;
                        Print("Market Close");
                        break;
                    case MQLError.ERR_TRADE_DISABLED:
                        OrderLoop = true;
                        Print("Trades Disabled");
                        break;
                    case MQLError.ERR_NOT_ENOUGH_MONEY:
                        OrderLoop = true;
                        Print("Not Enough Money");
                        break;
                    case MQLError.ERR_TRADE_TOO_MANY_ORDERS:
                        OrderLoop = true;
                        Print("Too Many Orders");
                        break;

                        //---- Fatal Unknown Error
                    case MQLError.ERR_NO_RESULT:
                    default:
                        OrderLoop = true;
                        Print("Unknown Error - " + Err);
                        break;
                        //----                         
                }
                // end switch 
                if (TryCount > 10)
                {
                    OrderLoop = true;
                }
            }
            //----               
            return (0);
        }

        //+------------------------------------------------------------------+
        //+------------------------------------------------------------------+
        //| Update for Buy Stop()                                                       |
        //+------------------------------------------------------------------+
        private int UpdateU(string Symbole, int Magic)
        {
            int Ticket = -1;
            int err = 0;
            bool OrderLoop = false;
            int TryCount = 0;
            int tic = 0;


            for (int i = OrdersTotal() - 1; i >= 0; i--)
            {
                OrderSelect(i, SELECT_BY.SELECT_BY_POS, MODE_TRADES);
                if (OrderMagicNumber() != Magic) continue;
                if (OrderSymbol() != Symbole) continue;

                if ((OrderOpenPrice() - Ask) < 0.0005)
                {
                    EnterLongUpdate(Symbole, Lots, "updated", OrderTicket());
                }
                else if ((OrderOpenPrice() - Ask) > 0.0007)
                {
                    EnterLongUpdate(Symbole, Lots, "updated", OrderTicket());
                }
            }
            //----
            return (0);
        }


        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Place Long Order                                                 |
        //+------------------------------------------------------------------+
        private int EnterLongUpdate(string FinalSymbol, double FinalLots, string EA_Comment, int tic)
        {
            int Ticket = -1;
            int err = 0;
            bool OrderLoop = false;
            int TryCount = 0;

            while (!OrderLoop)
            {
                while (IsTradeContextBusy())
                {
                    Thread.Sleep(10);
                }

                RefreshRates();
                double SymAsk = NormalizeDouble(MarketInfo(FinalSymbol, MODE_ASK), SymDigits);
                double SymBid = NormalizeDouble(MarketInfo(FinalSymbol, MODE_BID), SymDigits);
                double point = MarketInfo(Symbol, MODE_POINT);

                if (OrderSelect(tic, SELECT_BY.SELECT_BY_TICKET))

                    if ((int) OrderType() < 2) continue;

                {
                    OrderModify(tic, SymBid + 100*point, StopLong(SymBid + 80*point, StopLoss, SymPoints, SymDigits),
                                TakeLong(SymBid, ProfitTarget, SymPoints, SymDigits), DateTime.MaxValue, CLR_NONE);
                }

                int Err = GetLastError();

                switch ((MQLError) Err)
                {
                        //---- Success
                    case MQLError.ERR_NO_ERROR:
                        OrderLoop = true;
                        break;

                        //---- Retry Error     
                    case MQLError.ERR_SERVER_BUSY:
                    case MQLError.ERR_NO_CONNECTION:
                    case MQLError.ERR_INVALID_PRICE:
                    case MQLError.ERR_OFF_QUOTES:
                    case MQLError.ERR_BROKER_BUSY:
                    case MQLError.ERR_TRADE_CONTEXT_BUSY:
                        TryCount++;
                        break;
                    case MQLError.ERR_PRICE_CHANGED:
                    case MQLError.ERR_REQUOTE:
                        continue;

                        //---- Fatal known Error 
                    case MQLError.ERR_INVALID_STOPS:
                        OrderLoop = true;
                        Print("Invalid Stops");
                        break;
                    case MQLError.ERR_INVALID_TRADE_VOLUME:
                        OrderLoop = true;
                        Print("Invalid Lots");
                        break;
                    case MQLError.ERR_MARKET_CLOSED:
                        OrderLoop = true;
                        Print("Market Close");
                        break;
                    case MQLError.ERR_TRADE_DISABLED:
                        OrderLoop = true;
                        Print("Trades Disabled");
                        break;
                    case MQLError.ERR_NOT_ENOUGH_MONEY:
                        OrderLoop = true;
                        Print("Not Enough Money");
                        break;
                    case MQLError.ERR_TRADE_TOO_MANY_ORDERS:
                        OrderLoop = true;
                        Print("Too Many Orders");
                        break;

                        //---- Fatal Unknown Error
                    case MQLError.ERR_NO_RESULT:
                    default:
                        OrderLoop = true;
                        Print("Unknown Error - " + Err);
                        break;
                        //----                         
                }
                // end switch 
                if (TryCount > 10)
                {
                    OrderLoop = true;
                }
            }
            //----               
            return (0);
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| CountAll()                                                       |
        //+------------------------------------------------------------------+
        private int CountAll(string Symbole, int Magic)
        {
            //---- 
            int count = 0;
            if (OrdersTotal() < 1)
            {
                return (count);
            }


            for (int i = OrdersTotal() - 1; i >= 0; i--)
            {
                OrderSelect(i, SELECT_BY.SELECT_BY_POS, POOL_MODES.MODE_TRADES);
                if (OrderMagicNumber() != Magic) continue;
                if (OrderSymbol() != Symbole) continue;

                // ORDER TYPE Screw up !!
                if (OrderType() == ORDER_TYPE.OP_SELLLIMIT)
                {
                    count++;
                }
                else if (OrderType() == ORDER_TYPE.OP_SELLSTOP)
                {
                    count++;
                }
            }
            //----
            return (count);
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Calculate Stop Long                                              |
        //+------------------------------------------------------------------+
        private double StopLong(double price, double stop, double point, double SymDgts)
        {
            if (stop == 0)
            {
                return (0);
            }
            else
            {
                return (NormalizeDouble(price - (stop*point), SymDgts));
            }
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Calculate Stop Short                                             |
        //+------------------------------------------------------------------+
        private double StopShrt(double price, double stop, double point, double SymDgts)
        {
            if (stop == 0)
            {
                return (0);
            }
            else
            {
                return (NormalizeDouble(price + (stop*point), SymDgts));
            }
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Calculate Profit Target Long                                     |
        //+------------------------------------------------------------------+
        private double TakeLong(double price, double take, double point, double SymDgts)
        {
            if (take == 0)
            {
                return (0);
            }
            else
            {
                return (NormalizeDouble(price + (take*point), SymDgts));
            }
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Calculate Profit Target Long                                     |
        //+------------------------------------------------------------------+
        private double TakeShrt(double price, double take, double point, double SymDgts)
        {
            if (take == 0)
            {
                return (0);
            }
            else
            {
                return (NormalizeDouble(price - (take*point), SymDgts));
            }
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Place Long Order                                                 |
        //+------------------------------------------------------------------+
        private int EnterLong(string FinalSymbol, double FinalLots, string EA_Comment)
        {
            int Ticket = -1;
            int err = 0;
            bool OrderLoop = false;
            int TryCount = 0;

            while (!OrderLoop)
            {
                while (IsTradeContextBusy())
                {
                    Thread.Sleep(10);
                }

                RefreshRates();
                double SymAsk = NormalizeDouble(MarketInfo(FinalSymbol, MODE_ASK), SymDigits);
                double SymBid = NormalizeDouble(MarketInfo(FinalSymbol, MODE_BID), SymDigits);
                double point = MarketInfo(Symbol, MODE_POINT);

                // defect !!! ORDER_TYPE is screw up
                Ticket = OrderSend(FinalSymbol, ORDER_TYPE.OP_SELLLIMIT, FinalLots, SymBid + 100*point, 0,
                                   StopLong(SymAsk + 100*point, StopLoss + 200, SymPoints, SymDigits),
                                   TakeLong(SymBid, ProfitTarget, SymPoints, SymDigits), "some comment", MagicNumberU,
                                   DateTime.MaxValue, CLR_NONE);

                int Err = GetLastError();

                switch ((MQLError) Err)
                {
                        //---- Success
                        // case               ERR_NO_ERROR: OrderLoop = true; 
                        //     if( OrderSelect( Ticket, SELECT_BY_TICKET ) )
                        //      { OrderModify( Ticket, OrderOpenPrice(), StopLong(SymBid,StopLoss, SymPoints,SymDigits), TakeLong(SymAsk,ProfitTarget,SymPoints,SymDigits), 0, CLR_NONE ); }
                        //      break;

                        //---- Retry Error     
                    case MQLError.ERR_SERVER_BUSY:
                    case MQLError.ERR_NO_CONNECTION:
                    case MQLError.ERR_INVALID_PRICE:
                    case MQLError.ERR_OFF_QUOTES:
                    case MQLError.ERR_BROKER_BUSY:
                    case MQLError.ERR_TRADE_CONTEXT_BUSY:
                        TryCount++;
                        break;
                    case MQLError.ERR_PRICE_CHANGED:
                    case MQLError.ERR_REQUOTE:
                        continue;

                        //---- Fatal known Error 
                    case MQLError.ERR_INVALID_STOPS:
                        OrderLoop = true;
                        Print("Invalid Stops");
                        break;
                    case MQLError.ERR_INVALID_TRADE_VOLUME:
                        OrderLoop = true;
                        Print("Invalid Lots");
                        break;
                    case MQLError.ERR_MARKET_CLOSED:
                        OrderLoop = true;
                        Print("Market Close");
                        break;
                    case MQLError.ERR_TRADE_DISABLED:
                        OrderLoop = true;
                        Print("Trades Disabled");
                        break;
                    case MQLError.ERR_NOT_ENOUGH_MONEY:
                        OrderLoop = true;
                        Print("Not Enough Money");
                        break;
                    case MQLError.ERR_TRADE_TOO_MANY_ORDERS:
                        OrderLoop = true;
                        Print("Too Many Orders");
                        break;

                        //---- Fatal Unknown Error
                    case MQLError.ERR_NO_RESULT:
                    default:
                        OrderLoop = true;
                        Print("Unknown Error - " + Err);
                        break;
                        //----                         
                }
                // end switch 
                if (TryCount > 10)
                {
                    OrderLoop = true;
                }
            }
            //----               
            return (Ticket);
        }

        //+------------------------------------------------------------------+

        //+------------------------------------------------------------------+
        //| Place Shrt Order                                                 |
        //+------------------------------------------------------------------+
        private int EnterShrt(string FinalSymbol, double FinalLots, string EA_Comment)
        {
            int Ticket = -1;
            int err = 0;
            bool OrderLoop = false;
            int TryCount = 0;

            while (!OrderLoop)
            {
                while (IsTradeContextBusy())
                {
                    Thread.Sleep(10);
                }

                RefreshRates();
                double SymAsk = NormalizeDouble(MarketInfo(FinalSymbol, MODE_ASK), SymDigits);
                double SymBid = NormalizeDouble(MarketInfo(FinalSymbol, MODE_BID), SymDigits);
                double point = MarketInfo(Symbol, MODE_POINT);

                Ticket = OrderSend(FinalSymbol, ORDER_TYPE.OP_SELLSTOP, FinalLots, SymBid - 100*point, 0,
                                   StopShrt(SymAsk - 100*point, StopLoss, SymPoints, SymDigits),
                                   TakeShrt(SymBid, ProfitTarget, SymPoints, SymDigits), "some comment", MagicNumberD,
                                   DateTime.MaxValue, CLR_NONE);

                int Err = GetLastError();

                switch ((MQLError) Err)
                {
                        //---- Success
                        //    case               ERR_NO_ERROR: OrderLoop = true;
                        // if( OrderSelect( Ticket, SELECT_BY_TICKET ) )
                        // { OrderModify( Ticket, OrderOpenPrice(), StopShrt(SymAsk,StopLoss, SymPoints,SymDigits), TakeShrt(SymBid,ProfitTarget, SymPoints,SymDigits), 0, CLR_NONE ); }
                        // break;

                        //---- Retry Error     
                    case MQLError.ERR_SERVER_BUSY:
                    case MQLError.ERR_NO_CONNECTION:
                    case MQLError.ERR_INVALID_PRICE:
                    case MQLError.ERR_OFF_QUOTES:
                    case MQLError.ERR_BROKER_BUSY:
                    case MQLError.ERR_TRADE_CONTEXT_BUSY:
                        TryCount++;
                        break;
                    case MQLError.ERR_PRICE_CHANGED:
                    case MQLError.ERR_REQUOTE:
                        continue;

                        //---- Fatal known Error 
                    case MQLError.ERR_INVALID_STOPS:
                        OrderLoop = true;
                        Print("Invalid Stops");
                        break;
                    case MQLError.ERR_INVALID_TRADE_VOLUME:
                        OrderLoop = true;
                        Print("Invalid Lots");
                        break;
                    case MQLError.ERR_MARKET_CLOSED:
                        OrderLoop = true;
                        Print("Market Close");
                        break;
                    case MQLError.ERR_TRADE_DISABLED:
                        OrderLoop = true;
                        Print("Trades Disabled");
                        break;
                    case MQLError.ERR_NOT_ENOUGH_MONEY:
                        OrderLoop = true;
                        Print("Not Enough Money");
                        break;
                    case MQLError.ERR_TRADE_TOO_MANY_ORDERS:
                        OrderLoop = true;
                        Print("Too Many Orders");
                        break;

                        //---- Fatal Unknown Error
                    case MQLError.ERR_NO_RESULT:
                    default:
                        OrderLoop = true;
                        Print("Unknown Error - " + Err);
                        break;
                        //----                         
                }
                // end switch 
                if (TryCount > 10)
                {
                    OrderLoop = true;
                }
            }
            //----               
            return (Ticket);
        }

        //+------------------------------------------------------------------+
    }
}
using TradePlatform.MT4.Core;

namespace TradePlatform.MT4.SDK.Library.Scripts
{
    //using TradePlatform.MT4.Data;

    public class ImportOrdersScript : ExpertAdvisor
    {
        protected override int Init()
        {
            return 1;
        }

        protected override int Start()
        {
            //using (TradePlatformEntities model = new TradePlatformEntities())
            //{
            //    int currentNumber = this.AccountNumber();
            //    TradeAccount account = model.TradeAccounts.SingleOrDefault(x => x.AccountNumber == currentNumber);

            //    if (account == null)
            //    {
            //        account = new TradeAccount();
            //        model.TradeAccounts.AddObject(account);
            //    }

            //    account.AccountBalance = this.AccountBalance();
            //    account.AccountCredit = this.AccountCredit();
            //    account.AccountCompany = this.AccountCompany();
            //    account.AccountCurrency = this.AccountCurrency();
            //    account.AccountEquity = this.AccountEquity();
            //    account.AccountFreeMargin = this.AccountFreeMargin();
            //    account.AccountFreeMarginMode = this.AccountFreeMarginMode();
            //    account.AccountLeverage = this.AccountLeverage();
            //    account.AccountMargin = this.AccountMargin();
            //    account.AccountName = this.AccountName();
            //    account.AccountNumber = this.AccountNumber();
            //    account.AccountProfit = this.AccountProfit();
            //    account.AccountServer = this.AccountServer();
            //    account.AccountStopoutLevel = this.AccountStopoutLevel();
            //    account.AccountStopoutMode = this.AccountStopoutMode();

            //    List<TradeOrder> openedOrders = account.TradeOrders.Where(x => x.IsClosed == false).ToList();

            //    openedOrders.ForEach(x => model.TradeOrders.DeleteObject(x));

            //    for (int i = this.OrdersTotal() - 1; i >= 0; i--)
            //    {
            //        if (this.OrderSelect(i, SELECT_BY.SELECT_BY_POS, POOL_MODES.MODE_TRADES))
            //        {
            //            TradeOrder order = new TradeOrder();

            //            order.ClosePrice = this.OrderClosePrice();
            //            order.CloseTime = this.OrderCloseTime();
            //            order.Comment = this.OrderComment();
            //            order.Number = this.OrderTicket();
            //            order.OpenPrice = this.OrderOpenPrice();
            //            order.OpenTime = this.OrderOpenTime();
            //            order.Profit = this.OrderProfit();
            //            order.Size = this.OrderLots();
            //            order.StopLoss = this.OrderStopLoss();
            //            order.Swap = this.OrderSwap();
            //            order.Symbol = this.OrderSymbol();
            //            order.TakeProfit = this.OrderTakeProfit();
            //            order.Type = this.OrderType().ToString();
            //            order.MagicNumber = this.OrderMagicNumber();
            //            order.Commission = this.OrderCommission();
            //            order.IsClosed = false;

            //            account.TradeOrders.Add(order);
            //        }
            //    }

            //    List<int> list = account.TradeOrders.Select(order => order.Number).ToList();

            //    for (int i = this.HistoryTotal() - 1; i >= 0; i--)
            //    {
            //        if (this.OrderSelect(i, SELECT_BY.SELECT_BY_POS, POOL_MODES.MODE_HISTORY))
            //        {
            //            if (!list.Contains(this.OrderTicket()))
            //            {
            //                TradeOrder order = new TradeOrder();
            //                order.ClosePrice = this.OrderClosePrice();
            //                order.CloseTime = this.OrderCloseTime();
            //                order.Comment = this.OrderComment();
            //                order.Number = this.OrderTicket();
            //                order.OpenPrice = this.OrderOpenPrice();
            //                order.OpenTime = this.OrderOpenTime();
            //                order.Profit = this.OrderProfit();
            //                order.Size = this.OrderLots();
            //                order.StopLoss = this.OrderStopLoss();
            //                order.Swap = this.OrderSwap();
            //                order.Symbol = this.OrderSymbol();
            //                order.TakeProfit = this.OrderTakeProfit();
            //                order.Type = this.OrderType().ToString();
            //                order.MagicNumber = this.OrderMagicNumber();
            //                order.Commission = this.OrderCommission();
            //                order.IsClosed = true;

            //                account.TradeOrders.Add(order);
            //            }
            //        }
            //    }

            //    model.SaveChanges();
            //}

            return 2;
        }

        protected override int DeInit()
        {
            return 3;
        }
    }
}
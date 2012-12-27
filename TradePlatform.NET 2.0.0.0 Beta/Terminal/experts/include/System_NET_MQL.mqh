#property copyright "Copyright © 2012, Vladimir Kaloshin"
#property link      "http://solyanka.net"

string System_NET_MQL(string message[])
{
	string result = "###NORESULT###";
	
	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_Custom(message);
	}
	
	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_Variables(message);
	}
	
	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_Info(message);
	}
	
	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_Common(message);
	}

	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_Trading(message);
	}	
	
	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_WindowFunctions(message);
	}

	if(result == "###NORESULT###")
	{
		result = System_NET_MQL_TechIndicators(message);
	}
	
	return (result);
}

string System_NET_MQL_Variables(string message[])
{
	if(message[1] == "Ask")
	return(Ask);
	
	if(message[1] == "Bars")
	return(Bars);
	
	if(message[1] == "Bid")
	return(Bid);
	
	if(message[1] == "Close")
	return(Close[StrToInteger(message[2])]);
	
	if(message[1] == "Digits")
	return(Digits);
	
	if(message[1] == "High")
	return(High[StrToInteger(message[2])]);
	
	if(message[1] == "Low")
	return(Low[StrToInteger(message[2])]);
	
	if(message[1] == "Open")
	return(Open[StrToInteger(message[2])]);
	
	if(message[1] == "Point")
	return(Point);
	
	if(message[1] == "Time")
	return(TimeToStr(Time[StrToInteger(message[2])], TIME_DATE) + " " + TimeToStr(Time[StrToInteger(message[2])], TIME_SECONDS));
	
	if(message[1] == "Volume")
	return(Volume[StrToInteger(message[2])]);
	
	if(message[1] == "IndicatorCounted")
	return(IndicatorCounted());
	
	return("###NORESULT###");

}


string System_NET_MQL_Common(string message[])
{
	if(message[1] == "Alert")
	{
	  Alert(message[2]);
	  return("");
	}
	
	if(message[1] == "Comment")
	{
	  Comment(message[2]);
	  return("");
	}
	
	if(message[1] == "GetTickCount")
	return(GetTickCount());
	
	if(message[1] == "MarketInfo")
	return(MarketInfo(message[2], StrToInteger(message[3])));
	
	if(message[1] == "PlaySound")
	{
	  PlaySound(message[2]);
	  return("");
	}
	
	if(message[1] == "Print")
	{
	  Print(message[2]);
	  return("");
	}
	
	if(message[1] == "SendNotification")
	return(SendNotification(message[2]));
	
	return("###NORESULT###");
}

string System_NET_MQL_Info(string message[])
{
	if(message[1] == "AccountBalance")
	return(AccountBalance());
	
	if(message[1] == "AccountCredit")
	return(AccountCredit());
	
	if(message[1] == "AccountCompany")
	return(AccountCompany());
	
	if(message[1] == "AccountCurrency")
	return(AccountCurrency());
	
	if(message[1] == "AccountEquity")
	return(AccountEquity());
	
	if(message[1] == "AccountFreeMargin")
	return(AccountFreeMargin());
	
	if(message[1] == "AccountFreeMarginCheck")
	return(AccountFreeMarginCheck(message[2], StrToInteger(message[3]), StrToDouble(message[4])));
	
	if(message[1] == "AccountFreeMarginMode")
	return(AccountFreeMarginMode());
	
	if(message[1] == "AccountLeverage")
	return(AccountLeverage());
	
	if(message[1] == "AccountMargin")
	return(AccountMargin());
	
	if(message[1] == "AccountName")
	return(AccountName());
	
	if(message[1] == "AccountNumber")
	return(AccountNumber());
	
	if(message[1] == "AccountProfit")
	return(AccountProfit());
	
	if(message[1] == "AccountServer")
	return(AccountServer());
	
	if(message[1] == "AccountStopoutLevel")
	return(AccountStopoutLevel());
	
	if(message[1] == "AccountStopoutMode")
	return(AccountStopoutMode());	
	
	return("###NORESULT###");

}

string System_NET_MQL_Trading(string message[])
{
	if(message[1] == "OrderClose")
	return(OrderClose(StrToInteger(message[2]), StrToDouble(message[3]), StrToDouble(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "OrderCloseBy")
	return(OrderCloseBy(StrToInteger(message[2]), StrToInteger(message[3]), StrToInteger(message[4])));
	
	if(message[1] == "OrderClosePrice")
	return(OrderClosePrice());
	
	if(message[1] == "OrderCloseTime")
	return(TimeToStr(OrderCloseTime(), TIME_DATE) + " " + TimeToStr(OrderCloseTime(), TIME_SECONDS));
	
	if(message[1] == "OrderComment")
	return(OrderComment());

	if(message[1] == "OrderCommission")
	return(OrderCommission());
	
	if(message[1] == "OrderDelete")
	return(OrderDelete(StrToInteger(message[2]), StrToInteger(message[3])));
	
	if(message[1] == "OrderExpiration")
	return(TimeToStr(OrderExpiration(), TIME_DATE) + " " + TimeToStr(OrderExpiration(), TIME_SECONDS));
	
	if(message[1] == "OrderLots")
	return(OrderLots());
	
	if(message[1] == "OrderMagicNumber")
	return(OrderMagicNumber());
	
	if(message[1] == "OrderModify")
	return(OrderModify(StrToInteger(message[2]),StrToDouble(message[3]), StrToDouble(message[4]),StrToDouble(message[5]), StrToTime(message[6])));
	
	if(message[1] == "OrderOpenPrice")
	return(OrderOpenPrice());
	
	if(message[1] == "OrderOpenTime")
	return(TimeToStr(OrderOpenTime(), TIME_DATE) + " " + TimeToStr(OrderOpenTime(), TIME_SECONDS));
	
	if(message[1] == "OrderPrint")
	return(OrderPrint());
	
	if(message[1] == "OrderProfit")
	return(OrderProfit());
	
	if(message[1] == "OrderSelect")
	return(OrderSelect(StrToInteger(message[2]), StrToInteger(message[3]), StrToInteger(message[4])));

	if(message[1] == "OrderSend")
	return(OrderSend(message[2], StrToInteger(message[3]), StrToDouble(message[4]), StrToDouble(message[5]), StrToInteger(message[6]), StrToDouble(message[7]),StrToDouble(message[8]), StringTrimLeft(message[9]),StrToInteger(message[10]),StrToTime(message[11]),StrToInteger(message[12])));
	
	if(message[1] == "HistoryTotal")
	return(HistoryTotal());
	
	if(message[1] == "OrderStopLoss")
	return(OrderStopLoss());
	
	if(message[1] == "OrdersTotal")
	return(OrdersTotal());
	
	if(message[1] == "OrderSwap")
	return(OrderSwap());
	
	if(message[1] == "OrderSymbol")
	return(OrderSymbol());
	
	if(message[1] == "OrderTakeProfit")
	return(OrderTakeProfit());
	
	if(message[1] == "OrderTicket")
	return(OrderTicket());
	
	if(message[1] == "OrderType")
	return(OrderType());	
	
	return("###NORESULT###");
}

string System_NET_MQL_WindowFunctions(string message[])
{
	if(message[1] == "HideTestIndicators")
	{
		HideTestIndicators(StrToInteger(message[2]));
		return("");
	}
	
	if(message[1] == "Period")
	return(Period());
	
	if(message[1] == "RefreshRates")
	return(RefreshRates());
	
	if(message[1] == "Symbol")
	return(Symbol());
	
	if(message[1] == "WindowBarsPerChart")
	return(WindowBarsPerChart());
	
	if(message[1] == "WindowExpertName")
	return(WindowExpertName());
	
	if(message[1] == "WindowFind")
	return(WindowFind(message[2]));
	
	if(message[1] == "WindowFirstVisibleBar")
	return(WindowFirstVisibleBar());
	
	if(message[1] == "WindowHandle")
	return(WindowHandle(message[2], StrToInteger(message[3])));
	
	if(message[1] == "WindowIsVisible")
	return(WindowIsVisible(StrToInteger(message[2])));
	
	if(message[1] == "WindowOnDropped")
	return(WindowOnDropped());
	
	if(message[1] == "WindowPriceMax")
	return(WindowPriceMax(StrToInteger(message[2])));
	
	if(message[1] == "WindowPriceMin")
	return(WindowPriceMin(StrToInteger(message[2])));
	
	if(message[1] == "WindowPriceOnDropped")
	return(WindowPriceOnDropped());
	
	if(message[1] == "WindowRedraw")
	{
		WindowRedraw();
		return("");
	}
	
	if(message[1] == "WindowScreenShot")
	return(WindowScreenShot(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7])));
	
	if(message[1] == "WindowTimeOnDropped")
	return(TimeToStr(WindowTimeOnDropped(), TIME_DATE) + " " + TimeToStr(WindowTimeOnDropped(), TIME_SECONDS));
	
	if(message[1] == "WindowsTotal")
	return(WindowsTotal());
	
	if(message[1] == "WindowXOnDropped")
	return(WindowXOnDropped());
	
	if(message[1] == "WindowYOnDropped")
	return(WindowYOnDropped());
	
	return("###NORESULT###");
}

string System_NET_MQL_TechIndicators(string message[])
{
	if(message[1] == "iAC")
	return(iAC(message[2], StrToInteger(message[3]), StrToInteger(message[4])));
	
	if(message[1] == "iAD")
	return(iAD(message[2], StrToInteger(message[3]), StrToInteger(message[4])));
	
	if(message[1] == "iAlligator")
	return(iAlligator(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8]), StrToInteger(message[9]), StrToInteger(message[10]), StrToInteger(message[11]), StrToInteger(message[12]), StrToInteger(message[13])));
	
	if(message[1] == "iADX")
	return(iADX(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[6])));
	
	if(message[1] == "iATR")
	return(iATR(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	if(message[1] == "iAO")
	return(iAO(message[2], StrToInteger(message[3]), StrToInteger(message[4])));
	
	if(message[1] == "iBearsPower")
	return(iBearsPower(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iBands")
	return(iBands(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8]), StrToInteger(message[9])));
	
	if(message[1] == "iBullsPower")
	return(iBullsPower(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iCCI")
	return(iCCI(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iDeMarker")
	return(iDeMarker(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	if(message[1] == "iEnvelopes")
	return(iEnvelopes(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToDouble(message[8]), StrToInteger(message[9]), StrToInteger(message[10])));
	
	if(message[1] == "iForce")
	return(iForce(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7])));
	
	if(message[1] == "iFractals")
	return(iFractals(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	if(message[1] == "iGator")
	return(iGator(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8]), StrToInteger(message[9]), StrToInteger(message[10]), StrToInteger(message[11]), StrToInteger(message[12]), StrToInteger(message[13])));
	
	if(message[1] == "iIchimoku")
	return(iIchimoku(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8])));
	
	if(message[1] == "iBWMFI")
	return(iBWMFI(message[2], StrToInteger(message[3]), StrToInteger(message[4])));
	
	if(message[1] == "iMomentum")
	return(iMomentum(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iMFI")
	return(iMFI(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	if(message[1] == "iMA")
	return(iMA(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8])));
	
	if(message[1] == "iOsMA")
	return(iOsMA(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8])));
	
	if(message[1] == "iMACD")
	return(iMACD(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8]), StrToInteger(message[9])));
	
	if(message[1] == "iOBV")
	return(iOBV(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	if(message[1] == "iSAR")
	return(iSAR(message[2], StrToInteger(message[3]), StrToDouble(message[4]), StrToDouble(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iRSI")
	return(iRSI(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iRVI")
	return(iRVI(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6])));
	
	if(message[1] == "iStdDev")
	return(iStdDev(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8])));
	
	if(message[1] == "iStochastic")
	return(iStochastic(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5]), StrToInteger(message[6]), StrToInteger(message[7]), StrToInteger(message[8]), StrToInteger(message[9]), StrToInteger(message[10])));
	
	if(message[1] == "iWPR")
	return(iWPR(message[2], StrToInteger(message[3]), StrToInteger(message[4]), StrToInteger(message[5])));
	
	return("###NORESULT###");
}
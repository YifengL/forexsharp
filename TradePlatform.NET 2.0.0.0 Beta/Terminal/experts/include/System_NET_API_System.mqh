#property copyright "Copyright © 2012, Vladimir Kaloshin"
#property link      "http://solyanka.net"


void System_NET_API_SendMail(string subject, string body)
{
	System_NET_CallFunction(System_NET_ApplicationName, "SendMail", subject + "|" + body);
}

int System_NET_API_Strategy_Init()
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_Init");
	return(StrToInteger(result));
}

int System_NET_API_Strategy_Start(int i, double price)
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_Start", i + "|" + price);
	return(StrToInteger(result));
}

string System_NET_API_Strategy_OLP(int i, double price)
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_OpenLongPositions", i + "|" + price);
	return(result);
}

string System_NET_API_Strategy_OSP(int i, double price)
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_OpenShortPositions", i + "|" + price);
	return(result);
}

string System_NET_API_Strategy_MLP(int i, double price)
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_ModifyLongPositions", i + "|" + price);
	return(result);
}

string System_NET_API_Strategy_MSP(int i, double price)
{
	string result = System_NET_CallFunction(System_NET_ApplicationName, "Strategy_ModifyShortPositions", i + "|" + price);
	return(result);
}

void System_NET_API_Strategy_LOC()
{
	System_NET_CallFunction(System_NET_ApplicationName, "Strategy_LongOrderClosed");
}

void System_NET_API_Strategy_SOC()
{
	System_NET_CallFunction(System_NET_ApplicationName, "Strategy_ShortOrderClosed");
}

void System_NET_API_Strategy_LOE()
{
	System_NET_CallFunction(System_NET_ApplicationName, "Strategy_LongOrderEven");
}

void System_NET_API_Strategy_SOE()
{
	System_NET_CallFunction(System_NET_ApplicationName, "Strategy_ShortOrderEven");
}
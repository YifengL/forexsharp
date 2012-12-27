#property copyright "Copyright © 2012, Vladimir Kaloshin"
#property link      "http://solyanka.net"

int System_NET_API_Init()
{
	string result = System_NET_CallFunction(System_NET_HandlerName, "Init");
	return(StrToInteger(result));
}

int System_NET_API_Start()
{
	string result = System_NET_CallFunction(System_NET_HandlerName, "Start");
	return(StrToInteger(result));
}

int System_NET_API_DeInit()
{
	string result = System_NET_CallFunction(System_NET_HandlerName, "DeInit");
	return(StrToInteger(result));
}
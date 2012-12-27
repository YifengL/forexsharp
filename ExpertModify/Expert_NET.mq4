#property copyright "Copyright © 2012, Vladimir Kaloshin"
#property link      "http://solyanka.net/"

#property show_inputs

extern string System_NET_HandlerName = "";

// .NET Integration
#include <System_NET_API.mqh>
#include <System_NET_MQL.mqh>
#include <System_NET.mqh>

int init()
{
   System_NET_Init();

	System_NET_API_Init();
	//System_NET_CallFunction(System_NET_HandlerName, "SendMail", "Expert initialized|Expert initialized");
}

int start()
{
   //System_NET_CallFunction("TickCounter", "Begin");
	System_NET_API_Start();
	//System_NET_CallFunction("TickCounter", "End");
}

int deinit()
{
	//System_NET_CallFunction(System_NET_HandlerName, "SendMail", "Expert terminated|Expert terminated");
	System_NET_API_DeInit();
	
   System_NET_DeInit();
}

string System_NET_MQL_Custom(string message[])
{
	if(message[1] == "METHOD_NAME")
	return("RETURN_VALUE");
	
	return("###NORESULT###");
}
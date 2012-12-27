#property copyright "Copyright © 2012, Vladimir Kaloshin"
#property link      "http://solyanka.net"

#property indicator_chart_window
#property indicator_minimum 1
#property indicator_maximum 10
#property indicator_buffers 2
#property indicator_color1 Green
#property indicator_color2 Red

#property show_inputs

extern string System_NET_HandlerName = "PrevDayHighLowIndicator";

//--- buffers
double ExtMapBuffer1[];
double ExtMapBuffer2[];

// .NET Integration
#include <System_NET_API.mqh>
#include <System_NET_MQL.mqh>
#include <System_NET.mqh>

int init()
{
	SetIndexStyle(0,DRAW_LINE);
	SetIndexBuffer(0,ExtMapBuffer1);
	SetIndexStyle(1,DRAW_LINE);
	SetIndexBuffer(1,ExtMapBuffer2);

   System_NET_Init();
	System_NET_API_Init();
}

int start()
{
	System_NET_API_Start();
}

int deinit()
{
	System_NET_API_DeInit();	
	System_NET_DeInit();
}

string System_NET_MQL_Custom(string message[])
{
	if(message[1] == "ExtMapBuffer1")
	{
		ExtMapBuffer1[StrToInteger(message[2])] = StrToDouble(message[3]);
		return;
	}
	
	if(message[1] == "ExtMapBuffer2")
	{
		ExtMapBuffer2[StrToInteger(message[2])] = StrToDouble(message[3]);
		return;
	}
	
	return("###NORESULT###");
}
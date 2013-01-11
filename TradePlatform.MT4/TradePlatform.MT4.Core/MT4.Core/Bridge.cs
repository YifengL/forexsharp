using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel.Web;
using TradePlatform.MT4.Core.Config;
using TradePlatform.MT4.Core.Internals;
using TradePlatform.MT4.Core.Utils;
//using TradePlatform.MT4.Data;

namespace TradePlatform.MT4.Core
{
	public class Bridge
	{
		public static BridgeConfiguration Configuration
		{
			get
			{
				BridgeConfiguration section = (BridgeConfiguration)ConfigurationManager.GetSection("BridgeConfiguration");
				return section;
			}
		}

		public Bridge()
		{
		}

		public static MetaTrader4 GetTerminal(int accountNumber, string symbol)
		{
			return MetaTrader4.For(accountNumber, symbol);
		}

		public static void InitializeHosts(bool isBackground = false)
		{
            //foreach (HostElement host in Bridge.Configuration.Hosts)
            //{
            //    HandlerHost handlerHost = new HandlerHost(host.Name, host.IPAddress, host.Port, isBackground);
            //    handlerHost.Run();
            //}
            //try
            //{
            //    Uri[] uri = new Uri[1];
            //    uri[0] = new Uri(Bridge.Configuration.WcfBaseAddress);
            //    WebServiceHost webServiceHost = new WebServiceHost(typeof(TradePlatformDataService), uri);
            //    webServiceHost.Open();
            //    Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, string.Concat("TradePlatform Data Service is serving at ", Bridge.Configuration.WcfBaseAddress, "\n")));
            //}
            //catch (Exception exception1)
            //{
            //    Exception exception = exception1;
            //    Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
            //}
		}
	}
}
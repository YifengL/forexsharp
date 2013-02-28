using System;

namespace TradePlatform.MT4.Core.Utils
{
	public enum BridgeTraceErrorType
	{
		Execption,
		HandlerExecutionError,
		MqlError,
		HostInfo,
		CommunicationWorkflow,
		Service,
		Debug
	}
}
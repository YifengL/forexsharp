using System;
using System.Diagnostics;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Config;
using TradePlatform.MT4.Core.Exceptions;

namespace TradePlatform.MT4.Core.Utils
{
	public abstract class BridgeTraceListener : TraceListener
	{
		protected BridgeTraceListener()
		{
		}

		public abstract void Output(TraceInfo info);

		public override void Write(object TraceInfo)
		{
			try
			{
				if (TraceInfo is TradePlatform.MT4.Core.Utils.TraceInfo)
				{
					TradePlatform.MT4.Core.Utils.TraceInfo traceInfo = TraceInfo as TradePlatform.MT4.Core.Utils.TraceInfo;
					this.Output(traceInfo);
					if ((traceInfo.Type == BridgeTraceErrorType.Execption || traceInfo.Type == BridgeTraceErrorType.HandlerExecutionError) && Bridge.Configuration.UseEventLog)
					{
						if (!EventLog.SourceExists(traceInfo.Type.ToString()))
						{
							EventLog.CreateEventSource(traceInfo.Type.ToString(), "TradePlatform.MT4");
						}
						EventLog eventLog = new EventLog();
						eventLog.Source = traceInfo.Type.ToString();
						if (traceInfo.Exception == null)
						{
							eventLog.WriteEntry(traceInfo.Message, EventLogEntryType.Error);
						}
						else
						{
							string empty = string.Empty;
							while (traceInfo.Exception != null)
							{
								empty = string.Concat(empty, traceInfo.Exception, "\n\n");
								traceInfo.Exception = traceInfo.Exception.InnerException;
							}
							eventLog.WriteEntry(empty, EventLogEntryType.Error);
						}
					}
				}
				else
				{
					throw new ConsoleTraceListenerException(TraceInfo, "Use 'Write(object)' override and pass 'TraceInfo' instance");
				}
			}
			catch
			{
			}
		}

		public override void Write(string message)
		{
			throw new ConsoleTraceListenerException(message, "Use 'Write(object)' override and pass 'TraceInfo' instance");
		}

		public override void WriteLine(string message)
		{
			throw new ConsoleTraceListenerException(message, "Use 'Write(object)' override and pass 'TraceInfo' instance");
		}
	}
}
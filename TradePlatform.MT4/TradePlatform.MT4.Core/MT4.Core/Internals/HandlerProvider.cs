using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TradePlatform.MT4.Core;
using TradePlatform.MT4.Core.Config;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Internals
{
	internal sealed class HandlerProvider
	{
		private readonly static Dictionary<string, HandlerProvider> _storage;

		private readonly static object _storageLocker;

		internal readonly AutoResetEvent ClientCallSemaphore;

		internal readonly object Locker;

		internal readonly AutoResetEvent ServerCallSemaphore;

		private readonly MqlHandler _mqlHandler;

		internal MethodCallInfo ClientMethod;

		internal MethodCallInfo ServerMethod;

		internal DateTime BeginTime;

		internal DateTime EndTime;

		internal ExpertInfo ExpertInfo
		{
			get;
			set;
		}

		internal HandlerElement HandlerConfiguration
		{
			get;
			set;
		}

		static HandlerProvider()
		{
			HandlerProvider._storage = new Dictionary<string, HandlerProvider>();
			HandlerProvider._storageLocker = new object();
		}

		private HandlerProvider(MqlHandler mqlHandler, HandlerElement handlerElement, ExpertInfo expertInfo)
		{
			this.ClientCallSemaphore = new AutoResetEvent(false);
			this.Locker = new object();
			this.ServerCallSemaphore = new AutoResetEvent(false);
			this._mqlHandler = mqlHandler;
			this._mqlHandler.CallMqlInternal = new Func<string, IEnumerable<string>, string>(this.OnCallMqlMethod);
			this.HandlerConfiguration = handlerElement;
			this.ExpertInfo = expertInfo;
		}

		internal static HandlerProvider GetOrCreate(ExpertInfo expertInfo, HostElement hostElement)
		{
			Assembly assembly;
			Type type;
			MqlHandler mqlHandler;
			HandlerProvider item;
			lock (HandlerProvider._storageLocker)
			{
				if (!HandlerProvider._storage.ContainsKey(expertInfo.Discriminator))
				{
					if (hostElement.Handlers.ContainsKey(expertInfo.HandlerName))
					{
						HandlerElement handlerElement = hostElement.Handlers[expertInfo.HandlerName];
						try
						{
							assembly = Assembly.LoadFile(string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "\\", handlerElement.AssemblyName, ".dll"));
						}
						catch (Exception exception1)
						{
							Exception exception = exception1;
							throw new HandlerLoadException(expertInfo, "Requested assembly not found", exception);
						}
						try
						{
							type = assembly.GetType(handlerElement.TypeName);
						}
						catch (Exception exception3)
						{
							Exception exception2 = exception3;
							throw new HandlerLoadException(expertInfo, "Requested type not found in assembly.", exception2);
						}
						try
						{
							mqlHandler = (MqlHandler)Activator.CreateInstance(type);
						}
						catch (Exception exception5)
						{
							Exception exception4 = exception5;
							throw new HandlerLoadException(expertInfo, "Can't create intance of expert.", exception4);
						}
						try
						{
							foreach (ParameterElement inputParameter in handlerElement.InputParameters)
							{
								PropertyInfo property = mqlHandler.GetType().GetProperty(inputParameter.PropertyName);
								Type propertyType = property.PropertyType;
								object obj = Convert.ChangeType(inputParameter.PropertyValue, propertyType);
								property.SetValue(mqlHandler, obj);
							}
						}
						catch (Exception exception7)
						{
							Exception exception6 = exception7;
							throw new HandlerLoadException(expertInfo, "Can't set input parameters for expert", exception6);
						}
						HandlerProvider handlerProvider = new HandlerProvider(mqlHandler, handlerElement, expertInfo);
						HandlerProvider._storage.Add(expertInfo.Discriminator, handlerProvider);
						item = handlerProvider;
					}
					else
					{
						throw new HandlerLoadException(expertInfo, "Requested application not found in configuration", null);
					}
				}
				else
				{
					item = HandlerProvider._storage[expertInfo.Discriminator];
				}
			}
			return item;
		}

		private string OnCallMqlMethod(string methodName, IEnumerable<string> parameters)
		{
			string returnValue = "";
			try
			{
				try
				{
					this.ClientMethod = new MethodCallInfo(methodName, parameters);
					this.ClientCallSemaphore.Set();
					this.ServerCallSemaphore.WaitOne();
					returnValue = this.ClientMethod.ReturnValue;
					if (!string.IsNullOrEmpty(this.ClientMethod.ErrorMessage) && this._mqlHandler.MqlError != null)
					{
						MqlErrorException mqlErrorException = new MqlErrorException(this.ExpertInfo, this.ClientMethod);
						this._mqlHandler.MqlError(mqlErrorException);
						Trace.Write(new TraceInfo(BridgeTraceErrorType.MqlError, mqlErrorException, ""));
					}
				}
				catch (Exception exception1)
				{
					Exception exception = exception1;
					Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
				}
			}
			finally
			{
				this.ClientMethod = null;
			}
			return returnValue;
		}

		internal void ProceedServerMethod()
		{
			string str = this._mqlHandler.ResolveMethod(this.ServerMethod.MethodName, this.ServerMethod.Parameters);
			string str1 = str;
			if (str == null)
			{
				str1 = "";
			}
			string str2 = str1;
			this.ServerMethod.ReturnValue = str2;
		}
	}
}
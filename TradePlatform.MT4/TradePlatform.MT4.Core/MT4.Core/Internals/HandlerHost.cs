using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TradePlatform.MT4.Core.Config;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Internals
{
	internal sealed class HandlerHost
	{
		private readonly Thread _listenThread;

		private readonly string _name;

		private readonly bool _isBackground;

		private readonly TcpListener _tcpListener;

		public HostElement HostConfiguration
		{
			get
			{
				HostElement item = ((BridgeConfiguration)ConfigurationManager.GetSection("BridgeConfiguration")).Hosts[this._name];
				return item;
			}
		}

		public HandlerHost(string name, IPAddress ip, int port, bool isBackground)
		{
			this._name = name;
			this._isBackground = isBackground;
			this._tcpListener = new TcpListener(ip, port);
			Thread thread = new Thread(new ThreadStart(this.ListenForClients));
			thread.IsBackground = this._isBackground;
			this._listenThread = thread;
		}

		private string[] GetMessage(NetworkStream stream)
		{
			byte[] numArray = new byte[4096];
			int num = stream.Read(numArray, 0, 4096);
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			char[] chrArray = new char[1];
			string str = aSCIIEncoding.GetString(numArray, 0, num).Trim(chrArray);
			Trace.Write(new TraceInfo(BridgeTraceErrorType.CommunicationWorkflow, null, string.Concat(" --> ", str)));
			string[] array = HandlerHost.GetSplit(str, '|').ToArray<string>();
			return array;
		}

		public static IEnumerable<string> GetSplit(string s, char c)
		{
			int length = s.Length;
			int num = 0;
			for (int i = s.IndexOf(c, 0, length); i != -1; i = s.IndexOf(c, num, length - num))
			{
				if (i - num > 0)
				{
					yield return s.Substring(num, i - num);
				}
				num = i + 1;
			}
		}

		private void HandleClientComm(object client)
		{
			string str;
			string empty;
            //HandlerHost.HandlerHost variable = new HandlerHost.HandlerHost();
			TcpClient tcpClient = (TcpClient)client;
			Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, "Connection opened"));
            HandlerProvider handlerProvider = null;
			try
			{
				try
				{
					ParameterizedThreadStart parameterizedThreadStart = null;
					NetworkStream stream = tcpClient.GetStream();
					string[] message = this.GetMessage(stream);
					if ((int)message.Length >= 3)
					{
						MethodCallInfo methodCallInfo = new MethodCallInfo(message[2], message.Skip<string>(3));
						ExpertInfo expertInfo = new ExpertInfo(message[0], message[1], methodCallInfo);
                        //variable.handlerProvider = HandlerProvider.GetOrCreate(expertInfo, this.HostConfiguration);
                        handlerProvider = HandlerProvider.GetOrCreate(expertInfo, this.HostConfiguration);
						lock (handlerProvider.Locker)
						{
							handlerProvider.BeginTime = DateTime.Now;
							handlerProvider.ServerMethod = methodCallInfo;
							handlerProvider.ClientMethod = null;
							if (parameterizedThreadStart == null)
							{
								parameterizedThreadStart = (object x) => {
									try
									{
										try
										{
											((HandlerProvider)x).ProceedServerMethod();
										}
										catch (Exception exception1)
										{
											Exception exception = exception1;
											HandlerExecutionException handlerExecutionException = new HandlerExecutionException(expertInfo, exception);
                                            //MethodCallInfo methodCallInfo1.ErrorMessage = handlerExecutionException.Message;
											var ErrorMessage = handlerExecutionException.Message;
											Trace.Write(new TraceInfo(BridgeTraceErrorType.HandlerExecutionError, handlerExecutionException, ""));
										}
									}
									finally
									{
                                        handlerProvider.ClientCallSemaphore.Set();
                                        //LambdaVar2.Set();
									}
								}
								;
							}
							Thread thread = new Thread(parameterizedThreadStart);
							thread.IsBackground = this._isBackground;
							Thread cultureInfo = thread;
							cultureInfo.CurrentCulture = new CultureInfo("en-US");
							cultureInfo.Name = string.Concat(tcpClient.Client.RemoteEndPoint, " > ", this._tcpListener.Server.LocalEndPoint);
							cultureInfo.Start(handlerProvider);
							handlerProvider.ClientCallSemaphore.WaitOne();
							while (handlerProvider.ClientMethod != null)
							{
								string[] methodName = new string[2 + handlerProvider.ClientMethod.Parameters.Count<string>()];
								methodName[0] = "###MQL###";
								methodName[1] = handlerProvider.ClientMethod.MethodName;
								for (int i = 2; i < (int)methodName.Length; i++)
								{
									methodName[i] = handlerProvider.ClientMethod.Parameters[i - 2];
								}
								this.WriteMessage(stream, methodName);
								string[] strArrays = this.GetMessage(stream);
								if ((int)strArrays.Length >= 2)
								{
									MethodCallInfo clientMethod = handlerProvider.ClientMethod;
									if (strArrays[0] == "0:no error")
									{
										str = null;
									}
									else
									{
										str = strArrays[0];
									}
									clientMethod.ErrorMessage = str;
									MethodCallInfo clientMethod1 = handlerProvider.ClientMethod;
									if (strArrays[1] == "###EMPTY###")
									{
										empty = string.Empty;
									}
									else
									{
										empty = strArrays[1];
									}
									clientMethod1.ReturnValue = empty;
									handlerProvider.ServerCallSemaphore.Set();
									handlerProvider.ClientCallSemaphore.WaitOne();
								}
								else
								{
									throw new MessageException(strArrays, 2, "lastError|returnValue");
								}
							}
							if (handlerProvider.ServerMethod.ErrorMessage != null)
							{
								string[] errorMessage = new string[2];
								errorMessage[0] = "###ERR###";
								errorMessage[1] = handlerProvider.ServerMethod.ErrorMessage;
								this.WriteMessage(stream, errorMessage);
							}
							if (handlerProvider.ServerMethod.ReturnValue != null)
							{
								string[] returnValue = new string[1];
								returnValue[0] = handlerProvider.ServerMethod.ReturnValue;
								this.WriteMessage(stream, returnValue);
							}
						}
					}
					else
					{
						throw new MessageException(message, 3, "discriminator|applicationName|methodName|param1|param2|param3");
					}
				}
				catch (Exception exception3)
				{
					Exception exception2 = exception3;
					Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception2, ""));
				}
			}
			finally
			{
				if (handlerProvider != null)
				{
					handlerProvider.EndTime = DateTime.Now;
					TimeSpan endTime = handlerProvider.EndTime - handlerProvider.BeginTime;
					Trace.Write(new TraceInfo(BridgeTraceErrorType.Service, null, string.Concat("Method execution time: ", endTime.TotalMilliseconds, " ms.")));
				}
				tcpClient.Close();
			}
			Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, "Connection closed\n"));
		}

		private void ListenForClients()
		{
			this._tcpListener.Start();
			object[] pAddress = new object[5];
			pAddress[0] = "TCP listening for MT4 at ";
			pAddress[1] = this.HostConfiguration.IPAddress;
			pAddress[2] = ":";
			pAddress[3] = this.HostConfiguration.Port;
			pAddress[4] = "\n";
			Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, string.Concat(pAddress)));
			while (true)
			{
				TcpClient tcpClient = this._tcpListener.AcceptTcpClient();
				Thread thread = new Thread(new ParameterizedThreadStart(this.HandleClientComm));
				thread.IsBackground = this._isBackground;
				Thread thread1 = thread;
				thread1.Name = string.Concat(tcpClient.Client.RemoteEndPoint, " > ", this._tcpListener.Server.LocalEndPoint);
				thread1.Start(tcpClient);
			}
		}

		public void Run()
		{
			this._listenThread.Start();
		}

		private void WriteMessage(NetworkStream stream, params string[] message)
		{
			ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
			string str = "";
			message.ToList<string>().ForEach((string x) => {
                //HandlerHost.HandlerHost variable = this;
				str = string.Concat(str, x, "|");
			}
			);
			byte[] bytes = aSCIIEncoding.GetBytes(str);
			Trace.Write(new TraceInfo(BridgeTraceErrorType.CommunicationWorkflow, null, string.Concat(" <-- ", str)));
			stream.Write(bytes, 0, (int)bytes.Length);
			stream.Flush();
		}
	}
}
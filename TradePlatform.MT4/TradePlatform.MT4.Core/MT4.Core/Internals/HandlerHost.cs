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
        private readonly bool _isBackground;
        private readonly Thread _listenThread;

        private readonly string _name;

        private readonly TcpListener _tcpListener;

        public HandlerHost(string name, IPAddress ip, int port, bool isBackground)
        {
            _name = name;
            _isBackground = isBackground;
            _tcpListener = new TcpListener(IPAddress.Any, port);
            var thread = new Thread(ListenForClients);
            thread.IsBackground = _isBackground;
            _listenThread = thread;
        }

        public HostElement HostConfiguration
        {
            get
            {
                HostElement item =
                    ((BridgeConfiguration) ConfigurationManager.GetSection("BridgeConfiguration")).Hosts[_name];
                return item;
            }
        }

        private string[] GetMessage(NetworkStream stream)
        {
            var numArray = new byte[4096];
            int num = stream.Read(numArray, 0, 4096);
            var aSCIIEncoding = new ASCIIEncoding();
            var chrArray = new char[1];
            string str = aSCIIEncoding.GetString(numArray, 0, num).Trim(chrArray);
            Trace.Write(new TraceInfo(BridgeTraceErrorType.CommunicationWorkflow, null, string.Concat(" --> ", str)));
            string[] array = GetSplit(str, '|').ToArray();
            return array;
        }

        public static IEnumerable<string> GetSplit(string s, char c)
        {
            int length = s.Length;
            int num = 0;
            int num1 = s.IndexOf(c, 0, length);
            if (num1 != -1)
            {
                while (num1 != -1)
                {
                    if (num1 - num > 0)
                    {
                        yield return s.Substring(num, num1 - num);
                    }
                    num = num1 + 1;
                    num1 = s.IndexOf(c, num, length - num);
                }
                if (num < length)
                {
                    yield return s.Substring(num, length - num);
                }
            }
            else
            {
                yield return s;
            }
        }

        private void HandleClientComm(object client)
        {
            string str;
            string str1;
            var tcpClient = (TcpClient) client;
            Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, "Connection opened"));
            HandlerProvider orCreate = null;
            try
            {
                try
                {
                    NetworkStream stream = tcpClient.GetStream();
                    string[] message = GetMessage(stream);
                    if (message.Length >= 3)
                    {
                        var methodCallInfo = new MethodCallInfo(message[2], message.Skip(3));
                        var expertInfo = new ExpertInfo(message[0], message[1], methodCallInfo);
                        orCreate = HandlerProvider.GetOrCreate(expertInfo, HostConfiguration);
                        lock (orCreate.Locker)
                        {
                            orCreate.BeginTime = DateTime.Now;
                            orCreate.ServerMethod = methodCallInfo;
                            orCreate.ClientMethod = null;
                            var thread = new Thread((object x) =>
                                {
                                    try
                                    {
                                        try
                                        {
                                            ((HandlerProvider) x).ProceedServerMethod();
                                        }
                                        catch (Exception exception1)
                                        {
                                            Exception exception = exception1;
                                            var handlerExecutionException = new HandlerExecutionException(expertInfo,
                                                                                                          exception);
                                            orCreate.ServerMethod.ErrorMessage = handlerExecutionException.Message;
                                            Trace.Write(new TraceInfo(BridgeTraceErrorType.HandlerExecutionError,
                                                                      handlerExecutionException, ""));
                                        }
                                    }
                                    finally
                                    {
                                        orCreate.ClientCallSemaphore.Set();
                                    }
                                });
                            thread.IsBackground = _isBackground;
                            Thread cultureInfo = thread;
                            cultureInfo.CurrentCulture = new CultureInfo("en-US");
                            cultureInfo.Name = string.Concat(tcpClient.Client.RemoteEndPoint, " > ",
                                                             _tcpListener.Server.LocalEndPoint);
                            cultureInfo.Start(orCreate);
                            orCreate.ClientCallSemaphore.WaitOne();
                            while (orCreate.ClientMethod != null)
                            {
                                var methodName = new string[2 + orCreate.ClientMethod.Parameters.Count()];
                                methodName[0] = "###MQL###";
                                methodName[1] = orCreate.ClientMethod.MethodName;
                                for (int i = 2; i < methodName.Length; i++)
                                {
                                    methodName[i] = orCreate.ClientMethod.Parameters[i - 2];
                                }
                                WriteMessage(stream, methodName);
                                string[] strArrays = GetMessage(stream);
                                if (strArrays.Length >= 2)
                                {
                                    MethodCallInfo clientMethod = orCreate.ClientMethod;
                                    if (strArrays[0] == "0:no error")
                                    {
                                        str = null;
                                    }
                                    else
                                    {
                                        str = strArrays[0];
                                    }
                                    clientMethod.ErrorMessage = str;
                                    MethodCallInfo clientMethod1 = orCreate.ClientMethod;
                                    str1 = (strArrays[1] == "###EMPTY###" ? string.Empty : strArrays[1]);
                                    clientMethod1.ReturnValue = str1;
                                    orCreate.ServerCallSemaphore.Set();
                                    orCreate.ClientCallSemaphore.WaitOne();
                                }
                                else
                                {
                                    throw new MessageException(strArrays, 2, "lastError|returnValue");
                                }
                            }
                            if (orCreate.ServerMethod.ErrorMessage != null)
                            {
                                var errorMessage = new[] {"###ERR###", orCreate.ServerMethod.ErrorMessage};
                                WriteMessage(stream, errorMessage);
                            }
                            if (orCreate.ServerMethod.ReturnValue != null)
                            {
                                var returnValue = new[] {orCreate.ServerMethod.ReturnValue};
                                WriteMessage(stream, returnValue);
                            }
                        }
                    }
                    else
                    {
                        throw new MessageException(message, 3,
                                                   "discriminator|applicationName|methodName|param1|param2|param3");
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
                if (orCreate != null)
                {
                    orCreate.EndTime = DateTime.Now;
                    TimeSpan endTime = orCreate.EndTime - orCreate.BeginTime;
                    Trace.Write(new TraceInfo(BridgeTraceErrorType.Service, null,
                                              string.Concat("Method execution time: ", endTime.TotalMilliseconds, " ms.")));
                }
                tcpClient.Close();
            }
            Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, "Connection closed\n"));
        }

        private void ListenForClients()
        {
            _tcpListener.Start();
            var pAddress = new object[]
                {"TCP listening for MT4 at ", HostConfiguration.IPAddress, ":", HostConfiguration.Port, "\n"};
            Trace.Write(new TraceInfo(BridgeTraceErrorType.HostInfo, null, string.Concat(pAddress)));
            while (true)
            {
                TcpClient tcpClient = _tcpListener.AcceptTcpClient();
                var thread = new Thread(HandleClientComm);
                thread.IsBackground = _isBackground;
                Thread thread1 = thread;
                thread1.Name = string.Concat(tcpClient.Client.RemoteEndPoint, " > ", _tcpListener.Server.LocalEndPoint);
                thread1.Start(tcpClient);
            }
        }

        public void Run()
        {
            _listenThread.Start();
        }

        private void WriteMessage(NetworkStream stream, params string[] message)
        {
            var aSCIIEncoding = new ASCIIEncoding();
            string str = "";
            message.ToList().ForEach((string x) => str = string.Concat(str, x, "|"));
            byte[] bytes = aSCIIEncoding.GetBytes(str);
            Trace.Write(new TraceInfo(BridgeTraceErrorType.CommunicationWorkflow, null, string.Concat(" <-- ", str)));
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }
    }
}
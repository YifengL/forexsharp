using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using TradePlatform.MT4.Core.Config;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Internals
{
    internal sealed class HandlerProvider
    {
        private static readonly ConcurrentDictionary<string, HandlerProvider> _storage;

        private static readonly object _storageLocker;

        internal readonly AutoResetEvent ClientCallSemaphore = new AutoResetEvent(false);

        internal readonly object Locker = new object();

        internal readonly AutoResetEvent ServerCallSemaphore = new AutoResetEvent(false);

        private readonly MqlHandler _mqlHandler;

        internal DateTime BeginTime;
        internal MethodCallInfo ClientMethod;

        internal DateTime EndTime;
        internal MethodCallInfo ServerMethod;

        static HandlerProvider()
        {
            _storage = new ConcurrentDictionary<string, HandlerProvider>();
            _storageLocker = new object();
        }

        private HandlerProvider(MqlHandler mqlHandler, HandlerElement handlerElement, ExpertInfo expertInfo)
        {
            _mqlHandler = mqlHandler;
            _mqlHandler.CallMqlInternal = OnCallMqlMethod;
            HandlerConfiguration = handlerElement;
            ExpertInfo = expertInfo;
        }

        internal ExpertInfo ExpertInfo { get; set; }

        internal HandlerElement HandlerConfiguration { get; set; }

        internal static HandlerProvider GetOrCreate(ExpertInfo expertInfo, HostElement hostElement)
        {
            Assembly assembly;
            Type type;
            MqlHandler mqlHandler;
            HandlerProvider item;
            lock (_storageLocker)
            {
                if (!_storage.ContainsKey(expertInfo.Discriminator))
                {
                    if (hostElement.Handlers.ContainsKey(expertInfo.HandlerName))
                    {
                        HandlerElement handlerElement = hostElement.Handlers[expertInfo.HandlerName];
                        try
                        {
                            assembly =
                                Assembly.LoadFile(
                                    string.Concat(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "\\",
                                                  handlerElement.AssemblyName, ".dll"));
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
                            throw new HandlerLoadException(expertInfo, "Requested type not found in assembly.",
                                                           exception2);
                        }
                        try
                        {
                            mqlHandler = (MqlHandler) Activator.CreateInstance(type);
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
                            throw new HandlerLoadException(expertInfo, "Can't set input parameters for expert",
                                                           exception6);
                        }
                        var handlerProvider = new HandlerProvider(mqlHandler, handlerElement, expertInfo);
                        _storage.TryAdd(expertInfo.Discriminator, handlerProvider);
                        item = handlerProvider;
                    }
                    else
                    {
                        throw new HandlerLoadException(expertInfo, "Requested application not found in configuration",
                                                       null);
                    }
                }
                else
                {
                    item = _storage[expertInfo.Discriminator];
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
                    ClientMethod = new MethodCallInfo(methodName, parameters);
                    ClientCallSemaphore.Set();
                    ServerCallSemaphore.WaitOne();
                    returnValue = ClientMethod.ReturnValue;
                    if (!string.IsNullOrEmpty(ClientMethod.ErrorMessage) && _mqlHandler.MqlError != null)
                    {
                        var mqlErrorException = new MqlErrorException(ExpertInfo, ClientMethod);
                        _mqlHandler.MqlError(mqlErrorException);
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
                ClientMethod = null;
            }
            return returnValue;
        }

        internal void ProceedServerMethod()
        {
            string str = _mqlHandler.ResolveMethod(ServerMethod.MethodName, ServerMethod.Parameters);
            string str1 = str;
            if (str == null)
            {
                str1 = "";
            }
            string str2 = str1;
            ServerMethod.ReturnValue = str2;
        }
    }
}
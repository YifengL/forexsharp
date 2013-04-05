using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TradePlatform.MT4.Core.Exceptions;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core
{
    public abstract class MqlHandler
    {
        internal Func<string, IEnumerable<string>, string> CallMqlInternal;

        public Action<MqlErrorException> MqlError;

        public string CallMqlMethod(string methodName, params object[] parameters)
        {
            IEnumerable<string> strs;
            try
            {
                if (parameters == null)
                {
                    strs = new string[0];
                }
                else
                {
                    object[] objArray = parameters;
                    strs = objArray.Select((object x) => x.ToString());
                }
                IEnumerable<string> strs1 = strs;
                if (CallMqlInternal != null)
                {
                    string str = CallMqlInternal(methodName, new List<string>(strs1));
                    return str;
                }
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                Trace.Write(new TraceInfo(BridgeTraceErrorType.Execption, exception, ""));
            }
            return null;
        }

        protected internal abstract string ResolveMethod(string methodName, List<string> parameters);
    }
}
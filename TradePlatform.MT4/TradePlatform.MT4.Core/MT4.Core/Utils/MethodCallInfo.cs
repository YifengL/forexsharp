using System.Collections.Generic;
using System.Linq;

namespace TradePlatform.MT4.Core.Utils
{
    public sealed class MethodCallInfo
    {
        public readonly string MethodName;

        public readonly List<string> Parameters;

        public MethodCallInfo(string methodName, IEnumerable<string> parameters)
        {
            MethodName = methodName;
            Parameters = new List<string>(parameters);
            //MethodCallInfo strs = this;
            //IEnumerable<string> strs1 = parameters;
            //IEnumerable<u00210> u00210s = strs1;
            //if (strs1 == null)
            //{
            //    u00210s = (IEnumerable<string>)(new string[0]);
            //}
            //strs.Parameters = new List<string>(u00210s);
            ReturnValue = null;
            ErrorMessage = null;
        }

        public string ErrorMessage { get; set; }

        public string ReturnValue { get; set; }

        public override string ToString()
        {
            string str = string.Concat(MethodName, "(");
            List<string> parameters = Parameters;
            string str1 = str;
            str = parameters.Aggregate(str1, (string current, string prm) => string.Concat(current, prm, ", "));
            var chrArray = new[] {',', ' '};
            str = str.TrimEnd(chrArray);
            return string.Concat(str, ")");
        }
    }
}
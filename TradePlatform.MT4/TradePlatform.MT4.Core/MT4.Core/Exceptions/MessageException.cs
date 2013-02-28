using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TradePlatform.MT4.Core.Exceptions
{
	internal class MessageException : Exception
	{
		public MessageException(string[] message, int minLenght, string formatSample) : base(string.Format("Wrong message length. Expected minimum lenght: {0}, but actual lenght: {1}. Message was: '{2}', expected format: '{3}'", new object[] { minLenght, (int)message.Length, MessageException.ComputeArray(message), formatSample }))
		{
		}

		private static string ComputeArray(IEnumerable<string> message)
		{
			string str = "";
			message.ToList<string>().ForEach((string x) => str = string.Concat(str, x, "|"));
			return str;
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradePlatform.MT4.Core.Exceptions
{
	internal class MessageException : Exception
	{
		public MessageException(string[] message, int minLenght, string formatSample)
            : base(string.Format("Wrong message length. Expected minimum lenght: {0}, but actual lenght: {1}. Message was: '{2}', expected format: '{3}'",
            minLenght, message.Length, MessageException.ComputeArray(message), formatSample))
		{
            //object[] length = new object[4];
            //length[0] = minLenght;
            //length[1] = (int)message.Length;
            //length[2] = MessageException.ComputeArray(message);
            //length[3] = formatSample;
			
		}

		private static string ComputeArray(IEnumerable<string> message)
		{
			string str = "";
			message.ToList<string>().ForEach(x => {
                //MessageException.MessageException variable = this;
                str = string.Concat(str, x, "|");
			});
			return str;
		}
	}
}
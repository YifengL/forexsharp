using System;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core.Exceptions
{
	internal class HandlerLoadException : Exception
	{
		public HandlerLoadException(ExpertInfo expertInfo, string reason, Exception innerException) : base(string.Format("Can't load handler: {0}. Failure Context: {1}", reason, expertInfo), innerException)
		{
		}
	}
}
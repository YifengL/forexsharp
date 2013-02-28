using System;

namespace TradePlatform.MT4.Core.Utils
{
	public sealed class ExpertInfo
	{
		public readonly string HandlerName;

		public readonly string Discriminator;

		public readonly TradePlatform.MT4.Core.Utils.MethodCallInfo MethodCallInfo;

		public ExpertInfo(string discriminator, string handlerName, TradePlatform.MT4.Core.Utils.MethodCallInfo methodCallInfo)
		{
			this.Discriminator = discriminator;
			this.HandlerName = handlerName;
			this.MethodCallInfo = methodCallInfo;
		}

		public override string ToString()
		{
			object[] handlerName = new object[] { "[HandlerName=", this.HandlerName, ", Discriminator=", this.Discriminator, "].", this.MethodCallInfo };
			return string.Concat(handlerName);
		}
	}
}
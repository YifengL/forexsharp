using System;

namespace TradePlatform.MT4.Core.Utils
{
	public sealed class ExpertInfo
	{
		public readonly string HandlerName;

		public readonly string Discriminator;

		public readonly MethodCallInfo MethodCallInfo;

		public ExpertInfo(string discriminator, string handlerName, MethodCallInfo methodCallInfo)
		{
			this.Discriminator = discriminator;
			this.HandlerName = handlerName;
			this.MethodCallInfo = methodCallInfo;
		}

		public override string ToString()
		{
			object[] handlerName = new object[6];
			handlerName[0] = "[HandlerName=";
			handlerName[1] = this.HandlerName;
			handlerName[2] = ", Discriminator=";
			handlerName[3] = this.Discriminator;
			handlerName[4] = "].";
			handlerName[5] = this.MethodCallInfo;
			return string.Concat(handlerName);
		}
	}
}
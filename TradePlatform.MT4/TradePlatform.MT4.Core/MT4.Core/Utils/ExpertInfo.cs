namespace TradePlatform.MT4.Core.Utils
{
    public sealed class ExpertInfo
    {
        public readonly string Discriminator;
        public readonly string HandlerName;

        public readonly MethodCallInfo MethodCallInfo;

        public ExpertInfo(string discriminator, string handlerName, MethodCallInfo methodCallInfo)
        {
            Discriminator = discriminator;
            HandlerName = handlerName;
            MethodCallInfo = methodCallInfo;
        }

        public override string ToString()
        {
            var handlerName = new object[]
                {"[HandlerName=", HandlerName, ", Discriminator=", Discriminator, "].", MethodCallInfo};
            return string.Concat(handlerName);
        }
    }
}
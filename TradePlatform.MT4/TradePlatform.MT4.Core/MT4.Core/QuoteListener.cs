using System;
using TradePlatform.MT4.Core.Exceptions;

namespace TradePlatform.MT4.Core
{
	public class QuoteListener : ExpertAdvisor
	{
		public QuoteListener()
		{
			QuoteListener quoteListener = this;
			quoteListener.MqlError += new Action<MqlErrorException>(this.OnMqlError);
		}

		protected override int DeInit()
		{
			return 0;
		}

		protected override int Init()
		{
			return 0;
		}

		private void OnMqlError(MqlErrorException mqlErrorException)
		{
			int num = this.AccountNumber();
			string str = this.Symbol();
			MetaTrader4 metaTrader4 = MetaTrader4.For(num, str);
			metaTrader4.OnMqlError(mqlErrorException);
		}

		protected override int Start()
		{
			int num = this.AccountNumber();
			string str = this.Symbol();
			MetaTrader4 metaTrader4 = MetaTrader4.For(num, str);
			metaTrader4.OnQuote(this);
			return 1;
		}
	}
}
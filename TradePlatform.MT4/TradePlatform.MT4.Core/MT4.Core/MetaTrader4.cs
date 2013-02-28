using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using TradePlatform.MT4.Core.Exceptions;

namespace TradePlatform.MT4.Core
{
	public class MetaTrader4
	{
		private readonly static Dictionary<string, MetaTrader4> _listeners;

		private readonly static object _listernerLock;

		private readonly Queue<Action<MqlHandler>> _functionsBuffer = new Queue<Action<MqlHandler>>();

		public int AccountNumber
		{
			get;
			private set;
		}

		public string Symbol
		{
			get;
			private set;
		}

		static MetaTrader4()
		{
			MetaTrader4._listeners = new Dictionary<string, MetaTrader4>();
			MetaTrader4._listernerLock = new object();
		}

		private MetaTrader4(int accountNumber, string symbol)
		{
			this.AccountNumber = accountNumber;
			this.Symbol = symbol;
		}

		internal static List<MetaTrader4> All()
		{
			List<MetaTrader4> list;
			lock (MetaTrader4._listernerLock)
			{
				Dictionary<string, MetaTrader4> strs = MetaTrader4._listeners;
				list = strs.Select<KeyValuePair<string, MetaTrader4>, MetaTrader4>((KeyValuePair<string, MetaTrader4> x) => x.Value).ToList<MetaTrader4>();
			}
			return list;
		}

		internal static MetaTrader4 For(int accountNumber, string symbol)
		{
			MetaTrader4 item;
			lock (MetaTrader4._listernerLock)
			{
				string str = string.Concat(accountNumber, symbol);
				if (!MetaTrader4._listeners.ContainsKey(str))
				{
					MetaTrader4 metaTrader4 = new MetaTrader4(accountNumber, symbol);
					MetaTrader4._listeners.Add(str, metaTrader4);
					item = metaTrader4;
				}
				else
				{
					item = MetaTrader4._listeners[str];
				}
			}
			return item;
		}

		public void MqlScope(Action<MqlHandler> mqlScope)
		{
			this._functionsBuffer.Enqueue(mqlScope);
		}

		public void OnMqlError(MqlErrorException mqlErrorException)
		{
			if (this.MqlError != null)
			{
				this.MqlError(mqlErrorException);
			}
		}

		internal void OnQuote(QuoteListener quoteListener)
		{
			while (this._functionsBuffer.Count > 0)
			{
				Action<MqlHandler> action = this._functionsBuffer.Dequeue();
				action(quoteListener);
			}
			if (this.QuoteRecieved != null)
			{
				this.QuoteRecieved(quoteListener);
			}
		}

		public event Action<MqlErrorException> MqlError;

		public event Action<QuoteListener> QuoteRecieved;
	}
}
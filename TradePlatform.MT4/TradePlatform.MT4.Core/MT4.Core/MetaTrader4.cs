using System;
using System.Collections.Generic;

namespace TradePlatform.MT4.Core
{
	public class MetaTrader4
	{
		private readonly static Dictionary<string, MetaTrader4> _listeners;

		private readonly static object _listernerLock;

		private readonly Queue<Action<MqlHandler>> _functionsBuffer;

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
			this._functionsBuffer = new Queue<Action<MqlHandler>>();
			this.AccountNumber = accountNumber;
			this.Symbol = symbol;
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

		internal void OnQuote(MqlHandler mqlHandler)
		{
			while (this._functionsBuffer.Count > 0)
			{
				Action<MqlHandler> action = this._functionsBuffer.Dequeue();
				action(mqlHandler);
			}
			if (this.QuoteRecieved != null)
			{
				this.QuoteRecieved(mqlHandler);
			}
		}

		public event Action<MqlHandler> QuoteRecieved;
	}
}
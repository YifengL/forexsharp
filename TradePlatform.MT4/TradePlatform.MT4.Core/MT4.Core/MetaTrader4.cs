using System;
using System.Collections.Generic;
using System.Linq;
using TradePlatform.MT4.Core.Exceptions;

namespace TradePlatform.MT4.Core
{
    public class MetaTrader4
    {
        private static readonly Dictionary<string, MetaTrader4> _listeners;

        private static readonly object _listernerLock;

        private readonly Queue<Action<MqlHandler>> _functionsBuffer = new Queue<Action<MqlHandler>>();

        static MetaTrader4()
        {
            _listeners = new Dictionary<string, MetaTrader4>();
            _listernerLock = new object();
        }

        private MetaTrader4(int accountNumber, string symbol)
        {
            AccountNumber = accountNumber;
            Symbol = symbol;
        }

        public int AccountNumber { get; private set; }

        public string Symbol { get; private set; }

        internal static List<MetaTrader4> All()
        {
            List<MetaTrader4> list;
            lock (_listernerLock)
            {
                Dictionary<string, MetaTrader4> strs = _listeners;
                list = strs.Select((KeyValuePair<string, MetaTrader4> x) => x.Value).ToList<MetaTrader4>();
            }
            return list;
        }

        internal static MetaTrader4 For(int accountNumber, string symbol)
        {
            MetaTrader4 item;
            lock (_listernerLock)
            {
                string str = string.Concat(accountNumber, symbol);
                if (!_listeners.ContainsKey(str))
                {
                    var metaTrader4 = new MetaTrader4(accountNumber, symbol);
                    _listeners.Add(str, metaTrader4);
                    item = metaTrader4;
                }
                else
                {
                    item = _listeners[str];
                }
            }
            return item;
        }

        public void MqlScope(Action<MqlHandler> mqlScope)
        {
            _functionsBuffer.Enqueue(mqlScope);
        }

        public void OnMqlError(MqlErrorException mqlErrorException)
        {
            if (MqlError != null)
            {
                MqlError(mqlErrorException);
            }
        }

        internal void OnQuote(QuoteListener quoteListener)
        {
            while (_functionsBuffer.Count > 0)
            {
                Action<MqlHandler> action = _functionsBuffer.Dequeue();
                action(quoteListener);
            }
            if (QuoteRecieved != null)
            {
                QuoteRecieved(quoteListener);
            }
        }

        public event Action<MqlErrorException> MqlError;

        public event Action<QuoteListener> QuoteRecieved;
    }
}
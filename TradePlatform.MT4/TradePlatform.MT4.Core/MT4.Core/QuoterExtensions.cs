using System;
using System.Runtime.CompilerServices;
using TradePlatform.MT4.Core.Utils;

namespace TradePlatform.MT4.Core
{
	internal static class QuoterExtensions
	{
		internal static int AccountNumber(this QuoteListener nandler)
		{
			string str = nandler.CallMqlMethod("AccountNumber", null);
			return Convertor.ToInt(str);
		}

		internal static string Symbol(this QuoteListener handler)
		{
			string str = handler.CallMqlMethod("Symbol", null);
			return str;
		}
	}
}
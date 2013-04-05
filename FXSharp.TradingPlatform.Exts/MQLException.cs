using System;

namespace FXSharp.TradingPlatform.Exts
{
    public class MQLException : ApplicationException
    {
        public MQLException(string message) : base(message)
        {
        }
    }
}
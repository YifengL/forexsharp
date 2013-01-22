using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.TradingPlatform.Exts
{
    public class MQLException : ApplicationException
    {
        public MQLException(string message) : base(message)
        {
        }

    }
}

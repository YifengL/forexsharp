using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FXSharp.EA.NewsBox
{
    class SpreadInfo
    {
        public string Symbol { get; set; }
        public double Spread { get; set; }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Symbol, Spread);
        }
    }
}


using System;
namespace FXSharp.EA.NewsBox
{
    public class MagicBoxOrder
    {
        private Guid _unique;

        public string Id 
        { 
            get { return string.Format("{0}-{1}", Symbol, _unique.ToString()) ; } 
        }

        public string Symbol { get; set; }
        public DateTime ExecutingTime { get; set; }

        public MagicBoxOrder()
        {
            _unique = Guid.NewGuid();
        }

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals(obj, null)) return false;
            
            if (Object.ReferenceEquals(this, obj)) return true;
            
            if (this.GetType() != obj.GetType()) return false;

            var another = (MagicBoxOrder)obj;

            return another.Symbol == this.Symbol && another.ExecutingTime == this.ExecutingTime;
        }

        public override int GetHashCode()
        {
            return Symbol.Length + ExecutingTime.Hour;
        }
    }
}

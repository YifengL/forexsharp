using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FXSharp.EA.HappyDay
{
    public interface IOrderManager
    {
        void OnNewBar();

        void OnTick();
    }
}

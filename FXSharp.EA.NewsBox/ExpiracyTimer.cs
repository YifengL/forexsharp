using System;
using System.Timers;

namespace FXSharp.EA.NewsBox
{
    internal class ExpiracyTimer
    {
        private readonly Timer timers = new Timer();

        public ExpiracyTimer(double expiredTime)
        {
            timers = new Timer();
            timers.AutoReset = false;
            timers.Interval = expiredTime*MINUTE;
            timers.Elapsed += timers_Elapsed;
            timers.Start();
        }

        private int MINUTE
        {
            get { return 60*SECONDS; }
        }

        private int SECONDS
        {
            get { return 1000; }
        }

        public event EventHandler Expired;

        private void timers_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Expired == null) return;
            Expired(this, EventArgs.Empty);
        }

        internal void Finish()
        {
            timers.Stop();
            timers.Dispose();
        }
    }
}
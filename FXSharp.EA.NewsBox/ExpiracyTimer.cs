using System;
using System.Timers;

namespace FXSharp.EA.NewsBox
{
    class ExpiracyTimer
    {
        private Timer timers = new Timer();
        //private bool expired = false;
        public event EventHandler Expired;

        public ExpiracyTimer(double expiredTime)
        {
            this.timers = new Timer();
            this.timers.AutoReset = false;
            this.timers.Interval = expiredTime * MINUTE;
            this.timers.Elapsed += timers_Elapsed;
            this.timers.Start();
        }

        void timers_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Expired == null) return;
            Expired(this, EventArgs.Empty);
            //expired = true;
        }

        //public bool IsExpired
        //{
        //    get { return expired; }
        //}

        private int MINUTE
        {
            get { return 60 * SECONDS; }
        }

        private int SECONDS
        {
            get { return 1000; }
        }
    }
}

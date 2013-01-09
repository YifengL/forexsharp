using System.Timers;

namespace FXSharp.EA.NewsBox
{
    class AutoCloseOrder
    {
        private Timer timers = new Timer();
        private bool expired = false;

        public AutoCloseOrder()
        {
            this.timers = new Timer();
            this.timers.AutoReset = false;
            this.timers.Interval = 10 * MINUTE;
            this.timers.Elapsed += timers_Elapsed;
            this.timers.Start();
        }

        void timers_Elapsed(object sender, ElapsedEventArgs e)
        {
            expired = true;
        }

        public bool IsExpired
        {
            get { return expired; }
        }

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

namespace SPUtils.Core.v02.Multi.Timer
{
    public abstract class ATimerBaseHelper_1
    {
        public System.Timers.Timer baseTimer = null;

        public double TimerIntervalInSeconds
        {
            get
            {
                return (baseTimer.Interval / 1000.0);
            }
            set
            {
               baseTimer.Interval = value * 1000.0;
            }
        }

        public bool isAlive
        {
            get
            {
                return baseTimer.Enabled;
            }
            set
            {
                if (!value)
                    StopTimer();
                else
                    StartTimer();
            }
        }

        public virtual void InitializeTimer(double interval = 1000)
        {
            baseTimer = new System.Timers.Timer();
            baseTimer.Interval = interval;
            baseTimer.Elapsed += OnTimerElapsedDoWork;
            baseTimer.Stop();
        }

        public void StartTimer(double interval = -1)
        {
            if (baseTimer.Enabled)
                return;
            if (interval != -1)
                baseTimer.Interval = interval;
            baseTimer.Start();
        }

        public void StopTimer()
        {
            if (baseTimer.Enabled == false)
                return;
            baseTimer.Stop();
        }

        public abstract void OnTimerElapsedDoWork(object sender, System.Timers.ElapsedEventArgs e);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Multi.Timer
{
    public abstract class ATimerImplementationHelper_1
    {
        public System.Timers.Timer mMainTimer = null;

        public double TimerIntervalInSeconds
        {
            get
            {
                return (this.mMainTimer.Interval / 1000.0);
            }
            set
            {
               this.mMainTimer.Interval = value * 1000.0;
            }
        }

        public bool isAlive
        {
            get
            {
                return mMainTimer.Enabled;
            }
            set
            {
                if (!value)
                    this.StopTimer();
                else
                    this.StartTimer();
            }
        }

        public virtual void InitializeTimer(double interval = 1000)
        {
            this.mMainTimer = new System.Timers.Timer();
            this.mMainTimer.Interval = interval;
            this.mMainTimer.Elapsed += OnTimerElapsedDoWork;
            this.mMainTimer.Stop();
        }

        public void StartTimer(double interval = -1)
        {
            if (this.mMainTimer.Enabled)
                return;
            if (interval != -1)
                this.mMainTimer.Interval = interval;
            this.mMainTimer.Start();
        }

        public void StopTimer()
        {
            if (this.mMainTimer.Enabled == false)
                return;
            this.mMainTimer.Stop();
        }

        public abstract void OnTimerElapsedDoWork(object sender, System.Timers.ElapsedEventArgs e);
    }
}

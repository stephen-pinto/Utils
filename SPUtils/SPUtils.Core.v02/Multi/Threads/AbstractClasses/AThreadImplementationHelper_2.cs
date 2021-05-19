using System;

namespace SPUtils.Core.v02.Multi.Threads.AbstractClasses
{
    using Common;

    public abstract class AThreadImplementationHelper_2
    {
        //Local default constants
        private const int DEFAULT_THREAD_INTERVAL = 1000;
        private const int DEFAULT_PAUSE_INTERVAL = 5000;
        private const double DEFAULT_CHECK_INTERVAL = 30000.0;

        //Our base members for thread operation
        private System.Threading.Thread _mBaseThreadObj = null;
        private Multi_ExecutionStates _state = Multi_ExecutionStates.Undefined;
        private DateTime mThreadLastReportAsActive = DateTime.Now;

        private int mThreadRunInterval = DEFAULT_THREAD_INTERVAL;
        private int mThreadPauseInterval = DEFAULT_PAUSE_INTERVAL;

        //Check timer to make sure the thread is still running
        private double mTimerCheckInterval = DEFAULT_CHECK_INTERVAL;
        private System.Timers.Timer mCheckTimerForThread = null;

        //Events to inform about Thread termination or freeze
        public delegate void ThreadFrozeOrTerminatedEvntHndler(object info1, object info2);
        public event ThreadFrozeOrTerminatedEvntHndler Event_ThreadTerminatedOrFroze;

        public AThreadImplementationHelper_2()
        {
        }

        /* Implementation for check timer */

        private void StartCheckTimer(double interval_for_check)
        {
            if (mCheckTimerForThread == null)
            {
                mCheckTimerForThread = new System.Timers.Timer(interval_for_check);
                mCheckTimerForThread.Elapsed += CheckIfThreadIsRunning;
            }

            mCheckTimerForThread.Start();
        }

        private void StopCheckTimer()
        {
            if (mCheckTimerForThread != null)
                mCheckTimerForThread.Stop();
        }

        private void CheckIfThreadIsRunning(object sender, System.Timers.ElapsedEventArgs e)
        {
            #region TRY_BLOCK_REGION
#if DEBUG
            /*
            #endif
            try
            {
            #if DEBUG
            */
#endif
            #endregion

            if (_state == Multi_ExecutionStates.Running)
            {
                //If thread has not updated for more than the check interval time 
                //then we reset the flags and restart the thread.
                if (DateTime.Now.Subtract(mThreadLastReportAsActive).TotalSeconds > mTimerCheckInterval)
                {
                    if (Event_ThreadTerminatedOrFroze != null)
                        Event_ThreadTerminatedOrFroze(DateTime.Now, mThreadLastReportAsActive);

                    Stop();
                    Start(mThreadRunInterval);
                }
            }

            #region CATCH_BLOCK_REGION
#if DEBUG
            /*
            #endif

            }
            catch(Exception)
            {
                _state = Multi_ExecutionStates.Stopped;
                throw;
            }

            #if DEBUG
            */
#endif
            #endregion
        }

        /* Exported functions to control thread */

        public void Start(int interval = -1)
        {
            //Set basic members first
            _state = Multi_ExecutionStates.Running;

            //If specified specific interval then use that else use default
            if (interval != -1)
                mThreadRunInterval = interval;
            else if (mThreadRunInterval == -1)
                mThreadRunInterval = DEFAULT_THREAD_INTERVAL;

            //New or old we always create new thread on start
            _mBaseThreadObj = new System.Threading.Thread(ThreadBaseWorker);

            //Start the check timer
            StartCheckTimer(mTimerCheckInterval);

            //run the new thread
            _mBaseThreadObj.Start();
        }

        public void Stop()
        {
            //Set state to stopped
            _state = Multi_ExecutionStates.Stopped;
            StopCheckTimer();
        }

        public void Pause(int interval = -1)
        {
            //Set pause properties if specified by user
            if (interval != DEFAULT_PAUSE_INTERVAL)
                mThreadPauseInterval = interval;

            _state = Multi_ExecutionStates.Paused;
        }

        public void Resume()
        {
            //Set state to running
            _state = Multi_ExecutionStates.Running;
        }

        public void StartIfNotStarted(int interval = -1)
        {
            //If already running then leave
            if (_state == Multi_ExecutionStates.Running)
                return;
            else
                Start(interval);
        }

        public void SetInterval(int interval = DEFAULT_THREAD_INTERVAL, int pauseInterval = DEFAULT_PAUSE_INTERVAL)
        {
            mThreadRunInterval = interval;
            mThreadPauseInterval = pauseInterval;
        }

        public void ThreadBaseWorker()
        {
            #region TRY_BLOCK_REGION
#if DEBUG
            /*
            #endif
            try
            {
            #if DEBUG
            */
#endif
            #endregion

            //Run loop until state is set to stop or undefined
            while (_state != Multi_ExecutionStates.Undefined && _state != Multi_ExecutionStates.Stopped)
            {
                //Set the last timestamp
                mThreadLastReportAsActive = DateTime.Now;

                //If paused then go to sleep
                if (_state == Multi_ExecutionStates.Paused)
                {
                    if (mThreadPauseInterval == 0)
                    {
                        //if pause interval is 0 which means we need to pause for undefined time
                        while (_state == Multi_ExecutionStates.Running)
                            System.Threading.Thread.Sleep(DEFAULT_PAUSE_INTERVAL);
                    }
                    else
                    {
                        //if pause interval is > 0 then we need to pause for a defined time interval
                        System.Threading.Thread.Sleep(mThreadPauseInterval);
                    }
                }

                //Execute the worker method overriden by the user
                DoWork();

                //Sleep for certain interval
                System.Threading.Thread.Sleep(mThreadRunInterval);
            }

            #region CATCH_BLOCK_REGION
#if DEBUG
            /*
            #endif              
            }
            catch(System.Threading.ThreadAbortException)
            {
                _state = Multi_ExecutionStates.Stopped; 
                return;
            }
            catch(Exception)
            {
                _state = Multi_ExecutionStates.Stopped;                    
                throw;
            }
            #if DEBUG
            */
#endif
            #endregion
        }

        public abstract void DoWork();
    }
}

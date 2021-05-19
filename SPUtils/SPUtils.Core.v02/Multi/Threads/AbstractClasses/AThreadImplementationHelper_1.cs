namespace SPUtils.Core.v02.Multi.Threads.AbstractClasses
{
    /// <summary>
    /// This is a basic implementation helper for threads. 
    /// For more detailed version check the next version of the helper.
    /// </summary>
    public abstract class AThreadImplementationHelper_1
    {
        public System.Threading.Thread mMainThread = null;
        public bool ThreadActive = false;
        private bool ThreadStayAliveFlag = true;

        public void StartThread()
        {
            ThreadStayAliveFlag = true;
            ThreadActive = true;
            mMainThread = new System.Threading.Thread(CoreThreadMethod);
            mMainThread.IsBackground = true;
            mMainThread.Start();
        }

        public void StopThread()
        {
            ThreadActive = false;
            try { mMainThread.Abort(); }
            catch { };
            mMainThread = null;
            ThreadStayAliveFlag = false;
        }

        public void PauseThread()
        {
            ThreadActive = false;
        }

        public void ResumeThread()
        {
            ThreadActive = true;
        }

        public void CoreThreadMethod()
        {
            while (ThreadStayAliveFlag)
            {
                while (ThreadActive)
                {
                    DoWork();
                }
            }
        }

        public abstract void DoWork();
    }
}

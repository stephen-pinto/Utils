namespace SPUtils.Core.v02.Multi.Tasks.AbstractClasses
{
    public abstract class ATaskBaseHelper
    {
        public System.Threading.Tasks.Task mMainTask = null;
        public bool TaskStayAliveFlag = true;
        public bool isAlive
        {
            get
            {
                return (mMainTask.Status == System.Threading.Tasks.TaskStatus.Running);
            }
            private set
            {
                if (value == false)
                    TaskStayAliveFlag = false;
            }
        }

        public void InitializeThread()
        {
            mMainTask = new System.Threading.Tasks.Task(DoWork);
        }

        public void StartTask()
        {
            TaskStayAliveFlag = true;
            if (this.mMainTask.Status == System.Threading.Tasks.TaskStatus.Running)
                return;
            mMainTask = new System.Threading.Tasks.Task(DoWork);
            mMainTask.Start();
        }

        public void StopTask()
        {
            TaskStayAliveFlag = false;
        }

        public abstract void DoWork();
    }
}

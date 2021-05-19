using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SPUtils.Core.v02.Multi.MiniHelperModels
{
    public class TimerBased_HelperModel_1 : Timer.ATimerImplementationHelper_1
    {
        private Action targetAction = null;

        public TimerBased_HelperModel_1(Action callbackAction)
        {
            //Set action after timer interval
            targetAction = callbackAction;
        }

        public static TimerBased_HelperModel_1 PerformActionAtInterval(Action action, double interval = 60000)
        {
            //Initialize timer action and properties
            TimerBased_HelperModel_1 timerAction = new TimerBased_HelperModel_1(action);
            timerAction.InitializeTimer();
            timerAction.StartTimer(interval);
            return timerAction;
        }

        public static void StopPerformingAction(TimerBased_HelperModel_1 obj)
        {
            //Stop timer
            obj.StopTimer();
        }

        public override void OnTimerElapsedDoWork(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Perform the target action
            targetAction();
        }
    }
}

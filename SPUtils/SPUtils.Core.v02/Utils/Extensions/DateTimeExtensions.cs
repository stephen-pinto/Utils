using System;
using System.Collections.Generic;
using System.Text;

namespace SPUtils.Core.v02.Utils.Extensions
{
    public static class DateTimeExtenstions
    {
        public static int WeekDayNo(this DateTime timestamp)
        {
            //Note: If its a Sunday then we mark it as 7
            int day_no = (int)timestamp.DayOfWeek;

            if (day_no == 0)
                day_no = 7;

            return day_no;
        }

        public static DateTime TrimSeconds(this DateTime timestamp)
        {
            return timestamp.AddTicks(-(timestamp.Ticks % (TimeSpan.FromMinutes(1)).Ticks));
        }

        public static DateTime TrimMinutes(this DateTime timestamp)
        {
            return timestamp.AddTicks(-(timestamp.Ticks % (TimeSpan.FromHours(1)).Ticks));
        }

        public static DateTime TrimTimestamp(this DateTime timestamp, TimeSpan? timespan = null)
        {
            if (timespan == null)
                timespan = timestamp.TimeOfDay;

            return timestamp.AddTicks(-(timestamp.Ticks % timespan.Value.Ticks));
        }

        public static void MergeTime(this DateTime date, TimeSpan time)
        {
            date = new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds, date.Kind);
        }

        public static DateTime GetOnlyTime(this DateTime timestamp)
        {
            TimeSpan tempTimeSpan = timestamp.TimeOfDay;
            DateTime newDateTime = new DateTime(tempTimeSpan.Ticks);
            return newDateTime;
        }

        public static DateTime FirstDayOfTheWeek(this DateTime timestamp)
        {
            System.DayOfWeek dayOfTheWeek = System.DayOfWeek.Monday;

            int diff = timestamp.DayOfWeek - dayOfTheWeek;

            if (diff < 0)
                diff += 7;

            if (timestamp.AddDays(-1 * diff).Date.Day > timestamp.Date.Day)
                return new System.DateTime(timestamp.Year, timestamp.Month, 1);

            return timestamp.AddDays(-1 * diff).Date;
        }
    }
}

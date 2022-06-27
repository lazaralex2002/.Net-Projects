using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeRangeProgram
{
    public class WorkingTimeRange : TimeRange
    {
        public WorkingTimeRange(DateTime date1, DateTime date2) : base(date1, date2)
        {

        }

        override
        public int GetDayTimeRange()
        {
            int totalDays = 0;
            for (var date = StartDate; date <= EndDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    totalDays++;
                }
            }
            return totalDays;
        }
    }
}

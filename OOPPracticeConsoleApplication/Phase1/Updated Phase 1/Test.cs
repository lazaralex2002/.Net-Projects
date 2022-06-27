using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRangeProgram
{
    class Test
    {
        private void TestConstructors()
        {
            var startDate = new DateTime(2022, 05, 01);
            var endDate = new DateTime(2022, 06, 01);
            var timeRange = new TimeRange(startDate, endDate);
            Debug.Assert(timeRange.ToString() == "2022/5/1 to 2022/6/1");
            try
            {
                var timeRange1 = new TimeRange(endDate, startDate);
            }
            catch (Exception e)
            {
                Debug.Assert(e.ToString().Contains("is bigger"));
            }
        }

        private void TestGetters()
        {
            var startDate = new DateTime(2021, 05, 01);
            var endDate = new DateTime(2022, 06, 01);
            var timeRange = new TimeRange(startDate, endDate);
            Debug.Assert(timeRange.StartDay == 1);
            Debug.Assert(timeRange.StartMonth == 5);
            Debug.Assert(timeRange.StartYear == 2021);
            Debug.Assert(timeRange.EndDay == 1);
            Debug.Assert(timeRange.EndMonth == 6);
            Debug.Assert(timeRange.EndYear == 2022);
            Debug.Assert(timeRange.GetDayTimeRange() == 396);
        }

        private void TestSetters()
        {
            var startDate = new DateTime(2022, 05, 01);
            var endDate = new DateTime(2022, 06, 01);
            var timeRange = new TimeRange(startDate, endDate);
            timeRange.EndDay = 2;
            timeRange.EndMonth = 7;
            timeRange.EndYear = 2022;
            timeRange.StartDay = 2;
            timeRange.StartMonth = 6;

            try
            {
                timeRange.StartYear = 2023;
            }
            catch (Exception)
            {

            }
            timeRange.StartYear = 2022;
            Debug.Assert(timeRange.ToString() == "2022/6/2 to 2022/7/2");
        }

        private void TestOperators()
        {
            var startDate = new DateTime(2022, 05, 01);
            var endDate = new DateTime(2022, 06, 01);
            var timeRange = new TimeRange(startDate, endDate);
            Debug.Assert(timeRange.GetDayTimeRange() == 31);
            timeRange = timeRange + 3;
            Debug.Assert(timeRange.GetDayTimeRange() == 34);
            var timeRange1 = new TimeRange(new DateTime(2022, 05, 03), new DateTime(2022, 06, 02));
            var timeRange2 = new TimeRange(new DateTime(2021, 01, 01), new DateTime(2022, 06, 03));
            timeRange1 = timeRange1 + timeRange2;
            Debug.Assert(timeRange1.ToString() == "2021/1/1 to 2022/6/3");
        }

        private void TestWorkingTime()
        {
            var startDate = new DateTime(2022, 05, 01);
            var endDate = new DateTime(2022, 06, 01);
            var timeRange = new TimeRange(startDate, endDate);
            Debug.Assert(timeRange.GetDayTimeRange() == 31);
            WorkingTimeRange wt = new WorkingTimeRange(
                        new DateTime(timeRange.StartYear, timeRange.StartMonth, timeRange.StartDay),
                        new DateTime(timeRange.EndYear, timeRange.EndMonth, timeRange.EndDay)
                        );
            Debug.Assert(wt.GetDayTimeRange() == 23);
        }

        public void RunTests()
        {
            this.TestConstructors();
            this.TestGetters();
            this.TestSetters();
            this.TestOperators();
            this.TestWorkingTime();
            Debug.WriteLine("All tests passed!");
        }
    }
}

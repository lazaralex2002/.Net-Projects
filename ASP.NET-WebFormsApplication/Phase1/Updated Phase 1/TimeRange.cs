using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRangeProgram
{
	public class TimeRange
	{
		protected DateTime sDate;
		protected int DayTimeRange { get; set; }
		public DateTime StartDate
		{
            get 
			{
				return sDate;
			}
			set
            {
				sDate = value;
				DayTimeRange = EndDate.Subtract(StartDate).Days;
            }
		}

		public int StartDay
        {
			get
            {
				return StartDate.Day;
            }
            set 
			{
				if ( EndDate.CompareTo(new DateTime(StartDate.Year, StartDate.Month, value)) < 0)
                {
					throw new Exception ("End Date is smaller than Start Date");
                }
				StartDate = new DateTime(StartDate.Year, StartDate.Month, value);
			}
        }

		public int StartMonth
		{
			get
			{
				return StartDate.Month;
			}
			set
			{
				if (EndDate.CompareTo(new DateTime(StartDate.Year, value, StartDate.Day)) < 0)
				{
					throw new Exception("End Date is smaller than Start Date");
				}
				StartDate = new DateTime(StartDate.Year, value, StartDate.Day);
			}
		}

		public int StartYear
		{
			get
			{
				return StartDate.Year;
			}
			set
			{
				if (EndDate.CompareTo(new DateTime(value, StartDate.Month, StartDate.Day)) < 0)
				{
					throw new Exception("End Date is smaller than Start Date");
				}
				StartDate = new DateTime(value, StartDate.Month, StartDate.Day);
			}
		}

		protected DateTime eDate;
		public DateTime EndDate
		{
			get
			{
				return eDate;
			}
			set
			{
				eDate = value;
				DayTimeRange = EndDate.Subtract(StartDate).Days;
			}
		}

		public int EndDay
		{
			get
			{
				return EndDate.Day;
			}
			set
			{
				if (StartDate.CompareTo(new DateTime(EndDate.Year, EndDate.Month, value)) > 0)
				{
					throw new Exception("End Date is smaller than Start Date");
				}
				EndDate = new DateTime (EndDate.Year, EndDate.Month, value);
			}
		}

		public int EndMonth
		{
			get
			{
				return EndDate.Month;
			}
			set
			{
				if (StartDate.CompareTo(new DateTime(EndDate.Year, value, EndDate.Day)) > 0)
				{
					throw new Exception("End Date is smaller than Start Date");
				}
				EndDate = new DateTime(EndDate.Year, value, EndDate.Day);
			}
		}

		public int EndYear
		{
			get
			{
				return EndDate.Year;
			}
			set
			{
				if (StartDate.CompareTo(new DateTime(value, EndDate.Month, EndDate.Day)) > 0)
				{
					throw new Exception("End Date is smaller than Start Date");
				}
				EndDate = new DateTime(value, EndDate.Month, EndDate.Day);
			}
		}

		public TimeRange(DateTime date1, DateTime date2)
		{
			if (date2.CompareTo(date1) <= 0)
			{
				throw new Exception("End Date is bigger that the Start Date!");
			}
			StartDate = date1;
			EndDate = date2;
		}

		public static TimeRange operator +(TimeRange timeRange, int nrDays)
		{
			return new TimeRange(timeRange.StartDate, timeRange.EndDate.AddDays(nrDays));
		}

		public static TimeRange operator +(TimeRange Date1, TimeRange Date2)
		{
			int startDatesComparison = DateTime.Compare(Date1.StartDate, Date2.StartDate);
			int endDatesComparison = DateTime.Compare(Date1.EndDate, Date2.EndDate);

			if (startDatesComparison < 0)
            {
				if ( endDatesComparison < 0 )
                {
					return new TimeRange(Date1.StartDate, Date2.EndDate);
				}
				else
                {
					return new TimeRange(Date1.StartDate, Date1.EndDate);
                }
            }
			else
            {
				if ( endDatesComparison < 0 )
                {
					return new TimeRange(Date2.StartDate, Date2.EndDate);
                }
				else
                {
					return new TimeRange(Date2.StartDate, Date1.EndDate);
				}
            }
		}

		public virtual int GetDayTimeRange()
		{
			return EndDate.Subtract(StartDate).Days;
		}

		public override string ToString()
		{
			return this.StartYear + "/" + this.StartMonth + "/" + this.StartDay + " to "
				+ this.EndYear + "/" + this.EndMonth + "/" + this.EndDay;
		}
	}
}

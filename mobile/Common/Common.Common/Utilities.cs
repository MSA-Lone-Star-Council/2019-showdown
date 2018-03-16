using System;
using System.Globalization;
using Common.Common.Models;

namespace Common.Common
{
    public class Utilities
    {
        public static string FormatDateTime(DateTimeOffset time)
        {
			string format = "M/d h:mm tt";
			time = time.ToOffset(TimeSpan.FromHours(-5));
            return time.ToString(format, null as DateTimeFormatInfo);
        }

		public static string FormatEventTime(DateTimeOffset time)
		{
			string format = "h:mm tt";
			time = time.ToOffset(TimeSpan.FromHours(-5));
			return time.ToString(format, null as DateTimeFormatInfo).ToLower();
		}

		public static string FormatEventTimeSpan(Event e)
		{
			string dayFormat = "dddd";
			string time = "h:mm tt";

			var startTime = e.StartTime.ToOffset(TimeSpan.FromHours(-5));
			var endTime = e.EndTime.ToOffset(TimeSpan.FromHours(-5));

			string startDay = startTime.ToString(dayFormat, null as DateTimeFormatInfo);
			string startTimeStr = startTime.ToString(time, null as DateTimeFormatInfo).ToLower();
			string endTimeStr = endTime.ToString(time, null as DateTimeFormatInfo).ToLower();

			return $"{startDay} {startTimeStr} - {endTimeStr}";
		}

        public static string FormatDateTime(DateTimeOffset? time)
        {
            //throw new NotImplementedException();
            if (time == null) return null;
            return FormatDateTime((DateTimeOffset)time);
        }
    }
}
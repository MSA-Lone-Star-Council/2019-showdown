using System;
using System.Globalization;

namespace Common.Common
{
    public class Utilities
    {
        public static string FormatDateTime(DateTimeOffset time)
        {
			string format = "M/d h:mm:ss";
			time = time.ToOffset(TimeSpan.FromHours(-5));
            return time.ToString(format, null as DateTimeFormatInfo);
        }

		public static string FormatEventTime(DateTimeOffset time)
		{
			string format = "M/d h:mmtt";
			time = time.ToOffset(TimeSpan.FromHours(-5));
			return time.ToString(format, null as DateTimeFormatInfo).ToLower();
		}
    }
}
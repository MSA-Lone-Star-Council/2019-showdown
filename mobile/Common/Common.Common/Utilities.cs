using System;
using System.Globalization;

namespace Common.Common
{
    public class Utilities
    {
        public static string FormatDateTime(DateTimeOffset time)
        {
			string format = "M/d h:mm:ss";
            return time.ToString(format, null as DateTimeFormatInfo);
        }
    }
}
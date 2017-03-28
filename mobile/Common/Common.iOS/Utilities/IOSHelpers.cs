using System;
using Foundation;

namespace Common.iOS
{
	public class IOSHelpers
	{
		public static DateTimeOffset Convert(NSDate date)
		{
			var dt = (DateTime)date;
			return new DateTimeOffset(dt, TimeSpan.Zero);
		}

		public static NSDate ConvertToNSDate(DateTimeOffset date)
		{
			return (NSDate)date.LocalDateTime;
		}
	}
}

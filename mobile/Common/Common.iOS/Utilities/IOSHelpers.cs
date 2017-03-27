using System;
using Foundation;

namespace Common.iOS
{
	public class IOSHelpers
	{
		public static DateTimeOffset Convert(NSDate date)
		{
			var central = TimeZoneInfo.FindSystemTimeZoneById("America/Chicago");

			DateTime dt = new DateTime(2001, 1, 1).AddSeconds(date.SecondsSinceReferenceDate);

			var localTime = TimeZoneInfo.ConvertTimeFromUtc(dt, central);

			return new DateTimeOffset(localTime, TimeSpan.FromHours(-5));
		}
	}
}

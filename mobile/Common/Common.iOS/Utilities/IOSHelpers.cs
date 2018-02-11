using System;
using System.Threading.Tasks;
using Foundation;
using UserNotifications;

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

		public static async Task ScheduleNotification(DateTimeOffset notificationTime, string eventTitle)
		{
			var dateComponent = new NSDateComponents();
			dateComponent.SetValueForComponent(notificationTime.Year, NSCalendarUnit.Year);
			dateComponent.SetValueForComponent(notificationTime.Month, NSCalendarUnit.Month);
			dateComponent.SetValueForComponent(notificationTime.Day, NSCalendarUnit.Day);
			dateComponent.SetValueForComponent(notificationTime.Hour, NSCalendarUnit.Hour);
			dateComponent.SetValueForComponent(notificationTime.Minute, NSCalendarUnit.Minute);
			dateComponent.SetValueForComponent(-5, NSCalendarUnit.TimeZone);

			var trigger = UNCalendarNotificationTrigger.CreateTrigger(dateComponent, false);

			var center = UNUserNotificationCenter.Current;

            var content = new UNMutableNotificationContent
            {
                Title = eventTitle,
                Body = "Starts in 15 minutes",
                Sound = UNNotificationSound.Default
            };

            var request = UNNotificationRequest.FromIdentifier($"event_{content.Title}", content, trigger);
			await center.AddNotificationRequestAsync(request);
		}
	}
}

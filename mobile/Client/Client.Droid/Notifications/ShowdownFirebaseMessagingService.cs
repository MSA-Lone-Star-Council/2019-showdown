using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;

namespace Client.Droid
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
	public class ShowdownFirebaseMessagingService : FirebaseMessagingService
	{
		public override void OnMessageReceived(RemoteMessage message)
		{
			Log.Info("Showdown", "Message Received");
			string type = message.Data["type"];

			Notification notification = BuildAnnouncementNotification(message.Data);
			int notificationID = 0;

			switch (type)
			{
				case "announcement": notificationID = 1; break;
				default: notificationID = 2; break;
			}

			var notificationManager = GetSystemService(NotificationService) as NotificationManager;

			notificationManager.Notify(notificationID, notification);

		}

		private Notification BuildAnnouncementNotification(IDictionary<string, string> data)
		{
			Intent resultIntent = new Intent(this, typeof(MainActivity));

			PendingIntent resultPendingIntent =
				PendingIntent.GetActivity(this, new Random().Next(1438), resultIntent, PendingIntentFlags.UpdateCurrent);

			return
				new NotificationCompat.Builder(this)
				.SetSmallIcon(Resource.Drawable.lsc_icon)
				.SetContentTitle(data["title"])
				.SetContentText(data["subtitle"])
				.SetContentIntent(resultPendingIntent)
				.SetAutoCancel(true)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				.SetVibrate(new long[] { 750 })
				.Build();

		}

		private Notification BuildGameNotification(IDictionary<string, string> data)
		{
			Intent resultIntent = new Intent(this, typeof(MainActivity));
			resultIntent.PutExtra(MainActivity.ScreenIndexKey, 2);

			PendingIntent resultPendingIntent =
				PendingIntent.GetActivity(this, new Random().Next(1438), resultIntent, PendingIntentFlags.UpdateCurrent);

			var title = $"{data["away_team"]} {data["away_score"]} vs {data["home_team"]} {data["home_score"]}";
			var subtitle = $"{data["event_title"]} - {data["title"]}";

			return
				new NotificationCompat.Builder(this)
				.SetSmallIcon(Resource.Drawable.ic_launcher)
				.SetContentTitle(title)
				.SetContentText(subtitle)
				.SetContentIntent(resultPendingIntent)
				.SetAutoCancel(true)
				.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
				.SetVibrate(new long[] { 250, 250 })
				.Build();
		}
	}
}

using System;
using System.Collections.Generic;
using Android.App;
using Android.Util;
using Common.Common;
using Firebase.Iid;
using WindowsAzure.Messaging;

namespace Client.Droid
{
	[Service]
	[IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
	public class ShowdownFirebaseMessagingIIDService : FirebaseInstanceIdService
	{
		private NotificationHub Hub { get; set; }

		public override void OnTokenRefresh()
		{
            /*
			var refreshedToken = FirebaseInstanceId.Instance.Token;
			Hub = new NotificationHub(Secrets.NotificationHubPath, Secrets.AzureConnectionString, this);
			try
			{
				Hub.UnregisterAll(refreshedToken);
				Hub.Register(refreshedToken, (new List<string>()).ToArray());
			}
			catch (Exception ex)
			{
				Log.Error("ShowdownApp", ex.Message);
			}
            */
		}
	}
}

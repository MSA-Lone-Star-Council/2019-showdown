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

		public async override void OnTokenRefresh()
		{
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            var application = Application as ShowdownClientApplication;
            application.HubUtility.Token = refreshedToken;
            await application.SubscriptionManager.SaveToHub();
        }
    }
}

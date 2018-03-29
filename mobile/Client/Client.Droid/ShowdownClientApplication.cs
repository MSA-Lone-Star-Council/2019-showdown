using System;
using Android.App;
using Android.Runtime;
using Common.Common;
using Client.Common;
using Common.Droid;
using Firebase.Iid;

namespace Client.Droid
{
	[Application(
		Icon = "@drawable/ic_launcher_heart")]
	#if DEBUG
	[assembly: Application(Debuggable=true)]
	#else
	[assembly: Application(Debuggable = false)]
	#endif
	public class ShowdownClientApplication : Application
	{
		public NotificationHubUtility HubUtility { get; set; }
		public SubscriptionManager SubscriptionManager { get; set; }
		public ShowdownRESTClient BackendClient { get; set; }

		public ShowdownClientApplication(IntPtr handle, JniHandleOwnership ownership) : base(handle, ownership)
		{
			HubUtility = new NotificationHubUtility(this);
			BackendClient = new ShowdownRESTClient();
			SubscriptionManager = new SubscriptionManager(new DroidStorage(this), HubUtility);
		}

		public async override void OnCreate()
		{
			base.OnCreate();

			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

			HubUtility.Token = FirebaseInstanceId.Instance.Token;
			await SubscriptionManager.SaveToHub();
		}
	}
}

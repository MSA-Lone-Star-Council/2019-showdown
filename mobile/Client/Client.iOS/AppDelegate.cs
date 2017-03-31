using System.Threading.Tasks;
using Common.Common;
using Foundation;
using UIKit;
using UserNotifications;
using WindowsAzure.Messaging;

namespace Client.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
	[Register("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate, IUNUserNotificationCenterDelegate
	{
		// class-level declarations

		public override UIWindow Window
		{
			get;
			set;
		}

		private SBNotificationHub Hub { get; set; }

	    public ShowdownRESTClient BackendClient { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
		    BackendClient = new ShowdownRESTClient();

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			Window.RootViewController = BuildRootViewController();
			Window.MakeKeyAndVisible();

			RegisterForRemoteNotification();


			return true;
		}

		private void RegisterForRemoteNotification()
		{
			var center = UNUserNotificationCenter.Current;
			center.Delegate = this;
			center.RequestAuthorization(
				UNAuthorizationOptions.Sound | UNAuthorizationOptions.Alert,
				(granted, error) =>
				{
				if (error == null && granted) InvokeOnMainThread(() => UIApplication.SharedApplication.RegisterForRemoteNotifications());
				}
			);
		}

		private UIViewController BuildRootViewController()
		{
			var tabBarController = new UITabBarController();

			tabBarController.ViewControllers = new UIViewController[]
			{
				new UINavigationController(new ScheduleViewController()) { Title = "Schedule" },
				new UINavigationController(new AnnoucementsViewController())  { Title = "Annoucements" },
				new UINavigationController(new SportsViewController())  { Title = "Sports" },
				new UINavigationController(new AcknowledgementsViewController()) { Title = "Acknowledgemetns" },
			};

			return tabBarController;
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			Hub = new SBNotificationHub(Secrets.AzureConnectionString, Secrets.NotificationHubPath);
			Hub.UnregisterAllAsync(deviceToken, (error) =>
			{
				if (error != null)
				{
					return;
				}

				NSSet tags = null;
				Hub.RegisterNativeAsync(deviceToken, tags, callbackError =>
				{
					if (callbackError != null) return;
				});
			});
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			if (null != userInfo && userInfo.ContainsKey(new NSString("aps")))
			{
				string alert = string.Empty;

				NSDictionary aps = userInfo.ObjectForKey(new NSString("aps")) as NSDictionary;

				if (aps.ContainsKey(new NSString("alert")))
					alert = (aps[new NSString("alert")] as NSString).ToString();
			}
		}

	}
}


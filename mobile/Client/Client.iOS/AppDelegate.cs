using System;
using System.Threading.Tasks;
using Client.Common;
using Common.Common;
using Common.iOS;
using Foundation;
using Plugin.Iconize.iOS;
using Plugin.Iconize.iOS.Controls;
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

		NotificationHubUtility hub = new NotificationHubUtility();
	    public ShowdownRESTClient BackendClient { get; set; }
		public SubscriptionManager SubscriptionManager { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
		    BackendClient = new ShowdownRESTClient();
			SubscriptionManager = new SubscriptionManager(new iOSStorage(), hub);

			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

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
				new UINavigationController(new AnnoucementsViewController(BackendClient))  { Title = "Announcements" },
				new UINavigationController(new SportsViewController())  { Title = "Sports" },
				new UINavigationController(new AboutViewController()) { Title = "About" },
			};

			var size = 20;
			var scheduleIcon = Plugin.Iconize.Iconize.FindIconForKey("fa-calendar").ToUIImage(size);
			var announcementsIcon = Plugin.Iconize.Iconize.FindIconForKey("fa-bullhorn").ToUIImage(size);
			var sportsIcon = Plugin.Iconize.Iconize.FindIconForKey("fa-futbol-o").ToUIImage(size);
			var aboutIcon = Plugin.Iconize.Iconize.FindIconForKey("fa-info").ToUIImage(size);

			tabBarController.ViewControllers[0].TabBarItem.Image = scheduleIcon;
			tabBarController.ViewControllers[1].TabBarItem.Image = announcementsIcon;
			tabBarController.ViewControllers[2].TabBarItem.Image = sportsIcon;
			tabBarController.ViewControllers[3].TabBarItem.Image = aboutIcon;

			return tabBarController;
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			hub.DeviceToken = deviceToken;
			SubscriptionManager.SaveToHub();
		}


		public async override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, System.Action<UIBackgroundFetchResult> completionHandler)
		{
			NSDictionary data = (NSDictionary) userInfo[new NSString("data")];
			NSString type = (NSString)data[new NSString("type")];
			if (type == new NSString("event"))
			{
				NSString startTime = (NSString)data[new NSString("start_time")];
				DateTimeOffset notificationTime = DateTimeOffset.Parse(startTime).ToOffset(TimeSpan.FromHours(-5)).Subtract(TimeSpan.FromMinutes(15));
				NSString title = (NSString)data[new NSString("event_title")];

				await IOSHelpers.ScheduleNotification(notificationTime, title);
			}
		}
	}
}


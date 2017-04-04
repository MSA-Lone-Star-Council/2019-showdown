using System;
using System.Threading.Tasks;
using Client.Common;
using Common.Common;
using Common.iOS;
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

		NotificationHubUtility hub = new NotificationHubUtility();
	    public ShowdownRESTClient BackendClient { get; set; }
		public SubscriptionManager SubscriptionManager { get; set; }

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
		    BackendClient = new ShowdownRESTClient();
			SubscriptionManager = new SubscriptionManager(new iOSStorage(), hub);

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			Window.RootViewController = BuildRootViewController();
			Window.MakeKeyAndVisible();

			RegisterForRemoteNotification();

			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule());

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
				new UINavigationController(new AnnoucementsViewController(BackendClient))  { Title = "Annoucements" },
				new UINavigationController(new SportsViewController())  { Title = "Sports" },
				new UINavigationController(new AcknowledgementsViewController()) { Title = "Acknowledgemetns" },
			};

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

				var dateComponent = new NSDateComponents();
				dateComponent.SetValueForComponent(notificationTime.Year, NSCalendarUnit.Year);
				dateComponent.SetValueForComponent(notificationTime.Month, NSCalendarUnit.Month);
				dateComponent.SetValueForComponent(notificationTime.Day, NSCalendarUnit.Day);
				dateComponent.SetValueForComponent(notificationTime.Hour, NSCalendarUnit.Hour);
				dateComponent.SetValueForComponent(notificationTime.Minute, NSCalendarUnit.Minute);
				dateComponent.SetValueForComponent(-5, NSCalendarUnit.TimeZone);

				var trigger = UNCalendarNotificationTrigger.CreateTrigger(dateComponent, false);

				var center = UNUserNotificationCenter.Current;

				var content = new UNMutableNotificationContent();
				content.Title = (NSString)data[new NSString("event_title")];
				content.Body = "Starts in 15 minutes";
				content.Sound = UNNotificationSound.Default;

				var request = UNNotificationRequest.FromIdentifier("TEST", content, trigger);
				await center.AddNotificationRequestAsync(request);
			}
		}
	}
}


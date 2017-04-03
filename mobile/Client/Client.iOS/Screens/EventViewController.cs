using System;
using Client.Common;
using Common.Common.Models;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class EventViewController : UIViewController, IEventView
	{
		static NSString EventGameCellId = new NSString("SchoolGameCell");

		EventPresenter presenter;

		EventHeader header;
		UITableView gamesList;

		NSTimer timer;

		public EventViewController(Event e)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new EventPresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager) { Event = e};
			header = new EventHeader()
			{
				LocationTappedAction = () => 
				{ 
					var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
					var navController = tabBarController.SelectedViewController as UINavigationController;
					navController.PushViewController(new LocationViewController(e.Location), true);
				}
				NotificationTappedAction = () => presenter.EventSubscribeTapped(),
			};
			header.IsSubscribed = false;
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			await presenter.OnBegin();
			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(5), async (obj) => await presenter.OnTick());
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			presenter.RemoveView();
			timer.Invalidate();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = new UIColor(0.16f, 0.75f, 1.00f, 1.0f);

			gamesList = new UITableView()
			{
				BackgroundColor = UIColor.Clear,
				Source = new GamesTableSource(EventGameCellId, presenter),
				RowHeight = 130,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			gamesList.RegisterClassForCellReuse(typeof(GameCell), EventGameCellId);

			View.Add(header);
			View.Add(gamesList);

			header.MakeConstraints(make =>
			{
				make.Height.EqualTo((NSNumber)120);
				make.Top.EqualTo(View);
				make.Left.EqualTo(View);
				make.Width.EqualTo(View);
			});

			gamesList.MakeConstraints(make =>
			{
				make.Top.EqualTo(header.Bottom());
				make.Size.EqualTo(View);
			});
		}

		void IEventView.Refresh(Event e)
		{
			header.Event = e;
			header.IsSubscribed = presenter.IsSubscribedToEvent(e);
			gamesList.ReloadData();
		}

		void IEventView.OpenGame(Game game)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(game), true);
		}
	}
}

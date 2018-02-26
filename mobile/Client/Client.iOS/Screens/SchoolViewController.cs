using System;
using Client.Common;
using Common.Common.Models;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class SchoolViewController : UIViewController, ISchoolView
	{
		
		static NSString SchoolGameCellId = (NSString)"SchoolGameCell";

		SchoolPresenter presenter { get; set; }
		School school { get; set; }

		SchoolHeader Header { get; set; }
		UITableView GamesList { get; set; }

		NSTimer timer;

		public SchoolViewController(School s)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new SchoolPresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager) { School = s };
            Header = new SchoolHeader()
            {
                // NotificationTappedAction = presenter.SubscribeToSchool, //TODO
                IsSubscribed = presenter.SubscribedToSchool(),
            };
			school = s;
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			Header.School = school;
			await presenter.OnBegin();
			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(5), async (obj) => await presenter.OnTick());
      	}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			timer.Invalidate();
			presenter.RemoveView();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = Resources.Colors.backgroundColor;

			GamesList = new UITableView()
			{
				BackgroundColor = UIColor.Clear,
				Source = new GamesTableSource(SchoolGameCellId, presenter),
				RowHeight = 130,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			GamesList.RegisterClassForCellReuse(typeof(GameCell), SchoolGameCellId);

			View.Add(Header);
			View.Add(GamesList);

			Header.MakeConstraints(make =>
			{
				make.Height.EqualTo((NSNumber)80);
				make.Top.EqualTo(View);
				make.Left.EqualTo(View);
				make.Width.EqualTo(View);
			});

			GamesList.MakeConstraints(make =>
			{
				make.Top.EqualTo(Header.Bottom());
				make.Bottom.EqualTo(View).Offset(-49);
				make.Width.EqualTo(View);
			});
		}

		void ISchoolView.Refresh()
		{
			Header.IsSubscribed = presenter.SubscribedToSchool();
			GamesList.ReloadData();
		}

		void ISchoolView.OpenGame(Game game)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(game), true);
		}
	}
}

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
		static string EventGameCellId = "SchoolGameCell";

		EventPresenter presenter;

		EventHeader header;
		UITableView gamesList;

		public EventViewController(Event e)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new EventPresenter(appDelegate.BackendClient) { Event = e};
			header = new EventHeader()
			{
				LocationTappedAction = () => 
				{ 
					var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
					var navController = tabBarController.SelectedViewController as UINavigationController;
					navController.PushViewController(new LocationViewController(e.Location), true);
				}
			};
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			await presenter.OnBegin();
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
				Source = new GameTableSource(presenter),
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
			gamesList.ReloadData();
		}

		void IEventView.OpenGame(Game game)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(game), true);
		}

		class GameTableSource : UITableViewSource
		{
			EventPresenter presenter;

			public GameTableSource(EventPresenter presenter)
			{
				this.presenter = presenter;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var game = presenter.GetGame(indexPath.Row);

				var cell = tableView.DequeueReusableCell(EventGameCellId) as GameCell;
				cell.BackgroundColor = UIColor.Clear;
				cell.UpdateCell(game);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return presenter.GetGameCount();
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				presenter.OnClickRow(indexPath.Row);
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}

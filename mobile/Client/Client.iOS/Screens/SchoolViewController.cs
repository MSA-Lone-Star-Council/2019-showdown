using System;
using Client.Common;
using Common.Common.Models;
using Foundation;
using UIKit;

namespace Client.iOS
{
	public class SchoolViewController : UIViewController, ISchoolView
	{
		static string SchoolGameCellId = "SchoolGameCell";

		SchoolPresenter presenter { get; set; }

		UITableView GamesList { get; set; }

		public SchoolViewController(School s)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new SchoolPresenter(appDelegate.BackendClient) { School = s };
		}

		public async override void ViewDidAppear(bool animated)
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

			View.BackgroundColor = UIColor.White;

			GamesList = new UITableView(View.Bounds) // TODO: Remove this when you autolayout
			{
				BackgroundColor = UIColor.Clear,
				Source = new GameTableSource(presenter),
				RowHeight = 155,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			GamesList.RegisterClassForCellReuse(typeof(GameCell), SchoolGameCellId);

			View.Add(GamesList);
		}

		void ISchoolView.Refresh()
		{
			GamesList.ReloadData();
		}

		class GameTableSource : UITableViewSource
		{
			SchoolPresenter presenter;

			public GameTableSource(SchoolPresenter presenter)
			{
				this.presenter = presenter;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var game = presenter.GetGame(indexPath.Row);

				var cell = tableView.DequeueReusableCell(SchoolGameCellId) as GameCell;
				cell.UpdateCell(game);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return presenter.GetGameCount();
			}
		}
	}
}

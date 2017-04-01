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
		static string SchoolGameCellId = "SchoolGameCell";

		SchoolPresenter presenter { get; set; }
		School school { get; set; }

		SchoolHeader Header { get; set; }
		UITableView GamesList { get; set; }

		public SchoolViewController(School s)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new SchoolPresenter(appDelegate.BackendClient) { School = s };
			Header = new SchoolHeader();
			school = s;
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			Header.School = school;
			await presenter.OnBegin();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = new UIColor(0.90f, 1.0f, 0.91f, 1.0f);

			GamesList = new UITableView()
			{
				BackgroundColor = UIColor.Clear,
				Source = new GameTableSource(presenter),
				RowHeight = 155,
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
				make.Size.EqualTo(View);
			});
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
				cell.BackgroundColor = UIColor.Clear;
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

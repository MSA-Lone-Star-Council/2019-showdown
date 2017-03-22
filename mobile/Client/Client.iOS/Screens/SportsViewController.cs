using System;
using System.Collections.Generic;
using Client.Common;
using Common.Common.Models;
using Foundation;
using UIKit;

namespace Client.iOS
{
	public delegate void OnRowTapped(Game game);

	public class SportsViewController : UIViewController, ISportsView
	{
		static NSString GameCellID = new NSString("GameCellId");

		SportsPresenter Presenter { get; set; }

		UITableView GamesList { get; set; }


		public SportsViewController()
		{
			Presenter = new SportsPresenter();

			Presenter.TakeView(this);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			Presenter.TakeView(this);
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = new UIColor(0.90f, 1.0f, 0.91f, 1.0f);

			var tableSource = new SportsTableSource();
			tableSource.RowTappedEvent += async (game) => await Presenter.OnClickRow(game);

			GamesList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				RowHeight = 155,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			GamesList.RegisterClassForCellReuse(typeof(GameCell), GameCellID);
			View.AddSubview(GamesList);

			await Presenter.OnBegin();
		}


		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
		}

		public List<Game> Games
		{
			set
			{
				SportsTableSource sts = GamesList.Source as SportsTableSource;
				sts.Games = value;
				GamesList.ReloadData();
			}
		}

		public void OpenGame(Game g)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(g), true);
		}

		public void ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		class SportsTableSource : UITableViewSource
		{
			public List<Game> Games { get; set; }

			public event OnRowTapped RowTappedEvent;

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(GameCellID) as GameCell;
				cell.BackgroundColor = UIColor.Clear;

				var game = Games[indexPath.Row];

				cell.UpdateCell(game);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Games.Count;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var game = Games[indexPath.Row];
				RowTappedEvent(game);
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}

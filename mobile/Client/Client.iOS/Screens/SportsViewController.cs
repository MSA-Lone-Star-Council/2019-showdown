using System;
using System.Collections.Generic;
using Client.Common;
using Common.Common.Models;
using Foundation;
using UIKit;

namespace Client.iOS
{


	public class SportsViewController : UIViewController, ISportsView
	{
		static NSString GameCellID = new NSString("GameCellId");

		SportsPresenter Presenter { get; set; }

		UITableView GamesList { get; set; }


		public SportsViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new SportsPresenter(appDelegate.BackendClient);
			Presenter.TakeView(this);
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			Presenter.TakeView(this);
			await Presenter.OnBegin();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = new UIColor(0.90f, 1.0f, 0.91f, 1.0f);

			var tableSource = new SportsTableSource(Presenter);
			tableSource.RowTappedEvent += async (game) => Presenter.OnClickRow(game);

			GamesList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				RowHeight = 130,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			GamesList.RegisterClassForCellReuse(typeof(GameCell), GameCellID);
			View.AddSubview(GamesList);
		}


		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
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

		void ISportsView.ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		void ISportsView.Refresh()
		{
			GamesList.ReloadData();
		}

		class SportsTableSource : UITableViewSource
		{
			public delegate void OnRowTapped(Game game);

			public event OnRowTapped RowTappedEvent;

			public SportsPresenter presenter;

			public SportsTableSource(SportsPresenter presenter)
			{
				this.presenter = presenter;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(GameCellID) as GameCell;
				cell.BackgroundColor = UIColor.Clear;

				var game = presenter.GetGame(indexPath.Row);

				cell.UpdateCell(game);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return presenter.GamesCount();
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var game = presenter.GetGame(indexPath.Row);
				RowTappedEvent(game);
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}

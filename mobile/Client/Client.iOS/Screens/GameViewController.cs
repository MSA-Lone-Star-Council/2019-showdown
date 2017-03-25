using System;
using System.Collections.Generic;
using Client.Common;
using Common.Common.Models;
using Foundation;
using UIKit;
using Masonry;

namespace Client.iOS
{
	public class GameViewController : UIViewController, IGameView
	{
		static NSString ScoreCellID = new NSString("ScoreCellId");

		GamePresenter Presenter { get; set; }
		public Game Game { get; }
		UITableView ScoresList { get; set; }

		GameHeader Header { get; set; }

		public GameViewController(Game g)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new GamePresenter(appDelegate.BackendClient) { Game = g };

			Game = g;

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

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = new UIColor(0.90f, 1.0f, 0.91f, 1.0f);
			Header = new GameHeader();

			ScoresList = new UITableView()
			{
				BackgroundColor = UIColor.Clear,
				Source = new ScoreTableSource(),
				RowHeight = 50,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			ScoresList.RegisterClassForCellReuse(typeof(ScoreCell), ScoreCellID);

			View.Add(Header);
			View.Add(ScoresList);


			Header.MakeConstraints(make =>
			{
				make.Height.EqualTo(View).MultipliedBy(0.15f);
				make.Top.EqualTo(View);
				make.Left.EqualTo(View);
				make.Width.EqualTo(View);
			});

			ScoresList.MakeConstraints(make =>
			{
				make.Top.EqualTo(Header.Bottom());
				make.Size.EqualTo(View);
			});

			Header.LayoutView();

		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			var updateTask = Presenter.OnBegin();

			Header.AwayScore = Game.Score[0];
			Header.HomeScore = Game.Score[1];

			await updateTask;
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
		}

		public List<ScoreRecord> ScoreHistory
		{
			set
			{
				ScoreTableSource sts = ScoresList.Source as ScoreTableSource;
				sts.ScoreHistory = value;
				ScoresList.ReloadData();
			}
		}

		public void ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		class ScoreTableSource : UITableViewSource
		{
			public List<ScoreRecord> ScoreHistory { get; set; }

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(ScoreCellID) as ScoreCell;
				cell.BackgroundColor = UIColor.Clear;

				var scoreRecord = ScoreHistory[indexPath.Row];

				cell.UpdateCell(scoreRecord);

				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return ScoreHistory.Count;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var game = ScoreHistory[indexPath.Row];

			}
		}
	}
}

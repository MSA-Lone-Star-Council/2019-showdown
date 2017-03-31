using System;
using Admin.Common;
using Admin.Common.API.Entities;
using Foundation;
using UIKit;

namespace Admin.iOS
{
	public class GamesListViewController : UIViewController, IGamesListView
	{
		public delegate void OnRowTapped(Game row);

		GamesListPresenter Presenter { get; set; }

		UITableView GamesList { get; set; }

		public GamesListViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new GamesListPresenter(appDelegate.BackendClient);
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = UIColor.White;

			var tableSource = new GamesTableSource(Presenter);
			tableSource.RowTappedEvent += (row) => Presenter.OnClick(row);

			GamesList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			View.AddSubview(GamesList);
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			ParentViewController.NavigationItem.RightBarButtonItem =
                new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, e) => Presenter.OnClickAdd());

			Presenter.TakeView(this);
			await Presenter.OnBegin();
		}

		void IGamesListView.OpenGame(Game row)
		{
			
		}

		void IGamesListView.Refresh()
		{
			GamesList.ReloadData();
		}

		class GamesTableSource : UITableViewSource
		{
			GamesListPresenter _presenter;

			public event OnRowTapped RowTappedEvent;

			public GamesTableSource(GamesListPresenter presenter)
			{
				_presenter = presenter;
			}

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("GameCell") ??
									new UITableViewCell(UITableViewCellStyle.Subtitle, "GameCell");
				var game = _presenter.GetGame(indexPath.Row);

				var awayTeam = _presenter.GetSchool(game.AwayTeamId).ShortName;
				var homeTeam = _presenter.GetSchool(game.HomeTeamId).ShortName;
				var eventTitle = _presenter.GetEvent(game.EventId).Title;

				cell.TextLabel.Text = $"{awayTeam} vs {homeTeam}";
				cell.DetailTextLabel.Text = $"{ eventTitle } - { game.Title }";

				return cell;

			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return _presenter.GetNumGames();
			}
		}
	}
}

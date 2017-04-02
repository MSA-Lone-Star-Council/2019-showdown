using System;
using Client.Common;
using Foundation;
using UIKit;

namespace Client.iOS
{
	public class GamesTableSource : UITableViewSource
	{
		IGameHavingPresenter presenter;
		NSString gameCellId;

		public GamesTableSource(NSString gameCellId, IGameHavingPresenter presenter)
		{
			this.presenter = presenter;
			this.gameCellId = gameCellId;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(gameCellId) as GameCell;
			cell.BackgroundColor = UIColor.Clear;

			var game = presenter.GetGame(indexPath.Row);
			var subscribed = presenter.IsSubscribed(indexPath.Row);

			cell.UpdateCell(game, subscribed);
			cell.NotificationTappedAction = () => presenter.SubscribeTapped(indexPath.Row);

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return presenter.GameCount();
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			presenter.GameTapped(indexPath.Row);
			tableView.DeselectRow(indexPath, false);
		}
	}
}

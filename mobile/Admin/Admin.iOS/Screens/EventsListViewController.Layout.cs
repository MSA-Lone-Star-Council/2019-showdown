using System;
using System.Collections.Generic;
using Admin.Common.API.Entities;
using UIKit;

namespace Admin.iOS
{
	public partial class EventsListViewController : UIViewController
	{
		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			var tableSource = new EventsTableSource();
			tableSource.Events = new List<Event>();
			tableSource.RowTappedEvent += (row) => Presenter.OnClickRow(row);

			EventsList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			View.AddSubview(EventsList);

			await Presenter.OnBegin();
		}
	}
}

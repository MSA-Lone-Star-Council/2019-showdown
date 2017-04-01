using System;
using System.Collections.Generic;
using Admin.Common;
using Admin.Common.API.Entities;
using Foundation;
using UIKit;

namespace Admin.iOS
{
	public partial class EventsListViewController : IEventsListView
	{
		public delegate void OnRowTapped(Event row);

		EventsListPresenter Presenter { get; set; }

		UITableView EventsList { get; set; }

		public EventsListViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new EventsListPresenter(appDelegate.BackendClient);
			Presenter.TakeView(this);
		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			ParentViewController.NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, (sender, e) => Presenter.OnClickAdd());
			
			Presenter.TakeView(this);
			await Presenter.OnBegin();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
		}


		List<Event> IEventsListView.Events
		{
			set
			{
				EventsTableSource ets = EventsList.Source as EventsTableSource;
				ets.Events = value;
				EventsList.ReloadData();
			}
		}

		void IEventsListView.OpenEvent(Event row)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.PushViewController(new EventDetailViewController(row), true);
		}

		class EventsTableSource : UITableViewSource
		{
			public List<Event> Events { get; set; }

			public event OnRowTapped RowTappedEvent;

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("EventCell") ?? new UITableViewCell(UITableViewCellStyle.Default, "EventCell");
			    var item = Events[indexPath.Row];
				cell.TextLabel.Text = item.Title;
				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Events.Count;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var row = Events[indexPath.Row];
				RowTappedEvent(row);
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}

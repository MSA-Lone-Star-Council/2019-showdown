using System;
using System.Collections.Generic;
using Admin.Common;
using Admin.Common.API.Entities;
using Foundation;
using UIKit;

namespace Admin.iOS
{
	public class EventsListViewController : UIViewController, IEventsListView
	{
		EventsListPresenter Presenter { get; set; }

		UITableView EventsList { get; set; }

		public List<Event> Events
		{
			set
			{
				EventsTableSource ets = EventsList.Source as EventsTableSource;
				ets.Events = value;
				EventsList.ReloadData();
			}
		}

		public EventsListViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new EventsListPresenter(appDelegate.BackendClient);
			Presenter.TakeView(this);
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);

			ParentViewController.NavigationItem.RightBarButtonItem = new UIBarButtonItem(
				"Add Event",
				UIBarButtonItemStyle.Plain,
				(sender, e) =>
				{
					var newEvent = new Event()
					{
						Title = "Untitled Event",
						Audience = "general",
						StartTime = DateTimeOffset.Now,
						EndTime = DateTimeOffset.Now.AddHours(1),
						Description = "No description",
						LocationId = 1,
					};
					OpenEvent(newEvent);
				}
			);

			Presenter.TakeView(this);
			await Presenter.OnBegin();
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;



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

		public void OpenEvent(Event row)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;

			navController.PushViewController(new EventDetailViewController(row), true);
		}

		public delegate void OnRowTapped(Event game);
		class EventsTableSource : UITableViewSource
		{
			public List<Event> Events { get; set; }

			public event OnRowTapped RowTappedEvent;

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("EventCell");
				if (cell == null)
				{
					cell = new UITableViewCell(UITableViewCellStyle.Default, "EventCell");
				}

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

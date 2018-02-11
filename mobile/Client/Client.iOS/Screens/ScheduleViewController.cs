using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Common;
using Common.Common.Models;
using Common.iOS;
using Foundation;
using UIKit;

namespace Client.iOS
{
	public class ScheduleViewController : UIViewController, IScheduleView
	{
		static NSString EventCellID = new NSString("EventCellId");

		SchedulePresenter presenter;

		UITableView scheduleList;

		NSTimer timer;

		public ScheduleViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new SchedulePresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager);
		}

		public async override void ViewWillAppear(bool animated)
		{
			presenter.TakeView(this);
			await presenter.OnBegin();

			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(10), async (obj) => await presenter.OnTick());
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			presenter.RemoveView();
			if (timer != null) timer.Invalidate();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = new UIColor(0.16f, 0.75f, 1.00f, 1.0f);

			var tableSource = new ScheduleTableSource() { Presenter = presenter };
			tableSource.Events = new List<Event>();
			tableSource.RowTappedEvent += (row) => presenter.OnClickRow(row);

			scheduleList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				SeparatorStyle = UITableViewCellSeparatorStyle.None,
				RowHeight = 110,
			};
			scheduleList.RegisterClassForCellReuse(typeof(EventCell), EventCellID);
			this.AutomaticallyAdjustsScrollViewInsets = true;

			View.AddSubview(scheduleList);
		}

		List<Event> IScheduleView.Events
		{
			set
			{
				ScheduleTableSource sts = scheduleList.Source as ScheduleTableSource;
				sts.Events = value;
				scheduleList.ReloadData();
			}
		}

		void IScheduleView.OpenEvent(Event row)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new EventViewController(row), true);
		}

		void IScheduleView.ShowMessage(string message)
		{
			var alertView = new UIAlertView("", message, null, "OK", new string[] { });
			alertView.Show();
		}

		async Task IScheduleView.ScheduleReminder(Event eventToRemind)
		{
			IOSHelpers.ScheduleNotification(eventToRemind.StartTime.Subtract(TimeSpan.FromMinutes(15)), eventToRemind.Title);
		}

		class ScheduleTableSource : UITableViewSource
		{
			public delegate void OnRowTapped(Event row);

			public List<Event> Events { get; set; }

			public event OnRowTapped RowTappedEvent;

			public SchedulePresenter Presenter { get; set; }

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell(EventCellID) as EventCell;
				cell.BackgroundColor = UIColor.Clear;

				var item = Events[indexPath.Row];

				cell.UpdateCell(item, Presenter.IsSubscribed(item));
				cell.NotificationButtonAction = () => Presenter.OnStar(item);

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


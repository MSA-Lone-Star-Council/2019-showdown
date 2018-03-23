using System;
using System.Collections.Generic;
using System.Linq;
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

		//NSTimer timer;

		public ScheduleViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new SchedulePresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager);
            this.Title = "Schedule";
		}

		public async override void ViewWillAppear(bool animated)
		{
			presenter.TakeView(this);
			await presenter.OnBegin();

			//timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(10), async (obj) => await presenter.OnTick());
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			presenter.RemoveView();
			//if (timer != null) timer.Invalidate();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = Resources.Colors.backgroundColor;

			var tableSource = new ScheduleTableSource() { Presenter = presenter };
			tableSource.Events = new List<Event>();
			tableSource.RowTappedEvent += (row) => presenter.OnClickRow(row);

			scheduleList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
                SeparatorStyle = UITableViewCellSeparatorStyle.SingleLineEtched,
				RowHeight = 60,
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
            //var alertView = new UIAlertView("", message, null, "OK", new string[] { });
            //alertView.Show();
            throw new NotImplementedException();
		}

		async Task IScheduleView.ScheduleReminder(Event eventToRemind)
		{
            //IOSHelpers.ScheduleNotification(eventToRemind.StartTime.Subtract(TimeSpan.FromMinutes(15)), eventToRemind.Title);
            await Task.CompletedTask;
            throw new NotImplementedException();

		}

		class ScheduleTableSource : UITableViewSource
		{
            class SectionHeaderData 
            {
                public string Header;
                public int Count;
            }

            List<SectionHeaderData> eventHeaders = new List<SectionHeaderData>();

            List<Event> _events;
            public List<Event> Events //{ get; set; }

            {
                get
                {
                    return _events;
                }
                set
                {
                    _events = value;
                    eventHeaders = Events.GroupBy(item => item.StartTime.DayOfWeek.ToString())
                                  .Select(group => new SectionHeaderData
                                  {
                                      Header = group.Key,
                                      Count = group.Count()
                                  })
                                  .ToList();
                } 
            }

            public delegate void OnRowTapped(Event row);

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

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var row = Events[indexPath.Row];
				RowTappedEvent(row);
				tableView.DeselectRow(indexPath, false);
			}

            //
            public override nint NumberOfSections(UITableView tableView)
            {
                return eventHeaders.Count();
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                //return Events.Count();
                return eventHeaders[(int)section].Count;
            }


            public override string[] SectionIndexTitles(UITableView tableView)
            {
                string[] titles = new string[eventHeaders.Count()];
                for (int i = 0; i < eventHeaders.Count; i++) 
                {
                    titles[i] = eventHeaders[i].Header.Substring(0,3);
                }
                return titles;
            }


            public override string TitleForHeader(UITableView tableView, nint section)
            {
                return eventHeaders[(int)section].Header;
            }

		}
	}
}


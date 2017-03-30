using System;
using System.Collections.Generic;
using Admin.Common;
using Admin.Common.API.Entities;
using Foundation;
using UIKit;

namespace Admin.iOS
{
	public partial class LocationsListViewController : UIViewController, ILocationsListView
	{
		public delegate void OnRowTapped(Location row);

		LocationsListPresenter Presenter { get; set; }

		UITableView LocationsList { get; set; }

		public LocationsListViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new LocationsListPresenter(appDelegate.BackendClient);
			Presenter.TakeView(this);
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			var tableSource = new LocationsTableSource();
			tableSource.Locations = new List<Location>();
			tableSource.RowTappedEvent += (row) => Presenter.OnClickRow(row);

			LocationsList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			View.AddSubview(LocationsList);
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


		List<Location> ILocationsListView.Locations
		{
			set
			{
				LocationsTableSource ets = LocationsList.Source as LocationsTableSource;
				ets.Locations = value;
				LocationsList.ReloadData();
			}
		}

		void ILocationsListView.OpenLocation(Location row)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var navController = appDelegate.Window.RootViewController as UINavigationController;
			navController.PushViewController(new LocationViewController(row), true);
		}

		class LocationsTableSource : UITableViewSource
		{
			public List<Location> Locations { get; set; }

			public event OnRowTapped RowTappedEvent;

			public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
			{
				var cell = tableView.DequeueReusableCell("EventCell") ?? new UITableViewCell(UITableViewCellStyle.Subtitle, "LocationCell");
				var item = Locations[indexPath.Row];
				cell.TextLabel.Text = item.Name;
				cell.DetailTextLabel.Text = item.Address;
				return cell;
			}

			public override nint RowsInSection(UITableView tableview, nint section)
			{
				return Locations.Count;
			}

			public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
			{
				var row = Locations[indexPath.Row];
				RowTappedEvent(row);
				tableView.DeselectRow(indexPath, false);
			}
		}
	}
}

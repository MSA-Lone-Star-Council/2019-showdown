using System;
using Client.Common;
using Common.Common.Models;
using Common.iOS;
using CoreLocation;
using Foundation;
using MapKit;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class EventViewController : UIViewController, IEventView
	{
		static NSString EventGameCellId = new NSString("SchoolGameCell");

		EventPresenter presenter;

		EventHeader header;
        //UITableView gamesList;
        MKMapView mapView;

		NSTimer timer;

		public EventViewController(Event e)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new EventPresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager) { Event = e};
			header = new EventHeader()
			{
				LocationTappedAction = () =>
				{
					var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
					var navController = tabBarController.SelectedViewController as UINavigationController;
					navController.PushViewController(new LocationViewController(e.Location), true);
				},
				NotificationTappedAction = () => presenter.EventSubscribeTapped(),
			};
			header.IsSubscribed = false;

            mapView = new MKMapView();
            mapView.Delegate = new MapDelegate(this, presenter.Event);
		}

		public override async void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			presenter.TakeView(this);
			await presenter.OnBegin();
			timer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(5), async (obj) => await presenter.OnTick());
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			presenter.RemoveView();
			timer.Invalidate();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.NavigationBar.Translucent = false;

			View.BackgroundColor = Resources.Colors.backgroundColor;
            View.Add(header);
            View.Add(mapView);


            //Getting rid of Game list and putting a map view instead while sports is not working
            /*
			gamesList = new UITableView()
			{
				BackgroundColor = UIColor.Clear,
				Source = new GamesTableSource(EventGameCellId, presenter),
				RowHeight = 130,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			gamesList.RegisterClassForCellReuse(typeof(GameCell), EventGameCellId);

			View.Add(header);
			View.Add(gamesList);
			*/

			header.MakeConstraints(make =>
			{
				make.Height.EqualTo((NSNumber)120);
				make.Top.EqualTo(View);
				make.Left.EqualTo(View);
				make.Width.EqualTo(View);
			});

            mapView.MakeConstraints(make =>
            {
                make.Top.EqualTo(header.Bottom()).Offset(5);
                make.Left.EqualTo(View).Offset(5);
                make.Right.EqualTo(View).Offset(-5);
                make.Bottom.EqualTo(View);
            });
            /*
			gamesList.MakeConstraints(make =>
			{
				make.Top.EqualTo(header.Bottom());
				make.Bottom.EqualTo(View).Offset(-49);
				make.Width.EqualTo(View);
			});
			*/

            var mapCenter = new CLLocationCoordinate2D(presenter.Event.Location.Latitude, presenter.Event.Location.Longitude);
            mapView.SetRegion(MKCoordinateRegion.FromDistance(mapCenter, 1000, 1000), true); //1000 meter radius
            var annotation = new MKPointAnnotation()
            {
                Title = presenter.Event.Title,
                Subtitle = presenter.Event.Location.Name,
                Coordinate = new CLLocationCoordinate2D(presenter.Event.Location.Latitude, presenter.Event.Location.Longitude)
            };
            mapView.AddAnnotations(annotation);
            mapView.SelectAnnotation(annotation, true);

            CLLocationManager locationManager = new CLLocationManager();
            locationManager.RequestAlwaysAuthorization();
            mapView.ShowsUserLocation = true;
		}

		void IEventView.Refresh(Event e)
		{
			header.Event = e;
			header.IsSubscribed = presenter.IsSubscribedToEvent(e);
			//gamesList.ReloadData();
		}

		void IEventView.OpenGame(Game game)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(game), true);
		}

		void IEventView.ScheduleReminder(Event eventToRemind)
		{
            //IOSHelpers.ScheduleNotification(eventToRemind.StartTime.Subtract(TimeSpan.FromMinutes(15)), eventToRemind.Title);
            throw new NotImplementedException();
		}

		void IEventView.ShowMessage(string message)
		{
            //var alertView = new UIAlertView("", message, null, "OK", new string[] { });
            //alertView.Show();
            throw new NotImplementedException();
		}
	}
}

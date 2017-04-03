using System;
using System.Collections.Generic;
using Client.Common;
using Common.Common.Models;
using Foundation;
using UIKit;

namespace Client.iOS
{


	public class SportsViewController : UIViewController, ISportsView
	{
		static NSString GameCellID = new NSString("GameCellId");

		SportsPresenter Presenter { get; set; }

		UITableView GamesList { get; set; }

		NSTimer updateTimer;

		public SportsViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			Presenter = new SportsPresenter(appDelegate.BackendClient, appDelegate.SubscriptionManager);
			Presenter.TakeView(this);
		}

		public async override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear(animated);
			Presenter.TakeView(this);
			await Presenter.OnBegin();
			updateTimer = NSTimer.CreateRepeatingScheduledTimer(TimeSpan.FromSeconds(5), async (obj) => await Presenter.OnTick());
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = new UIColor(0.56f, 1.0f, 0.56f, 1.0f);

			var tableSource = new GamesTableSource(GameCellID, Presenter);

			GamesList = new UITableView(View.Bounds)
			{
				BackgroundColor = UIColor.Clear,
				Source = tableSource,
				RowHeight = 130,
				SeparatorStyle = UITableViewCellSeparatorStyle.None
			};
			GamesList.RegisterClassForCellReuse(typeof(GameCell), GameCellID);
			View.AddSubview(GamesList);
		}


		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			Presenter.RemoveView();
			updateTimer.Invalidate();
		}

		public void OpenGame(Game g)
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			var tabBarController = appDelegate.Window.RootViewController as UITabBarController;
			var navController = tabBarController.SelectedViewController as UINavigationController;

			navController.PushViewController(new GameViewController(g), true);
		}

		public void ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		void ISportsView.ShowMessage(string message)
		{
			throw new NotImplementedException();
		}

		void ISportsView.Refresh()
		{
			GamesList.ReloadData();
		}
	}
}

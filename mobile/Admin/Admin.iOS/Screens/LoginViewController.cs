using System;
using Common.Common.Screens.Login;
using Common.iOS;
using UIKit;
using Masonry;
using Foundation;

namespace Admin.iOS
{
	public class LoginViewController : UIViewController, ILoginView
	{
		UIButton button;
		UILabel error;

		LoginPresenter presenter;

		public LoginViewController()
		{
			var appDelegate = UIApplication.SharedApplication.Delegate as AppDelegate;
			presenter = new LoginPresenter(appDelegate.BackendClient, new iOSStorage());
			presenter.TakeView(this);
		}

		public string ErrorText
		{
			set
			{
				error.Text = value;
			}
		}

		public void Advance()
		{
			var tabBarController = new UITabBarController();

			tabBarController.ViewControllers = new UIViewController[]
			{
				new EventsListViewController() { Title = "Events" },
				new UIViewController() { Title = "Announcements" },
				new UIViewController()  { Title = "Locations" },
				new GamesListViewController()  { Title = "Games" },
			};
			tabBarController.NavigationItem.HidesBackButton = true;

			NavigationController.PushViewController(tabBarController, true);
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			button = new UIButton();
			button.BackgroundColor = UIColor.Blue;
			button.SetTitle("Login with Facebook", UIControlState.Normal);
			button.Layer.CornerRadius = 10;

			View.AddSubview(button);
			View.BackgroundColor = UIColor.White;

			button.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(View);
				make.CenterY.EqualTo(View);

				NSNumber height = 80;
				make.Height.EqualTo(height);
				NSNumber width = 200;
				make.Width.EqualTo(width);
			});

			button.TouchUpInside += (sender, e) =>
			{
				FacebookLoginViewController fbLoginVC = new FacebookLoginViewController();
				NavigationController.PushViewController(fbLoginVC, true);
			};

		}

		public async override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			presenter.TakeView(this);
			await presenter.SetupView();
		}

		public override void ViewWillDisappear(bool animated)
		{
			base.ViewWillDisappear(animated);
			presenter.RemoveView();
		}
	}
}

using System;
using Common.Common;
using UIKit;
using Masonry;

namespace Common.iOS
{
	public abstract class AbstractLoginViewController : UIViewController, ILoginView
	{
		UIButton button = new UIButton();

		LoginPresenter presenter;

		public AbstractLoginViewController()
		{
			presenter = new LoginPresenter();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			var buttonImage = UIImage.FromBundle("FacebookLogin");
			button.SetImage(buttonImage, UIControlState.Normal);
			button.SizeToFit();

			View.AddSubview(button);
			View.BackgroundColor = UIColor.White;

			button.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(View);
				make.CenterY.EqualTo(View);
			});

			button.TouchUpInside += (sender, e) =>
			{
				FacebookLoginViewController fbLoginVC = new FacebookLoginViewController();
				fbLoginVC.OnAccessToken += async (accessToken) => await presenter.OnAccessToken(accessToken);

			// Begin the Facebook login flow!
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

		/// <summary>
		/// Should advance to the next screen on successful login
		/// </summary>
		public abstract void Advance();
	}
}

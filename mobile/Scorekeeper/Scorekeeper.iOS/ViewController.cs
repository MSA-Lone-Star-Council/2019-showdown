using System;

using UIKit;
using Masonry;

namespace Scorekeeper.iOS
{
	public partial class ViewController : UIViewController
	{
		UIButton button = new UIButton();
		UILabel label = new UILabel();

		string FacebookAccessToken { get; set; }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			// Perform any additional setup after loading the view, typically from a nib.

			FacebookAccessToken = "";

			var buttonImage = UIImage.FromBundle("FacebookLogin");
			button.SetImage(buttonImage, UIControlState.Normal);
			button.SizeToFit();

			View.AddSubview(button);
			View.AddSubview(label);
			View.BackgroundColor = UIColor.White;

			button.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(View);
				make.CenterY.EqualTo(View);
			});

			label.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(button);
				make.Top.EqualTo(button.Bottom());
			});

			button.TouchUpInside += (sender, e) =>
			{
				FacebookLoginViewController fbLoginVC = new FacebookLoginViewController();
				fbLoginVC.OnAccessToken += (accessToken) =>
				{
					FacebookAccessToken = accessToken;
					label.Text = accessToken;
				};
				NavigationController.PushViewController(fbLoginVC, true);
			};
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}

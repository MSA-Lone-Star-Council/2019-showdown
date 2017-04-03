using System;
using Common.Common.Models;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class LocationViewController : UIViewController
	{
		static UIFont titleFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
		static UIFont timeFont = UIFont.SystemFontOfSize(16, UIFontWeight.Semibold);
		static UIFont locationFont = UIFont.SystemFontOfSize(16, UIFontWeight.Medium);
		static UIFont descriptionFont = UIFont.SystemFontOfSize(14, UIFontWeight.Regular);

		// Since locations will probably be rarely updated and it's mostly just view code, we're going to forgo all
		// the formalities of making a presenter and view implementation...
		Location location;

		public LocationViewController(Location l)
		{
			this.location = l;
		}


		UILabel nameLabel = new UILabel() { TextAlignment = UITextAlignment.Center, Font = titleFont };
		UIButton addressButton = new UIButton() { Font = timeFont, BackgroundColor = UIColor.White };
		UILabel notesLabel = new UILabel() { TextAlignment = UITextAlignment.Center, Font = descriptionFont };

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			View.BackgroundColor = new UIColor(0.16f, 0.75f, 1.00f, 1.0f);

			addressButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
			addressButton.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			addressButton.TouchUpInside += (sender, e) =>
			{
				var path = $"https://maps.apple.com/?daddr={ location.Address }";
				NSUrl url = new NSUrl(System.Uri.EscapeUriString(path));
				if (UIApplication.SharedApplication.CanOpenUrl(url))
				{
					UIApplication.SharedApplication.OpenUrl(url);
				}
			};

			UIView backgroundView = new UIView() { BackgroundColor = UIColor.White };

			View.AddSubviews(new UIView[] { backgroundView, nameLabel, addressButton, notesLabel });

			nameLabel.MakeConstraints(make =>
			{
				make.Width.EqualTo(View);
				make.CenterY.EqualTo(View.CenterY()).MultipliedBy(0.125f);
				make.Left.EqualTo(View);
			});

			addressButton.MakeConstraints(make =>
			{
				make.Width.EqualTo(View);
				make.Top.EqualTo(nameLabel.Bottom());
				make.Left.EqualTo(View);
			});

			notesLabel.MakeConstraints(make =>
			{
				make.Width.EqualTo(View);
				make.Top.EqualTo(addressButton.Bottom());
				make.Left.EqualTo(View);
				make.Height.EqualTo((NSNumber)100);
			});

			backgroundView.MakeConstraints(make =>
			{
				make.Top.EqualTo(nameLabel).Offset(-5);
				make.Bottom.EqualTo(notesLabel);
				make.Left.EqualTo(View);
				make.Width.EqualTo(View);
			});
		}

		public override void ViewDidAppear(bool animated)
		{
			nameLabel.Text = location.Name;
			addressButton.SetTitle(location.Address, UIControlState.Normal);
			notesLabel.Text = location.Notes;
		}
	}
}

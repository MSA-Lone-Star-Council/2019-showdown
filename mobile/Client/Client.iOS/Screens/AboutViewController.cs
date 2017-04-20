using System;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public partial class AboutViewController : UIViewController
	{

		UILabel title = new UILabel()
		{
			Text = "LSC Showdown 2017",
			Font = UIFont.SystemFontOfSize(24, UIFontWeight.Bold),
			TextAlignment = UITextAlignment.Center
		};

		UILabel description = new UILabel()
		{
			Text = "Showdown is a three-day, tournament style conference which strives to deliever a platform for MSAs in Texas to network in a competitve setting.",
			Font = UIFont.SystemFontOfSize(16, UIFontWeight.Regular),
			TextAlignment = UITextAlignment.Center,
			Lines = 0,
		};

		UIButton twitterButton = new UIButton();
		UIButton facebookButton = new UIButton();
		UIButton visitLink = new UIButton();

		UILabel authors = new UILabel()
		{
			Text = "Developed by Zonera Javed, Ali Kedwaii, Hamzah Khatri, Saad Najmi, and Zuhair Parvez",
			Font = UIFont.SystemFontOfSize(14, UIFontWeight.Light),
			TextAlignment = UITextAlignment.Center,
			Lines = 0
		};

		UIButton contributionLink = new UIButton() { Font = UIFont.SystemFontOfSize(10, UIFontWeight.Bold) };

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.BackgroundColor = UIColor.White;

			View.AddSubviews(new UIView[]
			{
				title,
				description,
				visitLink,
				twitterButton,
				facebookButton,
				authors,
				contributionLink
			});

			title.MakeConstraints(make =>
			{
				make.CenterY.EqualTo(View).MultipliedBy(0.3f);
				make.Width.EqualTo(View);
			});

			description.MakeConstraints(make =>
			{
				make.Top.EqualTo(title.Bottom()).Offset(5);
				make.Width.EqualTo(View).MultipliedBy(0.9f);
				make.Height.EqualTo(View.Height()).MultipliedBy(0.2f);
				make.CenterX.EqualTo(View);
			});

			visitLink.SetTitle("Learn more at msa-texas.org", UIControlState.Normal);
			visitLink.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			visitLink.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			visitLink.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(View);
				make.Width.EqualTo(View).MultipliedBy(0.9f);
				make.Height.EqualTo((NSNumber)40);
				make.Top.EqualTo(description.Bottom());
			});

			twitterButton.SetImage(UIImage.FromBundle("TwitterIcon"), UIControlState.Normal);
			twitterButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(visitLink.Bottom()).Offset(10);
				make.Width.EqualTo((NSNumber)64);
				make.Height.EqualTo((NSNumber)64);
				make.CenterX.EqualTo(View).MultipliedBy(0.7f);
			});

			facebookButton.SetImage(UIImage.FromBundle("FacebookIcon"), UIControlState.Normal);
			facebookButton.MakeConstraints(make =>
			{
				make.CenterY.EqualTo(twitterButton).Offset(10);
				make.Width.EqualTo((NSNumber)64);
				make.Height.EqualTo((NSNumber)64);
				make.CenterX.EqualTo(View).MultipliedBy(1.3f);
			});

			authors.MakeConstraints(make =>
			{
				make.Top.EqualTo(facebookButton.Bottom()).Offset(5);
				make.Width.EqualTo(View).MultipliedBy(0.9f);
				make.Height.EqualTo(View.Height()).MultipliedBy(0.1f);
				make.CenterX.EqualTo(View);
			});

			contributionLink.SetTitle("Find the app source on GitHub", UIControlState.Normal);
			contributionLink.SetTitleColor(UIColor.Blue, UIControlState.Normal);
			contributionLink.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			contributionLink.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(View);
				make.Width.EqualTo(View).MultipliedBy(0.9f);
				make.Height.EqualTo((NSNumber)40);
				make.Top.EqualTo(authors.Bottom());
			});

			visitLink.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("http://msa-texas.org"));
			facebookButton.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://www.facebook.com/msalonestarcouncil/"));
			twitterButton.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://twitter.com/msalsc"));
			contributionLink.TouchUpInside += (sender, e) => UIApplication.SharedApplication.OpenUrl(new NSUrl("https://github.com/MSA-Lone-Star-Council/Showdown"));
		}
	}
}


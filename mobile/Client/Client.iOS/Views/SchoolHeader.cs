using System;
using Common.Common.Models;
using Foundation;
using Masonry;
using Plugin.Iconize.iOS.Controls;
using UIKit;

namespace Client.iOS
{
	public class SchoolHeader : UIView
	{
		static UIFont SchoolTitleFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);

		IconButton notificationButton = new IconButton();
		UILabel schoolTitleLabel = new UILabel() { Font = SchoolTitleFont };

		public Action NotificationTappedAction { get; set; }

		public SchoolHeader()
		{
			BackgroundColor = UIColor.White;
			Layer.BorderColor = UIColor.LightGray.CGColor;
			Layer.BorderWidth = 0.5f;

			notificationButton.TouchUpInside += (sender, e) => NotificationTappedAction();

			AddSubview(schoolTitleLabel);
			AddSubview(notificationButton);

			notificationButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(this).Offset(5);
				make.Right.EqualTo(this).Offset(-5);
				make.Height.EqualTo((NSNumber)40);
				make.Width.EqualTo((NSNumber)40);
			});

			schoolTitleLabel.MakeConstraints(make =>
			{
				make.Center.EqualTo(this);
			});
		}

		public School School
		{
			set
			{
				schoolTitleLabel.Text = value.Name;
			}
		}

		public bool IsSubscribed
		{
			set
			{
				string normalText = value ? $"{{fa-bell 20pt}}" : $"{{fa-bell-o 20pt}}";
				string highligtedText = value ? $"{{fa-bell-o 20pt}}" : $"{{fa-bell 20pt}}";
				notificationButton.SetTitle(normalText, UIControlState.Normal);
				notificationButton.SetTitle(highligtedText, UIControlState.Highlighted);
			}
		}
	}
}

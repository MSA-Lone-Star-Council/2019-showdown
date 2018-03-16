using System;
using Common.Common;
using Common.Common.Models;
using Foundation;
using MapKit;
using Masonry;
using Plugin.Iconize.iOS.Controls;
using UIKit;

namespace Client.iOS
{
	public class EventHeader : UIView
	{
		static UIFont titleFont         = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
		static UIFont timeFont          = UIFont.SystemFontOfSize(16, UIFontWeight.Semibold);
		static UIFont locationFont      = UIFont.SystemFontOfSize(16, UIFontWeight.Medium);
		static UIFont descriptionFont   = UIFont.SystemFontOfSize(14, UIFontWeight.Regular);

		UILabel titleLabel              = new UILabel() { Font = titleFont, TextAlignment = UITextAlignment.Center };
		UILabel timeLabel               = new UILabel() { Font = timeFont, TextAlignment = UITextAlignment.Center };
        UIButton locationLabel = new UIButton();
		UILabel descriptionLabel = new UILabel()
		{
			Font = descriptionFont,
			Lines = 3,
			TextAlignment = UITextAlignment.Center
		};

		public Action LocationTappedAction { get; set; }
		public Action NotificationTappedAction { get; set; }

		public EventHeader()
		{
            BackgroundColor = BackgroundColor = Resources.Colors.backgroundColor;
			Layer.BorderColor = UIColor.LightGray.CGColor;
			Layer.BorderWidth = 0.5f;

            var containerView = new UIView() { BackgroundColor = Resources.Colors.backgroundColor };
            this.Add(containerView);
            containerView.MakeConstraints(make =>
            {
                make.Edges.EqualTo(this).Insets(new UIEdgeInsets(10, 10, 10, 10));
            });

			locationLabel.TitleLabel.Font = locationFont;
			locationLabel.SetTitleColor(UIColor.Black, UIControlState.Normal);
			//locationLabel.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			//locationLabel.TouchUpInside += (sender, e) => LocationTappedAction();

			AddSubviews(new UIView[] {
				titleLabel,
				timeLabel,
				locationLabel,
				descriptionLabel,
			});

            var parentView = containerView;

			titleLabel.MakeConstraints(make =>
			{
                make.CenterX.EqualTo(parentView);
				make.Top.EqualTo(parentView);
				make.Width.EqualTo(parentView);
			});

			timeLabel.MakeConstraints(make =>
			{
                make.CenterX.EqualTo(parentView);
                make.Top.EqualTo(titleLabel.Bottom()).Offset(5);
				make.Width.EqualTo(parentView);
			});

            descriptionLabel.MakeConstraints(make =>
            {
                make.CenterX.EqualTo(parentView);
                make.Width.EqualTo(parentView).MultipliedBy(0.95f);
                make.Top.EqualTo(timeLabel.Bottom());
            });

			locationLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(titleLabel);
                make.Top.EqualTo(descriptionLabel.Bottom()).Offset(5);
				make.Width.EqualTo(parentView);
				//make.Height.EqualTo((NSNumber)40);
			});
			locationLabel.ContentEdgeInsets = new UIEdgeInsets(0.01f, 0.01f, 0.01f, 0.01f);
		}

		public Event Event
		{
			set
			{
				titleLabel.Text = value.Title;
				timeLabel.Text = Utilities.FormatEventTimeSpan(value);
				descriptionLabel.Text = value.Description;
				locationLabel.SetTitle(value.Location.Name, UIControlState.Normal);
			}
		}

		public bool IsSubscribed
		{
			set
			{
				string normalText = value ? $"{{fa-bell 20pt}}" : $"{{fa-bell-o 20pt}}";
				string highligtedText = value ? $"{{fa-bell-o 20pt}}" : $"{{fa-bell 20pt}}";
				//notificationButton.SetTitle(normalText, UIControlState.Normal);
				//notificationButton.SetTitle(highligtedText, UIControlState.Highlighted);
			}
		}
	}
}

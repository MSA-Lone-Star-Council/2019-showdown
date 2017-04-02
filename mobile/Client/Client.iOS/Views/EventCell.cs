using System;
using Common.Common;
using Common.Common.Models;
using Foundation;
using Masonry;
using UIKit;

namespace Client.iOS
{
	public class EventCell : UITableViewCell
	{
		static UIFont titleFont = UIFont.SystemFontOfSize(20, UIFontWeight.Bold);
		static UIFont locationFont = UIFont.SystemFontOfSize(18, UIFontWeight.Semibold);
		static UIFont timeFont = UIFont.SystemFontOfSize(18, UIFontWeight.Regular);

		UIView audienceIcon = new UIView();
		UILabel titleLabel = new UILabel() { Font = titleFont };
		UILabel timeLabel = new UILabel() { Font = timeFont };
		UILabel locationLabel = new UILabel() { Font = locationFont };

		public EventCell(IntPtr p) : base (p)
		{
			var containerView = new UIView() { BackgroundColor = UIColor.White };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
				make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(5, 5, 5, 5));
			});
			containerView.Layer.CornerRadius = 3;
			containerView.Layer.BorderWidth = 0.5f;
			containerView.Layer.BorderColor = UIColor.LightGray.CGColor;

			containerView.AddSubviews(new UIView[] {
				audienceIcon,
				titleLabel,
				timeLabel,
				locationLabel
			});

			var parentView = containerView;

			titleLabel.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(5);
				make.Left.EqualTo(parentView).Offset(5);
				make.Right.EqualTo(parentView);
			});

			audienceIcon.MakeConstraints(make =>
			{
				make.Top.EqualTo(titleLabel.Bottom()).Offset(3);
				make.Right.EqualTo(parentView).Offset(-3);
				make.Left.EqualTo(titleLabel).Offset(3);
				make.Height.EqualTo((NSNumber)3);
			});

			locationLabel.MakeConstraints(make =>
			{
				make.Top.EqualTo(audienceIcon.Bottom()).Offset(10);
				make.Left.EqualTo(titleLabel);
				make.Right.EqualTo(parentView);
			});

			timeLabel.MakeConstraints(make =>
			{
				make.Top.EqualTo(locationLabel.Bottom()).Offset(5);
				make.Left.EqualTo(titleLabel);
				make.Right.EqualTo(parentView);
			});
		}

		public void UpdateCell(Event row)
		{
			titleLabel.Text = row.Title;
			locationLabel.Text = row.Location.Name;
			timeLabel.Text = Utilities.FormatEventTimeSpan(row);

			switch (row.Audience)
			{
				case "general": audienceIcon.BackgroundColor = UIColor.Green; break;
				case "brothers": audienceIcon.BackgroundColor = UIColor.Blue; break;
				case "sisters": audienceIcon.BackgroundColor = UIColor.Purple; break;
				default: audienceIcon.BackgroundColor = UIColor.Black; break;
			}
		}
	}
}

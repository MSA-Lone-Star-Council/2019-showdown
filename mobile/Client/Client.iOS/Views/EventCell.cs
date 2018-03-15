using System;
using System.Threading.Tasks;
using Common.Common;
using Common.Common.Models;
using Foundation;
using Masonry;
using Plugin.Iconize.iOS.Controls;
using UIKit;

namespace Client.iOS
{
	public class EventCell : UITableViewCell
	{
        public Func<Task> NotificationButtonAction { get; internal set; }

        static UIFont titleFont     = UIFont.SystemFontOfSize(16, UIFontWeight.Bold);
        static UIFont locationFont  = UIFont.SystemFontOfSize(16, UIFontWeight.Regular);
		static UIFont startTimeFont = UIFont.SystemFontOfSize(16, UIFontWeight.Regular);
        static UIFont endTimeFont   = UIFont.SystemFontOfSize(16, UIFontWeight.Regular);

        UIView divider = new UIView()
        {
            BackgroundColor = Resources.Colors.primaryLightColor
        };
		UILabel titleLabel = new UILabel() 
        { 
            Font = titleFont,
            LineBreakMode = UILineBreakMode.TailTruncation,
        };
        UILabel startTimeLabel = new UILabel() { Font = startTimeFont };
        UILabel endTimeLabel = new UILabel() 
        { 
            Font = endTimeFont,
            TextColor = UIColor.DarkGray
        };


		UILabel locationLabel = new UILabel() 
        { 
            Font = locationFont,
            TextColor = UIColor.DarkGray
        };

		public EventCell(IntPtr p) : base (p)
		{
            var containerView = new UIView() { BackgroundColor = Resources.Colors.backgroundColor };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
                make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(10,10,10,10));
			});

			containerView.AddSubviews(new UIView[] {
				divider,
				titleLabel,
                startTimeLabel,
                endTimeLabel,
				locationLabel
			});

			var parentView = containerView;

            startTimeLabel.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView);
                make.Left.EqualTo(parentView).Offset(10);
            });

            endTimeLabel.MakeConstraints(make =>
            {
                make.Left.EqualTo(startTimeLabel);
                make.Bottom.EqualTo(parentView);
            });

            divider.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView);
                make.Bottom.EqualTo(parentView);
                make.Left.EqualTo(parentView).Offset(80);
                make.Width.EqualTo((NSNumber)2);
            });

            titleLabel.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView);
                make.Right.EqualTo(parentView);
                make.Left.EqualTo(divider).Offset(10);
            });

            locationLabel.MakeConstraints(make =>
            {
                make.Bottom.EqualTo(parentView);
                make.Right.EqualTo(parentView);
                make.Left.EqualTo(titleLabel);
            });
		}

        public void UpdateCell(Event row, bool subscribed)
		{
			titleLabel.Text = row.Title;
			locationLabel.Text = row.Location.Name;
            startTimeLabel.Text = Utilities.FormatEventTime(row.StartTime);
            endTimeLabel.Text = Utilities.FormatEventTime(row.EndTime);
		}
	}
}

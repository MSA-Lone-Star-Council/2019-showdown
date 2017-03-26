using System;
using Common.Common;
using Common.Common.Models;
using UIKit;
using Masonry;

namespace Client.iOS
{
    public class AnnouncementCell : UITableViewCell
    {
        UILabel Title { get; set; }
		UILabel Body { get; set; }
		UILabel Time { get; set; }

		static UIFont TitleFont = UIFont.FromName("Helvetica-Bold", 16);
		static UIFont TimeFont =  UIFont.FromName("Helvetica-Oblique", 12);
		static UIFont BodyFont = UIFont.FromName("Helvetica", 14);

        public AnnouncementCell(IntPtr p) : base(p)
        {
			Title = new UILabel() {Font = TitleFont, Lines = 0};
            Body = new UILabel() {Font = BodyFont, Lines = 0};
            Time = new UILabel() {Font = TimeFont, Lines = 0};

			var containerView = new UIView() { BackgroundColor = UIColor.White };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
				make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(5, 5, 5, 5));
			});
			containerView.Layer.CornerRadius = 3;
			containerView.Layer.BorderWidth = 0.5f;
			containerView.Layer.BorderColor = UIColor.LightGray.CGColor;

            var parentView = containerView;

            parentView.AddSubviews(new UIView[] { Title, Body, Time });

            Title.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView).Offset(5);
                make.Left.EqualTo(parentView).Offset(5);
				make.Width.EqualTo(parentView);
            });

            Body.MakeConstraints(make =>
            {
                make.Top.EqualTo(Title.Bottom()).Offset(5);
                make.Left.EqualTo(Title);
				make.Width.EqualTo(parentView);
            });

            Time.MakeConstraints(make =>
            {
                make.Right.EqualTo(parentView).Offset(-5);
                make.Bottom.EqualTo(parentView).Offset(-5);
            });

        }

        public void UpdateCell(Announcement announcement)
        {
            Title.Text = announcement.Title;
            Body.Text = announcement.Body;
            Time.Text = Utilities.FormatDateTime(announcement.Time);
        }
    }
}
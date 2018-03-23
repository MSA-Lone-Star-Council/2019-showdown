using System;
using Common.Common;
using Common.Common.Models;
using UIKit;
using Masonry;

namespace Common.iOS
{
    public class AnnouncementCell : UITableViewCell
    {
        UILabel Title { get; set; }
		UILabel Body { get; set; }
		UILabel Time { get; set; }

        static UIFont TitleFont = UIFont.SystemFontOfSize(16, UIFontWeight.Bold);
        static UIFont TimeFont =  UIFont.SystemFontOfSize(16, UIFontWeight.Regular);
        static UIFont BodyFont = UIFont.SystemFontOfSize(16, UIFontWeight.Regular);

        public AnnouncementCell(IntPtr p) : base(p)
        {
			Title = new UILabel() 
            {
                Font = TitleFont,
                Lines = 2
            };
            Body = new UILabel() 
            {
                Font = BodyFont, 
                Lines = 0,
            };
            Time = new UILabel() 
            {
                Font = TimeFont,
                TextColor = UIColor.DarkGray,
                Lines = 2,
            };

			var containerView = new UIView() { BackgroundColor = UIColor.Clear };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
				make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(10, 10, 10, 10));
			});


            var parentView = containerView;

            parentView.AddSubviews(new UIView[] { Title, Body, Time });

            Title.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView);
                make.Left.EqualTo(parentView);
				make.Right.EqualTo(parentView).Offset(-80);
            });

            Time.MakeConstraints(make =>
            {
                make.Top.EqualTo(parentView);
                make.Right.EqualTo(parentView);
            });

            Body.MakeConstraints(make =>
            {
                make.Top.EqualTo(Title.Bottom()).Offset(5);
                make.Left.EqualTo(Title);
				make.Width.EqualTo(parentView);
            });



        }

        public void UpdateCell(Announcement announcement)
        {
            Title.Text = announcement.Title;
            Body.Text = announcement.Body;
            Time.Text = Utilities.FormatDateTime((System.DateTimeOffset)announcement.Time);
        }
    }
}
using System;
using Common.Common.Models;
using UIKit;
using Masonry;
using System.Globalization;

namespace Client.iOS
{
	public class ScoreCell : UITableViewCell
	{
		UILabel TimeLabel { get; set; }
		UILabel AwayScoreLabel { get; set; }
		UILabel HomeScoreLabel { get; set; }

		static UIFont TimeFont = UIFont.FromName("Helvetica", 12);
		static UIFont ScoreFont = UIFont.FromName("Helvetica-Bold", 20);

		public ScoreCell(IntPtr p) : base(p)
		{
			var containerView = new UIView() { BackgroundColor = UIColor.White };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
				make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(2, 0, 2, 0));
			});
			containerView.Layer.CornerRadius = 3;
			containerView.Layer.BorderWidth = 0.5f;
			containerView.Layer.BorderColor = UIColor.LightGray.CGColor;

			TimeLabel = new UILabel() { Font = TimeFont };
			AwayScoreLabel = new UILabel() { Font = ScoreFont };
			HomeScoreLabel = new UILabel() { Font = ScoreFont };

			containerView.Add(TimeLabel);
			containerView.Add(AwayScoreLabel);
			containerView.Add(HomeScoreLabel);

			var parentView = containerView;
			TimeLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView);
				make.Top.EqualTo(parentView).Offset(5);
			});

			AwayScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView.CenterX()).MultipliedBy(0.5f);
				make.Bottom.EqualTo(parentView).Offset(-5);
			});

			HomeScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView.CenterX()).MultipliedBy(1.5f);
				make.Baseline.EqualTo(AwayScoreLabel);
			});
		}

		public void UpdateCell(ScoreRecord record)
		{
			string format = "h:mm:ss";

			TimeLabel.Text = record.Time.ToString(format, null as DateTimeFormatInfo);

			var awayTeam = record.Scores[0];
			var homeTeam = record.Scores[1];

			AwayScoreLabel.Text = awayTeam.Points.ToString();
			HomeScoreLabel.Text = homeTeam.Points.ToString();
		}
	}
}

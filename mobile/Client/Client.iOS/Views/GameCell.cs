using System;
using Common.Common.Models;
using UIKit;
using Masonry;
using Foundation;
using System.Globalization;

namespace Client.iOS
{
	public class GameCell : UITableViewCell
	{
		UILabel Title { get; set; }
		UILabel Time { get; set; }
		UILabel EventName { get; set; }

		// TODO: Support variable number of teams - that would required a table view inside...
		UILabel HomeTeamName { get; set; }
		UILabel HomeTeamScore { get; set; }

		UILabel AwayTeamName { get; set; }
		UILabel AwayTeamScore { get; set; }

		static UIFont TitleFont = UIFont.FromName("Helvetica-Bold", 18);
		static UIFont TimeFont =  UIFont.FromName("Helvetica-Oblique", 12);
		static UIFont EventFont = UIFont.FromName("Helvetica-BoldOblique", 16);
		static UIFont ScoreFont = UIFont.FromName("Helvetica-Bold", 20);

		public GameCell(IntPtr p) : base (p)
		{
			var containerView = new UIView() { BackgroundColor = UIColor.White };
			ContentView.Add(containerView);
			containerView.MakeConstraints(make =>
			{
				make.Edges.EqualTo(ContentView).Insets(new UIEdgeInsets(5, 5, 5, 5));
			});
			containerView.Layer.CornerRadius = 10;
			containerView.Layer.BorderWidth = 1.5f;
			containerView.Layer.BorderColor = UIColor.LightGray.CGColor;


			Title = new UILabel() { Font = TitleFont };
			Time = new UILabel() { Font = TimeFont };
			EventName = new UILabel() { Font = EventFont };

			AwayTeamName = new UILabel() { Font = ScoreFont };
			AwayTeamScore = new UILabel() { Font = ScoreFont };
			HomeTeamName = new UILabel() { Font = ScoreFont };
			HomeTeamScore = new UILabel() { Font = ScoreFont };


			containerView.AddSubviews(new UIView[] { 
				Title, 
				EventName,
				AwayTeamName, 
				AwayTeamScore, 
				HomeTeamName, 
				HomeTeamScore,
				Time,

			});


			var parentView = containerView;

			var lrOffset = 7;
			var tbOffset = 5;

			Title.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(tbOffset);
				make.Left.EqualTo(parentView).Offset(lrOffset);
				make.Width.EqualTo(parentView);
			});

			EventName.MakeConstraints(make =>
			{
				make.Left.EqualTo(Title);
				make.Top.EqualTo(Title.Bottom()).Offset(tbOffset);
				make.Width.EqualTo(parentView);
			});


			AwayTeamName.MakeConstraints(make =>
			{
				make.Top.EqualTo(EventName.Bottom()).Offset(tbOffset + 10);
				make.Left.EqualTo(EventName);
			});
			AwayTeamScore.MakeConstraints(make =>
			{
				make.Top.EqualTo(AwayTeamName.Top());
				make.Right.EqualTo(Time);
			});

			HomeTeamName.MakeConstraints(make =>
			{
				make.Top.EqualTo(AwayTeamName.Bottom()).Offset(tbOffset);
				make.Left.EqualTo(AwayTeamName.Left());
			});
			HomeTeamScore.MakeConstraints(make =>
			{
				make.Top.EqualTo(HomeTeamName.Top());
				make.Right.EqualTo(AwayTeamScore.Right());
			});

			Time.MakeConstraints(make =>
			{
				make.Bottom.EqualTo(parentView.Bottom()).Offset(-tbOffset);
				make.Right.EqualTo(parentView).Offset(-lrOffset);
			});
		}

		public void UpdateCell(Game g)
		{
			string format = "M/d h:mm:ss";

			Title.Text = g.Title;
			Time.Text = g.Time.ToString(format, null as DateTimeFormatInfo);
			EventName.Text = g.Event.Title;

			var awayTeam = g.Score[0];
			var homeTeam = g.Score[1];

			AwayTeamName.Text = awayTeam.Team;
			AwayTeamScore.Text = awayTeam.Points.ToString();

			HomeTeamName.Text = homeTeam.Team;
			HomeTeamScore.Text = homeTeam.Points.ToString();
		}
	}
}

using System;
using Common.Common.Models;
using CoreGraphics;
using UIKit;
using Masonry;

namespace Client.iOS
{
	public class GameHeader : UIView
	{
	    public Game Game
	    {
	        set
	        {
				gameTitleLabel.Text = value.Title;
				eventTitleLabel.Text = value.Event.Title;
	            HomeTeamNameLabel.Text = value.HomeTeam.ShortName;
	            AwayTeamNameLabel.Text = value.AwayTeam.ShortName;
				HomeScoreLabel.Text = value.Score.HomePoints.ToString();
			    AwayScoreLabel.Text = value.Score.AwayPoints.ToString();

				inProgressLabel.Text = value.InProgress ? "Live" : "Final";
				inProgressLabel.TextColor = value.InProgress ? UIColor.FromRGB(0, 0.5f, 0) : UIColor.Black;

				if (!value.InProgress && value.Score.HomePoints != value.Score.AwayPoints) // Game is over, and not a tie
				{
					var winnerLabel = value.Score.HomePoints > value.Score.AwayPoints ? HomeScoreLabel : AwayScoreLabel;
					var loserLabel = value.Score.HomePoints > value.Score.AwayPoints ? AwayScoreLabel : HomeScoreLabel;

					winnerLabel.TextColor = UIColor.Black;
					loserLabel.TextColor = UIColor.LightGray;
				}
	        }
	    }

		static UIFont GameTitleFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
		static UIFont EventTitleFont = UIFont.SystemFontOfSize(16, UIFontWeight.Thin);
		static UIFont NameFont = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
		static UIFont ScoreFont = UIFont.SystemFontOfSize(32, UIFontWeight.Bold);
		static UIFont InProgressFont = UIFont.SystemFontOfSize(14, UIFontWeight.Heavy);

		UILabel gameTitleLabel = new UILabel() { Font = GameTitleFont };
		UILabel eventTitleLabel = new UILabel() { Font = EventTitleFont };

		UILabel AwayTeamNameLabel { get; set; }
		UILabel HomeTeamNameLabel { get; set; }
		UILabel AwayScoreLabel { get; set; }
		UILabel HomeScoreLabel { get; set; }

		UILabel inProgressLabel = new UILabel() { Font = InProgressFont };


		public GameHeader()
		{
			BackgroundColor = UIColor.White;
			Layer.BorderColor = UIColor.LightGray.CGColor;
			Layer.BorderWidth = 0.5f;
		}

		public void LayoutView()
		{
			BackgroundColor = UIColor.White;

			AwayTeamNameLabel = new UILabel { Font = NameFont };
			HomeTeamNameLabel = new UILabel { Font = NameFont };
			AwayScoreLabel = new UILabel { Font = ScoreFont };
			HomeScoreLabel = new UILabel { Font = ScoreFont };


			this.AddSubviews(new UIView[] {
				gameTitleLabel,
				eventTitleLabel,
				AwayTeamNameLabel, 
				HomeTeamNameLabel, 
				AwayScoreLabel, 
				HomeScoreLabel,
				inProgressLabel
			});

			var parentView = this;

			gameTitleLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView);
				make.Top.EqualTo(parentView).Offset(5);
			});

			eventTitleLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView);
				make.Top.EqualTo(gameTitleLabel.Bottom()).Offset(2);
			});

			AwayTeamNameLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView).MultipliedBy(0.5f);
				make.Top.EqualTo(eventTitleLabel.Bottom()).Offset(10);
			});

			HomeTeamNameLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView).MultipliedBy(1.5f);
				make.Baseline.EqualTo(AwayTeamNameLabel);
			});

			AwayScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(AwayTeamNameLabel);
				make.Top.EqualTo(AwayTeamNameLabel.Bottom()).Offset(3);
			});

			HomeScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(HomeTeamNameLabel);
				make.Baseline.EqualTo(AwayScoreLabel);
			});

			inProgressLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView);
				make.Baseline.EqualTo(AwayScoreLabel);
			});
		}
	}
}

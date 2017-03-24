using System;
using Common.Common.Models;
using CoreGraphics;
using UIKit;
using Masonry;

namespace Client.iOS
{
	public class GameHeader : UIView
	{
		public Score HomeScore
		{
			set
			{
				HomeTeamNameLabel.Text = value.Team;
				HomeScoreLabel.Text = value.Points.ToString();
			}
		}

		public Score AwayScore
		{
			set
			{
				AwayTeamNameLabel.Text = value.Team;
				AwayScoreLabel.Text = value.Points.ToString();
			}
		}

		UILabel AwayTeamNameLabel { get; set; }
		UILabel HomeTeamNameLabel { get; set; }
		UILabel AwayScoreLabel { get; set; }
		UILabel HomeScoreLabel { get; set; }

		static UIFont NameFont = UIFont.FromName("Helvetica-BoldOblique", 20);
		static UIFont ScoreFont = UIFont.FromName("Helvetica-Bold", 36);

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
				AwayTeamNameLabel, 
				HomeTeamNameLabel, 
				AwayScoreLabel, 
				HomeScoreLabel 
			});

			var parentView = this;
			AwayTeamNameLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView).MultipliedBy(0.5f);
				make.Top.EqualTo(parentView).Offset(5);
			});

			HomeTeamNameLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView).MultipliedBy(1.5f);
				make.Baseline.EqualTo(AwayTeamNameLabel);
			});

			AwayScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(AwayTeamNameLabel);
				make.Top.EqualTo(AwayTeamNameLabel.Bottom()).Offset(5);
			});

			HomeScoreLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(HomeTeamNameLabel);
				make.Baseline.EqualTo(AwayScoreLabel);
			});
		}
	}
}

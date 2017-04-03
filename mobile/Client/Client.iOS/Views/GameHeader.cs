using System;
using Common.Common.Models;
using CoreGraphics;
using UIKit;
using Masonry;
using Foundation;
using Plugin.Iconize.iOS.Controls;

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
				HomeTeamNameLabel.SetTitle(value.HomeTeam.ShortName, UIControlState.Normal);
				AwayTeamNameLabel.SetTitle(value.AwayTeam.ShortName, UIControlState.Normal);
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

		public bool IsSubscribed
		{
			set
			{
				string normalText = value ? $"{{fa-bell 20pt}}" : $"{{fa-bell-o 20pt}}";
				string highligtedText = value ? $"{{fa-bell-o 20pt}}" : $"{{fa-bell 20pt}}";
				NotificationButton.SetTitle(normalText, UIControlState.Normal);
				NotificationButton.SetTitle(highligtedText, UIControlState.Highlighted);
			}
		}

		static UIFont GameTitleFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);
		static UIFont EventTitleFont = UIFont.SystemFontOfSize(16, UIFontWeight.Thin);
		static UIFont NameFont = UIFont.SystemFontOfSize(20, UIFontWeight.Semibold);
		static UIFont ScoreFont = UIFont.SystemFontOfSize(32, UIFontWeight.Bold);
		static UIFont InProgressFont = UIFont.SystemFontOfSize(14, UIFontWeight.Heavy);



		IconButton NotificationButton { get; set; }
		UILabel gameTitleLabel = new UILabel() { Font = GameTitleFont };
		UILabel eventTitleLabel = new UILabel() { Font = EventTitleFont };

		UIButton AwayTeamNameLabel { get; set; }
		UIButton HomeTeamNameLabel { get; set; }
		UILabel AwayScoreLabel { get; set; }
		UILabel HomeScoreLabel { get; set; }

		UILabel inProgressLabel = new UILabel() { Font = InProgressFont };

		public Action AwayTeamAction { get; set; }
		public Action HomeTeamAction { get; set; }
		public Action NotificationTappedAction { get; set; }

		public GameHeader()
		{
			BackgroundColor = UIColor.White;
			Layer.BorderColor = UIColor.LightGray.CGColor;
			Layer.BorderWidth = 0.5f;
		}

		public void LayoutView()
		{
			BackgroundColor = UIColor.White;

			NotificationButton = new IconButton();
			NotificationButton.TouchUpInside += (sender, e) => NotificationTappedAction();

			AwayTeamNameLabel = new UIButton { Font = NameFont, BackgroundColor = UIColor.Clear };
			AwayTeamNameLabel.SetTitleColor(UIColor.Black, UIControlState.Normal);
			AwayTeamNameLabel.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			AwayTeamNameLabel.TouchUpInside += (sender, e) => AwayTeamAction();

			HomeTeamNameLabel = new UIButton { Font = NameFont };
			HomeTeamNameLabel.SetTitleColor(UIColor.Black, UIControlState.Normal);
			HomeTeamNameLabel.SetTitleColor(UIColor.LightGray, UIControlState.Highlighted);
			HomeTeamNameLabel.TouchUpInside += (sender, e) => HomeTeamAction();

			AwayScoreLabel = new UILabel { Font = ScoreFont };
			HomeScoreLabel = new UILabel { Font = ScoreFont };


			this.AddSubviews(new UIView[] {
				NotificationButton,
				gameTitleLabel,
				eventTitleLabel,
				AwayTeamNameLabel, 
				HomeTeamNameLabel, 
				AwayScoreLabel, 
				HomeScoreLabel,
				inProgressLabel
			});

			var parentView = this;

			NotificationButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(2);
				make.Right.EqualTo(parentView).Offset(-2);
				make.Width.EqualTo((NSNumber)40);
				make.Height.EqualTo((NSNumber)40);
			});

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
				make.Height.EqualTo((NSNumber)40);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
			});

			HomeTeamNameLabel.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(parentView).MultipliedBy(1.5f);
				make.Baseline.EqualTo(AwayTeamNameLabel);
				make.Height.EqualTo((NSNumber)40);
				make.Width.EqualTo(parentView).MultipliedBy(0.5f);
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

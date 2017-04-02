using System;
using Common.Common.Models;
using UIKit;
using Masonry;
using Foundation;
using System.Globalization;
using Plugin.Iconize.iOS.Controls;

namespace Client.iOS
{
	public class GameCell : UITableViewCell
	{
		public delegate void SubscribedToGame(Game game, bool currentValue);

		IconButton NotificationButton { get; set; }
		UILabel Title { get; set; }
		UILabel Time { get; set; }
		UILabel EventName { get; set; }

		// TODO: Support variable number of teams - that would required a table view inside...
		UILabel HomeTeamName { get; set; }
		UILabel HomeTeamScore { get; set; }

		UILabel AwayTeamName { get; set; }
		UILabel AwayTeamScore { get; set; }

		static UIFont TitleFont = UIFont.SystemFontOfSize(16, UIFontWeight.Bold);
		static UIFont TimeFont = UIFont.SystemFontOfSize(12, UIFontWeight.UltraLight);
		static UIFont EventFont = UIFont.SystemFontOfSize(14, UIFontWeight.Thin);
		static UIFont ScoreFont = UIFont.SystemFontOfSize(18, UIFontWeight.Bold);

		public GameCell(IntPtr p) : base (p)
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

			NotificationButton = new IconButton();

			Title = new UILabel() { Font = TitleFont, TextAlignment = UITextAlignment.Center };
			Time = new UILabel() { Font = TimeFont };
			EventName = new UILabel() { Font = EventFont, TextAlignment = UITextAlignment.Center };

			AwayTeamName = new UILabel() { Font = ScoreFont };
			AwayTeamScore = new UILabel() { Font = ScoreFont };
			HomeTeamName = new UILabel() { Font = ScoreFont };
			HomeTeamScore = new UILabel() { Font = ScoreFont };


			containerView.AddSubviews(new UIView[] {
				NotificationButton,
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

			NotificationButton.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(3);
				make.Right.EqualTo(parentView).Offset(-3);
				make.Height.EqualTo((NSNumber)20);
				make.Width.EqualTo((NSNumber)20);
			});

			Title.MakeConstraints(make =>
			{
				make.Top.EqualTo(parentView).Offset(tbOffset);
				make.CenterX.EqualTo(parentView);
				make.Width.EqualTo(parentView);
			});

			EventName.MakeConstraints(make =>
			{
				make.CenterX.EqualTo(Title);
				make.Top.EqualTo(Title.Bottom()).Offset(2);
				make.Width.EqualTo(parentView);
			});

			AwayTeamName.MakeConstraints(make =>
			{
				make.Top.EqualTo(EventName.Bottom()).Offset(tbOffset);
				make.Left.EqualTo(parentView).Offset(lrOffset);
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
				make.Bottom.EqualTo(parentView.Bottom()).Offset(-5);
				make.Right.EqualTo(parentView).Offset(-lrOffset);
			});
		}

		public void UpdateCell(Game g, bool subscribed = true)
		{
			string format = "M/d h:mm:ss";

			Title.Text = g.Title;
			Title.TextColor = g.InProgress ? UIColor.FromRGB(0, 0.5f, 0) : UIColor.Black;
			Time.Text = ((DateTimeOffset) g.Score.Time).ToString(format, null as DateTimeFormatInfo);
			EventName.Text = g.Event.Title;

			var awayTeam = g.HomeTeam;
			var homeTeam = g.AwayTeam;

			var awayTeamLost = g.Score.AwayPoints < g.Score.HomePoints;
			var isTie = g.Score.AwayPoints == g.Score.HomePoints;

			AwayTeamName.Text = awayTeam.ShortName;
			AwayTeamScore.Text = g.Score.AwayPoints.ToString();
			AwayTeamScore.TextColor = !g.InProgress && awayTeamLost && !isTie ? UIColor.LightGray : UIColor.Black;
			AwayTeamName.TextColor = !g.InProgress && awayTeamLost && !isTie ? UIColor.LightGray : UIColor.Black;

			HomeTeamName.Text = homeTeam.ShortName;
			HomeTeamScore.Text = g.Score.HomePoints.ToString();
			HomeTeamScore.TextColor = g.InProgress || awayTeamLost || isTie ? UIColor.Black : UIColor.LightGray;
			HomeTeamName.TextColor = g.InProgress || awayTeamLost || isTie ? UIColor.Black : UIColor.LightGray;

			string normalText = subscribed ? $"{{fa-bell 16pt}}" : $"{{fa-bell-o 16pt}}";
			string highligtedText = subscribed ? $"{{fa-bell-o 16pt}}" : $"{{fa-bell 16pt}}";

			NotificationButton.SetTitle(normalText, UIControlState.Normal);
			NotificationButton.SetTitle(highligtedText, UIControlState.Highlighted);
		}
	}
}

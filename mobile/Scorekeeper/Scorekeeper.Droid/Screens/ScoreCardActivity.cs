using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Scorekeeper.Common;
using Common.Common.Models;

namespace Scorekeeper.Droid
{
    [Activity(Label = "Score Card")]
    public class ScoreCardActivity : Activity, IScoreCardView
    {
        public Game Game { get; set; }

        private ScoreCardPresenter presenter;

        private TextView homeTeamName, awayTeamName;
        private TextView homeScoreTV, awayScoreTV;
        private TextView homeScoreDelta, awayScoreDelta;
        private Button homePlusOneButton, awayPlusOneButton;
        private Button HomeMinusOneButton, AwayMinusOneButton;
        private Button postScoreButton, endGameButton;

        string IScoreCardView.HomeTeamName
        {
            get
            {
                return homeTeamName.Text;
            }
            set
            {
                homeTeamName.Text = value;
            }
        }

        string IScoreCardView.AwayTeamName
        {
            get
            {
                return awayTeamName.Text;
            }
            set
            {
                awayTeamName.Text = value;
            }
        }

        int IScoreCardView.HomeScore
        {
            get
            {
                return int.Parse(homeScoreTV.Text);
            }

            set
            {
                homeScoreTV.Text = value.ToString();
            }
        }

        int IScoreCardView.AwayScore
        {
            get
            {
                return int.Parse(awayScoreTV.Text);
            }

            set
            {
                awayScoreTV.Text = value.ToString();
            }
        }

        int IScoreCardView.HomeScoreDelta
        {
            get
            {
                return int.Parse(homeScoreDelta.Text);
            }

            set
            {
                homeScoreDelta.Text = value.ToString();
            }
        }

        int IScoreCardView.AwayScoreDelta
        {
            get
            {
                return int.Parse(awayScoreDelta.Text);
            }

            set
            {
                awayScoreDelta.Text = value.ToString();
            }
        }

        bool IScoreCardView.CanPostScore
        {
            set
            {
                postScoreButton.Enabled = value;
            }
        }

        void IScoreCardView.EndGame()
        {
            //TODO
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.score_card_layout);

            string deserializedObject = this.Intent.GetStringExtra("game");
            Game = Game.FromJSON(deserializedObject);

            presenter = new ScoreCardPresenter(((ShowdownScorekeeperApplication)Application).BackendClient);

            homeTeamName = FindViewById<TextView>(Resource.Id.home_team_name);
            awayTeamName = FindViewById<TextView>(Resource.Id.away_team_name);

            homeScoreTV = FindViewById<TextView>(Resource.Id.home_score);
            awayScoreTV = FindViewById<TextView>(Resource.Id.away_score);

            homeScoreDelta = FindViewById<TextView>(Resource.Id.home_score_delta_TextView);
            awayScoreDelta = FindViewById<TextView>(Resource.Id.away_score_delta_TextView);

            homePlusOneButton = FindViewById<Button>(Resource.Id.home_score_plus_one_button);
            awayPlusOneButton = FindViewById<Button>(Resource.Id.away_score_plus_one_button);
            HomeMinusOneButton = FindViewById<Button>(Resource.Id.home_score_minus_one_button);
            AwayMinusOneButton = FindViewById<Button>(Resource.Id.away_score_minus_one_button);


            homePlusOneButton.Click += (sender, e) => { presenter.UpdateScore(ScoreCardPresenter.Team.Home, 1); };
            awayPlusOneButton.Click += (sender, e) => { presenter.UpdateScore(ScoreCardPresenter.Team.Away, 1); };
            HomeMinusOneButton.Click += (sender, e) => { presenter.UpdateScore(ScoreCardPresenter.Team.Home, -1); };
            AwayMinusOneButton.Click += (sender, e) => { presenter.UpdateScore(ScoreCardPresenter.Team.Away, -1); };

            postScoreButton = FindViewById<Button>(Resource.Id.post_score_button);
            postScoreButton.Click += async (sender, e) => { await presenter.PostScoreUpdateAsync(); };

            endGameButton = FindViewById<Button>(Resource.Id.end_game_button);
            endGameButton.Click += async (sender, e) => { await presenter.EndGameAsync(); };

        }

        protected override void OnResume()
        {
            base.OnResume();
            presenter.TakeView(this);
            presenter.SetupView();
        }

        protected override void OnPause()
        {
            base.OnPause();
            presenter.RemoveView();
        }
    }
}

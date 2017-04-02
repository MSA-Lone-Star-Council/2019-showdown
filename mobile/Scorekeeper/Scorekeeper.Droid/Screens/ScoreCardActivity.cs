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
using Admin.Common.API.Entities;

namespace Scorekeeper.Droid
{
    [Activity(Label = "ScoreCardActivity")]
    public class ScoreCardActivity : Activity, IScoreCardView
    {
        public Game Game { get; set; }

        private ScoreCardPresenter presenter;

        private TextView homeScoreTV, awayScoreTV;
        private Button HomePlusOneButton, AwayPlusOneButton;

        public int AwayScore
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

        public int HomeScore
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.score_card_layout);

            presenter = new ScoreCardPresenter();

            homeScoreTV = FindViewById<TextView>(Resource.Id.home_score);
            awayScoreTV = FindViewById<TextView>(Resource.Id.away_score);
            HomePlusOneButton = FindViewById<Button>(Resource.Id.home_update_score_button);
            AwayPlusOneButton = FindViewById<Button>(Resource.Id.away_update_score_button);
            
            HomePlusOneButton.Click += async (sender, e) => { await presenter.IncreaseScore(ScoreCardPresenter.Team.Home, 1); };
            AwayPlusOneButton.Click += async (sender, e) => { await presenter.IncreaseScore(ScoreCardPresenter.Team.Away, 1); };
        }

        protected async override void OnResume()
        {
            base.OnResume();
            presenter.TakeView(this);
            await presenter.SetupView();
        }

        protected override void OnPause()
        {
            base.OnPause();
            presenter.RemoveView();
        }
    }
}

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
using Client.Common;
using Common.Common;
using Common.Common.Models;
using Android.Support.V7.Widget;
using Client.Droid.Adapters;

namespace Client.Droid.Screens
{
    [Activity(Label = "Clicked Game")]
    public class GameScreenActivity : Activity, IGameView
    {
        GamePresenter Presenter;
        
        List<Score> IGameView.ScoreHistory
        {
            set
            {
                ScoreAdapter adapter = this.Adapter;
                if (adapter == null) return;
                adapter.Scores = value;
                adapter.NotifyDataSetChanged();
            }
        }
        RecyclerView ScoreView { get; set; }
        ScoreAdapter Adapter { get; set; }

        public Game Game { get; set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Game = Game.FromJSON(this.Intent.GetStringExtra("game"));

            SetContentView(Client.Droid.Resource.Layout.game_screen_layout);
            var gameTitle = FindViewById<TextView>(Resource.Id.game_title);
            var gameEvent = FindViewById<TextView>(Resource.Id.game_event);
            var team1 = FindViewById<TextView>(Resource.Id.team_1);
            var team2 = FindViewById<TextView>(Resource.Id.team_2);
            var score1 = FindViewById<TextView>(Resource.Id.score_1);
            var score2 = FindViewById<TextView>(Resource.Id.score_2);
            var isLive = FindViewById<TextView>(Resource.Id.is_live);

            gameTitle.Text = Game.Title;
            gameEvent.Text = Game.Event.Title;
            team1.Text = Game.AwayTeam.ShortName;
            team2.Text = Game.HomeTeam.ShortName;
            score1.Text = Game.Score.AwayPoints.ToString();
            score2.Text = Game.Score.HomePoints.ToString();
            isLive.Text = Game.InProgress ? ("Live") : ("Not in progress");

            Presenter = new GamePresenter(((ShowdownClientApplication)Application).BackendClient, null);
            Presenter.TakeView(this);

            Adapter = new ScoreAdapter()
            {
                Scores = new List<Score>()
            };
            ScoreView = FindViewById<RecyclerView>(Resource.Id.scoreRecyclerView);
            ScoreView.SetLayoutManager(new LinearLayoutManager(this));
            ScoreView.SetAdapter(Adapter);

            await Presenter.OnBegin();
        }

        public void ShowMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
            
        }
    }
}
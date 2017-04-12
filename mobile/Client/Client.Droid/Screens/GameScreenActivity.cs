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
using System.Timers;

namespace Client.Droid.Screens
{
    [Activity(Label = "Game Details", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
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

        private TextView gameTitle;
        private TextView gameEvent;
        private TextView team1;
        private TextView team2;
        private TextView score1;
        private TextView score2;
        private TextView isLive;

        Timer timer = new Timer(TimeSpan.FromSeconds(5).TotalMilliseconds) { AutoReset = true };

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Game = Game.FromJSON(this.Intent.GetStringExtra("game"));

            SetContentView(Resource.Layout.game_screen_layout);
            gameTitle = FindViewById<TextView>(Resource.Id.game_title);
            gameEvent = FindViewById<TextView>(Resource.Id.game_event);
            team1 = FindViewById<TextView>(Resource.Id.team_1);
            team2 = FindViewById<TextView>(Resource.Id.team_2);
            score1 = FindViewById<TextView>(Resource.Id.score_1);
            score2 = FindViewById<TextView>(Resource.Id.score_2);
            isLive = FindViewById<TextView>(Resource.Id.is_live);

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
        }

        protected async override void OnResume()
        {
            base.OnResume();
            await Presenter.OnBegin();
            timer.Elapsed += (sender, e) => RunOnUiThread(async () => await Presenter.OnTick());
            timer.Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            timer.Stop();
            Presenter.RemoveView();
        }

        public void ShowMessage(string message)
        {
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }

        public void Refresh()
        {
            //Update all the textviews
            
        }
    }
}
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
    [Activity(Label = "GameScreenActivity")]
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
            SetContentView(Client.Droid.Resource.Layout.game_screen_layout);
            Presenter = new GamePresenter(new ShowdownRESTClient(), null);
            Presenter.TakeView(this);

            Adapter = new ScoreAdapter()
            {
                Scores = new List<Score>()
            };
            await Presenter.OnBegin();
        }

        public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
                View view = inflater.Inflate(Resource.Layout.game_screen_layout, container, false);
                ScoreView = view.FindViewById<RecyclerView>(Resource.Id.scoreRecyclerView);
                ScoreView.SetLayoutManager(new LinearLayoutManager(this));
                ScoreView.SetAdapter(Adapter);
                return view;
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
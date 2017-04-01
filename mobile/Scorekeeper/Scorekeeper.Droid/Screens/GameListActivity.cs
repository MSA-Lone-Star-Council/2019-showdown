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
using Android.Support.V7.Widget;
using Scorekeeper.Common;
using Common.Common.Models;

namespace Scorekeeper.Droid
{
    [Activity(Label = "List of Games")]
    public class GameListActivity : Activity, IGameListView
    {
        GameListPresenter Presenter { get; set; }

        List<Game> IGameListView.Games
        {
            set
            {
                GameListAdapter adapter = this.Adapter;
                if (adapter == null) return;
                adapter.Games = value;
                adapter.NotifyDataSetChanged();
            }
        }
           
        RecyclerView GameList { get; set; }
        GameListAdapter Adapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.game_list_layout);

            Adapter = new GameListAdapter()
            {
                Games = new List<Game>()
            };

            GameList = FindViewById<RecyclerView>(Resource.Id.gameListRecyclerView);
            GameList.SetLayoutManager(new LinearLayoutManager(this));
            GameList.SetAdapter(Adapter);

        }

        protected override void OnResume()
        {
            base.OnResume();

            Presenter = new GameListPresenter();
            Presenter.TakeView(this);
            Adapter.ItemClick += (object sender, GameListAdapterClickEventArgs args) => Presenter.OnClickRow(args.Game);
            

        }

        void IGameListView.OpenGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
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
        string IGameListView.Token { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        List<Game> IGameListView.Games { set => throw new NotImplementedException(); }

        RecyclerView GameList { get; set; }
        GameListAdapter Adapter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
        }

        void IGameListView.OpenGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
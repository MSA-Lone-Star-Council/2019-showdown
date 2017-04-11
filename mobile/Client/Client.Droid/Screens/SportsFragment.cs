using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Common;
using Common.Common;
using Common.Droid;
using Common.Common.Models;
using Client.Droid.Adapters;
using Android.Support.V7.Widget;

namespace Client.Droid.Screens
{
    public class SportsFragment : Fragment, ISportsView
    {
        SportsPresenter Presenter;

        List<Game> ISportsView.Games
        {
            set
            {
                SportsAdapter adapter = this.Adapter;
                if (adapter == null) return;
                adapter.Games = value;
                adapter.NotifyDataSetChanged();
            }
            get
            {
                return this.Adapter.Games;
            }
        }
        RecyclerView SportsView { get; set; }
        SportsAdapter Adapter { get; set; }

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
            var application = Activity.Application as ShowdownClientApplication;

            Presenter = new SportsPresenter(application.BackendClient, application.SubscriptionManager);
            Presenter.TakeView(this);

            Adapter = new SportsAdapter()
            {
                Games = new List<Game>(),
                Presenter = Presenter
            };
            Adapter.ItemClick += (object sender, SportsAdapterClickEventArgs args) => Presenter.OnClickRow(args.Game);
            await Presenter.OnBegin();

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.fragment_sports, container, false);

            //Setup recyclerview
            SportsView = view.FindViewById<RecyclerView>(Resource.Id.sportsRecyclerView);
            SportsView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            //create adapter
            SportsView.SetAdapter(Adapter);
            //return the view
            return view;
        }

        public void Refresh()
        {
            Adapter?.NotifyDataSetChanged();
        }

        void ISportsView.OpenGame(Game g)
        {
            //throw new NotImplementedException();
            var intent = new Intent(this.Activity, typeof(GameScreenActivity));
            intent.PutExtra("game", g.ToJSON());
            StartActivity(intent);
        }

        void ISportsView.ShowMessage(string message)
        {
            Toast.MakeText(this.Activity, message, ToastLength.Short).Show();
        }

        //Add in handler for onClick events.
    }
}
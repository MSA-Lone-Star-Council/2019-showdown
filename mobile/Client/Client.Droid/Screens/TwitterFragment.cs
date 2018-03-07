using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using Client.Common;
using Client.Droid.Adapters;
using Tweetinvi.Events;
using Tweetinvi.Models;

namespace Client.Droid
{
    public class TwitterFragment : Fragment, ITweetView
    {
        TweetPresenter Presenter { get; set; }

        List<ITweet> ITweetView.Tweets
        {
            set
            {
                TwitterAdapter adapter = this.Adapter;
                if (adapter == null) return;
                adapter.Tweets = value;
                adapter.NotifyDataSetChanged();
            }
        }
        RecyclerView TwitterView { get; set; }
        TwitterAdapter Adapter { get; set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Presenter = new TweetPresenter(((ShowdownClientApplication)this.Activity.Application).BackendClient);
            Presenter.TakeView(this);

            Adapter = new TwitterAdapter()
            {
                Tweets = new List<ITweet>()
            };
            Adapter.ItemClick += (object sender, TwitterAdapterClickEventArgs args) => Presenter.OnClickRow(args.Tweet);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_twitter, container, false);

            // Set up Recycler View for the Schedule
            TwitterView = view.FindViewById<RecyclerView>(Resource.Id.twitterRecyclerView);
            TwitterView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            TwitterView.SetAdapter(Adapter);

            return view;
        }

        EventHandler<MatchedTweetReceivedEventArgs> ITweetView.AddTweetFromStream()
        {
            this.


            throw new NotImplementedException();
        }

        void ITweetView.OpenTweet(ITweet tweet)
        {
            throw new NotImplementedException();
        }

        void ITweetView.Refresh()
        {
            throw new NotImplementedException();
        }
    }
}
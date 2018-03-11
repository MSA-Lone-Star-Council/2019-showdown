using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using System.Threading.Tasks;
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
        FetchTweets task;

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
            task = new FetchTweets(Adapter.Tweets, this, Presenter);
            Presenter.ClientUpdateUi = task;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_twitter, container, false);

            // Set up Recycler View for the Schedule
            TwitterView = view.FindViewById<RecyclerView>(Resource.Id.twitterRecyclerView);
            TwitterView.SetLayoutManager(new LinearLayoutManager(this.Activity));
            TwitterView.SetAdapter(Adapter);
            task.Execute();

            return view;
        }

        public override void OnPause()
        {
            base.OnPause();
            Presenter.OnPause();
            task.Cancel(true);
        }

        public async override void OnResume()
        {
            base.OnResume();

            Presenter.TakeView(this);
            Presenter.OnResume();
        }

        public override void OnStop()
        {
            base.OnStop();
            Presenter.RemoveView();
        }

        void ITweetView.OpenTweet(ITweet tweet)
        {
            throw new NotImplementedException();
        }

        void ITweetView.Refresh()
        {
            //throw new NotImplementedException();
            this.Adapter.NotifyDataSetChanged();
        }

        public EventHandler<MatchedTweetReceivedEventArgs> AddTweetFromStream()
        {
            throw new NotImplementedException();
        }

        public class FetchTweets : AsyncTask<String, ITweet, String>, IClientUpdateUi
        {
            List<ITweet> tweets;
            TwitterAdapter adapter;
            TweetPresenter presenter;
            public FetchTweets(List<ITweet> tweets, TwitterFragment fragment, TweetPresenter presenter)
            {
                this.tweets = tweets;
                this.adapter = fragment.Adapter;
                this.presenter = presenter;
            }

            public void UpdateUi(ITweet tweet)
            {
                PublishProgress(tweet);
            }

            protected override void OnProgressUpdate(params ITweet[] tweetArray)
            {
                this.tweets.Insert(0, tweetArray[0]);
                adapter.NotifyDataSetChanged();
            }

            protected override String RunInBackground(params String[] @params)
            {
                presenter.StartStream();
                return null;
            }
        }
    }
}
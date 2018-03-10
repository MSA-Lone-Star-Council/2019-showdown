using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7;

using Common.Common;
using Common.Common.Models;
using System.Collections.Generic;
using Android.Util;
using Tweetinvi.Models;

namespace Client.Droid.Adapters
{
    class TwitterAdapter : RecyclerView.Adapter
    {
        public List<ITweet> Tweets { get; set; }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.tweet_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new TwitterAdapterViewHolder(itemView);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Tweets[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as TwitterAdapterViewHolder;
            holder.Tweet = item;
            holder.Username.Text = item.CreatedBy.ScreenName;
            holder.TweetBody.Text = item.FullText;

        }

        public override int ItemCount
        {
            get
            {
                return Tweets == null ? 0 : Tweets.Count;
            }

        }

    }

    public class TwitterAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ITweet Tweet { get; set; }

        public TextView Username;
        public TextView TweetBody;


        public TwitterAdapterViewHolder(View itemView) : base(itemView)
        {
            Username = itemView.FindViewById<TextView>(Resource.Id.tweet_username);
            TweetBody = itemView.FindViewById<TextView>(Resource.Id.tweet_body);
        }
    }
}

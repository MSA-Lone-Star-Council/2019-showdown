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
        public event EventHandler<TwitterAdapterClickEventArgs> ItemClick;
        public event EventHandler<TwitterAdapterClickEventArgs> ItemLongClick;
        public List<ITweet> Tweets { get; set; }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.event_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new TwitterAdapterViewHolder(itemView, OnClick, OnLongClick);
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

        void OnClick(TwitterAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(TwitterAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class TwitterAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ITweet Tweet { get; set; }

        public TextView Username { get; set; }
        public TextView TweetBody { get; set; }


        public TwitterAdapterViewHolder(View itemView, Action<TwitterAdapterClickEventArgs> clickListener,
                            Action<TwitterAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            Username = itemView.FindViewById<TextView>(Resource.Id.tweet_username);
            TweetBody = itemView.FindViewById<TextView>(Resource.Id.tweet_body);


            itemView.Click += (sender, e) => clickListener(new TwitterAdapterClickEventArgs
            {
                View = itemView,
                Position = AdapterPosition,
                Tweet = this.Tweet  //Edge case, Null if ViewHolder has been clicked before it was bound to a view
            });
            itemView.LongClick += (sender, e) => longClickListener(new TwitterAdapterClickEventArgs
            {
                View = itemView,
                Position = AdapterPosition,
                Tweet = this.Tweet //Edge case, Null if ViewHolder has been clicked before it was bound to a view
            });
        }
    }

    public class TwitterAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public ITweet Tweet { get; set; }
    }
}

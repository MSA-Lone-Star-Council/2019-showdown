using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7;

using Common.Common;
using Common.Common.Models;
using System.Collections.Generic;
using Android.Util;

namespace Client.Droid.Adapters
{
    class SportsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<SportsAdapterClickEventArgs> ItemClick;
        public event EventHandler<SportsAdapterClickEventArgs> ItemLongClick;
        public List<Game> Games { get; set; }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.sports_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new Adapter1ViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Games[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as Adapter1ViewHolder;
            holder.Game = item;
            holder.Title.Text = item.Title;
            holder.Category.Text = item.Event.Title;
            holder.Team1.Text = item.AwayTeam.ShortName;
            holder.Team2.Text = item.HomeTeam.ShortName;
            holder.Score1.Text = item.Score.AwayPoints.ToString();
            holder.Score2.Text = item.Score.HomePoints.ToString();
            holder.StartTime.Text = Utilities.FormatDateTime(item.Event.StartTime);
            

        }

        public override int ItemCount
        {
            get
            {
                return Games == null ? 0 : Games.Count;
            }

        }

        void OnClick(SportsAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(SportsAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class Adapter1ViewHolder : RecyclerView.ViewHolder
    {
        public Game Game { get; set; }
        public TextView Title { get; set; }
        public TextView Category { get; set; }
        public TextView Team1 { get; set; }
        public TextView Team2 { get; set; }
        public TextView Score1 { get; set; }
        public TextView Score2 { get; set; }
        public TextView StartTime { get; set; }


        public Adapter1ViewHolder(View itemView, Action<SportsAdapterClickEventArgs> clickListener,
                            Action<SportsAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.sport_title);
            Category = itemView.FindViewById<TextView>(Resource.Id.sport_category);
            Team1 = itemView.FindViewById<TextView>(Resource.Id.team_1);
            Team2 = itemView.FindViewById<TextView>(Resource.Id.team_2);
            Score1 = itemView.FindViewById<TextView>(Resource.Id.team_1_score);
            Score2 = itemView.FindViewById<TextView>(Resource.Id.team_2_score);
            StartTime = itemView.FindViewById<TextView>(Resource.Id.start_time);

            itemView.Click += (sender, e) => clickListener(new SportsAdapterClickEventArgs {
                View = itemView,
                Position = AdapterPosition,
                Game = this.Game
            });

            itemView.LongClick += (sender, e) => longClickListener(new SportsAdapterClickEventArgs {
                View = itemView,
                Position = AdapterPosition,
                Game = this.Game
            });
        }
    }

    public class SportsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public Game Game { get; set; }
    }
}
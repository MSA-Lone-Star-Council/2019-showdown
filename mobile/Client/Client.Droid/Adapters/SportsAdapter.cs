using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7;

using Common.Common;
using Common.Common.Models;
using System.Collections.Generic;
using Android.Util;
using Plugin.Iconize.Droid.Controls;
using Client.Common;

namespace Client.Droid.Adapters
{
    class SportsAdapter : RecyclerView.Adapter
    {
        public event EventHandler<SportsAdapterClickEventArgs> ItemClick;
        public event EventHandler<SportsAdapterClickEventArgs> ItemLongClick;
        public List<Game> Games { get; set; }

        public IGameHavingPresenter Presenter { get; set; }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.sports_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new Adapter1ViewHolder(itemView, OnClick, OnLongClick, Presenter);
            return vh;
        }
        
        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Presenter.GetGame(position);
            var isSubscribed = Presenter.IsSubscribed(position);

            // Replace the contents of the view with that element
            var holder = viewHolder as Adapter1ViewHolder;
            holder.Game = item;
            holder.Title.Text = item.Title;
            holder.Category.Text = item.Event.Title;
            holder.AwayTeam.Text = item.AwayTeam.ShortName;
            holder.HomeTeam.Text = item.HomeTeam.ShortName;
            holder.AwayScore.Text = item.Score.AwayPoints.ToString();
            holder.HomeScore.Text = item.Score.HomePoints.ToString();
            holder.StartTime.Text = Utilities.FormatDateTime(item.Event.StartTime);
            holder.BellButton.Text = isSubscribed ? $"{{fa-bell}}" : $"{{fa-bell-o}}";

           
        }

        public override int ItemCount
        {
            get
            {
                return Presenter.GameCount();
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
        public TextView AwayTeam { get; set; }
        public TextView HomeTeam { get; set; }
        public TextView AwayScore { get; set; }
        public TextView HomeScore { get; set; }
        public TextView StartTime { get; set; }
        public IconButton BellButton { get; set; }


        public Adapter1ViewHolder(View itemView, Action<SportsAdapterClickEventArgs> clickListener,
                            Action<SportsAdapterClickEventArgs> longClickListener, IGameHavingPresenter presenter) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.sport_title);
            Category = itemView.FindViewById<TextView>(Resource.Id.sport_category);
            AwayTeam = itemView.FindViewById<TextView>(Resource.Id.away_team);
            HomeTeam = itemView.FindViewById<TextView>(Resource.Id.home_team);
            AwayScore = itemView.FindViewById<TextView>(Resource.Id.away_team_score);
            HomeScore = itemView.FindViewById<TextView>(Resource.Id.home_team_score);
            StartTime = itemView.FindViewById<TextView>(Resource.Id.start_time);
            BellButton = itemView.FindViewById<IconButton>(Resource.Id.bellIconButton);

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

            BellButton.Click += async delegate
            {
                Log.Debug("ShowdownApp", "Bell tapped");
                await presenter.SubscribeTapped(AdapterPosition);
            };
        }
    }

    public class SportsAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public Game Game { get; set; }
    }
}
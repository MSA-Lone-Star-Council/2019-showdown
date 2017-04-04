using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Common.Common.Models;

namespace Scorekeeper.Droid
{
    class GameListAdapter : RecyclerView.Adapter
    {
        public event EventHandler<GameListAdapterClickEventArgs> ItemClick;
        public event EventHandler<GameListAdapterClickEventArgs> ItemLongClick;
        public List<Game> Games { get; set; }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Android.Resource.Layout.SimpleListItem1;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new GameListAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Games[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as GameListAdapterViewHolder;

            holder.Game = item;
            holder.GameName.Text = item.Title;
        }

        public override int ItemCount
        {
            get
            {
                return Games == null ? 0 : Games.Count;
            }

        }

        void OnClick(GameListAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(GameListAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class GameListAdapterViewHolder : RecyclerView.ViewHolder
    {
        public Game Game;

        public TextView GameName { get; set; }


        public GameListAdapterViewHolder(View itemView, Action<GameListAdapterClickEventArgs> clickListener,
                            Action<GameListAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            GameName = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);

            itemView.Click += (sender, e) => clickListener(new GameListAdapterClickEventArgs {
                View = itemView,
                Position = AdapterPosition,
                Game = this.Game
            });
            itemView.LongClick += (sender, e) => longClickListener(new GameListAdapterClickEventArgs {
                View = itemView,
                Position = AdapterPosition,
                Game = this.Game
            });
        }
    }

    public class GameListAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public Game Game { get; set; }
    }
}
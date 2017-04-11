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
using Android.Support.V7;
using Client.Common;
using Common.Common;
using Common.Common.Models;

namespace Client.Droid.Adapters
{
    class ScoreAdapter : RecyclerView.Adapter
    {

        public event EventHandler<ScoreAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScoreAdapterClickEventArgs> ItemLongClick;
        public List<Score> Scores { get; set; }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.score_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new ScoreAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }


        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = Scores[position];

            var holder = viewHolder as ScoreAdapterViewHolder;
            holder.Score = item;
            holder.AwayPoints.Text = item.AwayPoints.ToString();
            holder.HomePoints.Text = item.HomePoints.ToString();
            holder.Time.Text = Utilities.FormatDateTime(item.Time);
        }

        public override int ItemCount
        {
            get
            {
                return Scores == null ? 0 : Scores.Count;
            }

        }

        void OnClick(ScoreAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScoreAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
    }

   class ScoreAdapterViewHolder : RecyclerView.ViewHolder
        {
        public Score Score { get; set; }
        public TextView AwayPoints { get; set; }
        public TextView HomePoints { get; set; }
        public TextView Time { get; set; }

        public ScoreAdapterViewHolder(View itemView, Action<ScoreAdapterClickEventArgs> clickListener,
                                Action<ScoreAdapterClickEventArgs> longClickListener) : base(itemView)
            {
            AwayPoints = itemView.FindViewById<TextView>(Resource.Id.score_1);
            HomePoints = itemView.FindViewById<TextView>(Resource.Id.score_2);
            Time = itemView.FindViewById<TextView>(Resource.Id.time);
            
            itemView.Click += (sender, e) => clickListener(new ScoreAdapterClickEventArgs{
                View = itemView,
                Position = AdapterPosition,
                Score = this.Score
            });

            itemView.LongClick += (sender, e) => longClickListener(new ScoreAdapterClickEventArgs{
                View = itemView,
                Position = AdapterPosition,
                Score = this.Score
            });

        }
   }

    public class ScoreAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
        public Score Score { get; set; }
    }
}
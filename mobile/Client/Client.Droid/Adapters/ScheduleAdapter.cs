using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Android.Support.V7;

using Common.Common;
using Common.Common.Models;
using System.Collections.Generic;

namespace Client.Droid.Adapters
{
    class ScheduleAdapter : RecyclerView.Adapter
    {
        public event EventHandler<ScheduleAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScheduleAdapterClickEventArgs> ItemLongClick;
        List<Event> items;

        public ScheduleAdapter(List<Event> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            var id = Resource.Layout.event_layout;
            View itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new ScheduleAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as ScheduleAdapterViewHolder;
            holder.Title.Text = items[position].Title;
        }

        public override int ItemCount => items.Count;

        void OnClick(ScheduleAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScheduleAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class ScheduleAdapterViewHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; set; }
        public TextView Description { get; set; }


        public ScheduleAdapterViewHolder(View itemView, Action<ScheduleAdapterClickEventArgs> clickListener,
                            Action<ScheduleAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.eventTitle);
            Description = itemView.FindViewById<TextView>(Resource.Id.eventDescription);
            itemView.Click += (sender, e) => clickListener(new ScheduleAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new ScheduleAdapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class ScheduleAdapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}
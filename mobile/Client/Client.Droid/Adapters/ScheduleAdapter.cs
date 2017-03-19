using System;

using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Client.Droid.Adapters
{
    class ScheduleAdapter : RecyclerView.Adapter
    {
        public event EventHandler<ScheduleAdapterClickEventArgs> ItemClick;
        public event EventHandler<ScheduleAdapterClickEventArgs> ItemLongClick;
        string[] items;

        public ScheduleAdapter(string[] data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            //Setup your layout here
            View itemView = null;
            //var id = Resource.Layout.__YOUR_ITEM_HERE;
            //itemView = LayoutInflater.From(parent.Context).
            //       Inflate(id, parent, false);

            var vh = new ScheduleAdapterViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var item = items[position];

            // Replace the contents of the view with that element
            var holder = viewHolder as ScheduleAdapterViewHolder;
            //holder.TextView.Text = items[position];
        }

        public override int ItemCount => items.Length;

        void OnClick(ScheduleAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(ScheduleAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

    }

    public class ScheduleAdapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }


        public ScheduleAdapterViewHolder(View itemView, Action<ScheduleAdapterClickEventArgs> clickListener,
                            Action<ScheduleAdapterClickEventArgs> longClickListener) : base(itemView)
        {
            //TextView = v;
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
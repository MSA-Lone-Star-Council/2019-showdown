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
	class ScheduleAdapter : RecyclerView.Adapter
	{
		public event EventHandler<ScheduleAdapterClickEventArgs> ItemClick;
		public event EventHandler<ScheduleAdapterClickEventArgs> ItemLongClick;
		public List<Event> Events { get; set; }

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
			var item = Events[position];

			// Replace the contents of the view with that element
			var holder = viewHolder as ScheduleAdapterViewHolder;
			holder.Event = item;
			holder.Title.Text = item.Title;
			holder.Location.Text = item.Location.Name;
			holder.StartTime.Text = Utilities.FormatEventTimeSpan(item);
		}

		public override int ItemCount
		{
			get
			{
				return Events == null ? 0 : Events.Count;
			}

		}

		void OnClick(ScheduleAdapterClickEventArgs args) => ItemClick?.Invoke(this, args);
		void OnLongClick(ScheduleAdapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);

	}

	public class ScheduleAdapterViewHolder : RecyclerView.ViewHolder
	{
		public Event Event { get; set; }

		public TextView Title { get; set; }
		public TextView Location { get; set; }
		public TextView StartTime { get; set; }
		public ImageView EventPicture { get; set; }


		public ScheduleAdapterViewHolder(View itemView, Action<ScheduleAdapterClickEventArgs> clickListener,
							Action<ScheduleAdapterClickEventArgs> longClickListener) : base(itemView)
		{
			Title = itemView.FindViewById<TextView>(Resource.Id.event_title);
			Location = itemView.FindViewById<TextView>(Resource.Id.event_location);
			StartTime = itemView.FindViewById<TextView>(Resource.Id.event_start_time);
			EventPicture = itemView.FindViewById<ImageView>(Resource.Id.event_picture);

			itemView.Click += (sender, e) => clickListener(new ScheduleAdapterClickEventArgs
			{
				View = itemView,
				Position = AdapterPosition,
				Event = this.Event  //Edge case, Null if ViewHolder has been clicked before it was bound to a view
			});
			itemView.LongClick += (sender, e) => longClickListener(new ScheduleAdapterClickEventArgs
			{
				View = itemView,
				Position = AdapterPosition,
				Event = this.Event //Edge case, Null if ViewHolder has been clicked before it was bound to a view
			});
		}
	}

	public class ScheduleAdapterClickEventArgs : EventArgs
	{
		public View View { get; set; }
		public int Position { get; set; }
		public Event Event { get; set; }
	}
}

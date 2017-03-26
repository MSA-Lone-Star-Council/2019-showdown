using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Common.Common;
using Common.Common.Models;

namespace Client.Droid.Adapters
{
	class AnnouncementsAdapter : RecyclerView.Adapter
	{

		public List<Announcement> Announcements { get; set; }

		public override int ItemCount => Announcements?.Count ?? 0;


		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			var item = Announcements[position];
			AnnouncementViewHolder avh = holder as AnnouncementViewHolder;
			avh.UpdateWithItem(item);
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var id = Resource.Layout.layout_announcement_item;
			var itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
			return new AnnouncementViewHolder(itemView);
		}

		public class AnnouncementViewHolder : RecyclerView.ViewHolder
		{
			public TextView Title { get; set; }
			public TextView Body { get; set; }
			public TextView Time { get; set; }

			public AnnouncementViewHolder(View itemView) : base(itemView)
			{
				Title = itemView.FindViewById<TextView>(Resource.Id.announcement_title);
				Body = itemView.FindViewById<TextView>(Resource.Id.announcement_body);
				Time = itemView.FindViewById<TextView>(Resource.Id.announcement_time);
			}

			public void UpdateWithItem(Announcement announcement)
			{
				Title.Text = announcement.Title;
				Body.Text = announcement.Body;
				Time.Text = Utilities.FormatDateTime(announcement.Time);
			}
		}
	}
}

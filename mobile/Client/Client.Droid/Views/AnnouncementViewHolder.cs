using System;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Common.Common;
using Common.Common.Models;

namespace Client.Droid
{
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Common.Common.Models;
using Client.Common;

namespace Client.Droid.Screens
{
	public class AnnouncementsFragment : Fragment, IAnnouncementsView
	{
		RecyclerView _announcementsListView;

		AnnouncementsPresenter Presenter { get; set; }

		List<Announcement> IAnnouncementsView.Announcements
		{
			set
			{
				AnnouncementsAdapter adapter = _announcementsListView?.GetAdapter() as AnnouncementsAdapter;
				adapter.Announcements = value;
				adapter?.NotifyDataSetChanged();
			}
		}

		public override async void OnResume()
		{
			base.OnResume();
			var app = (Activity.Application);
			var client = (app as ShowdownClientApplication).BackendClient;

			Presenter = new AnnouncementsPresenter(client);
			Presenter.TakeView(this);
			await Presenter.OnBegin();
		}


		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			// Use this to return your custom view for this Fragment
			View v = inflater.Inflate(Resource.Layout.fragment_announcements, container, false);

			_announcementsListView = v.FindViewById<RecyclerView>(Resource.Id.announcementsRecyclerView);
			_announcementsListView.SetAdapter(new AnnouncementsAdapter()
			{
				Announcements = new List<Announcement>()
			});
			_announcementsListView.SetLayoutManager(new LinearLayoutManager(Activity));

		
			return v;
		}

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
		}
	}
}
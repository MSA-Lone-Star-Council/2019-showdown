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
using Client.Droid.Adapters;
using System.Timers;

namespace Client.Droid.Screens
{
	public class AnnouncementsFragment : Fragment, IAnnouncementsView
	{
		RecyclerView _announcementsListView;
		public RecyclerView AnnouncementList
		{
			get 
			{
				return _announcementsListView;
			}
		}

		AnnouncementsPresenter Presenter { get; set; }

        Timer timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds) { AutoReset = true };

        void IAnnouncementsView.ShowMessage(string message)
        {
            Toast.MakeText(this.Activity, message, ToastLength.Short);
        }

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
            timer.Elapsed += (sender, e) => Activity.RunOnUiThread(async () => await Presenter.OnTick());
            timer.Start();
		}

        public override void OnStop()
        {
            base.OnStop();
            timer.Stop();
            Presenter.RemoveView();
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

		void IAnnouncementsView.OpenNewAnnouncement()
		{
			// Admin only method
			throw new NotImplementedException();
		}
	}
}
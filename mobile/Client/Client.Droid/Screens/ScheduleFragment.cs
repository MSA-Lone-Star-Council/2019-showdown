using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Client.Droid.Adapters;
using Common.Common;
using Client.Common;
using Common.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;

namespace Client.Droid.Screens
{
	public class ScheduleFragment : Fragment, IScheduleView
	{
		SchedulePresenter Presenter { get; set; }
		public List<Event> DayEvents { get; set; }
		List<Event> IScheduleView.Events
		{
			set
			{
				return;
			}
		}
		RecyclerView ScheduleView { get; set; }
		ScheduleAdapter Adapter { get; set; }

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Presenter = new SchedulePresenter(null);
			Presenter.TakeView(this);

			Adapter = new ScheduleAdapter()
			{
				Events = DayEvents
			};
			Adapter.ItemClick += (object sender, ScheduleAdapterClickEventArgs args) => Presenter.OnClickRow(args.Event);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			View view = inflater.Inflate(Resource.Layout.fragment_schedule, container, false);

			// Set up Recycler View for the Schedule
			ScheduleView = view.FindViewById<RecyclerView>(Resource.Id.scheduleRecyclerView);
			ScheduleView.SetLayoutManager(new LinearLayoutManager(this.Activity));
			ScheduleView.SetAdapter(Adapter);

			return view;
		}

		void IScheduleView.ShowMessage(string message)
		{
			Toast.MakeText(this.Activity, message, ToastLength.Short).Show();
		}

		void IScheduleView.OpenEvent(Event row)
		{
			var Intent = new Intent(this.Activity, typeof(DetailedEventActivity));
			Intent.PutExtra("event", row.ToJSON());
			StartActivity(Intent);
		}

		Task IScheduleView.ScheduleReminder(Event eventToRemind)
		{
			return Task.CompletedTask;
		}
	}
}


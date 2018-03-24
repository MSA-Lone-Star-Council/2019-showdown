using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Client.Droid.Adapters;
using Client.Common;
using Common.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Linq;
using DroidUri = Android.Net.Uri;
using System;

namespace Client.Droid.Screens
{
	public class ScheduleFragment : Fragment, IScheduleView
	{
		SchedulePresenter Presenter { get; set; }


		List<Event> IScheduleView.Events
		{
			set
			{
				ScheduleAdapter adapter = this.Adapter;
				if (adapter == null) return;
				adapter.Events = GetDayEvents(value, this.day);
				adapter.NotifyDataSetChanged();
			}
		}
		RecyclerView ScheduleView;
		public RecyclerView ScheduleList 
		{
			get
			{
				return ScheduleView;
			}
		}
		ScheduleAdapter Adapter { get; set; }
		int day;

		Timer timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds) { AutoReset = true };
		private TextView emptyTextView;

		public ScheduleFragment(int dayIndex)
		{
			day = dayIndex;
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			Presenter = new SchedulePresenter(((ShowdownClientApplication)this.Activity.Application).BackendClient);
			Presenter.TakeView(this);

			Adapter = new ScheduleAdapter()
			{
				Events = new List<Event>()
			};
			Adapter.ItemClick += (object sender, ScheduleAdapterClickEventArgs args) => Presenter.OnClickRow(args.Event);
		}

		public async override void OnResume()
		{
			base.OnResume();

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
			View view = inflater.Inflate(Resource.Layout.fragment_schedule, container, false);

			// Set up Recycler View for the Schedule
			ScheduleView = view.FindViewById<RecyclerView>(Resource.Id.scheduleRecyclerView);
			emptyTextView = view.FindViewById<TextView>(Resource.Id.empty_view);
			ScheduleView.SetLayoutManager(new LinearLayoutManager(this.Activity));
			ScheduleView.SetAdapter(Adapter);

			return view;
		}

		void IScheduleView.ShowMessage(string message)
		{
			emptyTextView.Text = message;
		}

		void IScheduleView.OpenEvent(Event row)
		{
			//var Intent = new Intent(this.Activity, typeof(DetailedEventActivity));
			//Intent.PutExtra("event", row.ToJSON());
			//StartActivity(Intent);
			Intent intent = new Intent(Intent.ActionView);
			DroidUri locationUri = GenerateUri(row);
			intent.SetData(locationUri);
			Activity.StartActivity(intent);
		}

		Task IScheduleView.ScheduleReminder(Event eventToRemind)
		{
			//return Task.CompletedTask;
			return null;
		}

		private List<Event> GetDayEvents(List<Event> e, int day)
		{
			var eventsByDay = e.GroupBy(x => x.StartTime.Day).ToArray();
			if (day > eventsByDay.Length - 1)
			{
				if (emptyTextView.Text.Length == 0)
				{
					emptyTextView.Text = "No events on this day";
				}
				return new List<Event>();
			}
			else
			{
				emptyTextView.Text = "";
				return eventsByDay[day].ToList();
			}
		}

		private DroidUri GenerateUri(Event e) {
			DroidUri uri = DroidUri.Parse("geo:0,0?");
			DroidUri.Builder uriBuilder = uri.BuildUpon();
			uriBuilder.AppendQueryParameter("q", e.Location.Address);
			return uriBuilder.Build();
		}
	}
}


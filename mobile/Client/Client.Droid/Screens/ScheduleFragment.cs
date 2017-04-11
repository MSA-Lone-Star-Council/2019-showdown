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


        List<Event> IScheduleView.Events {
            set
            {
                ScheduleAdapter adapter = this.Adapter;
				if (adapter == null) return;
                adapter.Events = value;
                adapter.NotifyDataSetChanged();
            }
        }
        RecyclerView ScheduleView { get; set; }
        ScheduleAdapter Adapter { get; set; }

        Timer timer = new Timer(TimeSpan.FromSeconds(10).TotalMilliseconds) { AutoReset = true };

        public override async void OnCreate(Bundle savedInstanceState)
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

	    async Task IScheduleView.ScheduleReminder(Event eventToRemind)
	    {
            return;
	    }
	}
}


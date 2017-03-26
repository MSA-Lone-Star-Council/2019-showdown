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

        public override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Presenter = new SchedulePresenter(new ShowdownRESTClient());
            Presenter.TakeView(this);

            Adapter = new ScheduleAdapter()
            {
                //Events = new List<Event>()
                Events = MakeFakeData()
            };
            Adapter.ItemClick += OnItemClick;
            //await Presenter.OnBegin();
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

        // Handler for the item click event. This should be moved to the Presenter Methods instead
        void OnItemClick(object sender, ScheduleAdapterClickEventArgs args)
        {
             Presenter.OnClickRow(args.Event);
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

        //REST Client isn't working right now, so this comes back
        public static List<Event> MakeFakeData()
        {
            var event1 = new Event
            {
                Id = "0",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Description = "Listen to Dudes sing",
                Title = "Brothers Nasheed",
                Location = new Location
                {
                    Name = "Texas Union Ballroom"
                }
            };
            var event2 = new Event
            {
                Id = "1",
                StartTime = new DateTimeOffset(),
                EndTime = new DateTimeOffset(),
                Description = "Listen to gals spit fire",
                Title = "Sisters Poetry",
                Location = new Location
                {
                    Name = "Texas Union Ballroom"
                }
            };
            List<Event> events = new List<Event>();
            for (int i = 0; i < 5; i++)
            {
                if (i % 2 != 0) { event1.Id = i.ToString(); }
                else { event2.Id = i.ToString(); }

                events.Add(event1);
                events.Add(event2);
            }
            return events;
        }

    }
}


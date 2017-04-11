using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Common.Common;
using Common.Common.Models;


namespace Client.Droid.Screens
{
    [Activity(Label = "Event Details", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DetailedEventActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string serializedEvent = this.Intent.GetStringExtra("event");
            Event Event = Event.FromJSON(serializedEvent);

            SetContentView(Resource.Layout.event_detailed_layout);

            TextView EventTitle = FindViewById<TextView>(Resource.Id.event_detailed_title);
            TextView EventDescription = FindViewById<TextView>(Resource.Id.event_detailed_description);
            TextView EventTime = FindViewById<TextView>(Resource.Id.event_detailed_time);
            TextView LocationName = FindViewById<TextView>(Resource.Id.event_location_name);
            TextView LocationAddress = FindViewById<TextView>(Resource.Id.event_location_address);

            EventTitle.Text = Event.Title;
            EventDescription.Text = Event.Description;
            EventTime.Text = Utilities.FormatEventTimeSpan(Event);
            LocationName.Text = Event.Location.Name;
            LocationAddress.Text = Event.Location.Address;

            LocationAddress.Clickable = true;
            LocationAddress.Click += delegate
            {
                var uri = Android.Net.Uri.Parse("http://maps.google.co.in/maps?q=" + LocationAddress.Text);
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };


        }
    }
}
using Android.App;
using Android.Widget;
using Android.OS;

namespace Client.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            Button scheduleButton = FindViewById<Button>(Resource.Id.scheduleButton);
            Button announcementButton = FindViewById<Button>(Resource.Id.announcementsButton);
            Button mapButton = FindViewById<Button>(Resource.Id.mapsButton);
            Button sportsButton = FindViewById<Button>(Resource.Id.sportsButton);
            Button acknowledgementsButton = FindViewById<Button>(Resource.Id.acknowledgementsButton);

            scheduleButton.Click += delegate { StartActivity(typeof(ScheduleActivity)); };
            announcementButton.Click += delegate { StartActivity(typeof(AnnouncementActivity)); };
            mapButton.Click += delegate { StartActivity(typeof(MapsActivity)); };
            sportsButton.Click += delegate { StartActivity(typeof(SportsActivity)); };
            acknowledgementsButton.Click += delegate { StartActivity(typeof(AcknowledgementsActivity)); };

        }
    }
}


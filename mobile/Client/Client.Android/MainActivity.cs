using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Client.Android
{
	[Activity(Label = "Showdown", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		int count = 1;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = FindViewById<Button>(Resource.Id.myButton);

			button.Click += delegate { button.Text = $"{count++} clicks!"; };

            Button AnnouncementButton = FindViewById<Button>(Resource.Id.announcementsButton);
            Button ScheduleButton = FindViewById<Button>(Resource.Id.scheduleButton);
            Button MapButton = FindViewById<Button>(Resource.Id.mapsButton);
            Button SportsButton = FindViewById<Button>(Resource.Id.sportsButton);
            Button AcknowledgementsButton = FindViewById<Button>(Resource.Id.acknowledgementsButton);

            AnnouncementButton.Click += delegate { StartActivity(typeof(AnnouncementsActivity)); };
            ScheduleButton.Click += delegate { StartActivity(typeof(ScheduleActivity)); };
            MapButton.Click += delegate { StartActivity(typeof(MapActivity)); };
            SportsButton.Click += delegate { StartActivity(typeof(SportsActivity)); };
            AcknowledgementsButton.Click += delegate { StartActivity(typeof(AcknowledgementsActivity)); };

        }
    }
}

